using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class ObjectWorkQueue
{
	public static readonly List<ObjectWorkQueue> All = new List<ObjectWorkQueue>();

	public string Name = "Untitled ObjectWorkQueue";

	public TimeSpan WarningThreshold = TimeSpan.FromMilliseconds(200.0);

	public long TotalProcessedCount;

	public TimeSpan TotalExecutionTime;

	public int LastProcessedCount;

	public TimeSpan LastExecutionTime;

	public int HashSetMaxLength;

	public int LastQueueLength { get; protected set; }

	public abstract int QueueLength { get; }
}
public abstract class ObjectWorkQueue<T> : ObjectWorkQueue
{
	protected Queue<T> queue = new Queue<T>();

	protected HashSet<T> containerTest = new HashSet<T>();

	private readonly Stopwatch stopwatch = new Stopwatch();

	public override int QueueLength => queue.Count;

	public ObjectWorkQueue()
	{
		Name = GetType().Name;
		ObjectWorkQueue.All.Add(this);
	}

	public void Clear()
	{
		queue.Clear();
		containerTest.Clear();
		if (HashSetMaxLength > 256)
		{
			containerTest = new HashSet<T>();
			HashSetMaxLength = 0;
		}
	}

	public void RunQueue(double maximumMilliseconds)
	{
		LastExecutionTime = default(TimeSpan);
		LastProcessedCount = 0;
		base.LastQueueLength = QueueLength;
		if (queue.Count == 0)
		{
			return;
		}
		stopwatch.Restart();
		SortQueue();
		BeforeRunJobs();
		using (TimeWarning.New(Name, (int)WarningThreshold.TotalMilliseconds))
		{
			while (queue.Count > 0)
			{
				LastProcessedCount++;
				TotalProcessedCount++;
				T val = queue.Dequeue();
				containerTest.Remove(val);
				if (val != null)
				{
					RunJob(val);
				}
				if (stopwatch.Elapsed.TotalMilliseconds >= maximumMilliseconds)
				{
					break;
				}
			}
			LastExecutionTime = stopwatch.Elapsed;
			TotalExecutionTime += LastExecutionTime;
		}
		if (queue.Count == 0)
		{
			Clear();
		}
	}

	public void Add(T entity)
	{
		if (!Contains(entity) && ShouldAdd(entity))
		{
			queue.Enqueue(entity);
			containerTest.Add(entity);
			HashSetMaxLength = Mathf.Max(containerTest.Count, HashSetMaxLength);
		}
	}

	public void AddNoContainsCheck(T entity)
	{
		if (ShouldAdd(entity))
		{
			queue.Enqueue(entity);
		}
	}

	public bool Contains(T entity)
	{
		return containerTest.Contains(entity);
	}

	protected virtual void SortQueue()
	{
	}

	protected virtual bool ShouldAdd(T entity)
	{
		return true;
	}

	protected virtual void BeforeRunJobs()
	{
	}

	protected abstract void RunJob(T entity);

	public string Info()
	{
		return $"{QueueLength:n0}, lastCount: {LastProcessedCount:n0}, totCount: {TotalProcessedCount:n0}, totMS: {TotalExecutionTime:n0} ";
	}
}
