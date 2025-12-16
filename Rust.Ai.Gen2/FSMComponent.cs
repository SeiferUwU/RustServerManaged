using System.Collections.Generic;
using System.Text;
using ConVar;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[SoftRequireComponent(typeof(LimitedTurnNavAgent), typeof(RootMotionPlayer), typeof(SenseComponent))]
[SoftRequireComponent(typeof(BlackboardComponent), typeof(NPCEncounterTimer))]
public class FSMComponent : EntityComponent<BaseEntity>
{
	public class TickFSMWorkQueue : PersistentObjectWorkQueue<FSMComponent>
	{
		protected override void RunJob(FSMComponent component)
		{
			if (ShouldAdd(component) && component.enabled)
			{
				component.Senses.Tick();
				component.GetComponent<NPCEncounterTimer>().Tick();
				component.Tick();
			}
		}

		protected override bool ShouldAdd(FSMComponent component)
		{
			if (base.ShouldAdd(component))
			{
				return component.baseEntity.IsValid();
			}
			return false;
		}
	}

	private bool isRunning;

	private SenseComponent _senses;

	public const float minRefreshIntervalSeconds = 0f;

	public const float maxRefreshIntervalSeconds = 0.5f;

	private double? _lastTickTime;

	private double nextRefreshTime;

	private const int maxStateChangesPerTick = 3;

	private List<FSMStateBase> sameFrameStateChangesHistory = new List<FSMStateBase>();

	private FSMStateBase pendingStateChange;

	public static TickFSMWorkQueue workQueue = new TickFSMWorkQueue();

	public const float frameBudgetMs = 0.5f;

	public FSMStateBase CurrentState { get; private set; }

	private SenseComponent Senses => _senses ?? (_senses = base.baseEntity.GetComponent<SenseComponent>());

	private float RefreshInterval
	{
		get
		{
			if (!Senses.ShouldRefreshFast)
			{
				return 0.5f;
			}
			return 0f;
		}
	}

	private double LastTickTime
	{
		get
		{
			double valueOrDefault = _lastTickTime.GetValueOrDefault();
			if (!_lastTickTime.HasValue)
			{
				valueOrDefault = UnityEngine.Time.timeAsDouble;
				_lastTickTime = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
		set
		{
			_lastTickTime = value;
		}
	}

	public void Run()
	{
		if (isRunning)
		{
			Debug.LogWarning("[FSM] Trying to start a FSM that's already running on " + base.baseEntity.gameObject.name);
			return;
		}
		isRunning = true;
		_lastTickTime = null;
		workQueue.Add(this);
	}

	public void Stop()
	{
		if (!isRunning)
		{
			Debug.LogWarning("[FSM] Trying to stop a FSM that is not running on " + base.baseEntity.gameObject.name);
			return;
		}
		isRunning = false;
		workQueue.Remove(this);
	}

	private void OnDestroy()
	{
		Stop();
	}

	public static void ShowDebugInfoAroundLocation(BasePlayer player, float radius = 100f)
	{
		if (!player.IsValid())
		{
			return;
		}
		using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
		BaseEntity.Query.Server.GetBrainsInSphere(player.transform.position, radius, pooledList);
		foreach (BaseEntity item in pooledList)
		{
			FSMComponent component = item.GetComponent<FSMComponent>();
			if (!(component == null) && component.CurrentState != null && component.isRunning)
			{
				player.ClientRPC(RpcTarget.Player("CL_ShowStateDebugInfo", player), component.baseEntity.transform.position, component.CurrentState.Name);
			}
		}
	}

	protected void ForceTickOnTheNextUpdate()
	{
		nextRefreshTime = 0.0;
	}

	public void Tick()
	{
		using (TimeWarning.New("FSMComponent.Tick"))
		{
			if (UnityEngine.Time.timeAsDouble < nextRefreshTime)
			{
				return;
			}
			nextRefreshTime = UnityEngine.Time.timeAsDouble + (double)RefreshInterval;
			float deltaTime = (float)(UnityEngine.Time.timeAsDouble - LastTickTime);
			LastTickTime = UnityEngine.Time.timeAsDouble;
			sameFrameStateChangesHistory.Clear();
			if (pendingStateChange != null)
			{
				SetState(pendingStateChange);
			}
			else
			{
				if (CurrentState == null)
				{
					return;
				}
				using (TimeWarning.New("NormalTransitions"))
				{
					using PooledList<FSMStateBase> pooledList = Facepunch.Pool.Get<PooledList<FSMStateBase>>();
					CurrentState.FindAncestry(pooledList);
					foreach (FSMStateBase item in pooledList)
					{
						foreach (var (fSMTransitionBase, fSMStateBase) in item.transitions)
						{
							fSMTransitionBase.Init(base.baseEntity);
							if (fSMTransitionBase.Evaluate())
							{
								fSMStateBase.Owner = base.baseEntity;
								fSMTransitionBase.OnTransitionTaken(CurrentState, fSMStateBase);
								SetState(fSMStateBase);
								return;
							}
						}
					}
				}
				EFSMStateStatus currentStateStatus = EFSMStateStatus.None;
				using (TimeWarning.New("StateTick"))
				{
					using (TimeWarning.New(CurrentState.Name))
					{
						currentStateStatus = CurrentState.OnStateUpdate(deltaTime);
					}
				}
				EvaluateEndTransitions(currentStateStatus);
			}
		}
	}

	private void EvaluateEndTransitions(EFSMStateStatus currentStateStatus)
	{
		using (TimeWarning.New("EndTransitions"))
		{
			if (currentStateStatus == EFSMStateStatus.None)
			{
				return;
			}
			using PooledList<FSMStateBase> pooledList = Facepunch.Pool.Get<PooledList<FSMStateBase>>();
			CurrentState.FindAncestry(pooledList);
			foreach (FSMStateBase item in pooledList)
			{
				foreach (var (fSMTransitionBase, fSMStateBase, eFSMStateStatus) in item.endTransitions)
				{
					if (eFSMStateStatus == (EFSMStateStatus.Success | EFSMStateStatus.Failure) || eFSMStateStatus == currentStateStatus)
					{
						bool flag = true;
						if (fSMTransitionBase != null)
						{
							fSMTransitionBase.Init(base.baseEntity);
							flag = fSMTransitionBase.Evaluate();
						}
						if (flag)
						{
							fSMStateBase.Owner = base.baseEntity;
							fSMTransitionBase?.OnTransitionTaken(CurrentState, fSMStateBase);
							SetState(fSMStateBase);
							ForceTickOnTheNextUpdate();
							return;
						}
					}
				}
			}
		}
	}

	public void SetState(FSMStateBase newState)
	{
		using (TimeWarning.New("SetState"))
		{
			pendingStateChange = null;
			sameFrameStateChangesHistory.Add(newState);
			if (sameFrameStateChangesHistory.Count > 3)
			{
				if (!AI.logIssues)
				{
					return;
				}
				StringBuilder obj = Facepunch.Pool.Get<StringBuilder>();
				obj.AppendFormat("[FSM] Possible endless recursion detected from {0} to {1} on {2}\n", CurrentState?.Name, newState.Name, base.baseEntity);
				foreach (FSMStateBase item in sameFrameStateChangesHistory)
				{
					obj.AppendFormat("{0} -> ", item.Name);
				}
				Debug.LogWarning(obj);
				pendingStateChange = newState;
				Facepunch.Pool.FreeUnmanaged(ref obj);
				return;
			}
			if (CurrentState != null)
			{
				using (TimeWarning.New("Transitions OnStateExit"))
				{
					using PooledList<FSMStateBase> pooledList = Facepunch.Pool.Get<PooledList<FSMStateBase>>();
					CurrentState.FindAncestry(pooledList);
					foreach (FSMStateBase item2 in pooledList)
					{
						foreach (var endTransition in item2.endTransitions)
						{
							endTransition.transition?.OnStateExit();
						}
						foreach (var transition in item2.transitions)
						{
							transition.transition.OnStateExit();
						}
					}
				}
				using (TimeWarning.New("OnStateExit"))
				{
					using (TimeWarning.New(CurrentState.Name))
					{
						CurrentState.OnStateExit();
					}
				}
			}
			CurrentState = newState;
			using (TimeWarning.New("Transitions OnStateEnter"))
			{
				using PooledList<FSMStateBase> pooledList2 = Facepunch.Pool.Get<PooledList<FSMStateBase>>();
				CurrentState.FindAncestry(pooledList2);
				foreach (FSMStateBase item3 in pooledList2)
				{
					foreach (var endTransition2 in item3.endTransitions)
					{
						endTransition2.transition?.OnStateEnter();
					}
					foreach (var transition2 in item3.transitions)
					{
						transition2.transition.OnStateEnter();
					}
				}
			}
			using (TimeWarning.New("OnStateEnter"))
			{
				using (TimeWarning.New(CurrentState.Name))
				{
					EFSMStateStatus currentStateStatus = CurrentState.OnStateEnter();
					EvaluateEndTransitions(currentStateStatus);
				}
			}
		}
	}
}
