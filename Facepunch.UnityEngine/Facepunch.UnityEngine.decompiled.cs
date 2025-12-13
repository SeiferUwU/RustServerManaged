#define UNITY_ASSERTIONS
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Facepunch;
using Facepunch.Extend;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Jobs;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyVersion("0.0.0.0")]
public abstract class FacepunchBehaviour : MonoBehaviour
{
	public void Invoke(Action action, float time)
	{
		InvokeHandler.Invoke(this, action, time);
	}

	public void Invoke(Action action, float time, float random)
	{
		time += UnityEngine.Random.Range(0f - random, random);
		InvokeHandler.Invoke(this, action, time);
	}

	public void InvokeRepeating(Action action, float time, float repeat)
	{
		InvokeHandler.InvokeRepeating(this, action, time, repeat);
	}

	public void InvokeRandomized(Action action, float time, float repeat, float random)
	{
		InvokeHandler.InvokeRandomized(this, action, time, repeat, random);
	}

	public void CancelInvoke(Action action)
	{
		InvokeHandler.CancelInvoke(this, action);
	}

	public bool IsInvoking(Action action)
	{
		return InvokeHandler.IsInvoking(this, action);
	}

	public void InvokeRepeatingFixedTime(Action action)
	{
		InvokeHandlerFixedTime.InvokeRepeating(this, action, 0.01f, 0.01f);
	}

	public void InvokeRandomizedFixedTime(Action action, float time, float repeat, float random)
	{
		InvokeHandlerFixedTime.InvokeRandomized(this, action, time, repeat, random);
	}

	public void CancelInvokeFixedTime(Action action)
	{
		InvokeHandlerFixedTime.CancelInvoke(this, action);
	}

	public bool IsInvokingFixedTime(Action action)
	{
		return InvokeHandlerFixedTime.IsInvoking(this, action);
	}
}
public abstract class InvokeHandlerBase<T> : SingletonComponent<T> where T : MonoBehaviour
{
	protected ListDictionary<InvokeAction, float> curList = new ListDictionary<InvokeAction, float>(2048);

	protected ListHashSet<InvokeAction> addList = new ListHashSet<InvokeAction>(1024);

	protected ListHashSet<InvokeAction> delList = new ListHashSet<InvokeAction>(1024);

	public InvokeProfiler profiler;

	protected int nullIndex;

	protected const int nullChecks = 50;

	private Stopwatch doTickTimer = new Stopwatch();

	private Stopwatch invokeTimer = new Stopwatch();

	protected void LateUpdate()
	{
		if (profiler == null)
		{
			profiler = InvokeProfiler.update;
		}
		ApplyRemoves();
		ApplyAdds();
		DoTick();
		RemoveExpired();
		ApplyRemoves();
		ApplyAdds();
	}

	protected abstract float GetTime();

	protected void DoTick()
	{
		float[] buffer = curList.Values.Buffer;
		InvokeAction[] buffer2 = curList.Keys.Buffer;
		int count = curList.Count;
		float time = GetTime();
		profiler.tickCount = count;
		doTickTimer.Restart();
		for (int i = 0; i < count; i++)
		{
			if (!(time >= buffer[i]))
			{
				continue;
			}
			InvokeAction invokeAction = buffer2[i];
			if ((bool)invokeAction.sender && !delList.Contains(invokeAction))
			{
				if (invokeAction.repeat >= 0f)
				{
					float num = time + invokeAction.repeat;
					if (invokeAction.random > 0f)
					{
						num += UnityEngine.Random.Range(0f - invokeAction.random, invokeAction.random);
					}
					buffer[i] = num;
				}
				else
				{
					QueueRemove(invokeAction);
				}
				if (profiler.mode > 1)
				{
					invokeTimer.Restart();
					invokeAction.action();
					invokeAction.TrackingData.ExecutionTime += invokeTimer.Elapsed;
					invokeAction.TrackingData.Calls++;
				}
				else
				{
					invokeAction.action();
				}
			}
			else
			{
				QueueRemove(invokeAction);
			}
		}
		profiler.elapsedTime = doTickTimer.Elapsed;
	}

	protected void RemoveExpired()
	{
		InvokeAction[] buffer = curList.Keys.Buffer;
		int count = curList.Count;
		if (nullIndex >= count)
		{
			nullIndex = 0;
		}
		int num = Mathf.Min(nullIndex + 50, count);
		while (nullIndex < num)
		{
			InvokeAction invoke = buffer[nullIndex];
			if (!invoke.sender)
			{
				QueueRemove(invoke);
			}
			nullIndex++;
		}
	}

	protected void QueueAdd(InvokeAction invoke)
	{
		if (invoke.action == null)
		{
			UnityEngine.Debug.LogError($"Trying to add an invoke with a null action: {new StackTrace()}");
			return;
		}
		delList.Remove(invoke);
		addList.Remove(invoke);
		addList.Add(invoke);
	}

	protected void QueueRemove(InvokeAction invoke)
	{
		delList.Remove(invoke);
		addList.Remove(invoke);
		delList.Add(invoke);
	}

	protected bool Contains(InvokeAction invoke)
	{
		if (!delList.Contains(invoke))
		{
			if (!curList.Contains(invoke))
			{
				return addList.Contains(invoke);
			}
			return true;
		}
		return false;
	}

	protected void ApplyAdds()
	{
		InvokeAction[] buffer = addList.Values.Buffer;
		int count = addList.Count;
		float time = GetTime();
		profiler.addCount += count;
		for (int i = 0; i < count; i++)
		{
			InvokeAction key = buffer[i];
			curList.Remove(key);
			curList.Add(key, time + key.initial);
			key.TrackingData.InvokeCount++;
		}
		addList.Clear();
	}

	protected void ApplyRemoves()
	{
		InvokeAction[] buffer = delList.Values.Buffer;
		int count = delList.Count;
		profiler.deletedCount += count;
		for (int i = 0; i < count; i++)
		{
			InvokeAction key = buffer[i];
			curList.Remove(key);
			key.TrackingData.InvokeCount--;
		}
		delList.Clear();
	}

	public void ForEach(Action<InvokeAction> callback)
	{
		foreach (var (obj, _) in curList)
		{
			callback(obj);
		}
	}
}
public class InvokeHandler : InvokeHandlerBase<InvokeHandler>
{
	protected override float GetTime()
	{
		return Time.time;
	}

	public static void FindInvokes(Behaviour sender, List<InvokeAction> list)
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			return;
		}
		InvokeAction[] buffer = SingletonComponent<InvokeHandler>.Instance.curList.Keys.Buffer;
		int count = SingletonComponent<InvokeHandler>.Instance.curList.Count;
		for (int i = 0; i < count; i++)
		{
			InvokeAction item = buffer[i];
			if (item.sender == sender)
			{
				list.Add(item);
			}
		}
	}

	public static int Count()
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			return 0;
		}
		return SingletonComponent<InvokeHandler>.Instance.curList.Count;
	}

	public static bool IsInvoking(Behaviour sender, Action action)
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			return false;
		}
		return SingletonComponent<InvokeHandler>.Instance.Contains(new InvokeAction(sender, action, null));
	}

	public static void Invoke(Behaviour sender, Action action, float time)
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandler>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandler>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time));
	}

	public static void InvokeRepeating(Behaviour sender, Action action, float time, float repeat)
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandler>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandler>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time, repeat));
	}

	public static void InvokeRandomized(Behaviour sender, Action action, float time, float repeat, float random)
	{
		if (!SingletonComponent<InvokeHandler>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandler>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandler>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time, repeat, random));
	}

	public static void CancelInvoke(Behaviour sender, Action action)
	{
		if (!(SingletonComponent<InvokeHandler>.Instance == null))
		{
			InvokeTrackingData trackingData = SingletonComponent<InvokeHandler>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
			SingletonComponent<InvokeHandler>.Instance.QueueRemove(new InvokeAction(sender, action, trackingData));
		}
	}

	private static void CreateInstance()
	{
		GameObject obj = new GameObject();
		obj.name = "InvokeHandler";
		obj.hideFlags = HideFlags.DontSaveInEditor;
		obj.AddComponent<InvokeHandler>().profiler = InvokeProfiler.update;
		UnityEngine.Object.DontDestroyOnLoad(obj);
	}
}
public class InvokeHandlerFixedTime : InvokeHandlerBase<InvokeHandlerFixedTime>
{
	protected override float GetTime()
	{
		return Time.fixedTime;
	}

	public static void FindInvokes(Behaviour sender, List<InvokeAction> list)
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			return;
		}
		InvokeAction[] buffer = SingletonComponent<InvokeHandlerFixedTime>.Instance.curList.Keys.Buffer;
		int count = SingletonComponent<InvokeHandlerFixedTime>.Instance.curList.Count;
		for (int i = 0; i < count; i++)
		{
			InvokeAction item = buffer[i];
			if (item.sender == sender)
			{
				list.Add(item);
			}
		}
	}

	public static int Count()
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			return 0;
		}
		return SingletonComponent<InvokeHandlerFixedTime>.Instance.curList.Count;
	}

	public static bool IsInvoking(Behaviour sender, Action action)
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			return false;
		}
		return SingletonComponent<InvokeHandlerFixedTime>.Instance.Contains(new InvokeAction(sender, action, null));
	}

	public static void Invoke(Behaviour sender, Action action, float time)
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandlerFixedTime>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandlerFixedTime>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time));
	}

	public static void InvokeRepeating(Behaviour sender, Action action, float time, float repeat)
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandlerFixedTime>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandlerFixedTime>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time, repeat));
	}

	public static void InvokeRandomized(Behaviour sender, Action action, float time, float repeat, float random)
	{
		if (!SingletonComponent<InvokeHandlerFixedTime>.Instance)
		{
			CreateInstance();
		}
		InvokeTrackingData trackingData = SingletonComponent<InvokeHandlerFixedTime>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
		SingletonComponent<InvokeHandlerFixedTime>.Instance.QueueAdd(new InvokeAction(sender, action, trackingData, time, repeat, random));
	}

	public static void CancelInvoke(Behaviour sender, Action action)
	{
		if (!(SingletonComponent<InvokeHandlerFixedTime>.Instance == null))
		{
			InvokeTrackingData trackingData = SingletonComponent<InvokeHandlerFixedTime>.Instance.profiler.GetTrackingData(new InvokeTrackingKey(action));
			SingletonComponent<InvokeHandlerFixedTime>.Instance.QueueRemove(new InvokeAction(sender, action, trackingData));
		}
	}

	private static void CreateInstance()
	{
		GameObject obj = new GameObject();
		obj.name = "InvokeHandlerFixedTime";
		obj.AddComponent<InvokeHandlerFixedTime>().profiler = InvokeProfiler.fixedUpdate;
		UnityEngine.Object.DontDestroyOnLoad(obj);
	}
}
public struct InvokeTrackingKey : IEquatable<InvokeTrackingKey>
{
	public static readonly InvokeTrackingKey Unknown = new InvokeTrackingKey
	{
		MethodName = "Unknown",
		Type = typeof(InvokeTrackingKey)
	};

	public Type Type;

	public string MethodName;

	public InvokeTrackingKey(Action action)
	{
		if (action == null || action.Target == null)
		{
			Type = Unknown.Type;
			MethodName = Unknown.MethodName;
		}
		else
		{
			Type = action.Target.GetType();
			MethodName = action.Method.Name;
		}
	}

	public InvokeTrackingKey(Type type, string methodName)
	{
		Type = type;
		MethodName = methodName;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Type, MethodName);
	}

	public bool Equals(InvokeTrackingKey other)
	{
		if (Type == other.Type)
		{
			return MethodName == other.MethodName;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is InvokeTrackingKey)
		{
			return Equals((InvokeTrackingKey)obj);
		}
		return false;
	}
}
public class InvokeTrackingData
{
	public InvokeTrackingKey Key;

	public string TypeName;

	public int InvokeCount;

	public int Calls;

	public TimeSpan ExecutionTime;

	public InvokeTrackingData(InvokeTrackingKey key)
	{
		Key = key;
		TypeName = key.Type.Name;
		Calls = 0;
		ExecutionTime = TimeSpan.Zero;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Reset()
	{
		ExecutionTime = TimeSpan.Zero;
		Calls = 0;
	}
}
public struct InvokeAction : IEquatable<InvokeAction>
{
	public InvokeTrackingKey Key;

	public InvokeTrackingData TrackingData;

	public Behaviour sender;

	public Action action;

	public float initial;

	public float repeat;

	public float random;

	public InvokeAction(Behaviour sender, Action action, InvokeTrackingData tracking, float initial = 0f, float repeat = -1f, float random = 0f)
	{
		this.sender = sender;
		this.action = action;
		this.initial = initial;
		this.repeat = repeat;
		this.random = random;
		TrackingData = tracking;
		Key = tracking?.Key ?? InvokeTrackingKey.Unknown;
	}

	public bool Equals(InvokeAction other)
	{
		if (sender == other.sender)
		{
			return action == other.action;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is InvokeAction)
		{
			return Equals((InvokeAction)obj);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return sender.GetHashCode();
	}

	public static bool operator ==(InvokeAction x, InvokeAction y)
	{
		return x.Equals(y);
	}

	public static bool operator !=(InvokeAction x, InvokeAction y)
	{
		return !x.Equals(y);
	}
}
public class InvokeProfiler
{
	public static InvokeProfiler update = new InvokeProfiler("Update");

	public static InvokeProfiler fixedUpdate = new InvokeProfiler("FixedUpdate");

	public int mode;

	public TimeSpan elapsedTime;

	public int executedCount;

	public int tickCount;

	public int addCount;

	public int deletedCount;

	public List<InvokeTrackingData> trackingDataList = new List<InvokeTrackingData>();

	private readonly Dictionary<InvokeTrackingKey, InvokeTrackingData> trackingDataLookup = new Dictionary<InvokeTrackingKey, InvokeTrackingData>();

	public string Name { get; private set; }

	public InvokeProfiler(string name)
	{
		Name = name;
	}

	public void Reset()
	{
		executedCount = 0;
		tickCount = 0;
		elapsedTime = default(TimeSpan);
		addCount = 0;
		deletedCount = 0;
	}

	public InvokeTrackingData GetTrackingData(InvokeTrackingKey key)
	{
		if (trackingDataLookup.TryGetValue(key, out var value))
		{
			return value;
		}
		value = new InvokeTrackingData(key);
		trackingDataLookup.Add(key, value);
		trackingDataList.Add(value);
		return value;
	}
}
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
public sealed class JsonModelAttribute : Attribute
{
}
public class LeanTester : MonoBehaviour
{
	public float timeout = 15f;

	public void Start()
	{
		StartCoroutine(timeoutCheck());
	}

	private IEnumerator timeoutCheck()
	{
		float pauseEndTime = Time.realtimeSinceStartup + timeout;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		if (!LeanTest.testsFinished)
		{
			UnityEngine.Debug.Log(LeanTest.formatB("Tests timed out!"));
			LeanTest.overview();
		}
	}
}
public class LeanTest
{
	public static int expected = 0;

	private static int tests = 0;

	private static int passes = 0;

	public static float timeout = 15f;

	public static bool timeoutStarted = false;

	public static bool testsFinished = false;

	public static void debug(string name, bool didPass, string failExplaination = null)
	{
		expect(didPass, name, failExplaination);
	}

	public static void expect(bool didPass, string definition, string failExplaination = null)
	{
		float num = printOutLength(definition);
		int totalWidth = 40 - (int)(num * 1.05f);
		string text = "".PadRight(totalWidth, "_"[0]);
		string text2 = formatB(definition) + " " + text + " [ " + (didPass ? formatC("pass", "green") : formatC("fail", "red")) + " ]";
		if (!didPass && failExplaination != null)
		{
			text2 = text2 + " - " + failExplaination;
		}
		UnityEngine.Debug.Log(text2);
		if (didPass)
		{
			passes++;
		}
		tests++;
		if (tests == expected && !testsFinished)
		{
			overview();
		}
		else if (tests > expected)
		{
			UnityEngine.Debug.Log(formatB("Too many tests for a final report!") + " set LeanTest.expected = " + tests);
		}
		if (!timeoutStarted)
		{
			timeoutStarted = true;
			GameObject obj = new GameObject
			{
				name = "~LeanTest"
			};
			(obj.AddComponent(typeof(LeanTester)) as LeanTester).timeout = timeout;
			obj.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	public static string padRight(int len)
	{
		string text = "";
		for (int i = 0; i < len; i++)
		{
			text += "_";
		}
		return text;
	}

	public static float printOutLength(string str)
	{
		float num = 0f;
		for (int i = 0; i < str.Length; i++)
		{
			num = ((str[i] != "I"[0]) ? ((str[i] != "J"[0]) ? (num + 1f) : (num + 0.85f)) : (num + 0.5f));
		}
		return num;
	}

	public static string formatBC(string str, string color)
	{
		return formatC(formatB(str), color);
	}

	public static string formatB(string str)
	{
		return "<b>" + str + "</b>";
	}

	public static string formatC(string str, string color)
	{
		return "<color=" + color + ">" + str + "</color>";
	}

	public static void overview()
	{
		testsFinished = true;
		int num = expected - passes;
		string text = ((num > 0) ? formatBC(num.ToString() ?? "", "red") : (num.ToString() ?? ""));
		UnityEngine.Debug.Log(formatB("Final Report:") + " _____________________ PASSED: " + formatBC(passes.ToString() ?? "", "green") + " FAILED: " + text + " ");
	}
}
public enum TweenAction
{
	MOVE_X,
	MOVE_Y,
	MOVE_Z,
	MOVE_LOCAL_X,
	MOVE_LOCAL_Y,
	MOVE_LOCAL_Z,
	MOVE_CURVED,
	MOVE_CURVED_LOCAL,
	MOVE_SPLINE,
	MOVE_SPLINE_LOCAL,
	SCALE_X,
	SCALE_Y,
	SCALE_Z,
	ROTATE_X,
	ROTATE_Y,
	ROTATE_Z,
	ROTATE_AROUND,
	ROTATE_AROUND_LOCAL,
	CANVAS_ROTATEAROUND,
	CANVAS_ROTATEAROUND_LOCAL,
	CANVAS_PLAYSPRITE,
	ALPHA,
	TEXT_ALPHA,
	CANVAS_ALPHA,
	CANVASGROUP_ALPHA,
	ALPHA_VERTEX,
	COLOR,
	CALLBACK_COLOR,
	TEXT_COLOR,
	CANVAS_COLOR,
	CANVAS_MOVE_X,
	CANVAS_MOVE_Y,
	CANVAS_MOVE_Z,
	CALLBACK,
	MOVE,
	MOVE_LOCAL,
	MOVE_TO_TRANSFORM,
	ROTATE,
	ROTATE_LOCAL,
	SCALE,
	VALUE3,
	GUI_MOVE,
	GUI_MOVE_MARGIN,
	GUI_SCALE,
	GUI_ALPHA,
	GUI_ROTATE,
	DELAYED_SOUND,
	CANVAS_MOVE,
	CANVAS_SCALE,
	CANVAS_SIZEDELTA
}
public enum LeanTweenType
{
	notUsed,
	linear,
	easeOutQuad,
	easeInQuad,
	easeInOutQuad,
	easeInCubic,
	easeOutCubic,
	easeInOutCubic,
	easeInQuart,
	easeOutQuart,
	easeInOutQuart,
	easeInQuint,
	easeOutQuint,
	easeInOutQuint,
	easeInSine,
	easeOutSine,
	easeInOutSine,
	easeInExpo,
	easeOutExpo,
	easeInOutExpo,
	easeInCirc,
	easeOutCirc,
	easeInOutCirc,
	easeInBounce,
	easeOutBounce,
	easeInOutBounce,
	easeInBack,
	easeOutBack,
	easeInOutBack,
	easeInElastic,
	easeOutElastic,
	easeInOutElastic,
	easeSpring,
	easeShake,
	punch,
	once,
	clamp,
	pingPong,
	animationCurve
}
public class LeanTween : MonoBehaviour
{
	public static bool throwErrors = true;

	public static float tau = MathF.PI * 2f;

	public static float PI_DIV2 = MathF.PI / 2f;

	private static LTSeq[] sequences;

	private static LTDescr[] tweens;

	private static int[] tweensFinished;

	private static int[] tweensFinishedIds;

	private static LTDescr tween;

	private static int tweenMaxSearch = -1;

	private static int maxTweens = 4096;

	private static int maxSequences = 4096;

	private static int frameRendered = -1;

	private static GameObject _tweenEmpty;

	public static float dtEstimated = -1f;

	public static float dtManual;

	public static float dtActual;

	private static uint global_counter = 0u;

	private static int i;

	private static int j;

	private static int finishedCnt;

	public static AnimationCurve punch = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.112586f, 0.9976035f), new Keyframe(0.3120486f, -0.1720615f), new Keyframe(0.4316337f, 0.07030682f), new Keyframe(0.5524869f, -0.03141804f), new Keyframe(0.6549395f, 0.003909959f), new Keyframe(0.770987f, -0.009817753f), new Keyframe(0.8838775f, 0.001939224f), new Keyframe(1f, 0f));

	public static AnimationCurve shake = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.25f, 1f), new Keyframe(0.75f, -1f), new Keyframe(1f, 0f));

	private static int maxTweenReached;

	public static int startSearch = 0;

	public static LTDescr d;

	private static Action<LTEvent>[] eventListeners;

	private static GameObject[] goListeners;

	private static int eventsMaxSearch = 0;

	public static int EVENTS_MAX = 10;

	public static int LISTENERS_MAX = 10;

	private static int INIT_LISTENERS_MAX = LISTENERS_MAX;

	public static int maxSearch => tweenMaxSearch;

	public static int maxSimulataneousTweens => maxTweens;

	public static int tweensRunning
	{
		get
		{
			int num = 0;
			for (int i = 0; i <= tweenMaxSearch; i++)
			{
				if (tweens[i].toggle)
				{
					num++;
				}
			}
			return num;
		}
	}

	public static GameObject tweenEmpty
	{
		get
		{
			init(maxTweens);
			return _tweenEmpty;
		}
	}

	public static void init()
	{
		init(maxTweens);
	}

	public static void init(int maxSimultaneousTweens)
	{
		init(maxSimultaneousTweens, maxSequences);
	}

	public static void init(int maxSimultaneousTweens, int maxSimultaneousSequences)
	{
		if (tweens == null)
		{
			maxTweens = maxSimultaneousTweens;
			tweens = new LTDescr[maxTweens];
			tweensFinished = new int[maxTweens];
			tweensFinishedIds = new int[maxTweens];
			_tweenEmpty = new GameObject();
			_tweenEmpty.name = "~LeanTween";
			_tweenEmpty.AddComponent(typeof(LeanTween));
			_tweenEmpty.isStatic = true;
			_tweenEmpty.hideFlags = HideFlags.HideAndDontSave;
			UnityEngine.Object.DontDestroyOnLoad(_tweenEmpty);
			for (int i = 0; i < maxTweens; i++)
			{
				tweens[i] = new LTDescr();
			}
			SceneManager.sceneLoaded += onLevelWasLoaded54;
			sequences = new LTSeq[maxSimultaneousSequences];
			for (int j = 0; j < maxSimultaneousSequences; j++)
			{
				sequences[j] = new LTSeq();
			}
		}
	}

	public static void reset()
	{
		if (tweens != null)
		{
			for (int i = 0; i <= tweenMaxSearch; i++)
			{
				if (tweens[i] != null)
				{
					tweens[i].toggle = false;
				}
			}
		}
		tweens = null;
		if (Application.isPlaying)
		{
			UnityEngine.Object.Destroy(_tweenEmpty);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(_tweenEmpty);
		}
	}

	public void Update()
	{
		update();
	}

	private static void onLevelWasLoaded54(Scene scene, LoadSceneMode mode)
	{
		internalOnLevelWasLoaded(scene.buildIndex);
	}

	private static void internalOnLevelWasLoaded(int lvl)
	{
		LTGUI.reset();
	}

	public static void update()
	{
		if (frameRendered == Time.frameCount)
		{
			return;
		}
		init();
		dtEstimated = ((dtEstimated < 0f) ? 0f : (dtEstimated = Time.unscaledDeltaTime));
		dtActual = Time.deltaTime;
		maxTweenReached = 0;
		finishedCnt = 0;
		for (int i = 0; i <= tweenMaxSearch && i < maxTweens; i++)
		{
			tween = tweens[i];
			if (tween.toggle)
			{
				maxTweenReached = i;
				if (tween.updateInternal())
				{
					tweensFinished[finishedCnt] = i;
					tweensFinishedIds[finishedCnt] = tweens[i].id;
					finishedCnt++;
				}
			}
		}
		tweenMaxSearch = maxTweenReached;
		frameRendered = Time.frameCount;
		for (int j = 0; j < finishedCnt; j++)
		{
			LeanTween.j = tweensFinished[j];
			tween = tweens[LeanTween.j];
			if (tween.id == tweensFinishedIds[j])
			{
				removeTween(LeanTween.j);
				if (tween.hasExtraOnCompletes && tween.trans != null)
				{
					tween.callOnCompletes();
				}
			}
		}
	}

	public static void removeTween(int i, int uniqueId)
	{
		if (tweens[i].uniqueId == uniqueId)
		{
			removeTween(i);
		}
	}

	public static void removeTween(int i)
	{
		if (!tweens[i].toggle)
		{
			return;
		}
		tweens[i].toggle = false;
		if (tweens[i].destroyOnComplete)
		{
			if (tweens[i].ltRect != null)
			{
				LTGUI.destroy(tweens[i].ltRect.id);
			}
			else if (tweens[i].trans != null && tweens[i].trans.gameObject != _tweenEmpty)
			{
				UnityEngine.Object.Destroy(tweens[i].trans.gameObject);
			}
		}
		startSearch = i;
		if (i + 1 >= tweenMaxSearch)
		{
			startSearch = 0;
		}
	}

	public static Vector3[] add(Vector3[] a, Vector3 b)
	{
		Vector3[] array = new Vector3[a.Length];
		for (i = 0; i < a.Length; i++)
		{
			array[i] = a[i] + b;
		}
		return array;
	}

	public static float closestRot(float from, float to)
	{
		float num = 0f - (360f - to);
		float num2 = 360f + to;
		float num3 = Mathf.Abs(to - from);
		float num4 = Mathf.Abs(num - from);
		float num5 = Mathf.Abs(num2 - from);
		if (num3 < num4 && num3 < num5)
		{
			return to;
		}
		if (num4 < num5)
		{
			return num;
		}
		return num2;
	}

	public static void cancelAll()
	{
		cancelAll(callComplete: false);
	}

	public static void cancelAll(bool callComplete)
	{
		init();
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].trans != null)
			{
				if (callComplete && tweens[i].optional.onComplete != null)
				{
					tweens[i].optional.onComplete();
				}
				removeTween(i);
			}
		}
	}

	public static void cancel(GameObject gameObject)
	{
		cancel(gameObject, callOnComplete: false);
	}

	public static void cancel(GameObject gameObject, bool callOnComplete)
	{
		init();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].toggle && tweens[i].trans == transform)
			{
				if (callOnComplete && tweens[i].optional.onComplete != null)
				{
					tweens[i].optional.onComplete();
				}
				removeTween(i);
			}
		}
	}

	public static void cancel(RectTransform rect)
	{
		cancel(rect.gameObject, callOnComplete: false);
	}

	public static void cancel(GameObject gameObject, int uniqueId, bool callOnComplete = false)
	{
		if (uniqueId < 0)
		{
			return;
		}
		init();
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (tweens[num].trans == null || (tweens[num].trans.gameObject == gameObject && tweens[num].counter == num2))
		{
			if (callOnComplete && tweens[num].optional.onComplete != null)
			{
				tweens[num].optional.onComplete();
			}
			removeTween(num);
		}
	}

	public static void cancel(LTRect ltRect, int uniqueId)
	{
		if (uniqueId >= 0)
		{
			init();
			int num = uniqueId & 0xFFFF;
			int num2 = uniqueId >> 16;
			if (tweens[num].ltRect == ltRect && tweens[num].counter == num2)
			{
				removeTween(num);
			}
		}
	}

	public static void cancel(int uniqueId)
	{
		cancel(uniqueId, callOnComplete: false);
	}

	public static void cancel(int uniqueId, bool callOnComplete)
	{
		if (uniqueId < 0)
		{
			return;
		}
		init();
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (num > tweens.Length - 1)
		{
			int num3 = num - tweens.Length;
			LTSeq lTSeq = sequences[num3];
			for (int i = 0; i < maxSequences; i++)
			{
				if (lTSeq.current.tween != null)
				{
					removeTween(lTSeq.current.tween.uniqueId & 0xFFFF);
				}
				if (lTSeq.previous != null)
				{
					lTSeq.current = lTSeq.previous;
					continue;
				}
				break;
			}
		}
		else if (tweens[num].counter == num2)
		{
			if (callOnComplete && tweens[num].optional.onComplete != null)
			{
				tweens[num].optional.onComplete();
			}
			removeTween(num);
		}
	}

	public static LTDescr descr(int uniqueId)
	{
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (tweens[num] != null && tweens[num].uniqueId == uniqueId && tweens[num].counter == num2)
		{
			return tweens[num];
		}
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].uniqueId == uniqueId && tweens[i].counter == num2)
			{
				return tweens[i];
			}
		}
		return null;
	}

	public static LTDescr description(int uniqueId)
	{
		return descr(uniqueId);
	}

	public static LTDescr[] descriptions(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			return null;
		}
		List<LTDescr> list = new List<LTDescr>();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].toggle && tweens[i].trans == transform)
			{
				list.Add(tweens[i]);
			}
		}
		return list.ToArray();
	}

	[Obsolete("Use 'pause( id )' instead")]
	public static void pause(GameObject gameObject, int uniqueId)
	{
		pause(uniqueId);
	}

	public static void pause(int uniqueId)
	{
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (tweens[num].counter == num2)
		{
			tweens[num].pause();
		}
	}

	public static void pause(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].trans == transform)
			{
				tweens[i].pause();
			}
		}
	}

	public static void pauseAll()
	{
		init();
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			tweens[i].pause();
		}
	}

	public static void resumeAll()
	{
		init();
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			tweens[i].resume();
		}
	}

	[Obsolete("Use 'resume( id )' instead")]
	public static void resume(GameObject gameObject, int uniqueId)
	{
		resume(uniqueId);
	}

	public static void resume(int uniqueId)
	{
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (tweens[num].counter == num2)
		{
			tweens[num].resume();
		}
	}

	public static void resume(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].trans == transform)
			{
				tweens[i].resume();
			}
		}
	}

	public static bool isTweening(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			for (int i = 0; i <= tweenMaxSearch; i++)
			{
				if (tweens[i].toggle)
				{
					return true;
				}
			}
			return false;
		}
		Transform transform = gameObject.transform;
		for (int j = 0; j <= tweenMaxSearch; j++)
		{
			if (tweens[j].toggle && tweens[j].trans == transform)
			{
				return true;
			}
		}
		return false;
	}

	public static bool isTweening(RectTransform rect)
	{
		return isTweening(rect.gameObject);
	}

	public static bool isTweening(int uniqueId)
	{
		int num = uniqueId & 0xFFFF;
		int num2 = uniqueId >> 16;
		if (num < 0 || num >= maxTweens)
		{
			return false;
		}
		if (tweens[num].counter == num2 && tweens[num].toggle)
		{
			return true;
		}
		return false;
	}

	public static bool isTweening(LTRect ltRect)
	{
		for (int i = 0; i <= tweenMaxSearch; i++)
		{
			if (tweens[i].toggle && tweens[i].ltRect == ltRect)
			{
				return true;
			}
		}
		return false;
	}

	public static void drawBezierPath(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float arrowSize = 0f, Transform arrowTransform = null)
	{
		Vector3 vector = a;
		Vector3 vector2 = -a + 3f * (b - c) + d;
		Vector3 vector3 = 3f * (a + c) - 6f * b;
		Vector3 vector4 = 3f * (b - a);
		if (arrowSize > 0f)
		{
			Vector3 position = arrowTransform.position;
			Quaternion rotation = arrowTransform.rotation;
			float num = 0f;
			for (float num2 = 1f; num2 <= 120f; num2 += 1f)
			{
				float num3 = num2 / 120f;
				Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
				Gizmos.DrawLine(vector, vector5);
				num += (vector5 - vector).magnitude;
				if (num > 1f)
				{
					num -= 1f;
					arrowTransform.position = vector5;
					arrowTransform.LookAt(vector, Vector3.forward);
					Vector3 vector6 = arrowTransform.TransformDirection(Vector3.right);
					Vector3 normalized = (vector - vector5).normalized;
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
					vector6 = arrowTransform.TransformDirection(-Vector3.right);
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
				}
				vector = vector5;
			}
			arrowTransform.position = position;
			arrowTransform.rotation = rotation;
		}
		else
		{
			for (float num4 = 1f; num4 <= 30f; num4 += 1f)
			{
				float num3 = num4 / 30f;
				Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
				Gizmos.DrawLine(vector, vector5);
				vector = vector5;
			}
		}
	}

	public static object logError(string error)
	{
		if (throwErrors)
		{
			UnityEngine.Debug.LogError(error);
		}
		else
		{
			UnityEngine.Debug.Log(error);
		}
		return null;
	}

	public static LTDescr options(LTDescr seed)
	{
		UnityEngine.Debug.LogError("error this function is no longer used");
		return null;
	}

	public static LTDescr options()
	{
		init();
		bool flag = false;
		j = 0;
		i = startSearch;
		while (j <= maxTweens)
		{
			if (j >= maxTweens)
			{
				return logError("LeanTween - You have run out of available spaces for tweening. To avoid this error increase the number of spaces to available for tweening when you initialize the LeanTween class ex: LeanTween.init( " + maxTweens * 2 + " );") as LTDescr;
			}
			if (i >= maxTweens)
			{
				i = 0;
			}
			if (!tweens[i].toggle)
			{
				if (i + 1 > tweenMaxSearch)
				{
					tweenMaxSearch = i + 1;
				}
				startSearch = i + 1;
				flag = true;
				break;
			}
			j++;
			i++;
		}
		if (!flag)
		{
			logError("no available tween found!");
		}
		tweens[i].reset();
		global_counter++;
		if (global_counter > 32768)
		{
			global_counter = 0u;
		}
		tweens[i].setId((uint)i, global_counter);
		return tweens[i];
	}

	private static LTDescr pushNewTween(GameObject gameObject, Vector3 to, float time, LTDescr tween)
	{
		init(maxTweens);
		if (gameObject == null || tween == null)
		{
			return null;
		}
		tween.trans = gameObject.transform;
		tween.to = to;
		tween.time = time;
		return tween;
	}

	public static LTDescr play(RectTransform rectTransform, Sprite[] sprites)
	{
		float time = 0.25f * (float)sprites.Length;
		return pushNewTween(rectTransform.gameObject, new Vector3((float)sprites.Length - 1f, 0f, 0f), time, options().setCanvasPlaySprite().setSprites(sprites).setRepeat(-1));
	}

	public static LTDescr alpha(GameObject gameObject, float to, float time)
	{
		LTDescr lTDescr = pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setAlpha());
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		lTDescr.spriteRen = component;
		return lTDescr;
	}

	public static LTSeq sequence(bool initSequence = true)
	{
		init(maxTweens);
		for (int i = 0; i < sequences.Length; i++)
		{
			if ((sequences[i].tween != null && sequences[i].tween.toggle) || sequences[i].toggle)
			{
				continue;
			}
			LTSeq lTSeq = sequences[i];
			if (initSequence)
			{
				lTSeq.init((uint)(i + tweens.Length), global_counter);
				global_counter++;
				if (global_counter > 32768)
				{
					global_counter = 0u;
				}
			}
			else
			{
				lTSeq.reset();
			}
			return lTSeq;
		}
		return null;
	}

	public static LTDescr alpha(LTRect ltRect, float to, float time)
	{
		ltRect.alphaEnabled = true;
		return pushNewTween(tweenEmpty, new Vector3(to, 0f, 0f), time, options().setGUIAlpha().setRect(ltRect));
	}

	public static LTDescr textAlpha(RectTransform rectTransform, float to, float time)
	{
		return pushNewTween(rectTransform.gameObject, new Vector3(to, 0f, 0f), time, options().setTextAlpha());
	}

	public static LTDescr alphaText(RectTransform rectTransform, float to, float time)
	{
		return pushNewTween(rectTransform.gameObject, new Vector3(to, 0f, 0f), time, options().setTextAlpha());
	}

	public static LTDescr alphaCanvas(CanvasGroup canvasGroup, float to, float time)
	{
		return pushNewTween(canvasGroup.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasGroupAlpha());
	}

	public static LTDescr alphaVertex(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setAlphaVertex());
	}

	public static LTDescr color(GameObject gameObject, Color to, float time)
	{
		LTDescr lTDescr = pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, options().setColor().setPoint(new Vector3(to.r, to.g, to.b)));
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		lTDescr.spriteRen = component;
		return lTDescr;
	}

	public static LTDescr textColor(RectTransform rectTransform, Color to, float time)
	{
		return pushNewTween(rectTransform.gameObject, new Vector3(1f, to.a, 0f), time, options().setTextColor().setPoint(new Vector3(to.r, to.g, to.b)));
	}

	public static LTDescr colorText(RectTransform rectTransform, Color to, float time)
	{
		return pushNewTween(rectTransform.gameObject, new Vector3(1f, to.a, 0f), time, options().setTextColor().setPoint(new Vector3(to.r, to.g, to.b)));
	}

	public static LTDescr delayedCall(float delayTime, Action callback)
	{
		return pushNewTween(tweenEmpty, Vector3.zero, delayTime, options().setCallback().setOnComplete(callback));
	}

	public static LTDescr delayedCall(float delayTime, Action<object> callback)
	{
		return pushNewTween(tweenEmpty, Vector3.zero, delayTime, options().setCallback().setOnComplete(callback));
	}

	public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action callback)
	{
		return pushNewTween(gameObject, Vector3.zero, delayTime, options().setCallback().setOnComplete(callback));
	}

	public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action<object> callback)
	{
		return pushNewTween(gameObject, Vector3.zero, delayTime, options().setCallback().setOnComplete(callback));
	}

	public static LTDescr destroyAfter(LTRect rect, float delayTime)
	{
		return pushNewTween(tweenEmpty, Vector3.zero, delayTime, options().setCallback().setRect(rect).setDestroyOnComplete(doesDestroy: true));
	}

	public static LTDescr move(GameObject gameObject, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setMove());
	}

	public static LTDescr move(GameObject gameObject, Vector2 to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to.x, to.y, gameObject.transform.position.z), time, options().setMove());
	}

	public static LTDescr move(GameObject gameObject, Vector3[] to, float time)
	{
		d = options().setMoveCurved();
		if (d.optional.path == null)
		{
			d.optional.path = new LTBezierPath(to);
		}
		else
		{
			d.optional.path.setPoints(to);
		}
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr move(GameObject gameObject, LTBezierPath to, float time)
	{
		d = options().setMoveCurved();
		d.optional.path = to;
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr move(GameObject gameObject, LTSpline to, float time)
	{
		d = options().setMoveSpline();
		d.optional.spline = to;
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr moveSpline(GameObject gameObject, Vector3[] to, float time)
	{
		d = options().setMoveSpline();
		d.optional.spline = new LTSpline(to);
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr moveSpline(GameObject gameObject, LTSpline to, float time)
	{
		d = options().setMoveSpline();
		d.optional.spline = to;
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr moveSplineLocal(GameObject gameObject, Vector3[] to, float time)
	{
		d = options().setMoveSplineLocal();
		d.optional.spline = new LTSpline(to);
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr move(LTRect ltRect, Vector2 to, float time)
	{
		return pushNewTween(tweenEmpty, to, time, options().setGUIMove().setRect(ltRect));
	}

	public static LTDescr moveMargin(LTRect ltRect, Vector2 to, float time)
	{
		return pushNewTween(tweenEmpty, to, time, options().setGUIMoveMargin().setRect(ltRect));
	}

	public static LTDescr moveX(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveX());
	}

	public static LTDescr moveY(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveY());
	}

	public static LTDescr moveZ(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveZ());
	}

	public static LTDescr moveLocal(GameObject gameObject, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setMoveLocal());
	}

	public static LTDescr moveLocal(GameObject gameObject, Vector3[] to, float time)
	{
		d = options().setMoveCurvedLocal();
		if (d.optional.path == null)
		{
			d.optional.path = new LTBezierPath(to);
		}
		else
		{
			d.optional.path.setPoints(to);
		}
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr moveLocalX(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveLocalX());
	}

	public static LTDescr moveLocalY(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveLocalY());
	}

	public static LTDescr moveLocalZ(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setMoveLocalZ());
	}

	public static LTDescr moveLocal(GameObject gameObject, LTBezierPath to, float time)
	{
		d = options().setMoveCurvedLocal();
		d.optional.path = to;
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr moveLocal(GameObject gameObject, LTSpline to, float time)
	{
		d = options().setMoveSplineLocal();
		d.optional.spline = to;
		return pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, d);
	}

	public static LTDescr move(GameObject gameObject, Transform to, float time)
	{
		return pushNewTween(gameObject, Vector3.zero, time, options().setTo(to).setMoveToTransform());
	}

	public static LTDescr rotate(GameObject gameObject, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setRotate());
	}

	public static LTDescr rotate(LTRect ltRect, float to, float time)
	{
		return pushNewTween(tweenEmpty, new Vector3(to, 0f, 0f), time, options().setGUIRotate().setRect(ltRect));
	}

	public static LTDescr rotateLocal(GameObject gameObject, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setRotateLocal());
	}

	public static LTDescr rotateX(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setRotateX());
	}

	public static LTDescr rotateY(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setRotateY());
	}

	public static LTDescr rotateZ(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setRotateZ());
	}

	public static LTDescr rotateAround(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, options().setAxis(axis).setRotateAround());
	}

	public static LTDescr rotateAroundLocal(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, options().setRotateAroundLocal().setAxis(axis));
	}

	public static LTDescr scale(GameObject gameObject, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setScale());
	}

	public static LTDescr scale(LTRect ltRect, Vector2 to, float time)
	{
		return pushNewTween(tweenEmpty, to, time, options().setGUIScale().setRect(ltRect));
	}

	public static LTDescr scaleX(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setScaleX());
	}

	public static LTDescr scaleY(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setScaleY());
	}

	public static LTDescr scaleZ(GameObject gameObject, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setScaleZ());
	}

	public static LTDescr value(GameObject gameObject, float from, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setCallback().setFrom(new Vector3(from, 0f, 0f)));
	}

	public static LTDescr value(float from, float to, float time)
	{
		return pushNewTween(tweenEmpty, new Vector3(to, 0f, 0f), time, options().setCallback().setFrom(new Vector3(from, 0f, 0f)));
	}

	public static LTDescr value(GameObject gameObject, Vector2 from, Vector2 to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, options().setValue3().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f)));
	}

	public static LTDescr value(GameObject gameObject, Vector3 from, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setValue3().setFrom(from));
	}

	public static LTDescr value(GameObject gameObject, Color from, Color to, float time)
	{
		LTDescr lTDescr = pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setFromColor(from)
			.setHasInitialized(has: false));
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		lTDescr.spriteRen = component;
		return lTDescr;
	}

	public static LTDescr value(GameObject gameObject, Action<float> callOnUpdate, float from, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdate(callOnUpdate));
	}

	public static LTDescr value(GameObject gameObject, Action<float, float> callOnUpdateRatio, float from, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdateRatio(callOnUpdateRatio));
	}

	public static LTDescr value(GameObject gameObject, Action<Color> callOnUpdate, Color from, Color to, float time)
	{
		return pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setAxis(new Vector3(from.r, from.g, from.b))
			.setFrom(new Vector3(0f, from.a, 0f))
			.setHasInitialized(has: false)
			.setOnUpdateColor(callOnUpdate));
	}

	public static LTDescr value(GameObject gameObject, Action<Color, object> callOnUpdate, Color from, Color to, float time)
	{
		return pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setAxis(new Vector3(from.r, from.g, from.b))
			.setFrom(new Vector3(0f, from.a, 0f))
			.setHasInitialized(has: false)
			.setOnUpdateColor(callOnUpdate));
	}

	public static LTDescr value(GameObject gameObject, Action<Vector2> callOnUpdate, Vector2 from, Vector2 to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, options().setValue3().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f))
			.setOnUpdateVector2(callOnUpdate));
	}

	public static LTDescr value(GameObject gameObject, Action<Vector3> callOnUpdate, Vector3 from, Vector3 to, float time)
	{
		return pushNewTween(gameObject, to, time, options().setValue3().setTo(to).setFrom(from)
			.setOnUpdateVector3(callOnUpdate));
	}

	public static LTDescr value(GameObject gameObject, Action<float, object> callOnUpdate, float from, float to, float time)
	{
		return pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f))
			.setOnUpdate(callOnUpdate, gameObject));
	}

	public static LTDescr delayedSound(AudioClip audio, Vector3 pos, float volume)
	{
		return pushNewTween(tweenEmpty, pos, 0f, options().setDelayedSound().setTo(pos).setFrom(new Vector3(volume, 0f, 0f))
			.setAudio(audio));
	}

	public static LTDescr delayedSound(GameObject gameObject, AudioClip audio, Vector3 pos, float volume)
	{
		return pushNewTween(gameObject, pos, 0f, options().setDelayedSound().setTo(pos).setFrom(new Vector3(volume, 0f, 0f))
			.setAudio(audio));
	}

	public static LTDescr move(RectTransform rectTrans, Vector3 to, float time)
	{
		return pushNewTween(rectTrans.gameObject, to, time, options().setCanvasMove().setRect(rectTrans));
	}

	public static LTDescr moveX(RectTransform rectTrans, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasMoveX().setRect(rectTrans));
	}

	public static LTDescr moveY(RectTransform rectTrans, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasMoveY().setRect(rectTrans));
	}

	public static LTDescr moveZ(RectTransform rectTrans, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasMoveZ().setRect(rectTrans));
	}

	public static LTDescr rotate(RectTransform rectTrans, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasRotateAround().setRect(rectTrans).setAxis(Vector3.forward));
	}

	public static LTDescr rotate(RectTransform rectTrans, Vector3 to, float time)
	{
		return pushNewTween(rectTrans.gameObject, to, time, options().setCanvasRotateAround().setRect(rectTrans).setAxis(Vector3.forward));
	}

	public static LTDescr rotateAround(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasRotateAround().setRect(rectTrans).setAxis(axis));
	}

	public static LTDescr rotateAroundLocal(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasRotateAroundLocal().setRect(rectTrans).setAxis(axis));
	}

	public static LTDescr scale(RectTransform rectTrans, Vector3 to, float time)
	{
		return pushNewTween(rectTrans.gameObject, to, time, options().setCanvasScale().setRect(rectTrans));
	}

	public static LTDescr size(RectTransform rectTrans, Vector2 to, float time)
	{
		return pushNewTween(rectTrans.gameObject, to, time, options().setCanvasSizeDelta().setRect(rectTrans));
	}

	public static LTDescr alpha(RectTransform rectTrans, float to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, options().setCanvasAlpha().setRect(rectTrans));
	}

	public static LTDescr color(RectTransform rectTrans, Color to, float time)
	{
		return pushNewTween(rectTrans.gameObject, new Vector3(1f, to.a, 0f), time, options().setCanvasColor().setRect(rectTrans).setPoint(new Vector3(to.r, to.g, to.b)));
	}

	public static float tweenOnCurve(LTDescr tweenDescr, float ratioPassed)
	{
		return tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.optional.animationCurve.Evaluate(ratioPassed);
	}

	public static Vector3 tweenOnCurveVector(LTDescr tweenDescr, float ratioPassed)
	{
		return new Vector3(tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.optional.animationCurve.Evaluate(ratioPassed), tweenDescr.from.y + tweenDescr.diff.y * tweenDescr.optional.animationCurve.Evaluate(ratioPassed), tweenDescr.from.z + tweenDescr.diff.z * tweenDescr.optional.animationCurve.Evaluate(ratioPassed));
	}

	public static float easeOutQuadOpt(float start, float diff, float ratioPassed)
	{
		return (0f - diff) * ratioPassed * (ratioPassed - 2f) + start;
	}

	public static float easeInQuadOpt(float start, float diff, float ratioPassed)
	{
		return diff * ratioPassed * ratioPassed + start;
	}

	public static float easeInOutQuadOpt(float start, float diff, float ratioPassed)
	{
		ratioPassed /= 0.5f;
		if (ratioPassed < 1f)
		{
			return diff / 2f * ratioPassed * ratioPassed + start;
		}
		ratioPassed -= 1f;
		return (0f - diff) / 2f * (ratioPassed * (ratioPassed - 2f) - 1f) + start;
	}

	public static Vector3 easeInOutQuadOpt(Vector3 start, Vector3 diff, float ratioPassed)
	{
		ratioPassed /= 0.5f;
		if (ratioPassed < 1f)
		{
			return diff / 2f * ratioPassed * ratioPassed + start;
		}
		ratioPassed -= 1f;
		return -diff / 2f * (ratioPassed * (ratioPassed - 2f) - 1f) + start;
	}

	public static float linear(float start, float end, float val)
	{
		return Mathf.Lerp(start, end, val);
	}

	public static float clerp(float start, float end, float val)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float num4 = 0f;
		float num5 = 0f;
		if (end - start < 0f - num3)
		{
			num5 = (num2 - start + end) * val;
			return start + num5;
		}
		if (end - start > num3)
		{
			num5 = (0f - (num2 - end + start)) * val;
			return start + num5;
		}
		return start + (end - start) * val;
	}

	public static float spring(float start, float end, float val)
	{
		val = Mathf.Clamp01(val);
		val = (Mathf.Sin(val * MathF.PI * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
		return start + (end - start) * val;
	}

	public static float easeInQuad(float start, float end, float val)
	{
		end -= start;
		return end * val * val + start;
	}

	public static float easeOutQuad(float start, float end, float val)
	{
		end -= start;
		return (0f - end) * val * (val - 2f) + start;
	}

	public static float easeInOutQuad(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val + start;
		}
		val -= 1f;
		return (0f - end) / 2f * (val * (val - 2f) - 1f) + start;
	}

	public static float easeInOutQuadOpt2(float start, float diffBy2, float val, float val2)
	{
		val /= 0.5f;
		if (val < 1f)
		{
			return diffBy2 * val2 + start;
		}
		val -= 1f;
		return (0f - diffBy2) * (val2 - 2f - 1f) + start;
	}

	public static float easeInCubic(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val + start;
	}

	public static float easeOutCubic(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val + 1f) + start;
	}

	public static float easeInOutCubic(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val + 2f) + start;
	}

	public static float easeInQuart(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val + start;
	}

	public static float easeOutQuart(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return (0f - end) * (val * val * val * val - 1f) + start;
	}

	public static float easeInOutQuart(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val + start;
		}
		val -= 2f;
		return (0f - end) / 2f * (val * val * val * val - 2f) + start;
	}

	public static float easeInQuint(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val * val + start;
	}

	public static float easeOutQuint(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val * val * val + 1f) + start;
	}

	public static float easeInOutQuint(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val * val * val + 2f) + start;
	}

	public static float easeInSine(float start, float end, float val)
	{
		end -= start;
		return (0f - end) * Mathf.Cos(val / 1f * (MathF.PI / 2f)) + end + start;
	}

	public static float easeOutSine(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Sin(val / 1f * (MathF.PI / 2f)) + start;
	}

	public static float easeInOutSine(float start, float end, float val)
	{
		end -= start;
		return (0f - end) / 2f * (Mathf.Cos(MathF.PI * val / 1f) - 1f) + start;
	}

	public static float easeInExpo(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (val / 1f - 1f)) + start;
	}

	public static float easeOutExpo(float start, float end, float val)
	{
		end -= start;
		return end * (0f - Mathf.Pow(2f, -10f * val / 1f) + 1f) + start;
	}

	public static float easeInOutExpo(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (val - 1f)) + start;
		}
		val -= 1f;
		return end / 2f * (0f - Mathf.Pow(2f, -10f * val) + 2f) + start;
	}

	public static float easeInCirc(float start, float end, float val)
	{
		end -= start;
		return (0f - end) * (Mathf.Sqrt(1f - val * val) - 1f) + start;
	}

	public static float easeOutCirc(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - val * val) + start;
	}

	public static float easeInOutCirc(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return (0f - end) / 2f * (Mathf.Sqrt(1f - val * val) - 1f) + start;
		}
		val -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - val * val) + 1f) + start;
	}

	public static float easeInBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		return end - easeOutBounce(0f, end, num - val) + start;
	}

	public static float easeOutBounce(float start, float end, float val)
	{
		val /= 1f;
		end -= start;
		if (val < 0.36363637f)
		{
			return end * (7.5625f * val * val) + start;
		}
		if (val < 0.72727275f)
		{
			val -= 0.54545456f;
			return end * (7.5625f * val * val + 0.75f) + start;
		}
		if ((double)val < 0.9090909090909091)
		{
			val -= 0.8181818f;
			return end * (7.5625f * val * val + 0.9375f) + start;
		}
		val -= 21f / 22f;
		return end * (7.5625f * val * val + 63f / 64f) + start;
	}

	public static float easeInOutBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		if (val < num / 2f)
		{
			return easeInBounce(0f, end, val * 2f) * 0.5f + start;
		}
		return easeOutBounce(0f, end, val * 2f - num) * 0.5f + end * 0.5f + start;
	}

	public static float easeInBack(float start, float end, float val, float overshoot = 1f)
	{
		end -= start;
		val /= 1f;
		float num = 1.70158f * overshoot;
		return end * val * val * ((num + 1f) * val - num) + start;
	}

	public static float easeOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val = val / 1f - 1f;
		return end * (val * val * ((num + 1f) * val + num) + 1f) + start;
	}

	public static float easeInOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val /= 0.5f;
		if (val < 1f)
		{
			num *= 1.525f * overshoot;
			return end / 2f * (val * val * ((num + 1f) * val - num)) + start;
		}
		val -= 2f;
		num *= 1.525f * overshoot;
		return end / 2f * (val * val * ((num + 1f) * val + num) + 2f) + start;
	}

	public static float easeInElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		float num2 = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		if (num2 == 0f || num2 < Mathf.Abs(end))
		{
			num2 = end;
			num = period / 4f;
		}
		else
		{
			num = period / (MathF.PI * 2f) * Mathf.Asin(end / num2);
		}
		if (overshoot > 1f && val > 0.6f)
		{
			overshoot = 1f + (1f - val) / 0.4f * (overshoot - 1f);
		}
		val -= 1f;
		return start - num2 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num) * (MathF.PI * 2f) / period) * overshoot;
	}

	public static float easeOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		float num2 = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		if (num2 == 0f || num2 < Mathf.Abs(end))
		{
			num2 = end;
			num = period / 4f;
		}
		else
		{
			num = period / (MathF.PI * 2f) * Mathf.Asin(end / num2);
		}
		if (overshoot > 1f && val < 0.4f)
		{
			overshoot = 1f + val / 0.4f * (overshoot - 1f);
		}
		return start + end + num2 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num) * (MathF.PI * 2f) / period) * overshoot;
	}

	public static float easeInOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		float num2 = 0f;
		if (val == 0f)
		{
			return start;
		}
		val /= 0.5f;
		if (val == 2f)
		{
			return start + end;
		}
		if (num2 == 0f || num2 < Mathf.Abs(end))
		{
			num2 = end;
			num = period / 4f;
		}
		else
		{
			num = period / (MathF.PI * 2f) * Mathf.Asin(end / num2);
		}
		if (overshoot > 1f)
		{
			if (val < 0.2f)
			{
				overshoot = 1f + val / 0.2f * (overshoot - 1f);
			}
			else if (val > 0.8f)
			{
				overshoot = 1f + (1f - val) / 0.2f * (overshoot - 1f);
			}
		}
		if (val < 1f)
		{
			val -= 1f;
			return start - 0.5f * (num2 * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num) * (MathF.PI * 2f) / period)) * overshoot;
		}
		val -= 1f;
		return end + start + num2 * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num) * (MathF.PI * 2f) / period) * 0.5f * overshoot;
	}

	public static void addListener(int eventId, Action<LTEvent> callback)
	{
		addListener(tweenEmpty, eventId, callback);
	}

	public static void addListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		if (eventListeners == null)
		{
			INIT_LISTENERS_MAX = LISTENERS_MAX;
			eventListeners = new Action<LTEvent>[EVENTS_MAX * LISTENERS_MAX];
			goListeners = new GameObject[EVENTS_MAX * LISTENERS_MAX];
		}
		for (i = 0; i < INIT_LISTENERS_MAX; i++)
		{
			int num = eventId * INIT_LISTENERS_MAX + i;
			if (goListeners[num] == null || eventListeners[num] == null)
			{
				eventListeners[num] = callback;
				goListeners[num] = caller;
				if (i >= eventsMaxSearch)
				{
					eventsMaxSearch = i + 1;
				}
				return;
			}
			if (goListeners[num] == caller && object.Equals(eventListeners[num], callback))
			{
				return;
			}
		}
		UnityEngine.Debug.LogError("You ran out of areas to add listeners, consider increasing LISTENERS_MAX, ex: LeanTween.LISTENERS_MAX = " + LISTENERS_MAX * 2);
	}

	public static bool removeListener(int eventId, Action<LTEvent> callback)
	{
		return removeListener(tweenEmpty, eventId, callback);
	}

	public static bool removeListener(int eventId)
	{
		int num = eventId * INIT_LISTENERS_MAX + i;
		eventListeners[num] = null;
		goListeners[num] = null;
		return true;
	}

	public static bool removeListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		for (i = 0; i < eventsMaxSearch; i++)
		{
			int num = eventId * INIT_LISTENERS_MAX + i;
			if (goListeners[num] == caller && object.Equals(eventListeners[num], callback))
			{
				eventListeners[num] = null;
				goListeners[num] = null;
				return true;
			}
		}
		return false;
	}

	public static void dispatchEvent(int eventId)
	{
		dispatchEvent(eventId, null);
	}

	public static void dispatchEvent(int eventId, object data)
	{
		for (int i = 0; i < eventsMaxSearch; i++)
		{
			int num = eventId * INIT_LISTENERS_MAX + i;
			if (eventListeners[num] != null)
			{
				if ((bool)goListeners[num])
				{
					eventListeners[num](new LTEvent(eventId, data));
				}
				else
				{
					eventListeners[num] = null;
				}
			}
		}
	}
}
public class LTUtility
{
	public static Vector3[] reverse(Vector3[] arr)
	{
		int num = arr.Length;
		int num2 = 0;
		int num3 = num - 1;
		while (num2 < num3)
		{
			Vector3 vector = arr[num2];
			arr[num2] = arr[num3];
			arr[num3] = vector;
			num2++;
			num3--;
		}
		return arr;
	}
}
public class LTBezier
{
	public float length;

	private Vector3 a;

	private Vector3 aa;

	private Vector3 bb;

	private Vector3 cc;

	private float len;

	private float[] arcLengths;

	public LTBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float precision)
	{
		this.a = a;
		aa = -a + 3f * (b - c) + d;
		bb = 3f * (a + c) - 6f * b;
		cc = 3f * (b - a);
		len = 1f / precision;
		arcLengths = new float[(int)len + 1];
		arcLengths[0] = 0f;
		Vector3 vector = a;
		float num = 0f;
		for (int i = 1; (float)i <= len; i++)
		{
			Vector3 vector2 = bezierPoint((float)i * precision);
			num += (vector - vector2).magnitude;
			arcLengths[i] = num;
			vector = vector2;
		}
		length = num;
	}

	private float map(float u)
	{
		float num = u * arcLengths[(int)len];
		int num2 = 0;
		int num3 = (int)len;
		int num4 = 0;
		while (num2 < num3)
		{
			num4 = num2 + ((int)((float)(num3 - num2) / 2f) | 0);
			if (arcLengths[num4] < num)
			{
				num2 = num4 + 1;
			}
			else
			{
				num3 = num4;
			}
		}
		if (arcLengths[num4] > num)
		{
			num4--;
		}
		if (num4 < 0)
		{
			num4 = 0;
		}
		return ((float)num4 + (num - arcLengths[num4]) / (arcLengths[num4 + 1] - arcLengths[num4])) / len;
	}

	private Vector3 bezierPoint(float t)
	{
		return ((aa * t + bb) * t + cc) * t + a;
	}

	public Vector3 point(float t)
	{
		return bezierPoint(map(t));
	}
}
public class LTBezierPath
{
	public Vector3[] pts;

	public float length;

	public bool orientToPath;

	public bool orientToPath2d;

	private LTBezier[] beziers;

	private float[] lengthRatio;

	private int currentBezier;

	private int previousBezier;

	public float distance => length;

	public LTBezierPath()
	{
	}

	public LTBezierPath(Vector3[] pts_)
	{
		setPoints(pts_);
	}

	public void setPoints(Vector3[] pts_)
	{
		if (pts_.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, you must pass four or more values!");
		}
		if (pts_.Length % 4 != 0)
		{
			LeanTween.logError("LeanTween - When passing values for a vector path, they must be in sets of four: controlPoint1, controlPoint2, endPoint2, controlPoint2, controlPoint2...");
		}
		pts = pts_;
		int num = 0;
		beziers = new LTBezier[pts.Length / 4];
		lengthRatio = new float[beziers.Length];
		length = 0f;
		for (int i = 0; i < pts.Length; i += 4)
		{
			beziers[num] = new LTBezier(pts[i], pts[i + 2], pts[i + 1], pts[i + 3], 0.05f);
			length += beziers[num].length;
			num++;
		}
		for (int i = 0; i < beziers.Length; i++)
		{
			lengthRatio[i] = beziers[i].length / length;
		}
	}

	public Vector3 point(float ratio)
	{
		float num = 0f;
		for (int i = 0; i < lengthRatio.Length; i++)
		{
			num += lengthRatio[i];
			if (num >= ratio)
			{
				return beziers[i].point((ratio - (num - lengthRatio[i])) / lengthRatio[i]);
			}
		}
		return beziers[lengthRatio.Length - 1].point(1f);
	}

	public void place2d(Transform transform, float ratio)
	{
		transform.position = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = point(ratio) - transform.position;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void placeLocal2d(Transform transform, float ratio)
	{
		transform.localPosition = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = point(ratio) - transform.localPosition;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.localEulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void place(Transform transform, float ratio)
	{
		place(transform, ratio, Vector3.up);
	}

	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(point(ratio), worldUp);
		}
	}

	public void placeLocal(Transform transform, float ratio)
	{
		placeLocal(transform, ratio, Vector3.up);
	}

	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		ratio = Mathf.Clamp01(ratio);
		transform.localPosition = point(ratio);
		ratio = Mathf.Clamp01(ratio + 0.001f);
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(point(ratio)), worldUp);
		}
	}

	public void gizmoDraw(float t = -1f)
	{
		Vector3 to = point(0f);
		for (int i = 1; i <= 120; i++)
		{
			float ratio = (float)i / 120f;
			Vector3 vector = point(ratio);
			Gizmos.color = ((previousBezier == currentBezier) ? Color.magenta : Color.grey);
			Gizmos.DrawLine(vector, to);
			to = vector;
			previousBezier = currentBezier;
		}
	}
}
[Serializable]
public class LTSpline
{
	public static int DISTANCE_COUNT = 3;

	public static int SUBLINE_COUNT = 20;

	public float distance;

	public bool constantSpeed = true;

	public Vector3[] pts;

	[NonSerialized]
	public Vector3[] ptsAdj;

	public int ptsAdjLength;

	public bool orientToPath;

	public bool orientToPath2d;

	private int numSections;

	private int currPt;

	public LTSpline(Vector3[] pts)
	{
		init(pts, constantSpeed: true);
	}

	public LTSpline(Vector3[] pts, bool constantSpeed)
	{
		this.constantSpeed = constantSpeed;
		init(pts, constantSpeed);
	}

	private void init(Vector3[] pts, bool constantSpeed)
	{
		if (pts.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a spline path, you must pass four or more values!");
			return;
		}
		this.pts = new Vector3[pts.Length];
		Array.Copy(pts, this.pts, pts.Length);
		numSections = pts.Length - 3;
		float num = float.PositiveInfinity;
		Vector3 b = this.pts[1];
		float num2 = 0f;
		for (int i = 1; i < this.pts.Length - 1; i++)
		{
			float num3 = Vector3.Distance(this.pts[i], b);
			if (num3 < num)
			{
				num = num3;
			}
			num2 += num3;
		}
		if (!constantSpeed)
		{
			return;
		}
		num = num2 / (float)(numSections * SUBLINE_COUNT);
		float num4 = num / (float)SUBLINE_COUNT;
		int num5 = (int)Mathf.Ceil(num2 / num4) * DISTANCE_COUNT;
		if (num5 <= 1)
		{
			num5 = 2;
		}
		ptsAdj = new Vector3[num5];
		b = interp(0f);
		int num6 = 1;
		ptsAdj[0] = b;
		distance = 0f;
		for (int j = 0; j < num5 + 1; j++)
		{
			float num7 = (float)j / (float)num5;
			Vector3 vector = interp(num7);
			float num8 = Vector3.Distance(vector, b);
			if (num8 >= num4 || num7 >= 1f)
			{
				ptsAdj[num6] = vector;
				distance += num8;
				b = vector;
				num6++;
			}
		}
		ptsAdjLength = num6;
	}

	public Vector3 map(float u)
	{
		if (u >= 1f)
		{
			return pts[pts.Length - 2];
		}
		float num = u * (float)(ptsAdjLength - 1);
		int num2 = (int)Mathf.Floor(num);
		int num3 = (int)Mathf.Ceil(num);
		if (num2 < 0)
		{
			num2 = 0;
		}
		Vector3 vector = ptsAdj[num2];
		Vector3 vector2 = ptsAdj[num3];
		float num4 = num - (float)num2;
		return vector + (vector2 - vector) * num4;
	}

	public Vector3 interp(float t)
	{
		currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
		float num = t * (float)numSections - (float)currPt;
		Vector3 vector = pts[currPt];
		Vector3 vector2 = pts[currPt + 1];
		Vector3 vector3 = pts[currPt + 2];
		Vector3 vector4 = pts[currPt + 3];
		return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num * num * num) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num * num) + (-vector + vector3) * num + 2f * vector2);
	}

	public float ratioAtPoint(Vector3 pt)
	{
		float num = float.MaxValue;
		int num2 = 0;
		for (int i = 0; i < ptsAdjLength; i++)
		{
			float num3 = Vector3.Distance(pt, ptsAdj[i]);
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		return (float)num2 / (float)(ptsAdjLength - 1);
	}

	public Vector3 point(float ratio)
	{
		float num = ((ratio > 1f) ? 1f : ratio);
		if (!constantSpeed)
		{
			return interp(num);
		}
		return map(num);
	}

	public void place2d(Transform transform, float ratio)
	{
		transform.position = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = point(ratio) - transform.position;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void placeLocal2d(Transform transform, float ratio)
	{
		if (transform.parent == null)
		{
			place2d(transform, ratio);
			return;
		}
		transform.localPosition = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = point(ratio) - transform.localPosition;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.localEulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void place(Transform transform, float ratio)
	{
		place(transform, ratio, Vector3.up);
	}

	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(point(ratio), worldUp);
		}
	}

	public void placeLocal(Transform transform, float ratio)
	{
		placeLocal(transform, ratio, Vector3.up);
	}

	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(point(ratio)), worldUp);
		}
	}

	public void gizmoDraw(float t = -1f)
	{
		if (ptsAdj != null && ptsAdj.Length != 0)
		{
			Vector3 vector = ptsAdj[0];
			for (int i = 0; i < ptsAdjLength; i++)
			{
				Vector3 vector2 = ptsAdj[i];
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
		}
	}

	public void drawGizmo(Color color)
	{
		if (ptsAdjLength >= 4)
		{
			Vector3 vector = ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int i = 0; i < ptsAdjLength; i++)
			{
				Vector3 vector2 = ptsAdj[i];
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			Gizmos.color = color2;
		}
	}

	public static void drawGizmo(Transform[] arr, Color color)
	{
		if (arr.Length >= 4)
		{
			Vector3[] array = new Vector3[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				array[i] = arr[i].position;
			}
			LTSpline lTSpline = new LTSpline(array);
			Vector3 vector = lTSpline.ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int j = 0; j < lTSpline.ptsAdjLength; j++)
			{
				Vector3 vector2 = lTSpline.ptsAdj[j];
				Gizmos.DrawLine(vector, vector2);
				vector = vector2;
			}
			Gizmos.color = color2;
		}
	}

	public static void drawLine(Transform[] arr, float width, Color color)
	{
		_ = arr.Length;
		_ = 4;
	}

	public void drawLinesGLLines(Material outlineMaterial, Color color, float width)
	{
		GL.PushMatrix();
		outlineMaterial.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Begin(1);
		GL.Color(color);
		if (constantSpeed)
		{
			if (ptsAdjLength >= 4)
			{
				Vector3 v = ptsAdj[0];
				for (int i = 0; i < ptsAdjLength; i++)
				{
					Vector3 vector = ptsAdj[i];
					GL.Vertex(v);
					GL.Vertex(vector);
					v = vector;
				}
			}
		}
		else if (pts.Length >= 4)
		{
			Vector3 v2 = pts[0];
			float num = 1f / ((float)pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 vector2 = interp(t);
				GL.Vertex(v2);
				GL.Vertex(vector2);
				v2 = vector2;
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	public Vector3[] generateVectors()
	{
		if (pts.Length >= 4)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3 item = pts[0];
			list.Add(item);
			float num = 1f / ((float)pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 item2 = interp(t);
				list.Add(item2);
			}
			list.ToArray();
		}
		return null;
	}
}
[Serializable]
public class LTRect
{
	public Rect _rect;

	public float alpha = 1f;

	public float rotation;

	public Vector2 pivot;

	public Vector2 margin;

	public Rect relativeRect = new Rect(0f, 0f, float.PositiveInfinity, float.PositiveInfinity);

	public bool rotateEnabled;

	[HideInInspector]
	public bool rotateFinished;

	public bool alphaEnabled;

	public string labelStr;

	public LTGUI.Element_Type type;

	public GUIStyle style;

	public bool useColor;

	public Color color = Color.white;

	public bool fontScaleToFit;

	public bool useSimpleScale;

	public bool sizeByHeight;

	public Texture texture;

	private int _id = -1;

	[HideInInspector]
	public int counter;

	public static bool colorTouched;

	public bool hasInitiliazed => _id != -1;

	public int id => _id | (counter << 16);

	public float x
	{
		get
		{
			return _rect.x;
		}
		set
		{
			_rect.x = value;
		}
	}

	public float y
	{
		get
		{
			return _rect.y;
		}
		set
		{
			_rect.y = value;
		}
	}

	public float width
	{
		get
		{
			return _rect.width;
		}
		set
		{
			_rect.width = value;
		}
	}

	public float height
	{
		get
		{
			return _rect.height;
		}
		set
		{
			_rect.height = value;
		}
	}

	public Rect rect
	{
		get
		{
			if (colorTouched)
			{
				colorTouched = false;
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
			}
			if (rotateEnabled)
			{
				if (rotateFinished)
				{
					rotateFinished = false;
					rotateEnabled = false;
					pivot = Vector2.zero;
				}
				else
				{
					GUIUtility.RotateAroundPivot(rotation, pivot);
				}
			}
			if (alphaEnabled)
			{
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
				colorTouched = true;
			}
			if (fontScaleToFit)
			{
				if (useSimpleScale)
				{
					style.fontSize = (int)(_rect.height * relativeRect.height);
				}
				else
				{
					style.fontSize = (int)_rect.height;
				}
			}
			return _rect;
		}
		set
		{
			_rect = value;
		}
	}

	public LTRect()
	{
		reset();
		rotateEnabled = (alphaEnabled = true);
		_rect = new Rect(0f, 0f, 1f, 1f);
	}

	public LTRect(Rect rect)
	{
		_rect = rect;
		reset();
	}

	public LTRect(float x, float y, float width, float height)
	{
		_rect = new Rect(x, y, width, height);
		alpha = 1f;
		rotation = 0f;
		rotateEnabled = (alphaEnabled = false);
	}

	public LTRect(float x, float y, float width, float height, float alpha)
	{
		_rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		rotation = 0f;
		rotateEnabled = (alphaEnabled = false);
	}

	public LTRect(float x, float y, float width, float height, float alpha, float rotation)
	{
		_rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = rotation;
		rotateEnabled = (alphaEnabled = false);
		if (rotation != 0f)
		{
			rotateEnabled = true;
			resetForRotation();
		}
	}

	public void setId(int id, int counter)
	{
		_id = id;
		this.counter = counter;
	}

	public void reset()
	{
		alpha = 1f;
		rotation = 0f;
		rotateEnabled = (alphaEnabled = false);
		margin = Vector2.zero;
		sizeByHeight = false;
		useColor = false;
	}

	public void resetForRotation()
	{
		Vector3 vector = new Vector3(GUI.matrix[0, 0], GUI.matrix[1, 1], GUI.matrix[2, 2]);
		if (pivot == Vector2.zero)
		{
			pivot = new Vector2((_rect.x + _rect.width * 0.5f) * vector.x + GUI.matrix[0, 3], (_rect.y + _rect.height * 0.5f) * vector.y + GUI.matrix[1, 3]);
		}
	}

	public LTRect setStyle(GUIStyle style)
	{
		this.style = style;
		return this;
	}

	public LTRect setFontScaleToFit(bool fontScaleToFit)
	{
		this.fontScaleToFit = fontScaleToFit;
		return this;
	}

	public LTRect setColor(Color color)
	{
		this.color = color;
		useColor = true;
		return this;
	}

	public LTRect setAlpha(float alpha)
	{
		this.alpha = alpha;
		return this;
	}

	public LTRect setLabel(string str)
	{
		labelStr = str;
		return this;
	}

	public LTRect setUseSimpleScale(bool useSimpleScale, Rect relativeRect)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = relativeRect;
		return this;
	}

	public LTRect setUseSimpleScale(bool useSimpleScale)
	{
		this.useSimpleScale = useSimpleScale;
		relativeRect = new Rect(0f, 0f, Screen.width, Screen.height);
		return this;
	}

	public LTRect setSizeByHeight(bool sizeByHeight)
	{
		this.sizeByHeight = sizeByHeight;
		return this;
	}

	public override string ToString()
	{
		return "x:" + _rect.x + " y:" + _rect.y + " width:" + _rect.width + " height:" + _rect.height;
	}
}
public class LTEvent
{
	public int id;

	public object data;

	public LTEvent(int id, object data)
	{
		this.id = id;
		this.data = data;
	}
}
public class LTGUI
{
	public enum Element_Type
	{
		Texture,
		Label
	}

	public static int RECT_LEVELS = 5;

	public static int RECTS_PER_LEVEL = 10;

	public static int BUTTONS_MAX = 24;

	private static LTRect[] levels;

	private static int[] levelDepths;

	private static Rect[] buttons;

	private static int[] buttonLevels;

	private static int[] buttonLastFrame;

	private static LTRect r;

	private static Color color = Color.white;

	private static bool isGUIEnabled = false;

	private static int global_counter = 0;

	public static void init()
	{
		if (levels == null)
		{
			levels = new LTRect[RECT_LEVELS * RECTS_PER_LEVEL];
			levelDepths = new int[RECT_LEVELS];
		}
	}

	public static void initRectCheck()
	{
		if (buttons == null)
		{
			buttons = new Rect[BUTTONS_MAX];
			buttonLevels = new int[BUTTONS_MAX];
			buttonLastFrame = new int[BUTTONS_MAX];
			for (int i = 0; i < buttonLevels.Length; i++)
			{
				buttonLevels[i] = -1;
			}
		}
	}

	public static void reset()
	{
		if (isGUIEnabled)
		{
			isGUIEnabled = false;
			for (int i = 0; i < levels.Length; i++)
			{
				levels[i] = null;
			}
			for (int j = 0; j < levelDepths.Length; j++)
			{
				levelDepths[j] = 0;
			}
		}
	}

	public static void update(int updateLevel)
	{
		if (!isGUIEnabled)
		{
			return;
		}
		init();
		if (levelDepths[updateLevel] <= 0)
		{
			return;
		}
		color = GUI.color;
		int num = updateLevel * RECTS_PER_LEVEL;
		int num2 = num + levelDepths[updateLevel];
		for (int i = num; i < num2; i++)
		{
			r = levels[i];
			if (r == null)
			{
				continue;
			}
			if (r.useColor)
			{
				GUI.color = r.color;
			}
			if (r.type == Element_Type.Label)
			{
				if (r.style != null)
				{
					GUI.skin.label = r.style;
				}
				if (r.useSimpleScale)
				{
					GUI.Label(new Rect((r.rect.x + r.margin.x + r.relativeRect.x) * r.relativeRect.width, (r.rect.y + r.margin.y + r.relativeRect.y) * r.relativeRect.height, r.rect.width * r.relativeRect.width, r.rect.height * r.relativeRect.height), r.labelStr);
				}
				else
				{
					GUI.Label(new Rect(r.rect.x + r.margin.x, r.rect.y + r.margin.y, r.rect.width, r.rect.height), r.labelStr);
				}
			}
			else if (r.type == Element_Type.Texture && r.texture != null)
			{
				Vector2 vector = (r.useSimpleScale ? new Vector2(0f, r.rect.height * r.relativeRect.height) : new Vector2(r.rect.width, r.rect.height));
				if (r.sizeByHeight)
				{
					vector.x = (float)r.texture.width / (float)r.texture.height * vector.y;
				}
				if (r.useSimpleScale)
				{
					GUI.DrawTexture(new Rect((r.rect.x + r.margin.x + r.relativeRect.x) * r.relativeRect.width, (r.rect.y + r.margin.y + r.relativeRect.y) * r.relativeRect.height, vector.x, vector.y), r.texture);
				}
				else
				{
					GUI.DrawTexture(new Rect(r.rect.x + r.margin.x, r.rect.y + r.margin.y, vector.x, vector.y), r.texture);
				}
			}
		}
		GUI.color = color;
	}

	public static bool checkOnScreen(Rect rect)
	{
		bool num = rect.x + rect.width < 0f;
		bool flag = rect.x > (float)Screen.width;
		bool flag2 = rect.y > (float)Screen.height;
		bool flag3 = rect.y + rect.height < 0f;
		return !(num || flag || flag2 || flag3);
	}

	public static void destroy(int id)
	{
		int num = id & 0xFFFF;
		int num2 = id >> 16;
		if (id >= 0 && levels[num] != null && levels[num].hasInitiliazed && levels[num].counter == num2)
		{
			levels[num] = null;
		}
	}

	public static void destroyAll(int depth)
	{
		int num = depth * RECTS_PER_LEVEL + RECTS_PER_LEVEL;
		int num2 = depth * RECTS_PER_LEVEL;
		while (levels != null && num2 < num)
		{
			levels[num2] = null;
			num2++;
		}
	}

	public static LTRect label(Rect rect, string label, int depth)
	{
		return LTGUI.label(new LTRect(rect), label, depth);
	}

	public static LTRect label(LTRect rect, string label, int depth)
	{
		rect.type = Element_Type.Label;
		rect.labelStr = label;
		return element(rect, depth);
	}

	public static LTRect texture(Rect rect, Texture texture, int depth)
	{
		return LTGUI.texture(new LTRect(rect), texture, depth);
	}

	public static LTRect texture(LTRect rect, Texture texture, int depth)
	{
		rect.type = Element_Type.Texture;
		rect.texture = texture;
		return element(rect, depth);
	}

	public static LTRect element(LTRect rect, int depth)
	{
		isGUIEnabled = true;
		init();
		int num = depth * RECTS_PER_LEVEL + RECTS_PER_LEVEL;
		int num2 = 0;
		if (rect != null)
		{
			destroy(rect.id);
		}
		if (rect.type == Element_Type.Label && rect.style != null && rect.style.normal.textColor.a <= 0f)
		{
			UnityEngine.Debug.LogWarning("Your GUI normal color has an alpha of zero, and will not be rendered.");
		}
		if (rect.relativeRect.width == float.PositiveInfinity)
		{
			rect.relativeRect = new Rect(0f, 0f, Screen.width, Screen.height);
		}
		for (int i = depth * RECTS_PER_LEVEL; i < num; i++)
		{
			r = levels[i];
			if (r == null)
			{
				r = rect;
				r.rotateEnabled = true;
				r.alphaEnabled = true;
				r.setId(i, global_counter);
				levels[i] = r;
				if (num2 >= levelDepths[depth])
				{
					levelDepths[depth] = num2 + 1;
				}
				global_counter++;
				return r;
			}
			num2++;
		}
		UnityEngine.Debug.LogError("You ran out of GUI Element spaces");
		return null;
	}

	public static bool hasNoOverlap(Rect rect, int depth)
	{
		initRectCheck();
		bool result = true;
		bool flag = false;
		for (int i = 0; i < buttonLevels.Length; i++)
		{
			if (buttonLevels[i] >= 0)
			{
				if (buttonLastFrame[i] + 1 < Time.frameCount)
				{
					buttonLevels[i] = -1;
				}
				else if (buttonLevels[i] > depth && pressedWithinRect(buttons[i]))
				{
					result = false;
				}
			}
			if (!flag && buttonLevels[i] < 0)
			{
				flag = true;
				buttonLevels[i] = depth;
				buttons[i] = rect;
				buttonLastFrame[i] = Time.frameCount;
			}
		}
		return result;
	}

	public static bool pressedWithinRect(Rect rect)
	{
		Vector2 vector = firstTouch();
		if (vector.x < 0f)
		{
			return false;
		}
		float num = (float)Screen.height - vector.y;
		if (vector.x > rect.x && vector.x < rect.x + rect.width && num > rect.y)
		{
			return num < rect.y + rect.height;
		}
		return false;
	}

	public static bool checkWithinRect(Vector2 vec2, Rect rect)
	{
		vec2.y = (float)Screen.height - vec2.y;
		if (vec2.x > rect.x && vec2.x < rect.x + rect.width && vec2.y > rect.y)
		{
			return vec2.y < rect.y + rect.height;
		}
		return false;
	}

	public static Vector2 firstTouch()
	{
		if (Input.touchCount > 0)
		{
			return Input.touches[0].position;
		}
		if (Input.GetMouseButton(0))
		{
			return Input.mousePosition;
		}
		return new Vector2(float.NegativeInfinity, float.NegativeInfinity);
	}
}
public static class LeanTweenHelpers
{
	public static float ease(float val, LeanTweenType tweenType)
	{
		return ease(0f, 1f, val, tweenType);
	}

	public static float ease(float start, float end, float progress, LeanTweenType tweenType)
	{
		switch (tweenType)
		{
		case LeanTweenType.linear:
			return LeanTween.linear(start, end, progress);
		case LeanTweenType.easeOutQuad:
			return LeanTween.easeOutQuad(start, end, progress);
		case LeanTweenType.easeInQuad:
			return LeanTween.easeInQuad(start, end, progress);
		case LeanTweenType.easeInOutQuad:
			return LeanTween.easeInOutQuad(start, end, progress);
		case LeanTweenType.easeInCubic:
			return LeanTween.easeInCubic(start, end, progress);
		case LeanTweenType.easeOutCubic:
			return LeanTween.easeOutCubic(start, end, progress);
		case LeanTweenType.easeInOutCubic:
			return LeanTween.easeInOutCubic(start, end, progress);
		case LeanTweenType.easeInQuart:
			return LeanTween.easeInQuart(start, end, progress);
		case LeanTweenType.easeOutQuart:
			return LeanTween.easeOutQuart(start, end, progress);
		case LeanTweenType.easeInOutQuart:
			return LeanTween.easeInOutQuart(start, end, progress);
		case LeanTweenType.easeInQuint:
			return LeanTween.easeInQuint(start, end, progress);
		case LeanTweenType.easeOutQuint:
			return LeanTween.easeOutQuint(start, end, progress);
		case LeanTweenType.easeInOutQuint:
			return LeanTween.easeInOutQuint(start, end, progress);
		case LeanTweenType.easeInSine:
			return LeanTween.easeInSine(start, end, progress);
		case LeanTweenType.easeOutSine:
			return LeanTween.easeOutSine(start, end, progress);
		case LeanTweenType.easeInOutSine:
			return LeanTween.easeInOutSine(start, end, progress);
		case LeanTweenType.easeInExpo:
			return LeanTween.easeInExpo(start, end, progress);
		case LeanTweenType.easeOutExpo:
			return LeanTween.easeOutExpo(start, end, progress);
		case LeanTweenType.easeInOutExpo:
			return LeanTween.easeInOutExpo(start, end, progress);
		case LeanTweenType.easeInCirc:
			return LeanTween.easeInCirc(start, end, progress);
		case LeanTweenType.easeOutCirc:
			return LeanTween.easeOutCirc(start, end, progress);
		case LeanTweenType.easeInOutCirc:
			return LeanTween.easeInOutCirc(start, end, progress);
		case LeanTweenType.easeInBounce:
			return LeanTween.easeInBounce(start, end, progress);
		case LeanTweenType.easeOutBounce:
			return LeanTween.easeOutBounce(start, end, progress);
		case LeanTweenType.easeInOutBounce:
			return LeanTween.easeInOutBounce(start, end, progress);
		case LeanTweenType.easeInBack:
			return LeanTween.easeInBack(start, end, progress);
		case LeanTweenType.easeOutBack:
			return LeanTween.easeOutBack(start, end, progress);
		case LeanTweenType.easeInOutBack:
			return LeanTween.easeInOutBack(start, end, progress);
		case LeanTweenType.easeInElastic:
			return LeanTween.easeInElastic(start, end, progress);
		case LeanTweenType.easeOutElastic:
			return LeanTween.easeOutElastic(start, end, progress);
		case LeanTweenType.easeInOutElastic:
			return LeanTween.easeInOutElastic(start, end, progress);
		default:
			UnityEngine.Debug.LogError($"Ease type {tweenType} is unsupported for this helper method.");
			return float.NaN;
		}
	}
}
public class LTDescr
{
	public delegate Vector3 EaseTypeDelegate();

	public delegate void ActionMethodDelegate();

	public bool toggle;

	public bool useEstimatedTime;

	public bool useFrames;

	public bool useManualTime;

	public bool usesNormalDt;

	public bool hasInitiliazed;

	public bool hasExtraOnCompletes;

	public bool hasPhysics;

	public bool onCompleteOnRepeat;

	public bool onCompleteOnStart;

	public bool useRecursion;

	public float ratioPassed;

	public float passed;

	public float delay;

	public float time;

	public float speed;

	public float lastVal;

	private uint _id;

	public int loopCount;

	public uint counter;

	public float direction;

	public float directionLast;

	public float overshoot;

	public float period;

	public float scale;

	public bool destroyOnComplete;

	public Transform trans;

	public LTRect ltRect;

	internal Vector3 fromInternal;

	internal Vector3 toInternal;

	internal Vector3 diff;

	internal Vector3 diffDiv2;

	public TweenAction type;

	private LeanTweenType easeType;

	public LeanTweenType loopType;

	public bool hasUpdateCallback;

	public EaseTypeDelegate easeMethod;

	public SpriteRenderer spriteRen;

	public RectTransform rectTransform;

	public Text uiText;

	public Image uiImage;

	public RawImage rawImage;

	public Sprite[] sprites;

	public LTDescrOptional _optional = new LTDescrOptional();

	public static float val;

	public static float dt;

	public static Vector3 newVect;

	public Vector3 from
	{
		get
		{
			return fromInternal;
		}
		set
		{
			fromInternal = value;
		}
	}

	public Vector3 to
	{
		get
		{
			return toInternal;
		}
		set
		{
			toInternal = value;
		}
	}

	public ActionMethodDelegate easeInternal { get; set; }

	public ActionMethodDelegate initInternal { get; set; }

	public int uniqueId => (int)(_id | (counter << 16));

	public int id => uniqueId;

	public LTDescrOptional optional
	{
		get
		{
			return _optional;
		}
		set
		{
			_optional = optional;
		}
	}

	public override string ToString()
	{
		string[] obj = new string[27]
		{
			(trans != null) ? ("name:" + trans.gameObject.name) : "gameObject:null",
			" toggle:",
			toggle.ToString(),
			" passed:",
			passed.ToString(),
			" time:",
			time.ToString(),
			" delay:",
			delay.ToString(),
			" direction:",
			direction.ToString(),
			" from:",
			from.ToString(),
			" to:",
			to.ToString(),
			" diff:",
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		};
		Vector3 vector = diff;
		obj[16] = vector.ToString();
		obj[17] = " type:";
		obj[18] = type.ToString();
		obj[19] = " ease:";
		obj[20] = easeType.ToString();
		obj[21] = " useEstimatedTime:";
		obj[22] = useEstimatedTime.ToString();
		obj[23] = " id:";
		obj[24] = id.ToString();
		obj[25] = " hasInitiliazed:";
		obj[26] = hasInitiliazed.ToString();
		return string.Concat(obj);
	}

	[Obsolete("Use 'LeanTween.cancel( id )' instead")]
	public LTDescr cancel(GameObject gameObject)
	{
		if (gameObject == trans.gameObject)
		{
			LeanTween.removeTween((int)_id, uniqueId);
		}
		return this;
	}

	public void reset()
	{
		toggle = (useRecursion = (usesNormalDt = true));
		trans = null;
		spriteRen = null;
		passed = (delay = (lastVal = 0f));
		hasUpdateCallback = (useEstimatedTime = (useFrames = (hasInitiliazed = (onCompleteOnRepeat = (destroyOnComplete = (onCompleteOnStart = (useManualTime = (hasExtraOnCompletes = false))))))));
		easeType = LeanTweenType.linear;
		loopType = LeanTweenType.once;
		loopCount = 0;
		direction = (directionLast = (overshoot = (scale = 1f)));
		period = 0.3f;
		speed = -1f;
		easeMethod = easeLinear;
		Vector3 vector = (to = Vector3.zero);
		from = vector;
		_optional.reset();
	}

	public LTDescr setMoveX()
	{
		type = TweenAction.MOVE_X;
		initInternal = delegate
		{
			fromInternal.x = trans.position.x;
		};
		easeInternal = delegate
		{
			trans.position = new Vector3(easeMethod().x, trans.position.y, trans.position.z);
		};
		return this;
	}

	public LTDescr setMoveY()
	{
		type = TweenAction.MOVE_Y;
		initInternal = delegate
		{
			fromInternal.x = trans.position.y;
		};
		easeInternal = delegate
		{
			trans.position = new Vector3(trans.position.x, easeMethod().x, trans.position.z);
		};
		return this;
	}

	public LTDescr setMoveZ()
	{
		type = TweenAction.MOVE_Z;
		initInternal = delegate
		{
			fromInternal.x = trans.position.z;
		};
		easeInternal = delegate
		{
			trans.position = new Vector3(trans.position.x, trans.position.y, easeMethod().x);
		};
		return this;
	}

	public LTDescr setMoveLocalX()
	{
		type = TweenAction.MOVE_LOCAL_X;
		initInternal = delegate
		{
			fromInternal.x = trans.localPosition.x;
		};
		easeInternal = delegate
		{
			trans.localPosition = new Vector3(easeMethod().x, trans.localPosition.y, trans.localPosition.z);
		};
		return this;
	}

	public LTDescr setMoveLocalY()
	{
		type = TweenAction.MOVE_LOCAL_Y;
		initInternal = delegate
		{
			fromInternal.x = trans.localPosition.y;
		};
		easeInternal = delegate
		{
			trans.localPosition = new Vector3(trans.localPosition.x, easeMethod().x, trans.localPosition.z);
		};
		return this;
	}

	public LTDescr setMoveLocalZ()
	{
		type = TweenAction.MOVE_LOCAL_Z;
		initInternal = delegate
		{
			fromInternal.x = trans.localPosition.z;
		};
		easeInternal = delegate
		{
			trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, easeMethod().x);
		};
		return this;
	}

	private void initFromInternal()
	{
		fromInternal.x = 0f;
	}

	public LTDescr setMoveCurved()
	{
		type = TweenAction.MOVE_CURVED;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.path.orientToPath)
			{
				if (_optional.path.orientToPath2d)
				{
					_optional.path.place2d(trans, val);
				}
				else
				{
					_optional.path.place(trans, val);
				}
			}
			else
			{
				trans.position = _optional.path.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveCurvedLocal()
	{
		type = TweenAction.MOVE_CURVED_LOCAL;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.path.orientToPath)
			{
				if (_optional.path.orientToPath2d)
				{
					_optional.path.placeLocal2d(trans, val);
				}
				else
				{
					_optional.path.placeLocal(trans, val);
				}
			}
			else
			{
				trans.localPosition = _optional.path.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveSpline()
	{
		type = TweenAction.MOVE_SPLINE;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.spline.orientToPath)
			{
				if (_optional.spline.orientToPath2d)
				{
					_optional.spline.place2d(trans, val);
				}
				else
				{
					_optional.spline.place(trans, val);
				}
			}
			else
			{
				trans.position = _optional.spline.point(val);
			}
		};
		return this;
	}

	public LTDescr setMoveSplineLocal()
	{
		type = TweenAction.MOVE_SPLINE_LOCAL;
		initInternal = initFromInternal;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (_optional.spline.orientToPath)
			{
				if (_optional.spline.orientToPath2d)
				{
					_optional.spline.placeLocal2d(trans, val);
				}
				else
				{
					_optional.spline.placeLocal(trans, val);
				}
			}
			else
			{
				trans.localPosition = _optional.spline.point(val);
			}
		};
		return this;
	}

	public LTDescr setScaleX()
	{
		type = TweenAction.SCALE_X;
		initInternal = delegate
		{
			fromInternal.x = trans.localScale.x;
		};
		easeInternal = delegate
		{
			trans.localScale = new Vector3(easeMethod().x, trans.localScale.y, trans.localScale.z);
		};
		return this;
	}

	public LTDescr setScaleY()
	{
		type = TweenAction.SCALE_Y;
		initInternal = delegate
		{
			fromInternal.x = trans.localScale.y;
		};
		easeInternal = delegate
		{
			trans.localScale = new Vector3(trans.localScale.x, easeMethod().x, trans.localScale.z);
		};
		return this;
	}

	public LTDescr setScaleZ()
	{
		type = TweenAction.SCALE_Z;
		initInternal = delegate
		{
			fromInternal.x = trans.localScale.z;
		};
		easeInternal = delegate
		{
			trans.localScale = new Vector3(trans.localScale.x, trans.localScale.y, easeMethod().x);
		};
		return this;
	}

	public LTDescr setRotateX()
	{
		type = TweenAction.ROTATE_X;
		initInternal = delegate
		{
			fromInternal.x = trans.eulerAngles.x;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			trans.eulerAngles = new Vector3(easeMethod().x, trans.eulerAngles.y, trans.eulerAngles.z);
		};
		return this;
	}

	public LTDescr setRotateY()
	{
		type = TweenAction.ROTATE_Y;
		initInternal = delegate
		{
			fromInternal.x = trans.eulerAngles.y;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			trans.eulerAngles = new Vector3(trans.eulerAngles.x, easeMethod().x, trans.eulerAngles.z);
		};
		return this;
	}

	public LTDescr setRotateZ()
	{
		type = TweenAction.ROTATE_Z;
		initInternal = delegate
		{
			fromInternal.x = trans.eulerAngles.z;
			toInternal.x = LeanTween.closestRot(fromInternal.x, toInternal.x);
		};
		easeInternal = delegate
		{
			trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, easeMethod().x);
		};
		return this;
	}

	public LTDescr setRotateAround()
	{
		type = TweenAction.ROTATE_AROUND;
		initInternal = delegate
		{
			fromInternal.x = 0f;
			_optional.origRotation = trans.rotation;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Vector3 localPosition = trans.localPosition;
			Vector3 point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, _optional.axis, 0f - _optional.lastVal);
			Vector3 vector = localPosition - trans.localPosition;
			trans.localPosition = localPosition - vector;
			trans.rotation = _optional.origRotation;
			point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, _optional.axis, val);
			_optional.lastVal = val;
		};
		return this;
	}

	public LTDescr setRotateAroundLocal()
	{
		type = TweenAction.ROTATE_AROUND_LOCAL;
		initInternal = delegate
		{
			fromInternal.x = 0f;
			_optional.origRotation = trans.localRotation;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Vector3 localPosition = trans.localPosition;
			trans.RotateAround(trans.TransformPoint(_optional.point), trans.TransformDirection(_optional.axis), 0f - _optional.lastVal);
			Vector3 vector = localPosition - trans.localPosition;
			trans.localPosition = localPosition - vector;
			trans.localRotation = _optional.origRotation;
			Vector3 point = trans.TransformPoint(_optional.point);
			trans.RotateAround(point, trans.TransformDirection(_optional.axis), val);
			_optional.lastVal = val;
		};
		return this;
	}

	public LTDescr setAlpha()
	{
		type = TweenAction.ALPHA;
		initInternal = delegate
		{
			SpriteRenderer component = trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				fromInternal.x = component.color.a;
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				fromInternal.x = trans.GetComponent<Renderer>().material.color.a;
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color = trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				fromInternal.x = color.a;
			}
			else if (trans.childCount > 0)
			{
				foreach (Transform tran in trans)
				{
					if (tran.gameObject.GetComponent<Renderer>() != null)
					{
						Color color2 = tran.gameObject.GetComponent<Renderer>().material.color;
						fromInternal.x = color2.a;
						break;
					}
				}
			}
			easeInternal = delegate
			{
				val = easeMethod().x;
				if (spriteRen != null)
				{
					spriteRen.color = new Color(spriteRen.color.r, spriteRen.color.g, spriteRen.color.b, val);
					alphaRecursiveSprite(trans, val);
				}
				else
				{
					alphaRecursive(trans, val, useRecursion);
				}
			};
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (spriteRen != null)
			{
				spriteRen.color = new Color(spriteRen.color.r, spriteRen.color.g, spriteRen.color.b, val);
				alphaRecursiveSprite(trans, val);
			}
			else
			{
				alphaRecursive(trans, val, useRecursion);
			}
		};
		return this;
	}

	public LTDescr setTextAlpha()
	{
		type = TweenAction.TEXT_ALPHA;
		initInternal = delegate
		{
			uiText = trans.GetComponent<Text>();
			fromInternal.x = ((uiText != null) ? uiText.color.a : 1f);
		};
		easeInternal = delegate
		{
			textAlphaRecursive(trans, easeMethod().x, useRecursion);
		};
		return this;
	}

	public LTDescr setAlphaVertex()
	{
		type = TweenAction.ALPHA_VERTEX;
		initInternal = delegate
		{
			fromInternal.x = (int)trans.GetComponent<MeshFilter>().mesh.colors32[0].a;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Mesh mesh = trans.GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = mesh.vertices;
			Color32[] array = new Color32[vertices.Length];
			if (array.Length == 0)
			{
				Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				array = new Color32[mesh.vertices.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = color;
				}
				mesh.colors32 = array;
			}
			Color32 color2 = mesh.colors32[0];
			color2 = new Color((int)color2.r, (int)color2.g, (int)color2.b, val);
			for (int j = 0; j < vertices.Length; j++)
			{
				array[j] = color2;
			}
			mesh.colors32 = array;
		};
		return this;
	}

	public LTDescr setColor()
	{
		type = TweenAction.COLOR;
		initInternal = delegate
		{
			SpriteRenderer component = trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				setFromColor(component.color);
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				Color color = trans.GetComponent<Renderer>().material.color;
				setFromColor(color);
			}
			else if (trans.GetComponent<Renderer>() != null && trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color2 = trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				setFromColor(color2);
			}
			else if (trans.childCount > 0)
			{
				foreach (Transform tran in trans)
				{
					if (tran.gameObject.GetComponent<Renderer>() != null)
					{
						Color color3 = tran.gameObject.GetComponent<Renderer>().material.color;
						setFromColor(color3);
						break;
					}
				}
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (spriteRen != null)
			{
				spriteRen.color = color;
				colorRecursiveSprite(trans, color);
			}
			else if (type == TweenAction.COLOR)
			{
				colorRecursive(trans, color, useRecursion);
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			else if (dt != 0f && _optional.onUpdateColorObject != null)
			{
				_optional.onUpdateColorObject(color, _optional.onUpdateParam);
			}
		};
		return this;
	}

	public LTDescr setCallbackColor()
	{
		type = TweenAction.CALLBACK_COLOR;
		initInternal = delegate
		{
			diff = new Vector3(1f, 0f, 0f);
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (spriteRen != null)
			{
				spriteRen.color = color;
				colorRecursiveSprite(trans, color);
			}
			else if (type == TweenAction.COLOR)
			{
				colorRecursive(trans, color, useRecursion);
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			else if (dt != 0f && _optional.onUpdateColorObject != null)
			{
				_optional.onUpdateColorObject(color, _optional.onUpdateParam);
			}
		};
		return this;
	}

	public LTDescr setTextColor()
	{
		type = TweenAction.TEXT_COLOR;
		initInternal = delegate
		{
			uiText = trans.GetComponent<Text>();
			setFromColor((uiText != null) ? uiText.color : Color.white);
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			uiText.color = color;
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			if (useRecursion && trans.childCount > 0)
			{
				textColorRecursive(trans, color);
			}
		};
		return this;
	}

	public LTDescr setCanvasAlpha()
	{
		type = TweenAction.CANVAS_ALPHA;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			if (uiImage != null)
			{
				fromInternal.x = uiImage.color.a;
			}
			else
			{
				rawImage = trans.GetComponent<RawImage>();
				if (rawImage != null)
				{
					fromInternal.x = rawImage.color.a;
				}
				else
				{
					fromInternal.x = 1f;
				}
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			if (uiImage != null)
			{
				Color color = uiImage.color;
				color.a = val;
				uiImage.color = color;
			}
			else if (rawImage != null)
			{
				Color color2 = rawImage.color;
				color2.a = val;
				rawImage.color = color2;
			}
			if (useRecursion)
			{
				alphaRecursive(rectTransform, val);
				textAlphaChildrenRecursive(rectTransform, val);
			}
		};
		return this;
	}

	public LTDescr setCanvasGroupAlpha()
	{
		type = TweenAction.CANVASGROUP_ALPHA;
		initInternal = delegate
		{
			fromInternal.x = trans.GetComponent<CanvasGroup>().alpha;
		};
		easeInternal = delegate
		{
			trans.GetComponent<CanvasGroup>().alpha = easeMethod().x;
		};
		return this;
	}

	public LTDescr setCanvasColor()
	{
		type = TweenAction.CANVAS_COLOR;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			if (uiImage == null)
			{
				rawImage = trans.GetComponent<RawImage>();
				setFromColor((rawImage != null) ? rawImage.color : Color.white);
			}
			else
			{
				setFromColor(uiImage.color);
			}
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			Color color = tweenColor(this, val);
			if (uiImage != null)
			{
				uiImage.color = color;
			}
			else if (rawImage != null)
			{
				rawImage.color = color;
			}
			if (dt != 0f && _optional.onUpdateColor != null)
			{
				_optional.onUpdateColor(color);
			}
			if (useRecursion)
			{
				colorRecursive(rectTransform, color);
			}
		};
		return this;
	}

	public LTDescr setCanvasMoveX()
	{
		type = TweenAction.CANVAS_MOVE_X;
		initInternal = delegate
		{
			fromInternal.x = rectTransform.anchoredPosition3D.x;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			rectTransform.anchoredPosition3D = new Vector3(easeMethod().x, anchoredPosition3D.y, anchoredPosition3D.z);
		};
		return this;
	}

	public LTDescr setCanvasMoveY()
	{
		type = TweenAction.CANVAS_MOVE_Y;
		initInternal = delegate
		{
			fromInternal.x = rectTransform.anchoredPosition3D.y;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D.x, easeMethod().x, anchoredPosition3D.z);
		};
		return this;
	}

	public LTDescr setCanvasMoveZ()
	{
		type = TweenAction.CANVAS_MOVE_Z;
		initInternal = delegate
		{
			fromInternal.x = rectTransform.anchoredPosition3D.z;
		};
		easeInternal = delegate
		{
			Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
			rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D.x, anchoredPosition3D.y, easeMethod().x);
		};
		return this;
	}

	private void initCanvasRotateAround()
	{
		lastVal = 0f;
		fromInternal.x = 0f;
		_optional.origRotation = rectTransform.rotation;
	}

	public LTDescr setCanvasRotateAround()
	{
		type = TweenAction.CANVAS_ROTATEAROUND;
		initInternal = initCanvasRotateAround;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), _optional.axis, 0f - val);
			Vector3 vector = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - vector;
			rectTransform.rotation = _optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), _optional.axis, val);
		};
		return this;
	}

	public LTDescr setCanvasRotateAroundLocal()
	{
		type = TweenAction.CANVAS_ROTATEAROUND_LOCAL;
		initInternal = initCanvasRotateAround;
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), rectTransform.TransformDirection(_optional.axis), 0f - val);
			Vector3 vector = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - vector;
			rectTransform.rotation = _optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(_optional.point), rectTransform.TransformDirection(_optional.axis), val);
		};
		return this;
	}

	public LTDescr setCanvasPlaySprite()
	{
		type = TweenAction.CANVAS_PLAYSPRITE;
		initInternal = delegate
		{
			uiImage = trans.GetComponent<Image>();
			fromInternal.x = 0f;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			val = newVect.x;
			int num = (int)Mathf.Round(val);
			uiImage.sprite = sprites[num];
		};
		return this;
	}

	public LTDescr setCanvasMove()
	{
		type = TweenAction.CANVAS_MOVE;
		initInternal = delegate
		{
			fromInternal = rectTransform.anchoredPosition3D;
		};
		easeInternal = delegate
		{
			rectTransform.anchoredPosition3D = easeMethod();
		};
		return this;
	}

	public LTDescr setCanvasScale()
	{
		type = TweenAction.CANVAS_SCALE;
		initInternal = delegate
		{
			from = rectTransform.localScale;
		};
		easeInternal = delegate
		{
			rectTransform.localScale = easeMethod();
		};
		return this;
	}

	public LTDescr setCanvasSizeDelta()
	{
		type = TweenAction.CANVAS_SIZEDELTA;
		initInternal = delegate
		{
			from = rectTransform.sizeDelta;
		};
		easeInternal = delegate
		{
			rectTransform.sizeDelta = easeMethod();
		};
		return this;
	}

	private void callback()
	{
		newVect = easeMethod();
		val = newVect.x;
	}

	public LTDescr setCallback()
	{
		type = TweenAction.CALLBACK;
		initInternal = delegate
		{
		};
		easeInternal = callback;
		return this;
	}

	public LTDescr setValue3()
	{
		type = TweenAction.VALUE3;
		initInternal = delegate
		{
		};
		easeInternal = callback;
		return this;
	}

	public LTDescr setMove()
	{
		type = TweenAction.MOVE;
		initInternal = delegate
		{
			from = trans.position;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.position = newVect;
		};
		return this;
	}

	public LTDescr setMoveLocal()
	{
		type = TweenAction.MOVE_LOCAL;
		initInternal = delegate
		{
			from = trans.localPosition;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localPosition = newVect;
		};
		return this;
	}

	public LTDescr setMoveToTransform()
	{
		type = TweenAction.MOVE_TO_TRANSFORM;
		initInternal = delegate
		{
			from = trans.position;
		};
		easeInternal = delegate
		{
			to = _optional.toTrans.position;
			diff = to - from;
			diffDiv2 = diff * 0.5f;
			newVect = easeMethod();
			trans.position = newVect;
		};
		return this;
	}

	public LTDescr setRotate()
	{
		type = TweenAction.ROTATE;
		initInternal = delegate
		{
			from = trans.eulerAngles;
			to = new Vector3(LeanTween.closestRot(fromInternal.x, toInternal.x), LeanTween.closestRot(from.y, to.y), LeanTween.closestRot(from.z, to.z));
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.eulerAngles = newVect;
		};
		return this;
	}

	public LTDescr setRotateLocal()
	{
		type = TweenAction.ROTATE_LOCAL;
		initInternal = delegate
		{
			from = trans.localEulerAngles;
			to = new Vector3(LeanTween.closestRot(fromInternal.x, toInternal.x), LeanTween.closestRot(from.y, to.y), LeanTween.closestRot(from.z, to.z));
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localEulerAngles = newVect;
		};
		return this;
	}

	public LTDescr setScale()
	{
		type = TweenAction.SCALE;
		initInternal = delegate
		{
			from = trans.localScale;
		};
		easeInternal = delegate
		{
			newVect = easeMethod();
			trans.localScale = newVect;
		};
		return this;
	}

	public LTDescr setGUIMove()
	{
		type = TweenAction.GUI_MOVE;
		initInternal = delegate
		{
			from = new Vector3(_optional.ltRect.rect.x, _optional.ltRect.rect.y, 0f);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.rect = new Rect(vector.x, vector.y, _optional.ltRect.rect.width, _optional.ltRect.rect.height);
		};
		return this;
	}

	public LTDescr setGUIMoveMargin()
	{
		type = TweenAction.GUI_MOVE_MARGIN;
		initInternal = delegate
		{
			from = new Vector2(_optional.ltRect.margin.x, _optional.ltRect.margin.y);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.margin = new Vector2(vector.x, vector.y);
		};
		return this;
	}

	public LTDescr setGUIScale()
	{
		type = TweenAction.GUI_SCALE;
		initInternal = delegate
		{
			from = new Vector3(_optional.ltRect.rect.width, _optional.ltRect.rect.height, 0f);
		};
		easeInternal = delegate
		{
			Vector3 vector = easeMethod();
			_optional.ltRect.rect = new Rect(_optional.ltRect.rect.x, _optional.ltRect.rect.y, vector.x, vector.y);
		};
		return this;
	}

	public LTDescr setGUIAlpha()
	{
		type = TweenAction.GUI_ALPHA;
		initInternal = delegate
		{
			fromInternal.x = _optional.ltRect.alpha;
		};
		easeInternal = delegate
		{
			_optional.ltRect.alpha = easeMethod().x;
		};
		return this;
	}

	public LTDescr setGUIRotate()
	{
		type = TweenAction.GUI_ROTATE;
		initInternal = delegate
		{
			if (!_optional.ltRect.rotateEnabled)
			{
				_optional.ltRect.rotateEnabled = true;
				_optional.ltRect.resetForRotation();
			}
			fromInternal.x = _optional.ltRect.rotation;
		};
		easeInternal = delegate
		{
			_optional.ltRect.rotation = easeMethod().x;
		};
		return this;
	}

	public LTDescr setDelayedSound()
	{
		type = TweenAction.DELAYED_SOUND;
		initInternal = delegate
		{
			hasExtraOnCompletes = true;
		};
		easeInternal = callback;
		return this;
	}

	private void init()
	{
		hasInitiliazed = true;
		usesNormalDt = !useEstimatedTime && !useManualTime && !useFrames;
		if (useFrames)
		{
			optional.initFrameCount = Time.frameCount;
		}
		if (time <= 0f)
		{
			time = Mathf.Epsilon;
		}
		initInternal();
		diff = to - from;
		diffDiv2 = diff * 0.5f;
		if (_optional.onStart != null)
		{
			_optional.onStart();
		}
		if (onCompleteOnStart)
		{
			callOnCompletes();
		}
		if (speed >= 0f)
		{
			initSpeed();
		}
	}

	private void initSpeed()
	{
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			time = _optional.path.distance / speed;
		}
		else if (type == TweenAction.MOVE_SPLINE || type == TweenAction.MOVE_SPLINE_LOCAL)
		{
			time = _optional.spline.distance / speed;
		}
		else
		{
			time = (to - from).magnitude / speed;
		}
	}

	public LTDescr updateNow()
	{
		updateInternal();
		return this;
	}

	public bool updateInternal()
	{
		float num = direction;
		if (usesNormalDt)
		{
			dt = LeanTween.dtActual;
		}
		else if (useEstimatedTime)
		{
			dt = LeanTween.dtEstimated;
		}
		else if (useFrames)
		{
			dt = ((optional.initFrameCount != 0) ? 1 : 0);
			optional.initFrameCount = Time.frameCount;
		}
		else if (useManualTime)
		{
			dt = LeanTween.dtManual;
		}
		if (delay <= 0f && num != 0f)
		{
			if (trans == null)
			{
				return true;
			}
			if (!hasInitiliazed)
			{
				init();
			}
			dt *= num;
			passed += dt;
			if (passed > time)
			{
				passed = time;
			}
			ratioPassed = passed / time;
			easeInternal();
			if (hasUpdateCallback)
			{
				_optional.callOnUpdate(val, ratioPassed);
			}
			if ((num > 0f) ? (passed >= time) : (passed <= 0f))
			{
				loopCount--;
				if (loopType == LeanTweenType.pingPong)
				{
					direction = 0f - num;
				}
				else
				{
					passed = Mathf.Epsilon;
				}
				int num2;
				if (loopCount != 0)
				{
					num2 = ((loopType == LeanTweenType.once) ? 1 : 0);
					if (num2 == 0 && onCompleteOnRepeat && hasExtraOnCompletes)
					{
						callOnCompletes();
					}
				}
				else
				{
					num2 = 1;
				}
				return (byte)num2 != 0;
			}
		}
		else
		{
			delay -= dt;
		}
		return false;
	}

	public void callOnCompletes()
	{
		if (type == TweenAction.GUI_ROTATE)
		{
			_optional.ltRect.rotateFinished = true;
		}
		if (type == TweenAction.DELAYED_SOUND)
		{
			AudioSource.PlayClipAtPoint((AudioClip)_optional.onCompleteParam, to, from.x);
		}
		if (_optional.onComplete != null)
		{
			_optional.onComplete();
		}
		else if (_optional.onCompleteObject != null)
		{
			_optional.onCompleteObject(_optional.onCompleteParam);
		}
	}

	public LTDescr setFromColor(Color col)
	{
		from = new Vector3(0f, col.a, 0f);
		diff = new Vector3(1f, 0f, 0f);
		_optional.axis = new Vector3(col.r, col.g, col.b);
		return this;
	}

	private static void alphaRecursive(Transform transform, float val, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			foreach (Material material in materials)
			{
				if (material.HasProperty("_Color"))
				{
					material.color = new Color(material.color.r, material.color.g, material.color.b, val);
				}
				else if (material.HasProperty("_TintColor"))
				{
					Color color = material.GetColor("_TintColor");
					material.SetColor("_TintColor", new Color(color.r, color.g, color.b, val));
				}
			}
		}
		if (!useRecursion || transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in transform)
		{
			alphaRecursive(item, val);
		}
	}

	private static void colorRecursive(Transform transform, Color toColor, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			for (int i = 0; i < materials.Length; i++)
			{
				materials[i].color = toColor;
			}
		}
		if (!useRecursion || transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in transform)
		{
			colorRecursive(item, toColor);
		}
	}

	private static void alphaRecursive(RectTransform rectTransform, float val, int recursiveLevel = 0)
	{
		if (rectTransform.childCount <= 0)
		{
			return;
		}
		foreach (RectTransform item in rectTransform)
		{
			MaskableGraphic component = item.GetComponent<Image>();
			if (component != null)
			{
				Color color = component.color;
				color.a = val;
				component.color = color;
			}
			else
			{
				component = item.GetComponent<RawImage>();
				if (component != null)
				{
					Color color2 = component.color;
					color2.a = val;
					component.color = color2;
				}
			}
			alphaRecursive(item, val, recursiveLevel + 1);
		}
	}

	private static void alphaRecursiveSprite(Transform transform, float val)
	{
		if (transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in transform)
		{
			SpriteRenderer component = item.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				component.color = new Color(component.color.r, component.color.g, component.color.b, val);
			}
			alphaRecursiveSprite(item, val);
		}
	}

	private static void colorRecursiveSprite(Transform transform, Color toColor)
	{
		if (transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in transform)
		{
			SpriteRenderer component = transform.gameObject.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				component.color = toColor;
			}
			colorRecursiveSprite(item, toColor);
		}
	}

	private static void colorRecursive(RectTransform rectTransform, Color toColor)
	{
		if (rectTransform.childCount <= 0)
		{
			return;
		}
		foreach (RectTransform item in rectTransform)
		{
			MaskableGraphic component = item.GetComponent<Image>();
			if (component != null)
			{
				component.color = toColor;
			}
			else
			{
				component = item.GetComponent<RawImage>();
				if (component != null)
				{
					component.color = toColor;
				}
			}
			colorRecursive(item, toColor);
		}
	}

	private static void textAlphaChildrenRecursive(Transform trans, float val, bool useRecursion = true)
	{
		if (!useRecursion || trans.childCount <= 0)
		{
			return;
		}
		foreach (Transform tran in trans)
		{
			Text component = tran.GetComponent<Text>();
			if (component != null)
			{
				Color color = component.color;
				color.a = val;
				component.color = color;
			}
			textAlphaChildrenRecursive(tran, val);
		}
	}

	private static void textAlphaRecursive(Transform trans, float val, bool useRecursion = true)
	{
		Text component = trans.GetComponent<Text>();
		if (component != null)
		{
			Color color = component.color;
			color.a = val;
			component.color = color;
		}
		if (!useRecursion || trans.childCount <= 0)
		{
			return;
		}
		foreach (Transform tran in trans)
		{
			textAlphaRecursive(tran, val);
		}
	}

	private static void textColorRecursive(Transform trans, Color toColor)
	{
		if (trans.childCount <= 0)
		{
			return;
		}
		foreach (Transform tran in trans)
		{
			Text component = tran.GetComponent<Text>();
			if (component != null)
			{
				component.color = toColor;
			}
			textColorRecursive(tran, toColor);
		}
	}

	private static Color tweenColor(LTDescr tween, float val)
	{
		Vector3 vector = tween._optional.point - tween._optional.axis;
		float num = tween.to.y - tween.from.y;
		return new Color(tween._optional.axis.x + vector.x * val, tween._optional.axis.y + vector.y * val, tween._optional.axis.z + vector.z * val, tween.from.y + num * val);
	}

	public LTDescr pause()
	{
		if (direction != 0f)
		{
			directionLast = direction;
			direction = 0f;
		}
		return this;
	}

	public LTDescr resume()
	{
		direction = directionLast;
		return this;
	}

	public LTDescr setAxis(Vector3 axis)
	{
		_optional.axis = axis;
		return this;
	}

	public LTDescr setDelay(float delay)
	{
		this.delay = delay;
		return this;
	}

	public LTDescr setEase(LeanTweenType easeType)
	{
		switch (easeType)
		{
		case LeanTweenType.linear:
			setEaseLinear();
			break;
		case LeanTweenType.easeOutQuad:
			setEaseOutQuad();
			break;
		case LeanTweenType.easeInQuad:
			setEaseInQuad();
			break;
		case LeanTweenType.easeInOutQuad:
			setEaseInOutQuad();
			break;
		case LeanTweenType.easeInCubic:
			setEaseInCubic();
			break;
		case LeanTweenType.easeOutCubic:
			setEaseOutCubic();
			break;
		case LeanTweenType.easeInOutCubic:
			setEaseInOutCubic();
			break;
		case LeanTweenType.easeInQuart:
			setEaseInQuart();
			break;
		case LeanTweenType.easeOutQuart:
			setEaseOutQuart();
			break;
		case LeanTweenType.easeInOutQuart:
			setEaseInOutQuart();
			break;
		case LeanTweenType.easeInQuint:
			setEaseInQuint();
			break;
		case LeanTweenType.easeOutQuint:
			setEaseOutQuint();
			break;
		case LeanTweenType.easeInOutQuint:
			setEaseInOutQuint();
			break;
		case LeanTweenType.easeInSine:
			setEaseInSine();
			break;
		case LeanTweenType.easeOutSine:
			setEaseOutSine();
			break;
		case LeanTweenType.easeInOutSine:
			setEaseInOutSine();
			break;
		case LeanTweenType.easeInExpo:
			setEaseInExpo();
			break;
		case LeanTweenType.easeOutExpo:
			setEaseOutExpo();
			break;
		case LeanTweenType.easeInOutExpo:
			setEaseInOutExpo();
			break;
		case LeanTweenType.easeInCirc:
			setEaseInCirc();
			break;
		case LeanTweenType.easeOutCirc:
			setEaseOutCirc();
			break;
		case LeanTweenType.easeInOutCirc:
			setEaseInOutCirc();
			break;
		case LeanTweenType.easeInBounce:
			setEaseInBounce();
			break;
		case LeanTweenType.easeOutBounce:
			setEaseOutBounce();
			break;
		case LeanTweenType.easeInOutBounce:
			setEaseInOutBounce();
			break;
		case LeanTweenType.easeInBack:
			setEaseInBack();
			break;
		case LeanTweenType.easeOutBack:
			setEaseOutBack();
			break;
		case LeanTweenType.easeInOutBack:
			setEaseInOutBack();
			break;
		case LeanTweenType.easeInElastic:
			setEaseInElastic();
			break;
		case LeanTweenType.easeOutElastic:
			setEaseOutElastic();
			break;
		case LeanTweenType.easeInOutElastic:
			setEaseInOutElastic();
			break;
		case LeanTweenType.punch:
			setEasePunch();
			break;
		case LeanTweenType.easeShake:
			setEaseShake();
			break;
		case LeanTweenType.easeSpring:
			setEaseSpring();
			break;
		default:
			setEaseLinear();
			break;
		}
		return this;
	}

	public LTDescr setEaseLinear()
	{
		easeType = LeanTweenType.linear;
		easeMethod = easeLinear;
		return this;
	}

	public LTDescr setEaseSpring()
	{
		easeType = LeanTweenType.easeSpring;
		easeMethod = easeSpring;
		return this;
	}

	public LTDescr setEaseInQuad()
	{
		easeType = LeanTweenType.easeInQuad;
		easeMethod = easeInQuad;
		return this;
	}

	public LTDescr setEaseOutQuad()
	{
		easeType = LeanTweenType.easeOutQuad;
		easeMethod = easeOutQuad;
		return this;
	}

	public LTDescr setEaseInOutQuad()
	{
		easeType = LeanTweenType.easeInOutQuad;
		easeMethod = easeInOutQuad;
		return this;
	}

	public LTDescr setEaseInCubic()
	{
		easeType = LeanTweenType.easeInCubic;
		easeMethod = easeInCubic;
		return this;
	}

	public LTDescr setEaseOutCubic()
	{
		easeType = LeanTweenType.easeOutCubic;
		easeMethod = easeOutCubic;
		return this;
	}

	public LTDescr setEaseInOutCubic()
	{
		easeType = LeanTweenType.easeInOutCubic;
		easeMethod = easeInOutCubic;
		return this;
	}

	public LTDescr setEaseInQuart()
	{
		easeType = LeanTweenType.easeInQuart;
		easeMethod = easeInQuart;
		return this;
	}

	public LTDescr setEaseOutQuart()
	{
		easeType = LeanTweenType.easeOutQuart;
		easeMethod = easeOutQuart;
		return this;
	}

	public LTDescr setEaseInOutQuart()
	{
		easeType = LeanTweenType.easeInOutQuart;
		easeMethod = easeInOutQuart;
		return this;
	}

	public LTDescr setEaseInQuint()
	{
		easeType = LeanTweenType.easeInQuint;
		easeMethod = easeInQuint;
		return this;
	}

	public LTDescr setEaseOutQuint()
	{
		easeType = LeanTweenType.easeOutQuint;
		easeMethod = easeOutQuint;
		return this;
	}

	public LTDescr setEaseInOutQuint()
	{
		easeType = LeanTweenType.easeInOutQuint;
		easeMethod = easeInOutQuint;
		return this;
	}

	public LTDescr setEaseInSine()
	{
		easeType = LeanTweenType.easeInSine;
		easeMethod = easeInSine;
		return this;
	}

	public LTDescr setEaseOutSine()
	{
		easeType = LeanTweenType.easeOutSine;
		easeMethod = easeOutSine;
		return this;
	}

	public LTDescr setEaseInOutSine()
	{
		easeType = LeanTweenType.easeInOutSine;
		easeMethod = easeInOutSine;
		return this;
	}

	public LTDescr setEaseInExpo()
	{
		easeType = LeanTweenType.easeInExpo;
		easeMethod = easeInExpo;
		return this;
	}

	public LTDescr setEaseOutExpo()
	{
		easeType = LeanTweenType.easeOutExpo;
		easeMethod = easeOutExpo;
		return this;
	}

	public LTDescr setEaseInOutExpo()
	{
		easeType = LeanTweenType.easeInOutExpo;
		easeMethod = easeInOutExpo;
		return this;
	}

	public LTDescr setEaseInCirc()
	{
		easeType = LeanTweenType.easeInCirc;
		easeMethod = easeInCirc;
		return this;
	}

	public LTDescr setEaseOutCirc()
	{
		easeType = LeanTweenType.easeOutCirc;
		easeMethod = easeOutCirc;
		return this;
	}

	public LTDescr setEaseInOutCirc()
	{
		easeType = LeanTweenType.easeInOutCirc;
		easeMethod = easeInOutCirc;
		return this;
	}

	public LTDescr setEaseInBounce()
	{
		easeType = LeanTweenType.easeInBounce;
		easeMethod = easeInBounce;
		return this;
	}

	public LTDescr setEaseOutBounce()
	{
		easeType = LeanTweenType.easeOutBounce;
		easeMethod = easeOutBounce;
		return this;
	}

	public LTDescr setEaseInOutBounce()
	{
		easeType = LeanTweenType.easeInOutBounce;
		easeMethod = easeInOutBounce;
		return this;
	}

	public LTDescr setEaseInBack()
	{
		easeType = LeanTweenType.easeInBack;
		easeMethod = easeInBack;
		return this;
	}

	public LTDescr setEaseOutBack()
	{
		easeType = LeanTweenType.easeOutBack;
		easeMethod = easeOutBack;
		return this;
	}

	public LTDescr setEaseInOutBack()
	{
		easeType = LeanTweenType.easeInOutBack;
		easeMethod = easeInOutBack;
		return this;
	}

	public LTDescr setEaseInElastic()
	{
		easeType = LeanTweenType.easeInElastic;
		easeMethod = easeInElastic;
		return this;
	}

	public LTDescr setEaseOutElastic()
	{
		easeType = LeanTweenType.easeOutElastic;
		easeMethod = easeOutElastic;
		return this;
	}

	public LTDescr setEaseInOutElastic()
	{
		easeType = LeanTweenType.easeInOutElastic;
		easeMethod = easeInOutElastic;
		return this;
	}

	public LTDescr setEasePunch()
	{
		_optional.animationCurve = LeanTween.punch;
		toInternal.x = from.x + to.x;
		easeMethod = tweenOnCurve;
		return this;
	}

	public LTDescr setEaseShake()
	{
		_optional.animationCurve = LeanTween.shake;
		toInternal.x = from.x + to.x;
		easeMethod = tweenOnCurve;
		return this;
	}

	private Vector3 tweenOnCurve()
	{
		return new Vector3(from.x + diff.x * _optional.animationCurve.Evaluate(ratioPassed), from.y + diff.y * _optional.animationCurve.Evaluate(ratioPassed), from.z + diff.z * _optional.animationCurve.Evaluate(ratioPassed));
	}

	private Vector3 easeInOutQuad()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val *= val;
			return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
		}
		val = (1f - val) * (val - 3f) + 1f;
		return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
	}

	private Vector3 easeInQuad()
	{
		val = ratioPassed * ratioPassed;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeOutQuad()
	{
		val = ratioPassed;
		val = (0f - val) * (val - 2f);
		return diff * val + from;
	}

	private Vector3 easeLinear()
	{
		val = ratioPassed;
		return new Vector3(from.x + diff.x * val, from.y + diff.y * val, from.z + diff.z * val);
	}

	private Vector3 easeSpring()
	{
		val = Mathf.Clamp01(ratioPassed);
		val = (Mathf.Sin(val * MathF.PI * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
		return from + diff * val;
	}

	private Vector3 easeInCubic()
	{
		val = ratioPassed * ratioPassed * ratioPassed;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeOutCubic()
	{
		val = ratioPassed - 1f;
		val = val * val * val + 1f;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutCubic()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val;
			return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
		}
		val -= 2f;
		val = val * val * val + 2f;
		return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
	}

	private Vector3 easeInQuart()
	{
		val = ratioPassed * ratioPassed * ratioPassed * ratioPassed;
		return diff * val + from;
	}

	private Vector3 easeOutQuart()
	{
		val = ratioPassed - 1f;
		val = 0f - (val * val * val * val - 1f);
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutQuart()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val * val;
			return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
		}
		val -= 2f;
		return -diffDiv2 * (val * val * val * val - 2f) + from;
	}

	private Vector3 easeInQuint()
	{
		val = ratioPassed;
		val = val * val * val * val * val;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeOutQuint()
	{
		val = ratioPassed - 1f;
		val = val * val * val * val * val + 1f;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutQuint()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = val * val * val * val * val;
			return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
		}
		val -= 2f;
		val = val * val * val * val * val + 2f;
		return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
	}

	private Vector3 easeInSine()
	{
		val = 0f - Mathf.Cos(ratioPassed * LeanTween.PI_DIV2);
		return new Vector3(diff.x * val + diff.x + from.x, diff.y * val + diff.y + from.y, diff.z * val + diff.z + from.z);
	}

	private Vector3 easeOutSine()
	{
		val = Mathf.Sin(ratioPassed * LeanTween.PI_DIV2);
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutSine()
	{
		val = 0f - (Mathf.Cos(MathF.PI * ratioPassed) - 1f);
		return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
	}

	private Vector3 easeInExpo()
	{
		val = Mathf.Pow(2f, 10f * (ratioPassed - 1f));
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeOutExpo()
	{
		val = 0f - Mathf.Pow(2f, -10f * ratioPassed) + 1f;
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutExpo()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			return diffDiv2 * Mathf.Pow(2f, 10f * (val - 1f)) + from;
		}
		val -= 1f;
		return diffDiv2 * (0f - Mathf.Pow(2f, -10f * val) + 2f) + from;
	}

	private Vector3 easeInCirc()
	{
		val = 0f - (Mathf.Sqrt(1f - ratioPassed * ratioPassed) - 1f);
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeOutCirc()
	{
		val = ratioPassed - 1f;
		val = Mathf.Sqrt(1f - val * val);
		return new Vector3(diff.x * val + from.x, diff.y * val + from.y, diff.z * val + from.z);
	}

	private Vector3 easeInOutCirc()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			val = 0f - (Mathf.Sqrt(1f - val * val) - 1f);
			return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
		}
		val -= 2f;
		val = Mathf.Sqrt(1f - val * val) + 1f;
		return new Vector3(diffDiv2.x * val + from.x, diffDiv2.y * val + from.y, diffDiv2.z * val + from.z);
	}

	private Vector3 easeInBounce()
	{
		val = ratioPassed;
		val = 1f - val;
		return new Vector3(diff.x - LeanTween.easeOutBounce(0f, diff.x, val) + from.x, diff.y - LeanTween.easeOutBounce(0f, diff.y, val) + from.y, diff.z - LeanTween.easeOutBounce(0f, diff.z, val) + from.z);
	}

	private Vector3 easeOutBounce()
	{
		val = ratioPassed;
		float num2;
		float num;
		if (val < (num = 1f - 1.75f * overshoot / 2.75f))
		{
			val = 1f / num / num * val * val;
		}
		else if (val < (num2 = 1f - 0.75f * overshoot / 2.75f))
		{
			val -= (num + num2) / 2f;
			val = 7.5625f * val * val + 1f - 0.25f * overshoot * overshoot;
		}
		else if (val < (num = 1f - 0.25f * overshoot / 2.75f))
		{
			val -= (num + num2) / 2f;
			val = 7.5625f * val * val + 1f - 0.0625f * overshoot * overshoot;
		}
		else
		{
			val -= (num + 1f) / 2f;
			val = 7.5625f * val * val + 1f - 1f / 64f * overshoot * overshoot;
		}
		return diff * val + from;
	}

	private Vector3 easeInOutBounce()
	{
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			return new Vector3(LeanTween.easeInBounce(0f, diff.x, val) * 0.5f + from.x, LeanTween.easeInBounce(0f, diff.y, val) * 0.5f + from.y, LeanTween.easeInBounce(0f, diff.z, val) * 0.5f + from.z);
		}
		val -= 1f;
		return new Vector3(LeanTween.easeOutBounce(0f, diff.x, val) * 0.5f + diffDiv2.x + from.x, LeanTween.easeOutBounce(0f, diff.y, val) * 0.5f + diffDiv2.y + from.y, LeanTween.easeOutBounce(0f, diff.z, val) * 0.5f + diffDiv2.z + from.z);
	}

	private Vector3 easeInBack()
	{
		val = ratioPassed;
		val /= 1f;
		float num = 1.70158f * overshoot;
		return diff * val * val * ((num + 1f) * val - num) + from;
	}

	private Vector3 easeOutBack()
	{
		float num = 1.70158f * overshoot;
		val = ratioPassed / 1f - 1f;
		val = val * val * ((num + 1f) * val + num) + 1f;
		return diff * val + from;
	}

	private Vector3 easeInOutBack()
	{
		float num = 1.70158f * overshoot;
		val = ratioPassed * 2f;
		if (val < 1f)
		{
			num *= 1.525f * overshoot;
			return diffDiv2 * (val * val * ((num + 1f) * val - num)) + from;
		}
		val -= 2f;
		num *= 1.525f * overshoot;
		val = val * val * ((num + 1f) * val + num) + 2f;
		return diffDiv2 * val + from;
	}

	private Vector3 easeInElastic()
	{
		return new Vector3(LeanTween.easeInElastic(from.x, to.x, ratioPassed, overshoot, period), LeanTween.easeInElastic(from.y, to.y, ratioPassed, overshoot, period), LeanTween.easeInElastic(from.z, to.z, ratioPassed, overshoot, period));
	}

	private Vector3 easeOutElastic()
	{
		return new Vector3(LeanTween.easeOutElastic(from.x, to.x, ratioPassed, overshoot, period), LeanTween.easeOutElastic(from.y, to.y, ratioPassed, overshoot, period), LeanTween.easeOutElastic(from.z, to.z, ratioPassed, overshoot, period));
	}

	private Vector3 easeInOutElastic()
	{
		return new Vector3(LeanTween.easeInOutElastic(from.x, to.x, ratioPassed, overshoot, period), LeanTween.easeInOutElastic(from.y, to.y, ratioPassed, overshoot, period), LeanTween.easeInOutElastic(from.z, to.z, ratioPassed, overshoot, period));
	}

	public LTDescr setOvershoot(float overshoot)
	{
		this.overshoot = overshoot;
		return this;
	}

	public LTDescr setPeriod(float period)
	{
		this.period = period;
		return this;
	}

	public LTDescr setScale(float scale)
	{
		this.scale = scale;
		return this;
	}

	public LTDescr setEase(AnimationCurve easeCurve)
	{
		_optional.animationCurve = easeCurve;
		easeMethod = tweenOnCurve;
		easeType = LeanTweenType.animationCurve;
		return this;
	}

	public LTDescr setTo(Vector3 to)
	{
		if (hasInitiliazed)
		{
			this.to = to;
			diff = to - from;
		}
		else
		{
			this.to = to;
		}
		return this;
	}

	public LTDescr setTo(Transform to)
	{
		_optional.toTrans = to;
		return this;
	}

	public LTDescr setFrom(Vector3 from)
	{
		if ((bool)trans)
		{
			init();
		}
		this.from = from;
		diff = to - this.from;
		diffDiv2 = diff * 0.5f;
		return this;
	}

	public LTDescr setFrom(float from)
	{
		return setFrom(new Vector3(from, 0f, 0f));
	}

	public LTDescr setDiff(Vector3 diff)
	{
		this.diff = diff;
		return this;
	}

	public LTDescr setHasInitialized(bool has)
	{
		hasInitiliazed = has;
		return this;
	}

	public LTDescr setId(uint id, uint global_counter)
	{
		_id = id;
		counter = global_counter;
		return this;
	}

	public LTDescr setPassed(float passed)
	{
		this.passed = passed;
		return this;
	}

	public LTDescr setTime(float time)
	{
		float num = passed / this.time;
		passed = time * num;
		this.time = time;
		return this;
	}

	public LTDescr setSpeed(float speed)
	{
		this.speed = speed;
		if (hasInitiliazed)
		{
			initSpeed();
		}
		return this;
	}

	public LTDescr setRepeat(int repeat)
	{
		loopCount = repeat;
		if ((repeat > 1 && loopType == LeanTweenType.once) || (repeat < 0 && loopType == LeanTweenType.once))
		{
			loopType = LeanTweenType.clamp;
		}
		if (type == TweenAction.CALLBACK || type == TweenAction.CALLBACK_COLOR)
		{
			setOnCompleteOnRepeat(isOn: true);
		}
		return this;
	}

	public LTDescr setLoopType(LeanTweenType loopType)
	{
		this.loopType = loopType;
		return this;
	}

	public LTDescr setUseEstimatedTime(bool useEstimatedTime)
	{
		this.useEstimatedTime = useEstimatedTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setIgnoreTimeScale(bool useUnScaledTime)
	{
		useEstimatedTime = useUnScaledTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setUseFrames(bool useFrames)
	{
		this.useFrames = useFrames;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setUseManualTime(bool useManualTime)
	{
		this.useManualTime = useManualTime;
		usesNormalDt = false;
		return this;
	}

	public LTDescr setLoopCount(int loopCount)
	{
		loopType = LeanTweenType.clamp;
		this.loopCount = loopCount;
		return this;
	}

	public LTDescr setLoopOnce()
	{
		loopType = LeanTweenType.once;
		return this;
	}

	public LTDescr setLoopClamp()
	{
		loopType = LeanTweenType.clamp;
		if (loopCount == 0)
		{
			loopCount = -1;
		}
		return this;
	}

	public LTDescr setLoopClamp(int loops)
	{
		loopCount = loops;
		return this;
	}

	public LTDescr setLoopPingPong()
	{
		loopType = LeanTweenType.pingPong;
		if (loopCount == 0)
		{
			loopCount = -1;
		}
		return this;
	}

	public LTDescr setLoopPingPong(int loops)
	{
		loopType = LeanTweenType.pingPong;
		loopCount = ((loops == -1) ? loops : (loops * 2));
		return this;
	}

	public LTDescr setOnComplete(Action onComplete)
	{
		_optional.onComplete = onComplete;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnComplete(Action<object> onComplete)
	{
		_optional.onCompleteObject = onComplete;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnComplete(Action<object> onComplete, object onCompleteParam)
	{
		_optional.onCompleteObject = onComplete;
		hasExtraOnCompletes = true;
		if (onCompleteParam != null)
		{
			_optional.onCompleteParam = onCompleteParam;
		}
		return this;
	}

	public LTDescr setOnCompleteParam(object onCompleteParam)
	{
		_optional.onCompleteParam = onCompleteParam;
		hasExtraOnCompletes = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<float> onUpdate)
	{
		_optional.onUpdateFloat = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateRatio(Action<float, float> onUpdate)
	{
		_optional.onUpdateFloatRatio = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateObject(Action<float, object> onUpdate)
	{
		_optional.onUpdateFloatObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateVector2(Action<Vector2> onUpdate)
	{
		_optional.onUpdateVector2 = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateVector3(Action<Vector3> onUpdate)
	{
		_optional.onUpdateVector3 = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateColor(Action<Color> onUpdate)
	{
		_optional.onUpdateColor = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdateColor(Action<Color, object> onUpdate)
	{
		_optional.onUpdateColorObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<Color> onUpdate)
	{
		_optional.onUpdateColor = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<Color, object> onUpdate)
	{
		_optional.onUpdateColorObject = onUpdate;
		hasUpdateCallback = true;
		return this;
	}

	public LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateFloatObject = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3, object> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector3Object = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector2> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector2 = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdate(Action<Vector3> onUpdate, object onUpdateParam = null)
	{
		_optional.onUpdateVector3 = onUpdate;
		hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			_optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	public LTDescr setOnUpdateParam(object onUpdateParam)
	{
		_optional.onUpdateParam = onUpdateParam;
		return this;
	}

	public LTDescr setOrientToPath(bool doesOrient)
	{
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			if (_optional.path == null)
			{
				_optional.path = new LTBezierPath();
			}
			_optional.path.orientToPath = doesOrient;
		}
		else
		{
			_optional.spline.orientToPath = doesOrient;
		}
		return this;
	}

	public LTDescr setOrientToPath2d(bool doesOrient2d)
	{
		setOrientToPath(doesOrient2d);
		if (type == TweenAction.MOVE_CURVED || type == TweenAction.MOVE_CURVED_LOCAL)
		{
			_optional.path.orientToPath2d = doesOrient2d;
		}
		else
		{
			_optional.spline.orientToPath2d = doesOrient2d;
		}
		return this;
	}

	public LTDescr setRect(LTRect rect)
	{
		_optional.ltRect = rect;
		return this;
	}

	public LTDescr setRect(Rect rect)
	{
		_optional.ltRect = new LTRect(rect);
		return this;
	}

	public LTDescr setPath(LTBezierPath path)
	{
		_optional.path = path;
		return this;
	}

	public LTDescr setPoint(Vector3 point)
	{
		_optional.point = point;
		return this;
	}

	public LTDescr setDestroyOnComplete(bool doesDestroy)
	{
		destroyOnComplete = doesDestroy;
		return this;
	}

	public LTDescr setAudio(object audio)
	{
		_optional.onCompleteParam = audio;
		return this;
	}

	public LTDescr setOnCompleteOnRepeat(bool isOn)
	{
		onCompleteOnRepeat = isOn;
		return this;
	}

	public LTDescr setOnCompleteOnStart(bool isOn)
	{
		onCompleteOnStart = isOn;
		return this;
	}

	public LTDescr setRect(RectTransform rect)
	{
		rectTransform = rect;
		return this;
	}

	public LTDescr setSprites(Sprite[] sprites)
	{
		this.sprites = sprites;
		return this;
	}

	public LTDescr setFrameRate(float frameRate)
	{
		time = (float)sprites.Length / frameRate;
		return this;
	}

	public LTDescr setOnStart(Action onStart)
	{
		_optional.onStart = onStart;
		return this;
	}

	public LTDescr setDirection(float direction)
	{
		if (this.direction != -1f && this.direction != 1f)
		{
			UnityEngine.Debug.LogWarning("You have passed an incorrect direction of '" + direction + "', direction must be -1f or 1f");
			return this;
		}
		if (this.direction != direction)
		{
			if (hasInitiliazed)
			{
				this.direction = direction;
			}
			else if (_optional.path != null)
			{
				_optional.path = new LTBezierPath(LTUtility.reverse(_optional.path.pts));
			}
			else if (_optional.spline != null)
			{
				_optional.spline = new LTSpline(LTUtility.reverse(_optional.spline.pts));
			}
		}
		return this;
	}

	public LTDescr setRecursive(bool useRecursion)
	{
		this.useRecursion = useRecursion;
		return this;
	}
}
public class LTDescrOptional
{
	public AnimationCurve animationCurve;

	public int initFrameCount;

	public Transform toTrans { get; set; }

	public Vector3 point { get; set; }

	public Vector3 axis { get; set; }

	public float lastVal { get; set; }

	public Quaternion origRotation { get; set; }

	public LTBezierPath path { get; set; }

	public LTSpline spline { get; set; }

	public LTRect ltRect { get; set; }

	public Action<float> onUpdateFloat { get; set; }

	public Action<float, float> onUpdateFloatRatio { get; set; }

	public Action<float, object> onUpdateFloatObject { get; set; }

	public Action<Vector2> onUpdateVector2 { get; set; }

	public Action<Vector3> onUpdateVector3 { get; set; }

	public Action<Vector3, object> onUpdateVector3Object { get; set; }

	public Action<Color> onUpdateColor { get; set; }

	public Action<Color, object> onUpdateColorObject { get; set; }

	public Action onComplete { get; set; }

	public Action<object> onCompleteObject { get; set; }

	public object onCompleteParam { get; set; }

	public object onUpdateParam { get; set; }

	public Action onStart { get; set; }

	public void reset()
	{
		animationCurve = null;
		onUpdateFloat = null;
		onUpdateFloatRatio = null;
		onUpdateVector2 = null;
		onUpdateVector3 = null;
		onUpdateFloatObject = null;
		onUpdateVector3Object = null;
		onUpdateColor = null;
		onComplete = null;
		onCompleteObject = null;
		onCompleteParam = null;
		onStart = null;
		point = Vector3.zero;
		initFrameCount = 0;
	}

	public void callOnUpdate(float val, float ratioPassed)
	{
		if (onUpdateFloat != null)
		{
			onUpdateFloat(val);
		}
		if (onUpdateFloatRatio != null)
		{
			onUpdateFloatRatio(val, ratioPassed);
		}
		else if (onUpdateFloatObject != null)
		{
			if (onUpdateParam != null)
			{
				onUpdateFloatObject(val, onUpdateParam);
			}
		}
		else if (onUpdateVector3Object != null)
		{
			if (onUpdateParam != null)
			{
				onUpdateVector3Object(LTDescr.newVect, onUpdateParam);
			}
		}
		else if (onUpdateVector3 != null)
		{
			onUpdateVector3(LTDescr.newVect);
		}
		else if (onUpdateVector2 != null)
		{
			onUpdateVector2(new Vector2(LTDescr.newVect.x, LTDescr.newVect.y));
		}
	}
}
public class LTSeq
{
	public LTSeq previous;

	public LTSeq current;

	public LTDescr tween;

	public float totalDelay;

	public float timeScale;

	private int debugIter;

	public uint counter;

	public bool toggle;

	private uint _id;

	public int id => (int)(_id | (counter << 16));

	public void reset()
	{
		previous = null;
		tween = null;
		totalDelay = 0f;
	}

	public void init(uint id, uint global_counter)
	{
		reset();
		_id = id;
		counter = global_counter;
		current = this;
	}

	private LTSeq addOn()
	{
		current.toggle = true;
		LTSeq lTSeq = current;
		current = LeanTween.sequence(initSequence: false);
		current.previous = lTSeq;
		lTSeq.toggle = false;
		current.totalDelay = lTSeq.totalDelay;
		current.debugIter = lTSeq.debugIter + 1;
		return current;
	}

	private float addPreviousDelays()
	{
		LTSeq lTSeq = current.previous;
		if (lTSeq != null && lTSeq.tween != null)
		{
			return current.totalDelay + lTSeq.tween.time;
		}
		return current.totalDelay;
	}

	public LTSeq append(float delay)
	{
		current.totalDelay += delay;
		return current;
	}

	public LTSeq append(Action callback)
	{
		LTDescr lTDescr = LeanTween.delayedCall(0f, callback);
		append(lTDescr);
		return addOn();
	}

	public LTSeq append(Action<object> callback, object obj)
	{
		append(LeanTween.delayedCall(0f, callback).setOnCompleteParam(obj));
		return addOn();
	}

	public LTSeq append(GameObject gameObject, Action callback)
	{
		append(LeanTween.delayedCall(gameObject, 0f, callback));
		return addOn();
	}

	public LTSeq append(GameObject gameObject, Action<object> callback, object obj)
	{
		append(LeanTween.delayedCall(gameObject, 0f, callback).setOnCompleteParam(obj));
		return addOn();
	}

	public LTSeq append(LTDescr tween)
	{
		current.tween = tween;
		current.totalDelay = addPreviousDelays();
		tween.setDelay(current.totalDelay);
		return addOn();
	}

	public LTSeq insert(LTDescr tween)
	{
		current.tween = tween;
		tween.setDelay(addPreviousDelays());
		return addOn();
	}

	public LTSeq setScale(float timeScale)
	{
		setScaleRecursive(current, timeScale, 500);
		return addOn();
	}

	private void setScaleRecursive(LTSeq seq, float timeScale, int count)
	{
		if (count <= 0)
		{
			return;
		}
		this.timeScale = timeScale;
		seq.totalDelay *= timeScale;
		if (seq.tween != null)
		{
			if (seq.tween.time != 0f)
			{
				seq.tween.setTime(seq.tween.time * timeScale);
			}
			seq.tween.setDelay(seq.tween.delay * timeScale);
		}
		if (seq.previous != null)
		{
			setScaleRecursive(seq.previous, timeScale, count - 1);
		}
	}

	public LTSeq reverse()
	{
		return addOn();
	}
}
public static class TweenMode
{
	public static AnimationCurve Punch = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.112586f, 0.9976035f), new Keyframe(0.3120486f, 0.01720615f), new Keyframe(0.4316337f, 0.17030682f), new Keyframe(0.5524869f, 0.03141804f), new Keyframe(0.6549395f, 0.002909959f), new Keyframe(0.770987f, 0.009817753f), new Keyframe(0.8838775f, 0.001939224f), new Keyframe(1f, 0f));
}
public abstract class ListComponent<T> : ListComponent where T : MonoBehaviour
{
	public static ListHashSet<T> InstanceList = new ListHashSet<T>();

	public override void Setup()
	{
		if (!InstanceList.Contains(this as T))
		{
			InstanceList.Add(this as T);
		}
	}

	public override void Clear()
	{
		InstanceList.Remove(this as T);
	}

	public static void RunOnAll(Action<T> toRun)
	{
		foreach (T instance in InstanceList)
		{
			toRun?.Invoke(instance);
		}
	}
}
public abstract class ListComponent : FacepunchBehaviour
{
	public abstract void Setup();

	public abstract void Clear();

	protected virtual void OnEnable()
	{
		Setup();
	}

	protected virtual void OnDisable()
	{
		Clear();
	}
}
public static class MurmurHash
{
	private const uint seed = 1337u;

	public static int Signed(Stream stream)
	{
		return (int)Unsigned(stream);
	}

	public static uint Unsigned(Stream stream)
	{
		uint num = 1337u;
		uint num2 = 0u;
		uint num3 = 0u;
		using (BinaryReader binaryReader = new BinaryReader(stream))
		{
			byte[] array = binaryReader.ReadBytes(4);
			while (array.Length != 0)
			{
				num3 += (uint)array.Length;
				switch (array.Length)
				{
				case 4:
					num2 = (uint)(array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24));
					num2 *= 3432918353u;
					num2 = rot(num2, 15);
					num2 *= 461845907;
					num ^= num2;
					num = rot(num, 13);
					num = num * 5 + 3864292196u;
					break;
				case 3:
					num2 = (uint)(array[0] | (array[1] << 8) | (array[2] << 16));
					num2 *= 3432918353u;
					num2 = rot(num2, 15);
					num2 *= 461845907;
					num ^= num2;
					break;
				case 2:
					num2 = (uint)(array[0] | (array[1] << 8));
					num2 *= 3432918353u;
					num2 = rot(num2, 15);
					num2 *= 461845907;
					num ^= num2;
					break;
				case 1:
					num2 = array[0];
					num2 *= 3432918353u;
					num2 = rot(num2, 15);
					num2 *= 461845907;
					num ^= num2;
					break;
				}
				array = binaryReader.ReadBytes(4);
			}
		}
		num ^= num3;
		return mix(num);
	}

	private static uint rot(uint x, byte r)
	{
		return (x << (int)r) | (x >> 32 - r);
	}

	private static uint mix(uint h)
	{
		h ^= h >> 16;
		h *= 2246822507u;
		h ^= h >> 13;
		h *= 3266489909u;
		h ^= h >> 16;
		return h;
	}
}
public static class MurmurHashEx
{
	public static int MurmurHashSigned(this string str)
	{
		return MurmurHash.Signed(StringToStream(str));
	}

	public static uint MurmurHashUnsigned(this string str)
	{
		return MurmurHash.Unsigned(StringToStream(str));
	}

	private static MemoryStream StringToStream(string str)
	{
		return new MemoryStream(Encoding.UTF8.GetBytes(str ?? string.Empty));
	}
}
public class PriorityListComponent<T> : ListComponent<T> where T : PriorityListComponent<T>
{
	public static T Instance { get; private set; }

	public virtual int Priority => 100;

	public override void Setup()
	{
		base.Setup();
		UpdateInstance();
	}

	public override void Clear()
	{
		base.Clear();
		UpdateInstance();
	}

	private static void UpdateInstance()
	{
		if (ListComponent<T>.InstanceList.Count == 0)
		{
			Instance = null;
			return;
		}
		T instance = null;
		int num = int.MinValue;
		for (int num2 = ListComponent<T>.InstanceList.Count; num2 >= 0; num2--)
		{
			T val = ListComponent<T>.InstanceList[num2];
			if (!(val == null))
			{
				int priority = val.Priority;
				if (priority > num)
				{
					instance = val;
					num = priority;
				}
			}
		}
		Instance = instance;
	}
}
public static class ShaderEx
{
	public static bool SetTextureEx(this ComputeShader shader, int kernelIndex, string name, Texture texture)
	{
		if (texture == null)
		{
			return false;
		}
		shader.SetTexture(kernelIndex, name, texture);
		return true;
	}

	public static bool SetComputeTextureParamEx(this CommandBuffer cb, ComputeShader shader, int kernelIndex, string name, Texture texture)
	{
		if (texture == null)
		{
			return false;
		}
		cb.SetComputeTextureParam(shader, kernelIndex, name, texture);
		return true;
	}
}
public abstract class SingletonComponent<T> : SingletonComponent where T : MonoBehaviour
{
	public static T Instance;

	public override void SingletonSetup()
	{
		if (Instance != this)
		{
			Instance = this as T;
		}
	}

	public override void SingletonClear()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}
}
public abstract class SingletonComponent : FacepunchBehaviour
{
	public abstract void SingletonSetup();

	public abstract void SingletonClear();

	protected virtual void Awake()
	{
		SingletonSetup();
	}

	protected virtual void OnDestroy()
	{
		SingletonClear();
	}

	public static void InitializeSingletons(GameObject go)
	{
		SingletonComponent[] componentsInChildren = go.GetComponentsInChildren<SingletonComponent>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SingletonSetup();
		}
	}
}
public interface IStableIndex
{
	int StableIndex { get; set; }
}
public class StableEntityCache
{
	public const int NotInCache = -1;
}
public class StableObjectCache<TObject> : StableEntityCache where TObject : MonoBehaviour, IStableIndex
{
	public struct ValidEnumerator : IEnumerator<TObject>, IEnumerator, IDisposable
	{
		private StableObjectCache<TObject> cache;

		private int index;

		private int found;

		public TObject Current => cache._objects[index];

		TObject IEnumerator<TObject>.Current => Current;

		object IEnumerator.Current => Current;

		public ValidEnumerator(StableObjectCache<TObject> cache)
		{
			this.cache = cache;
			index = -1;
			found = 0;
		}

		public void Dispose()
		{
			cache = null;
		}

		void IDisposable.Dispose()
		{
			Dispose();
		}

		public bool MoveNext()
		{
			if (found >= cache._count || index >= cache._objects.Length)
			{
				return false;
			}
			while (++index < cache._objects.Length && cache._objects[index] == null)
			{
			}
			found++;
			return index < cache._objects.Length;
		}

		bool IEnumerator.MoveNext()
		{
			return MoveNext();
		}

		public void Reset()
		{
			index = -1;
			found = 0;
		}

		void IEnumerator.Reset()
		{
			Reset();
		}
	}

	public struct ValidView : IEnumerable<TObject>, IEnumerable
	{
		private StableObjectCache<TObject> cache;

		public ValidView(StableObjectCache<TObject> cache)
		{
			this.cache = cache;
		}

		public ValidEnumerator GetEnumerator()
		{
			return new ValidEnumerator(cache);
		}

		IEnumerator<TObject> IEnumerable<TObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	private TObject[] _objects;

	private int _count;

	private int[] nextIndices;

	private int firstFreeIndex;

	private int lastFreeIndex;

	private const int Occupied = -1;

	private BufferList<int> changes = new BufferList<int>();

	protected TObject[] Objects => _objects;

	public int Count => _count;

	public StableObjectCache(int initialCapacity)
	{
		_objects = new TObject[initialCapacity];
		nextIndices = new int[initialCapacity];
		SetupFreeList(0, initialCapacity);
	}

	public void UpdateTransformAccessArray(TransformAccessArray accessArray)
	{
		using (TimeWarning.New("TransformAccessRefresh"))
		{
			if (accessArray.length < _objects.Length)
			{
				accessArray.capacity = _objects.Length;
				for (int i = accessArray.length; i < _objects.Length; i++)
				{
					accessArray.Add(null);
				}
			}
			BufferList<int> obj = Pool.Get<BufferList<int>>();
			GetDirtyIndices(obj);
			for (int j = 0; j < obj.Count; j++)
			{
				int num = obj[j];
				TObject val = _objects[num];
				if ((bool)val)
				{
					accessArray[num] = val.transform;
				}
				else
				{
					accessArray[num] = null;
				}
			}
			ResetDirtyTracking();
			Pool.FreeUnmanaged(ref obj);
		}
	}

	public void Add(TObject obj)
	{
		UnityEngine.Debug.Assert(obj != null, "Entity is dead!");
		UnityEngine.Debug.Assert(obj.StableIndex == -1, "Entity is already in the cache or property not correctly initialized to StableEntityCache.NotInCache");
		bool num = firstFreeIndex == lastFreeIndex;
		int num2 = firstFreeIndex;
		_objects[num2] = obj;
		obj.StableIndex = num2;
		firstFreeIndex = nextIndices[num2];
		nextIndices[num2] = -1;
		if (num)
		{
			Grow();
		}
		_count++;
		changes.Add(num2);
	}

	public void Remove(TObject obj)
	{
		UnityEngine.Debug.Assert(obj.StableIndex != -1, "Entity is not in the cache!");
		int stableIndex = obj.StableIndex;
		obj.StableIndex = -1;
		_objects[stableIndex] = null;
		if (stableIndex < firstFreeIndex)
		{
			nextIndices[stableIndex] = firstFreeIndex;
			firstFreeIndex = stableIndex;
		}
		else
		{
			nextIndices[lastFreeIndex] = stableIndex;
			lastFreeIndex = stableIndex;
		}
		_count--;
		changes.Add(stableIndex);
	}

	public void Clear()
	{
		if (_count > 0)
		{
			for (int i = 0; i < _objects.Length; i++)
			{
				TObject val = _objects[i];
				if ((object)val != null)
				{
					val.StableIndex = -1;
					_objects[i] = null;
					changes.Add(i);
				}
			}
			_count = 0;
		}
		SetupFreeList(0, _objects.Length);
	}

	public void ResetDirtyTracking()
	{
		changes.Clear();
	}

	public void GetDirtyIndices(BufferList<int> indices)
	{
		indices.Clear();
		HashSet<int> obj = Pool.Get<HashSet<int>>();
		foreach (int change in changes)
		{
			if (obj.Add(change))
			{
				indices.Add(change);
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	private void SetupFreeList(int from, int to)
	{
		for (int i = from; i < to; i++)
		{
			nextIndices[i] = i + 1;
		}
		firstFreeIndex = from;
		lastFreeIndex = to - 1;
	}

	private void Grow()
	{
		int num = _objects.Length;
		int num2 = num * 2;
		Array.Resize(ref _objects, num2);
		Array.Resize(ref nextIndices, num2);
		SetupFreeList(num, num2);
	}
}
public sealed class TimeWarning : IDisposable
{
	[NonSerialized]
	public object Placeholder;

	public static TimeWarning New(string name, int maxmilliseconds = 0)
	{
		return null;
	}

	public void Dispose()
	{
	}

	[Conditional("ENABLE_PROFILER")]
	public static void BeginSample(string name)
	{
	}

	[Conditional("ENABLE_PROFILER")]
	public static void EndSample()
	{
	}
}
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[1830]
			{
				0, 0, 0, 2, 0, 0, 0, 49, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 85, 110, 105, 116, 121, 69,
				110, 103, 105, 110, 101, 92, 67, 97, 109, 101,
				114, 97, 69, 120, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 48, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 92, 67, 111, 108, 111, 114, 69, 120,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				48, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 92, 68,
				101, 98, 117, 103, 69, 120, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 59, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 85, 110, 105, 116, 121, 69, 110,
				103, 105, 110, 101, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 66, 101, 104, 97, 118, 105,
				111, 117, 114, 46, 99, 115, 0, 0, 0, 6,
				0, 0, 0, 54, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 110, 105, 116, 121, 69, 110, 103, 105, 110,
				101, 92, 73, 110, 118, 111, 107, 101, 72, 97,
				110, 100, 108, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 55, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 92, 73, 110, 118, 111, 107, 101,
				80, 114, 111, 102, 105, 108, 101, 114, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 59, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 85, 110, 105, 116, 121,
				69, 110, 103, 105, 110, 101, 92, 74, 115, 111,
				110, 77, 111, 100, 101, 108, 65, 116, 116, 114,
				105, 98, 117, 116, 101, 46, 99, 115, 0, 0,
				0, 2, 0, 0, 0, 59, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 92, 76, 101, 97, 110, 84, 119,
				101, 101, 110, 92, 76, 101, 97, 110, 84, 101,
				115, 116, 46, 99, 115, 0, 0, 0, 9, 0,
				0, 0, 60, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 85,
				110, 105, 116, 121, 69, 110, 103, 105, 110, 101,
				92, 76, 101, 97, 110, 84, 119, 101, 101, 110,
				92, 76, 101, 97, 110, 84, 119, 101, 101, 110,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				67, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 92, 76,
				101, 97, 110, 84, 119, 101, 101, 110, 92, 76,
				101, 97, 110, 84, 119, 101, 101, 110, 72, 101,
				108, 112, 101, 114, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 58, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 92, 76, 101, 97, 110, 84, 119,
				101, 101, 110, 92, 76, 84, 68, 101, 115, 99,
				114, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 66, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 85, 110,
				105, 116, 121, 69, 110, 103, 105, 110, 101, 92,
				76, 101, 97, 110, 84, 119, 101, 101, 110, 92,
				76, 84, 68, 101, 115, 99, 114, 79, 112, 116,
				105, 111, 110, 97, 108, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 56, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 92, 76, 101, 97, 110, 84, 119,
				101, 101, 110, 92, 76, 84, 83, 101, 113, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 61,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 85, 110, 105, 116,
				121, 69, 110, 103, 105, 110, 101, 92, 76, 101,
				97, 110, 84, 119, 101, 101, 110, 92, 84, 119,
				101, 101, 110, 84, 121, 112, 101, 115, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 54, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 85, 110, 105, 116, 121,
				69, 110, 103, 105, 110, 101, 92, 76, 105, 115,
				116, 67, 111, 109, 112, 111, 110, 101, 110, 116,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				51, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 92, 77,
				117, 114, 109, 117, 114, 72, 97, 115, 104, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 49,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 85, 110, 105, 116,
				121, 69, 110, 103, 105, 110, 101, 92, 80, 97,
				114, 97, 108, 108, 101, 108, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 62, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 85, 110, 105, 116, 121, 69, 110,
				103, 105, 110, 101, 92, 80, 114, 105, 111, 114,
				105, 116, 121, 76, 105, 115, 116, 67, 111, 109,
				112, 111, 110, 101, 110, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 56, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 85, 110, 105, 116, 121, 69, 110,
				103, 105, 110, 101, 92, 82, 101, 110, 100, 101,
				114, 84, 101, 120, 116, 117, 114, 101, 69, 120,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				49, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 92, 83,
				104, 97, 100, 101, 114, 69, 120, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 59, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 85, 110, 105, 116, 121, 69,
				110, 103, 105, 110, 101, 92, 83, 105, 110, 103,
				108, 101, 116, 111, 110, 67, 111, 109, 112, 111,
				110, 101, 110, 116, 46, 99, 115, 0, 0, 0,
				5, 0, 0, 0, 58, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 92, 83, 116, 97, 98, 108, 101, 79,
				98, 106, 101, 99, 116, 67, 97, 99, 104, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				47, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 92, 83,
				116, 114, 105, 110, 103, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 50, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 92, 84, 101, 120, 116, 117, 114,
				101, 69, 120, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 52, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 110, 105, 116, 121, 69, 110, 103, 105, 110,
				101, 92, 84, 105, 109, 101, 87, 97, 114, 110,
				105, 110, 103, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 52, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 110, 105, 116, 121, 69, 110, 103, 105, 110,
				101, 92, 84, 114, 97, 110, 115, 102, 111, 114,
				109, 69, 120, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 53, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 110, 105, 116, 121, 69, 110, 103, 105, 110,
				101, 92, 85, 116, 105, 108, 105, 116, 121, 92,
				77, 101, 115, 104, 46, 99, 115, 0, 0, 0,
				4, 0, 0, 0, 51, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 92, 85, 116, 105, 108, 105, 116, 121,
				92, 79, 115, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 56, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 110, 105, 116, 121, 69, 110, 103, 105, 110,
				101, 92, 85, 116, 105, 108, 105, 116, 121, 92,
				84, 101, 120, 116, 117, 114, 101, 46, 99, 115
			},
			TypesData = new byte[1244]
			{
				0, 0, 0, 0, 25, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 69, 120, 116, 101, 110,
				100, 124, 67, 97, 109, 101, 114, 97, 69, 120,
				0, 0, 0, 0, 31, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 69, 120, 116, 101, 110,
				100, 124, 84, 101, 120, 116, 117, 114, 101, 83,
				97, 109, 112, 108, 101, 114, 0, 0, 0, 0,
				19, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 124, 67, 111, 108, 111, 114, 69, 120,
				0, 0, 0, 0, 19, 85, 110, 105, 116, 121,
				69, 110, 103, 105, 110, 101, 124, 68, 101, 98,
				117, 103, 69, 120, 0, 0, 0, 0, 19, 124,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 66,
				101, 104, 97, 118, 105, 111, 117, 114, 0, 0,
				0, 0, 18, 124, 73, 110, 118, 111, 107, 101,
				72, 97, 110, 100, 108, 101, 114, 66, 97, 115,
				101, 0, 0, 0, 0, 14, 124, 73, 110, 118,
				111, 107, 101, 72, 97, 110, 100, 108, 101, 114,
				0, 0, 0, 0, 23, 124, 73, 110, 118, 111,
				107, 101, 72, 97, 110, 100, 108, 101, 114, 70,
				105, 120, 101, 100, 84, 105, 109, 101, 0, 0,
				0, 0, 18, 124, 73, 110, 118, 111, 107, 101,
				84, 114, 97, 99, 107, 105, 110, 103, 75, 101,
				121, 0, 0, 0, 0, 19, 124, 73, 110, 118,
				111, 107, 101, 84, 114, 97, 99, 107, 105, 110,
				103, 68, 97, 116, 97, 0, 0, 0, 0, 13,
				124, 73, 110, 118, 111, 107, 101, 65, 99, 116,
				105, 111, 110, 0, 0, 0, 0, 15, 124, 73,
				110, 118, 111, 107, 101, 80, 114, 111, 102, 105,
				108, 101, 114, 0, 0, 0, 0, 19, 124, 74,
				115, 111, 110, 77, 111, 100, 101, 108, 65, 116,
				116, 114, 105, 98, 117, 116, 101, 0, 0, 0,
				0, 11, 124, 76, 101, 97, 110, 84, 101, 115,
				116, 101, 114, 0, 0, 0, 0, 9, 124, 76,
				101, 97, 110, 84, 101, 115, 116, 0, 0, 0,
				0, 10, 124, 76, 101, 97, 110, 84, 119, 101,
				101, 110, 0, 0, 0, 0, 10, 124, 76, 84,
				85, 116, 105, 108, 105, 116, 121, 0, 0, 0,
				0, 9, 124, 76, 84, 66, 101, 122, 105, 101,
				114, 0, 0, 0, 0, 13, 124, 76, 84, 66,
				101, 122, 105, 101, 114, 80, 97, 116, 104, 0,
				0, 0, 0, 9, 124, 76, 84, 83, 112, 108,
				105, 110, 101, 0, 0, 0, 0, 7, 124, 76,
				84, 82, 101, 99, 116, 0, 0, 0, 0, 8,
				124, 76, 84, 69, 118, 101, 110, 116, 0, 0,
				0, 0, 6, 124, 76, 84, 71, 85, 73, 0,
				0, 0, 0, 21, 68, 101, 110, 116, 101, 100,
				80, 105, 120, 101, 108, 124, 76, 101, 97, 110,
				68, 117, 109, 109, 121, 0, 0, 0, 0, 17,
				124, 76, 101, 97, 110, 84, 119, 101, 101, 110,
				72, 101, 108, 112, 101, 114, 115, 0, 0, 0,
				0, 8, 124, 76, 84, 68, 101, 115, 99, 114,
				0, 0, 0, 0, 16, 124, 76, 84, 68, 101,
				115, 99, 114, 79, 112, 116, 105, 111, 110, 97,
				108, 0, 0, 0, 0, 6, 124, 76, 84, 83,
				101, 113, 0, 0, 0, 0, 10, 124, 84, 119,
				101, 101, 110, 77, 111, 100, 101, 1, 0, 0,
				0, 14, 124, 76, 105, 115, 116, 67, 111, 109,
				112, 111, 110, 101, 110, 116, 1, 0, 0, 0,
				14, 124, 76, 105, 115, 116, 67, 111, 109, 112,
				111, 110, 101, 110, 116, 0, 0, 0, 0, 11,
				124, 77, 117, 114, 109, 117, 114, 72, 97, 115,
				104, 0, 0, 0, 0, 13, 124, 77, 117, 114,
				109, 117, 114, 72, 97, 115, 104, 69, 120, 0,
				0, 0, 0, 22, 85, 110, 105, 116, 121, 69,
				110, 103, 105, 110, 101, 124, 80, 97, 114, 97,
				108, 108, 101, 108, 69, 120, 0, 0, 0, 0,
				22, 124, 80, 114, 105, 111, 114, 105, 116, 121,
				76, 105, 115, 116, 67, 111, 109, 112, 111, 110,
				101, 110, 116, 0, 0, 0, 0, 32, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 69, 120,
				116, 101, 110, 100, 124, 82, 101, 110, 100, 101,
				114, 84, 101, 120, 116, 117, 114, 101, 69, 120,
				0, 0, 0, 0, 9, 124, 83, 104, 97, 100,
				101, 114, 69, 120, 1, 0, 0, 0, 19, 124,
				83, 105, 110, 103, 108, 101, 116, 111, 110, 67,
				111, 109, 112, 111, 110, 101, 110, 116, 1, 0,
				0, 0, 19, 124, 83, 105, 110, 103, 108, 101,
				116, 111, 110, 67, 111, 109, 112, 111, 110, 101,
				110, 116, 0, 0, 0, 0, 13, 124, 73, 83,
				116, 97, 98, 108, 101, 73, 110, 100, 101, 120,
				0, 0, 0, 0, 18, 124, 83, 116, 97, 98,
				108, 101, 69, 110, 116, 105, 116, 121, 67, 97,
				99, 104, 101, 0, 0, 0, 0, 18, 124, 83,
				116, 97, 98, 108, 101, 79, 98, 106, 101, 99,
				116, 67, 97, 99, 104, 101, 0, 0, 0, 0,
				33, 83, 116, 97, 98, 108, 101, 79, 98, 106,
				101, 99, 116, 67, 97, 99, 104, 101, 124, 86,
				97, 108, 105, 100, 69, 110, 117, 109, 101, 114,
				97, 116, 111, 114, 0, 0, 0, 0, 27, 83,
				116, 97, 98, 108, 101, 79, 98, 106, 101, 99,
				116, 67, 97, 99, 104, 101, 124, 86, 97, 108,
				105, 100, 86, 105, 101, 119, 0, 0, 0, 0,
				28, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 124, 83, 116, 114, 105, 110, 103, 69,
				120, 116, 101, 110, 115, 105, 111, 110, 115, 0,
				0, 0, 0, 21, 85, 110, 105, 116, 121, 69,
				110, 103, 105, 110, 101, 124, 84, 101, 120, 116,
				117, 114, 101, 69, 120, 0, 0, 0, 0, 12,
				124, 84, 105, 109, 101, 87, 97, 114, 110, 105,
				110, 103, 0, 0, 0, 0, 28, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 69, 120, 116,
				101, 110, 100, 124, 84, 114, 97, 110, 115, 102,
				111, 114, 109, 69, 120, 0, 0, 0, 0, 22,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				85, 116, 105, 108, 105, 116, 121, 124, 77, 101,
				115, 104, 0, 0, 0, 0, 20, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 116, 105,
				108, 105, 116, 121, 124, 79, 115, 0, 0, 0,
				0, 40, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 85, 116, 105, 108, 105, 116, 121, 46,
				79, 115, 124, 80, 82, 79, 67, 69, 83, 83,
				95, 73, 78, 70, 79, 82, 77, 65, 84, 73,
				79, 78, 0, 0, 0, 0, 32, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 85, 116, 105,
				108, 105, 116, 121, 46, 79, 115, 124, 83, 84,
				65, 82, 84, 85, 80, 73, 78, 70, 79, 0,
				0, 0, 0, 40, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 85, 116, 105, 108, 105, 116,
				121, 46, 79, 115, 124, 83, 69, 67, 85, 82,
				73, 84, 89, 95, 65, 84, 84, 82, 73, 66,
				85, 84, 69, 83, 0, 0, 0, 0, 25, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 85,
				116, 105, 108, 105, 116, 121, 124, 84, 101, 120,
				116, 117, 114, 101
			},
			TotalFiles = 29,
			TotalTypes = 54,
			IsEditorOnly = false
		};
	}
}
namespace DentedPixel
{
	public class LeanDummy
	{
	}
}
namespace UnityEngine
{
	public static class ColorEx
	{
		public static string ToHex(this Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		public static string ToString(Color color)
		{
			return $"{color.r} {color.g} {color.b} {color.a}";
		}

		public static Color Parse(string str)
		{
			string[] array = str.Split(' ');
			if (array.Length == 3 && float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2) && float.TryParse(array[2], out var result3))
			{
				return new Color(result, result2, result3);
			}
			if (array.Length == 4 && float.TryParse(array[0], out result) && float.TryParse(array[1], out result2) && float.TryParse(array[2], out result3) && float.TryParse(array[3], out var result4))
			{
				return new Color(result, result2, result3, result4);
			}
			return Color.white;
		}

		public static Color WithAlpha(this Color color, float a)
		{
			return new Color(color.r, color.g, color.b, a);
		}

		public static int ToInt32(this Color32 color)
		{
			return (color.r << 24) | (color.g << 16) | (color.b << 8) | color.a;
		}

		public static Color32 FromInt32(int value)
		{
			return new Color32((byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value);
		}
	}
	public static class DebugEx
	{
		private static float startTime;

		public static void Log(object message, StackTraceLogType stacktrace = StackTraceLogType.None)
		{
			StackTraceLogType stackTraceLogType = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, stacktrace);
			Debug.Log(message);
			Application.SetStackTraceLogType(LogType.Log, stackTraceLogType);
		}

		public static void Log(object message, Object context, StackTraceLogType stacktrace = StackTraceLogType.None)
		{
			StackTraceLogType stackTraceLogType = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, stacktrace);
			Debug.Log(message, context);
			Application.SetStackTraceLogType(LogType.Log, stackTraceLogType);
		}

		public static void LogWarning(object message, StackTraceLogType stacktrace = StackTraceLogType.None)
		{
			StackTraceLogType stackTraceLogType = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, stacktrace);
			Debug.LogWarning(message);
			Application.SetStackTraceLogType(LogType.Log, stackTraceLogType);
		}

		public static void LogWarning(object message, Object context, StackTraceLogType stacktrace = StackTraceLogType.None)
		{
			StackTraceLogType stackTraceLogType = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, stacktrace);
			Debug.LogWarning(message, context);
			Application.SetStackTraceLogType(LogType.Log, stackTraceLogType);
		}

		public static void DrawWireCube(Vector3 centre, Vector3 size, Color colour, float duration)
		{
			DrawWireCube(centre, size, Quaternion.identity, colour, duration);
		}

		public static void DrawWireCube(Vector3 centre, Vector3 size, Quaternion rotation, Color colour, float duration)
		{
			Vector3 vector = centre - size / 2f;
			Vector3 vector2 = centre + size / 2f;
			Vector3[] array = new Vector3[8]
			{
				new Vector3(vector.x, vector.y, vector.z),
				new Vector3(vector2.x, vector.y, vector.z),
				new Vector3(vector.x, vector2.y, vector.z),
				new Vector3(vector.x, vector.y, vector2.z),
				new Vector3(vector2.x, vector.y, vector2.z),
				new Vector3(vector2.x, vector2.y, vector.z),
				new Vector3(vector.x, vector2.y, vector2.z),
				new Vector3(vector2.x, vector2.y, vector2.z)
			};
			if (rotation != Quaternion.identity)
			{
				for (int i = 0; i < 8; i++)
				{
					array[i] = centre + rotation * (array[i] - centre);
				}
			}
			DrawWireCube(array, colour, duration);
		}

		public static void DrawWireCube(Vector3[] points, Color colour, float duration)
		{
			if (points.Length != 8)
			{
				Debug.LogError("DrawWireCube: Expected eight points.");
				return;
			}
			Debug.DrawLine(points[0], points[1], colour, duration);
			Debug.DrawLine(points[0], points[3], colour, duration);
			Debug.DrawLine(points[3], points[4], colour, duration);
			Debug.DrawLine(points[1], points[4], colour, duration);
			Debug.DrawLine(points[2], points[5], colour, duration);
			Debug.DrawLine(points[2], points[6], colour, duration);
			Debug.DrawLine(points[6], points[7], colour, duration);
			Debug.DrawLine(points[5], points[7], colour, duration);
			Debug.DrawLine(points[0], points[2], colour, duration);
			Debug.DrawLine(points[1], points[5], colour, duration);
			Debug.DrawLine(points[3], points[6], colour, duration);
			Debug.DrawLine(points[4], points[7], colour, duration);
		}

		public static void StartTiming()
		{
			startTime = Time.realtimeSinceStartup;
		}

		public static void StopTiming()
		{
			Debug.Log("Time: " + (Time.realtimeSinceStartup - startTime) * 1000f + "ms");
		}
	}
	public static class ParallelEx
	{
		public static void Call(Action<int, int> action)
		{
			Task[] tasks = new Task[Environment.ProcessorCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				int threadId = i;
				tasks[i] = Task.Run(delegate
				{
					action(threadId, tasks.Length);
				});
			}
			Task[] array = tasks;
			for (int num = 0; num < array.Length; num++)
			{
				array[num].Wait();
			}
		}

		[Obsolete("Use Task.Run instead - see ParallelEx.Coroutine implementation for an example")]
		public static IEnumerator Coroutine(Action action)
		{
			Task task = Task.Run(action);
			while (!task.IsCompleted)
			{
				yield return null;
			}
			task.Wait();
		}
	}
	public static class StringExtensions
	{
		public static string BBCodeToUnity(this string x)
		{
			x = x.Replace("[", "<");
			x = x.Replace("]", ">");
			return x;
		}

		public static Vector3 ToVector3(this string str)
		{
			Vector3 result = default(Vector3);
			string[] array = str.Trim('(', ')', ' ').Replace(",", " ").Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 3)
			{
				return result;
			}
			result.x = array[0].ToFloat();
			result.y = array[1].ToFloat();
			result.z = array[2].ToFloat();
			return result;
		}

		public static Color ToColor(this string str)
		{
			Color result = new Color(1f, 1f, 1f, 1f);
			string[] array = str.Split(',');
			if (array.Length != 3 && array.Length != 4)
			{
				return result;
			}
			result.r = array[0].ToFloat();
			result.g = array[1].ToFloat();
			result.b = array[2].ToFloat();
			if (array.Length == 4)
			{
				result.a = array[3].ToFloat();
			}
			return result;
		}
	}
	public static class TextureEx
	{
		private static Color32[] buffer = new Color32[8192];

		public static void Clear(this Texture2D tex, Color32 color)
		{
			if (tex.width > buffer.Length)
			{
				Debug.LogError("Trying to clear texture that is too big: " + tex.width);
				return;
			}
			for (int i = 0; i < tex.width; i++)
			{
				buffer[i] = color;
			}
			for (int j = 0; j < tex.height; j++)
			{
				tex.SetPixels32(0, j, tex.width, 1, buffer);
			}
			tex.Apply();
		}

		public static int GetSizeInBytes(this Texture texture)
		{
			int num = texture.width;
			int num2 = texture.height;
			if (texture is Texture2D)
			{
				Texture2D obj = texture as Texture2D;
				int bitsPerPixel = GetBitsPerPixel(obj.format);
				int mipmapCount = obj.mipmapCount;
				int i = 1;
				int num3 = 0;
				int loadedMipmapLevel = obj.loadedMipmapLevel;
				for (; i <= mipmapCount; i++)
				{
					if (i >= loadedMipmapLevel)
					{
						num3 += num * num2 * bitsPerPixel / 8;
					}
					num /= 2;
					num2 /= 2;
				}
				return num3;
			}
			if (texture is Texture2DArray)
			{
				Texture2DArray obj2 = texture as Texture2DArray;
				int bitsPerPixel2 = GetBitsPerPixel(obj2.format);
				int num4 = 10;
				int j = 1;
				int num5 = 0;
				int depth = obj2.depth;
				for (; j <= num4; j++)
				{
					num5 += num * num2 * bitsPerPixel2 / 8;
					num /= 2;
					num2 /= 2;
				}
				return num5 * depth;
			}
			if (texture is Cubemap)
			{
				int bitsPerPixel3 = GetBitsPerPixel((texture as Cubemap).format);
				int num6 = num * num2 * bitsPerPixel3 / 8;
				int num7 = 6;
				return num6 * num7;
			}
			if (texture is RenderTexture)
			{
				RenderTexture renderTexture = texture as RenderTexture;
				int bitsPerPixel4 = GetBitsPerPixel(renderTexture.format, renderTexture.depth);
				int mipmapCount2 = renderTexture.mipmapCount;
				int k = 1;
				int num8 = 0;
				for (; k <= mipmapCount2; k++)
				{
					num8 += num * num2 * bitsPerPixel4 / 8;
					num /= 2;
					num2 /= 2;
				}
				return num8;
			}
			return 0;
		}

		public static int GetBitsPerPixel(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
				return 8;
			case TextureFormat.ARGB4444:
				return 16;
			case TextureFormat.RGBA4444:
				return 16;
			case TextureFormat.RGB24:
				return 24;
			case TextureFormat.RGBA32:
				return 32;
			case TextureFormat.ARGB32:
				return 32;
			case TextureFormat.RGB565:
				return 16;
			case TextureFormat.DXT1:
			case TextureFormat.DXT1Crunched:
				return 4;
			case TextureFormat.DXT5:
			case TextureFormat.BC7:
			case TextureFormat.DXT5Crunched:
				return 8;
			case TextureFormat.PVRTC_RGB2:
				return 2;
			case TextureFormat.PVRTC_RGBA2:
				return 2;
			case TextureFormat.PVRTC_RGB4:
				return 4;
			case TextureFormat.PVRTC_RGBA4:
				return 4;
			case TextureFormat.ETC_RGB4:
				return 4;
			case TextureFormat.ETC2_RGBA8:
				return 8;
			case TextureFormat.BGRA32:
				return 32;
			default:
				return 0;
			}
		}

		public static int GetBitsPerPixel(RenderTextureFormat format, int depthBits)
		{
			switch (format)
			{
			case RenderTextureFormat.ARGBFloat:
			case RenderTextureFormat.ARGBInt:
				return 128;
			case RenderTextureFormat.ARGBHalf:
			case RenderTextureFormat.DefaultHDR:
			case RenderTextureFormat.ARGB64:
			case RenderTextureFormat.RGFloat:
			case RenderTextureFormat.RGInt:
			case RenderTextureFormat.RGBAUShort:
			case RenderTextureFormat.BGRA10101010_XR:
				return 64;
			default:
				return 32;
			case RenderTextureFormat.RGB565:
			case RenderTextureFormat.ARGB4444:
			case RenderTextureFormat.ARGB1555:
			case RenderTextureFormat.RHalf:
			case RenderTextureFormat.RG16:
			case RenderTextureFormat.R16:
				return 16;
			case RenderTextureFormat.R8:
				return 8;
			case RenderTextureFormat.Depth:
			case RenderTextureFormat.Shadowmap:
				return depthBits;
			}
		}
	}
}
namespace Facepunch.Utility
{
	public static class Mesh
	{
		public static void Export(this UnityEngine.Mesh mesh, string filename)
		{
			StringBuilder stringBuilder = new StringBuilder();
			_ = mesh.vertices;
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector = vertices[i];
				stringBuilder.AppendLine($"v {vector.x} {vector.y} {vector.z}");
			}
			Vector2[] uv = mesh.uv;
			for (int i = 0; i < uv.Length; i++)
			{
				Vector2 vector2 = uv[i];
				stringBuilder.AppendLine($"vt {vector2.x} {vector2.y}");
			}
			vertices = mesh.normals;
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vector3 = vertices[i];
				stringBuilder.AppendLine($"vn {vector3.x} {vector3.y} {vector3.z}");
			}
			for (int j = 0; j < mesh.subMeshCount; j++)
			{
				int[] indices = mesh.GetIndices(j);
				for (int k = 0; k < indices.Length; k += 3)
				{
					stringBuilder.AppendLine($"f {indices[k] + 1}/{indices[k] + 1}/{indices[k] + 1} {indices[k + 1] + 1}/{indices[k + 1] + 1}/{indices[k + 1] + 1} {indices[k + 2] + 1}/{indices[k + 2] + 1}/{indices[k + 2] + 1}");
				}
			}
			if (File.Exists(filename))
			{
				File.Delete(filename);
			}
			File.WriteAllText(filename, stringBuilder.ToString());
		}
	}
	public static class Os
	{
		internal struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;

			public IntPtr hThread;

			public int dwProcessId;

			public int dwThreadId;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct STARTUPINFO
		{
			public int cb;

			public IntPtr lpReserved;

			public IntPtr lpDesktop;

			public IntPtr lpTitle;

			public int dwX;

			public int dwY;

			public int dwXSize;

			public int dwYSize;

			public int dwXCountChars;

			public int dwYCountChars;

			public int dwFillAttribute;

			public int dwFlags;

			public short wShowWindow;

			public short cbReserved2;

			public IntPtr lpReserved2;

			public IntPtr hStdInput;

			public IntPtr hStdOutput;

			public IntPtr hStdError;
		}

		public struct SECURITY_ATTRIBUTES
		{
			public int nLength;

			public IntPtr lpSecurityDescriptor;

			public int bInheritHandle;
		}

		public static void OpenFolder(string folder)
		{
			if (Directory.Exists(folder))
			{
				Application.OpenURL(folder);
			}
			else
			{
				StartProcess(Environment.GetEnvironmentVariable("windir") + "\\explorer.exe", "/select," + folder.Replace("/", "\\"));
			}
		}

		public static bool StartProcess(string executable, string arguments)
		{
			PROCESS_INFORMATION lpProcessInformation = default(PROCESS_INFORMATION);
			STARTUPINFO lpStartupInfo = default(STARTUPINFO);
			SECURITY_ATTRIBUTES lpProcessAttributes = default(SECURITY_ATTRIBUTES);
			SECURITY_ATTRIBUTES lpThreadAttributes = default(SECURITY_ATTRIBUTES);
			lpProcessAttributes.nLength = Marshal.SizeOf(lpProcessAttributes);
			lpThreadAttributes.nLength = Marshal.SizeOf(lpThreadAttributes);
			return CreateProcess(executable, " " + arguments, ref lpProcessAttributes, ref lpThreadAttributes, bInheritHandles: false, 32u, IntPtr.Zero, null, ref lpStartupInfo, out lpProcessInformation);
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
	}
	public static class Texture
	{
		public static void CompressNormals(this Texture2D tex)
		{
			Color color = default(Color);
			for (int i = 0; i < tex.width; i++)
			{
				for (int j = 0; j < tex.height; j++)
				{
					Color pixel = tex.GetPixel(i, j);
					color.r = pixel.g;
					color.g = pixel.g;
					color.b = pixel.g;
					color.a = pixel.r;
					tex.SetPixel(i, j, color);
				}
			}
			tex.Apply(updateMipmaps: true);
		}

		public static void DecompressNormals(this Texture2D tex)
		{
			Color color = default(Color);
			for (int i = 0; i < tex.width; i++)
			{
				for (int j = 0; j < tex.height; j++)
				{
					Color pixel = tex.GetPixel(i, j);
					color.r = pixel.a;
					color.g = Mathf.GammaToLinearSpace(pixel.g);
					color.b = 1f;
					color.a = 1f;
					tex.SetPixel(i, j, color);
				}
			}
			tex.Apply(updateMipmaps: true);
		}

		public static bool SaveAsPng(this UnityEngine.Texture texture, string fileName)
		{
			Texture2D texture2D = texture as Texture2D;
			if (texture2D == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				array = texture2D.EncodeToPNG();
			}
			catch (Exception)
			{
			}
			if (array == null)
			{
				Texture2D texture2D2 = CreateReadableCopy(texture2D);
				array = texture2D2.EncodeToPNG();
				UnityEngine.Object.DestroyImmediate(texture2D2);
			}
			if (array == null)
			{
				return false;
			}
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			File.WriteAllBytes(fileName, array);
			return true;
		}

		public static Texture2D CreateReadableCopy(Texture2D texture, int width = 0, int height = 0, bool linear = false)
		{
			if (width <= 0)
			{
				width = texture.width;
			}
			if (height <= 0)
			{
				height = texture.height;
			}
			RenderTexture renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, linear ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.Default);
			Texture2D obj = new Texture2D(width, height, TextureFormat.ARGB32, texture.mipmapCount > 0, linear)
			{
				name = texture.name
			};
			Graphics.Blit(texture, renderTexture);
			RenderTexture.active = renderTexture;
			obj.ReadPixels(new Rect(0f, 0f, width, height), 0, 0, recalculateMipMaps: true);
			RenderTexture.active = null;
			obj.Apply(updateMipmaps: true);
			UnityEngine.Object.DestroyImmediate(renderTexture);
			return obj;
		}

		public static Texture2D LimitSize(Texture2D tex, int w, int h, bool linear = false)
		{
			if (tex.width <= w && tex.height <= h)
			{
				return tex;
			}
			int num = tex.width;
			int num2 = tex.height;
			if (num > w)
			{
				double num3 = (double)w / (double)num;
				num = (int)((double)num * num3);
				num2 = (int)((double)num2 * num3);
			}
			if (num2 > h)
			{
				double num4 = (double)h / (double)num2;
				num = (int)((double)num * num4);
				num2 = (int)((double)num2 * num4);
			}
			return CreateReadableCopy(tex, num, num2, linear);
		}

		public static Texture2D LimitSize(Texture2D tex, object maxTextureSize1, object maxTextureSize2)
		{
			throw new NotImplementedException();
		}
	}
}
namespace Facepunch.Extend
{
	public static class CameraEx
	{
		public static void FocusOnRenderer(this Camera cam, GameObject obj, Vector3 lookDirection, Vector3 Up, int layerMask = -1, float distanceModifier = 0f)
		{
			Vector3 position = obj.transform.position;
			Quaternion rotation = obj.transform.rotation;
			obj.transform.SetPositionAndRotation(Vector3.one, Quaternion.identity);
			obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 0.01f);
			bool flag = true;
			Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				if (renderer.enabled && renderer.gameObject.activeInHierarchy && !(renderer is ParticleSystemRenderer) && !renderer.gameObject.name.EndsWith("lod01", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod02", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod03", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod04", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod1", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod2", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod3", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod4", StringComparison.InvariantCultureIgnoreCase) && (layerMask & (1 << renderer.gameObject.layer)) != 0)
				{
					if (flag)
					{
						bounds = renderer.bounds;
						flag = false;
					}
					else
					{
						bounds.Encapsulate(renderer.bounds);
					}
				}
			}
			float num = bounds.size.magnitude * 0.33f / Mathf.Tan(cam.fieldOfView * 0.5f * (MathF.PI / 180f));
			Vector3 point = obj.transform.worldToLocalMatrix.MultiplyPoint(bounds.center);
			obj.transform.SetPositionAndRotation(position, rotation);
			point = obj.transform.localToWorldMatrix.MultiplyPoint(point);
			cam.transform.position = point + obj.transform.TransformDirection(lookDirection.normalized) * (num + distanceModifier);
			cam.transform.LookAt(point, obj.transform.TransformDirection(Up.normalized));
		}

		public static void SavePNG(string path, Texture2D texture)
		{
			byte[] bytes = texture.EncodeToPNG();
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllBytes(path, bytes);
		}

		public static void ScreenshotToDisk(this Camera cam, string name, int width, int height, bool transparent, int SuperSampleSize, Color? background = null)
		{
			Texture2D texture2D = cam.ScreenshotToTexture(width, height, transparent, SuperSampleSize, background);
			SavePNG(name, texture2D);
			UnityEngine.Object.DestroyImmediate(texture2D, allowDestroyingAssets: true);
		}

		public static Texture2D ScreenshotToTexture(this Camera cam, int width, int height, bool transparent, int superSampleSize, Color? background = null)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width * superSampleSize, height * superSampleSize, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			Color backgroundColor = cam.backgroundColor;
			CameraClearFlags clearFlags = cam.clearFlags;
			RenderTexture targetTexture = cam.targetTexture;
			int antiAliasing = QualitySettings.antiAliasing;
			AnisotropicFiltering anisotropicFiltering = QualitySettings.anisotropicFiltering;
			bool sRGBWrite = GL.sRGBWrite;
			GameObject obj = new GameObject();
			cam.forceIntoRenderTexture = true;
			cam.targetTexture = temporary;
			cam.aspect = 1f;
			cam.renderingPath = RenderingPath.UsePlayerSettings;
			cam.rect = new Rect(0f, 0f, 1f, 1f);
			cam.allowHDR = true;
			Texture.SetGlobalAnisotropicFilteringLimits(16, 16);
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
			QualitySettings.antiAliasing = 8;
			if (transparent)
			{
				cam.clearFlags = CameraClearFlags.Depth;
				cam.backgroundColor = background ?? new Color(0f, 0f, 0f, 0f);
			}
			RenderTexture.active = temporary;
			GL.Clear(clearDepth: true, clearColor: true, background ?? new Color(0f, 0f, 0f, 0f));
			GL.sRGBWrite = true;
			cam.Render();
			RenderTexture.active = null;
			RenderTexture.active = temporary;
			Texture2D texture2D = new Texture2D(temporary.width, temporary.height, TextureFormat.ARGB32, mipChain: true);
			texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0, recalculateMipMaps: true);
			texture2D.filterMode = FilterMode.Trilinear;
			texture2D.anisoLevel = 32;
			RenderTexture.active = null;
			cam.targetTexture = targetTexture;
			QualitySettings.antiAliasing = antiAliasing;
			QualitySettings.anisotropicFiltering = anisotropicFiltering;
			Texture.SetGlobalAnisotropicFilteringLimits(1, 16);
			if (superSampleSize != 1)
			{
				texture2D.Apply();
				RenderTexture renderTexture = (RenderTexture.active = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB));
				GL.Clear(clearDepth: true, clearColor: true, new Color(0f, 0f, 0f, 0f));
				GL.sRGBWrite = true;
				Graphics.Blit(texture2D, renderTexture);
				texture2D.Resize(width, height);
				texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
				RenderTexture.active = null;
				texture2D.Apply();
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			RenderTexture.ReleaseTemporary(temporary);
			UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets: true);
			if (transparent)
			{
				cam.clearFlags = clearFlags;
				cam.backgroundColor = backgroundColor;
			}
			GL.sRGBWrite = sRGBWrite;
			return texture2D;
		}
	}
	public class TextureSampler
	{
		private Color[] _data;

		private int _height;

		private int _width;

		public Color GetPixelBilinear(float u, float v)
		{
			u *= (float)_width;
			v *= (float)_height;
			int num = Mathf.FloorToInt(u);
			int num2 = Mathf.FloorToInt(v);
			float num3 = u - (float)num;
			float num4 = v - (float)num2;
			float num5 = 1f - num3;
			float num6 = 1f - num4;
			return (GetPixel(num, num2) * num5 + GetPixel(num + 1, num2) * num3) * num6 + (GetPixel(num, num2 + 1) * num5 + GetPixel(num + 1, num2 + 1) * num3) * num4;
		}

		public Color GetPixel(float x, float y)
		{
			int num = (int)WrapBetween(x, 0f, _width);
			int num2 = (int)WrapBetween(y, 0f, _height);
			return _data[num2 * _width + num];
		}

		private float Mod(float x, float y)
		{
			if (0f == y)
			{
				return x;
			}
			return x - y * Mathf.Floor(x / y);
		}

		private float WrapBetween(float value, float min, float max)
		{
			return Mod(value - min, max - min) + min;
		}

		public TextureSampler(Texture2D source)
		{
			_data = source.GetPixels();
			_width = source.width;
			_height = source.height;
		}
	}
	public static class RenderTextureEx
	{
		private static Material _alphaBlending;

		public static Material AlphaBlending
		{
			get
			{
				if (!_alphaBlending)
				{
					_alphaBlending = new Material(Shader.Find("Hidden/BlitAlphaBlend"));
				}
				return _alphaBlending;
			}
		}

		public static void Blit(this RenderTexture t, Texture tex)
		{
			Graphics.Blit(tex, t);
		}

		public static void BlitWithAlphaBlending(this RenderTexture t, Texture tex)
		{
			Graphics.Blit(tex, t, AlphaBlending, 0);
		}

		public static void ToTexture(this RenderTexture t, Texture texture)
		{
			if (texture.width != t.width)
			{
				throw new Exception("width should match!");
			}
			if (texture.height != t.height)
			{
				throw new Exception("height should match!");
			}
			Graphics.SetRenderTarget(t);
			(texture as Texture2D).ReadPixels(new Rect(0f, 0f, texture.width, texture.height), 0, 0);
			Graphics.SetRenderTarget(null);
		}
	}
	public static class TransformEx
	{
		private static PointerEventData _pointerEvent;

		public static Transform FindChildRecursive(this Transform transform, string name)
		{
			Transform transform2 = transform.Find(name);
			for (int i = 0; i < transform.childCount; i++)
			{
				if (!(transform2 == null))
				{
					break;
				}
				transform2 = transform.GetChild(i).FindChildRecursive(name);
			}
			return transform2;
		}

		public static T GetOrAddComponent<T>(this Transform transform) where T : UnityEngine.Component
		{
			T val = transform.GetComponent<T>();
			if (val == null)
			{
				val = transform.gameObject.AddComponent<T>();
			}
			return val;
		}

		public static void DestroyAllChildren(this Transform transform, bool immediate = false)
		{
			for (int num = transform.childCount; num > 0; num--)
			{
				Transform child = transform.GetChild(num - 1);
				if (!child.CompareTag("persist"))
				{
					if (immediate)
					{
						UnityEngine.Object.DestroyImmediate(child.gameObject);
					}
					else
					{
						UnityEngine.Object.Destroy(child.gameObject);
					}
				}
			}
		}

		public static float AngleToPos(this Transform transform, Vector3 targetPos)
		{
			float y = transform.eulerAngles.y;
			Vector3 vector = targetPos - transform.position;
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f - y;
			if (num > 180f)
			{
				num -= 360f;
			}
			else if (num < -180f)
			{
				num += 360f;
			}
			return num;
		}

		public static int GetDepth(this Transform transform)
		{
			int num = 0;
			Transform parent = transform.parent;
			while (parent != null)
			{
				num++;
				parent = parent.parent;
			}
			return num;
		}

		public static bool ClickedInsideTransformOrChild(this Transform t, int? mouseButton = null)
		{
			if (mouseButton.HasValue && !Input.GetMouseButton(mouseButton.Value))
			{
				return false;
			}
			EventSystem current = EventSystem.current;
			if (current == null)
			{
				return false;
			}
			if (_pointerEvent == null)
			{
				_pointerEvent = new PointerEventData(current);
			}
			_pointerEvent.position = Input.mousePosition;
			List<RaycastResult> obj = Pool.Get<List<RaycastResult>>();
			EventSystem.current.RaycastAll(_pointerEvent, obj);
			foreach (RaycastResult item in obj)
			{
				if (item.gameObject.transform.IsChildOf(t))
				{
					Pool.FreeUnmanaged(ref obj);
					return true;
				}
			}
			Pool.FreeUnmanaged(ref obj);
			return false;
		}
	}
}
