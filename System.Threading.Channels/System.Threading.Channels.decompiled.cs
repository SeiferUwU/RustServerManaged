using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using FxResources.System.Threading.Channels;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyTitle("System.Threading.Channels")]
[assembly: AssemblyDescription("System.Threading.Channels")]
[assembly: AssemblyDefaultAlias("System.Threading.Channels")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyProduct("Microsoft® .NET Framework")]
[assembly: AssemblyCopyright("© Microsoft Corporation.  All rights reserved.")]
[assembly: AssemblyFileVersion("4.6.26515.06")]
[assembly: AssemblyInformationalVersion("4.6.26515.06 @BuiltBy: dlab-DDVSOWINAGE059 @Branch: release/2.1 @SrcCode: https://github.com/dotnet/corefx/tree/30ab651fcb4354552bd4891619a0bdd81e0ebdbf")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: AssemblyMetadata("PreferInbox", "True")]
[assembly: AssemblyVersion("4.0.0.0")]
namespace FxResources.System.Threading.Channels
{
	internal static class SR
	{
	}
}
namespace System
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct VoidResult
	{
	}
	internal static class SR
	{
		private static ResourceManager s_resourceManager;

		private static ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new ResourceManager(ResourceType));

		internal static Type ResourceType { get; } = typeof(SR);

		internal static string ChannelClosedException_DefaultMessage => GetResourceString("ChannelClosedException_DefaultMessage", null);

		internal static string InvalidOperation_IncompleteAsyncOperation => GetResourceString("InvalidOperation_IncompleteAsyncOperation", null);

		internal static string InvalidOperation_MultipleContinuations => GetResourceString("InvalidOperation_MultipleContinuations", null);

		internal static string InvalidOperation_IncorrectToken => GetResourceString("InvalidOperation_IncorrectToken", null);

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args != null)
			{
				if (UsingResourceKeys())
				{
					return resourceFormat + string.Join(", ", args);
				}
				return string.Format(resourceFormat, args);
			}
			return resourceFormat;
		}

		internal static string Format(string resourceFormat, object p1)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1);
			}
			return string.Format(resourceFormat, p1);
		}

		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2);
			}
			return string.Format(resourceFormat, p1, p2);
		}

		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (UsingResourceKeys())
			{
				return string.Join(", ", resourceFormat, p1, p2, p3);
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}
	}
}
namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.All)]
	internal class __BlockReflectionAttribute : Attribute
	{
	}
}
namespace System.Threading.Channels
{
	[DebuggerDisplay("Items={ItemsCountForDebugger}, Capacity={_bufferedCapacity}, Mode={_mode}, Closed={ChannelIsClosedForDebugger}")]
	[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
	internal sealed class BoundedChannel<T> : Channel<T>, IDebugEnumerable<T>
	{
		[DebuggerDisplay("Items={ItemsCountForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class BoundedChannelReader : ChannelReader<T>, IDebugEnumerable<T>
		{
			internal readonly BoundedChannel<T> _parent;

			private readonly AsyncOperation<T> _readerSingleton;

			private readonly AsyncOperation<bool> _waiterSingleton;

			public override Task Completion => _parent._completion.Task;

			private int ItemsCountForDebugger => _parent._items.Count;

			internal BoundedChannelReader(BoundedChannel<T> parent)
			{
				_parent = parent;
				_readerSingleton = new AsyncOperation<T>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
				_waiterSingleton = new AsyncOperation<bool>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
			}

			public override bool TryRead(out T item)
			{
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (!parent._items.IsEmpty)
					{
						item = DequeueItemAndPostProcess();
						return true;
					}
				}
				item = default(T);
				return false;
			}

			public override ValueTask<T> ReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<T>(Task.FromCanceled<T>(cancellationToken));
				}
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (!parent._items.IsEmpty)
					{
						return new ValueTask<T>(DequeueItemAndPostProcess());
					}
					if (parent._doneWriting != null)
					{
						return ChannelUtilities.GetInvalidCompletionValueTask<T>(parent._doneWriting);
					}
					if (!cancellationToken.CanBeCanceled)
					{
						AsyncOperation<T> readerSingleton = _readerSingleton;
						if (readerSingleton.TryOwnAndReset())
						{
							parent._blockedReaders.EnqueueTail(readerSingleton);
							return readerSingleton.ValueTaskOfT;
						}
					}
					AsyncOperation<T> asyncOperation = new AsyncOperation<T>(parent._runContinuationsAsynchronously, cancellationToken);
					parent._blockedReaders.EnqueueTail(asyncOperation);
					return asyncOperation.ValueTaskOfT;
				}
			}

			public override ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
				}
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (!parent._items.IsEmpty)
					{
						return new ValueTask<bool>(result: true);
					}
					if (parent._doneWriting != null)
					{
						return (parent._doneWriting != ChannelUtilities.s_doneWritingSentinel) ? new ValueTask<bool>(Task.FromException<bool>(parent._doneWriting)) : default(ValueTask<bool>);
					}
					if (!cancellationToken.CanBeCanceled)
					{
						AsyncOperation<bool> waiterSingleton = _waiterSingleton;
						if (waiterSingleton.TryOwnAndReset())
						{
							ChannelUtilities.QueueWaiter(ref parent._waitingReadersTail, waiterSingleton);
							return waiterSingleton.ValueTaskOfT;
						}
					}
					AsyncOperation<bool> asyncOperation = new AsyncOperation<bool>(parent._runContinuationsAsynchronously, cancellationToken);
					ChannelUtilities.QueueWaiter(ref _parent._waitingReadersTail, asyncOperation);
					return asyncOperation.ValueTaskOfT;
				}
			}

			private T DequeueItemAndPostProcess()
			{
				BoundedChannel<T> parent = _parent;
				T result = parent._items.DequeueHead();
				if (parent._doneWriting != null)
				{
					if (parent._items.IsEmpty)
					{
						ChannelUtilities.Complete(parent._completion, parent._doneWriting);
					}
				}
				else
				{
					while (!parent._blockedWriters.IsEmpty)
					{
						VoidAsyncOperationWithData<T> voidAsyncOperationWithData = parent._blockedWriters.DequeueHead();
						if (voidAsyncOperationWithData.TrySetResult(default(VoidResult)))
						{
							parent._items.EnqueueTail(voidAsyncOperationWithData.Item);
							return result;
						}
					}
					ChannelUtilities.WakeUpWaiters(ref parent._waitingWritersTail, result: true);
				}
				return result;
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		[DebuggerDisplay("Items={ItemsCountForDebugger}, Capacity={CapacityForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class BoundedChannelWriter : ChannelWriter<T>, IDebugEnumerable<T>
		{
			internal readonly BoundedChannel<T> _parent;

			private readonly VoidAsyncOperationWithData<T> _writerSingleton;

			private readonly AsyncOperation<bool> _waiterSingleton;

			private int ItemsCountForDebugger => _parent._items.Count;

			private int CapacityForDebugger => _parent._bufferedCapacity;

			internal BoundedChannelWriter(BoundedChannel<T> parent)
			{
				_parent = parent;
				_writerSingleton = new VoidAsyncOperationWithData<T>(runContinuationsAsynchronously: true, default(CancellationToken), pooled: true);
				_waiterSingleton = new AsyncOperation<bool>(runContinuationsAsynchronously: true, default(CancellationToken), pooled: true);
			}

			public override bool TryComplete(Exception error)
			{
				BoundedChannel<T> parent = _parent;
				bool isEmpty;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return false;
					}
					parent._doneWriting = error ?? ChannelUtilities.s_doneWritingSentinel;
					isEmpty = parent._items.IsEmpty;
				}
				if (isEmpty)
				{
					ChannelUtilities.Complete(parent._completion, error);
				}
				ChannelUtilities.FailOperations<AsyncOperation<T>, T>(parent._blockedReaders, ChannelUtilities.CreateInvalidCompletionException(error));
				ChannelUtilities.FailOperations<VoidAsyncOperationWithData<T>, VoidResult>(parent._blockedWriters, ChannelUtilities.CreateInvalidCompletionException(error));
				ChannelUtilities.WakeUpWaiters(ref parent._waitingReadersTail, result: false, error);
				ChannelUtilities.WakeUpWaiters(ref parent._waitingWritersTail, result: false, error);
				return true;
			}

			public override bool TryWrite(T item)
			{
				AsyncOperation<T> asyncOperation = null;
				AsyncOperation<bool> listTail = null;
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return false;
					}
					int count = parent._items.Count;
					if (count != 0)
					{
						if (count < parent._bufferedCapacity)
						{
							parent._items.EnqueueTail(item);
							return true;
						}
						if (parent._mode == BoundedChannelFullMode.Wait)
						{
							return false;
						}
						if (parent._mode == BoundedChannelFullMode.DropWrite)
						{
							return true;
						}
						T val = ((parent._mode == BoundedChannelFullMode.DropNewest) ? parent._items.DequeueTail() : parent._items.DequeueHead());
						parent._items.EnqueueTail(item);
						return true;
					}
					while (!parent._blockedReaders.IsEmpty)
					{
						AsyncOperation<T> asyncOperation2 = parent._blockedReaders.DequeueHead();
						asyncOperation2.UnregisterCancellation();
						if (!asyncOperation2.IsCompleted)
						{
							asyncOperation = asyncOperation2;
							break;
						}
					}
					if (asyncOperation == null)
					{
						parent._items.EnqueueTail(item);
						listTail = parent._waitingReadersTail;
						if (listTail == null)
						{
							return true;
						}
						parent._waitingReadersTail = null;
					}
				}
				if (asyncOperation != null)
				{
					bool flag = asyncOperation.TrySetResult(item);
				}
				else
				{
					ChannelUtilities.WakeUpWaiters(ref listTail, result: true);
				}
				return true;
			}

			public override ValueTask<bool> WaitToWriteAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
				}
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return (parent._doneWriting != ChannelUtilities.s_doneWritingSentinel) ? new ValueTask<bool>(Task.FromException<bool>(parent._doneWriting)) : default(ValueTask<bool>);
					}
					if (parent._items.Count < parent._bufferedCapacity || parent._mode != BoundedChannelFullMode.Wait)
					{
						return new ValueTask<bool>(result: true);
					}
					if (!cancellationToken.CanBeCanceled)
					{
						AsyncOperation<bool> waiterSingleton = _waiterSingleton;
						if (waiterSingleton.TryOwnAndReset())
						{
							ChannelUtilities.QueueWaiter(ref parent._waitingWritersTail, waiterSingleton);
							return waiterSingleton.ValueTaskOfT;
						}
					}
					AsyncOperation<bool> asyncOperation = new AsyncOperation<bool>(runContinuationsAsynchronously: true, cancellationToken);
					ChannelUtilities.QueueWaiter(ref parent._waitingWritersTail, asyncOperation);
					return asyncOperation.ValueTaskOfT;
				}
			}

			public override ValueTask WriteAsync(T item, CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask(Task.FromCanceled(cancellationToken));
				}
				AsyncOperation<T> asyncOperation = null;
				AsyncOperation<bool> listTail = null;
				BoundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return new ValueTask(Task.FromException(ChannelUtilities.CreateInvalidCompletionException(parent._doneWriting)));
					}
					int count = parent._items.Count;
					if (count != 0)
					{
						if (count < parent._bufferedCapacity)
						{
							parent._items.EnqueueTail(item);
							return default(ValueTask);
						}
						if (parent._mode == BoundedChannelFullMode.Wait)
						{
							if (!cancellationToken.CanBeCanceled)
							{
								VoidAsyncOperationWithData<T> writerSingleton = _writerSingleton;
								if (writerSingleton.TryOwnAndReset())
								{
									writerSingleton.Item = item;
									parent._blockedWriters.EnqueueTail(writerSingleton);
									return writerSingleton.ValueTask;
								}
							}
							VoidAsyncOperationWithData<T> voidAsyncOperationWithData = new VoidAsyncOperationWithData<T>(runContinuationsAsynchronously: true, cancellationToken);
							voidAsyncOperationWithData.Item = item;
							parent._blockedWriters.EnqueueTail(voidAsyncOperationWithData);
							return voidAsyncOperationWithData.ValueTask;
						}
						if (parent._mode == BoundedChannelFullMode.DropWrite)
						{
							return default(ValueTask);
						}
						T val = ((parent._mode == BoundedChannelFullMode.DropNewest) ? parent._items.DequeueTail() : parent._items.DequeueHead());
						parent._items.EnqueueTail(item);
						return default(ValueTask);
					}
					while (!parent._blockedReaders.IsEmpty)
					{
						AsyncOperation<T> asyncOperation2 = parent._blockedReaders.DequeueHead();
						asyncOperation2.UnregisterCancellation();
						if (!asyncOperation2.IsCompleted)
						{
							asyncOperation = asyncOperation2;
							break;
						}
					}
					if (asyncOperation == null)
					{
						parent._items.EnqueueTail(item);
						listTail = parent._waitingReadersTail;
						if (listTail == null)
						{
							return default(ValueTask);
						}
						parent._waitingReadersTail = null;
					}
				}
				if (asyncOperation != null)
				{
					bool flag = asyncOperation.TrySetResult(item);
				}
				else
				{
					ChannelUtilities.WakeUpWaiters(ref listTail, result: true);
				}
				return default(ValueTask);
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		private readonly BoundedChannelFullMode _mode;

		private readonly TaskCompletionSource<VoidResult> _completion;

		private readonly int _bufferedCapacity;

		private readonly Dequeue<T> _items = new Dequeue<T>();

		private readonly Dequeue<AsyncOperation<T>> _blockedReaders = new Dequeue<AsyncOperation<T>>();

		private readonly Dequeue<VoidAsyncOperationWithData<T>> _blockedWriters = new Dequeue<VoidAsyncOperationWithData<T>>();

		private AsyncOperation<bool> _waitingReadersTail;

		private AsyncOperation<bool> _waitingWritersTail;

		private readonly bool _runContinuationsAsynchronously;

		private Exception _doneWriting;

		private object SyncObj => _items;

		private int ItemsCountForDebugger => _items.Count;

		private bool ChannelIsClosedForDebugger => _doneWriting != null;

		internal BoundedChannel(int bufferedCapacity, BoundedChannelFullMode mode, bool runContinuationsAsynchronously)
		{
			_bufferedCapacity = bufferedCapacity;
			_mode = mode;
			_runContinuationsAsynchronously = runContinuationsAsynchronously;
			_completion = new TaskCompletionSource<VoidResult>(runContinuationsAsynchronously ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
			base.Reader = new BoundedChannelReader(this);
			base.Writer = new BoundedChannelWriter(this);
		}

		[Conditional("DEBUG")]
		private void AssertInvariants()
		{
			_ = _items.IsEmpty;
			_ = _items.Count;
			_ = _bufferedCapacity;
			_ = _blockedReaders.IsEmpty;
			_ = _blockedWriters.IsEmpty;
			_ = _completion.Task.IsCompleted;
		}

		IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}
	public enum BoundedChannelFullMode
	{
		Wait,
		DropNewest,
		DropOldest,
		DropWrite
	}
	public static class Channel
	{
		public static Channel<T> CreateUnbounded<T>()
		{
			return new UnboundedChannel<T>(runContinuationsAsynchronously: true);
		}

		public static Channel<T> CreateUnbounded<T>(UnboundedChannelOptions options)
		{
			if (options != null)
			{
				if (!options.SingleReader)
				{
					return new UnboundedChannel<T>(!options.AllowSynchronousContinuations);
				}
				return new SingleConsumerUnboundedChannel<T>(!options.AllowSynchronousContinuations);
			}
			throw new ArgumentNullException("options");
		}

		public static Channel<T> CreateBounded<T>(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			return new BoundedChannel<T>(capacity, BoundedChannelFullMode.Wait, runContinuationsAsynchronously: true);
		}

		public static Channel<T> CreateBounded<T>(BoundedChannelOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return new BoundedChannel<T>(options.Capacity, options.FullMode, !options.AllowSynchronousContinuations);
		}
	}
	public class ChannelClosedException : InvalidOperationException
	{
		public ChannelClosedException()
			: base(System.SR.ChannelClosedException_DefaultMessage)
		{
		}

		public ChannelClosedException(string message)
			: base(message)
		{
		}

		public ChannelClosedException(Exception innerException)
			: base(System.SR.ChannelClosedException_DefaultMessage, innerException)
		{
		}

		public ChannelClosedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
	public abstract class ChannelOptions
	{
		public bool SingleWriter { get; set; }

		public bool SingleReader { get; set; }

		public bool AllowSynchronousContinuations { get; set; }
	}
	public sealed class BoundedChannelOptions : ChannelOptions
	{
		private int _capacity;

		private BoundedChannelFullMode _mode;

		public int Capacity
		{
			get
			{
				return _capacity;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_capacity = value;
			}
		}

		public BoundedChannelFullMode FullMode
		{
			get
			{
				return _mode;
			}
			set
			{
				if ((uint)value <= 3u)
				{
					_mode = value;
					return;
				}
				throw new ArgumentOutOfRangeException("value");
			}
		}

		public BoundedChannelOptions(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			Capacity = capacity;
		}
	}
	public sealed class UnboundedChannelOptions : ChannelOptions
	{
	}
	public abstract class ChannelReader<T>
	{
		public virtual Task Completion => ChannelUtilities.s_neverCompletingTask;

		public abstract bool TryRead(out T item);

		public abstract ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default(CancellationToken));

		public virtual ValueTask<T> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<T>(Task.FromCanceled<T>(cancellationToken));
			}
			try
			{
				if (TryRead(out var item))
				{
					return new ValueTask<T>(item);
				}
			}
			catch (Exception ex) when (!(ex is ChannelClosedException) && !(ex is OperationCanceledException))
			{
				return new ValueTask<T>(Task.FromException<T>(ex));
			}
			return ReadAsyncCore(cancellationToken);
			async ValueTask<T> ReadAsyncCore(CancellationToken ct)
			{
				T item2;
				do
				{
					if (!(await WaitToReadAsync(ct).ConfigureAwait(continueOnCapturedContext: false)))
					{
						throw new ChannelClosedException();
					}
				}
				while (!TryRead(out item2));
				return item2;
			}
		}
	}
	internal static class ChannelUtilities
	{
		internal static readonly Exception s_doneWritingSentinel = new Exception("s_doneWritingSentinel");

		internal static readonly Task<bool> s_trueTask = Task.FromResult(result: true);

		internal static readonly Task<bool> s_falseTask = Task.FromResult(result: false);

		internal static readonly Task s_neverCompletingTask = new TaskCompletionSource<bool>().Task;

		internal static void Complete(TaskCompletionSource<VoidResult> tcs, Exception error = null)
		{
			if (error is OperationCanceledException ex)
			{
				tcs.TrySetCanceled(ex.CancellationToken);
			}
			else if (error != null && error != s_doneWritingSentinel)
			{
				tcs.TrySetException(error);
			}
			else
			{
				tcs.TrySetResult(default(VoidResult));
			}
		}

		internal static ValueTask<T> GetInvalidCompletionValueTask<T>(Exception error)
		{
			Task<T> task = ((error == s_doneWritingSentinel) ? Task.FromException<T>(CreateInvalidCompletionException()) : ((error is OperationCanceledException { CancellationToken: var cancellationToken } ex) ? Task.FromCanceled<T>(cancellationToken.IsCancellationRequested ? ex.CancellationToken : new CancellationToken(canceled: true)) : Task.FromException<T>(CreateInvalidCompletionException(error))));
			return new ValueTask<T>(task);
		}

		internal static ValueTask<bool> QueueWaiter(ref AsyncOperation<bool> tail, AsyncOperation<bool> waiter)
		{
			AsyncOperation<bool> asyncOperation = tail;
			if (asyncOperation == null)
			{
				waiter.Next = waiter;
			}
			else
			{
				waiter.Next = asyncOperation.Next;
				asyncOperation.Next = waiter;
			}
			tail = waiter;
			return waiter.ValueTaskOfT;
		}

		internal static void WakeUpWaiters(ref AsyncOperation<bool> listTail, bool result, Exception error = null)
		{
			AsyncOperation<bool> asyncOperation = listTail;
			if (asyncOperation != null)
			{
				listTail = null;
				AsyncOperation<bool> next = asyncOperation.Next;
				AsyncOperation<bool> asyncOperation2 = next;
				do
				{
					AsyncOperation<bool> next2 = asyncOperation2.Next;
					asyncOperation2.Next = null;
					bool flag = ((error != null) ? asyncOperation2.TrySetException(error) : asyncOperation2.TrySetResult(result));
					asyncOperation2 = next2;
				}
				while (asyncOperation2 != next);
			}
		}

		internal static void FailOperations<T, TInner>(Dequeue<T> operations, Exception error) where T : AsyncOperation<TInner>
		{
			while (!operations.IsEmpty)
			{
				operations.DequeueHead().TrySetException(error);
			}
		}

		internal static Exception CreateInvalidCompletionException(Exception inner = null)
		{
			if (!(inner is OperationCanceledException))
			{
				if (inner == null || inner == s_doneWritingSentinel)
				{
					return new ChannelClosedException();
				}
				return new ChannelClosedException(inner);
			}
			return inner;
		}
	}
	public abstract class ChannelWriter<T>
	{
		public virtual bool TryComplete(Exception error = null)
		{
			return false;
		}

		public abstract bool TryWrite(T item);

		public abstract ValueTask<bool> WaitToWriteAsync(CancellationToken cancellationToken = default(CancellationToken));

		public virtual ValueTask WriteAsync(T item, CancellationToken cancellationToken = default(CancellationToken))
		{
			try
			{
				return cancellationToken.IsCancellationRequested ? new ValueTask(Task.FromCanceled<T>(cancellationToken)) : (TryWrite(item) ? default(ValueTask) : new ValueTask(WriteAsyncCore(item, cancellationToken)));
			}
			catch (Exception exception)
			{
				return new ValueTask(Task.FromException(exception));
			}
		}

		private async Task WriteAsyncCore(T innerItem, CancellationToken ct)
		{
			while (await WaitToWriteAsync(ct).ConfigureAwait(continueOnCapturedContext: false))
			{
				if (TryWrite(innerItem))
				{
					return;
				}
			}
			throw ChannelUtilities.CreateInvalidCompletionException();
		}

		public void Complete(Exception error = null)
		{
			if (!TryComplete(error))
			{
				throw ChannelUtilities.CreateInvalidCompletionException();
			}
		}
	}
	public abstract class Channel<T> : Channel<T, T>
	{
	}
	public abstract class Channel<TWrite, TRead>
	{
		public ChannelReader<TRead> Reader { get; protected set; }

		public ChannelWriter<TWrite> Writer { get; protected set; }

		public static implicit operator ChannelReader<TRead>(Channel<TWrite, TRead> channel)
		{
			return channel.Reader;
		}

		public static implicit operator ChannelWriter<TWrite>(Channel<TWrite, TRead> channel)
		{
			return channel.Writer;
		}
	}
	internal interface IDebugEnumerable<T>
	{
		IEnumerator<T> GetEnumerator();
	}
	internal sealed class DebugEnumeratorDebugView<T>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items { get; }

		public DebugEnumeratorDebugView(IDebugEnumerable<T> enumerable)
		{
			List<T> list = new List<T>();
			foreach (T item in enumerable)
			{
				list.Add(item);
			}
			Items = list.ToArray();
		}
	}
	internal abstract class AsyncOperation
	{
		protected static readonly Action<object> s_availableSentinel = delegate
		{
		};

		protected static readonly Action<object> s_completedSentinel = delegate
		{
		};

		protected static void ThrowIncompleteOperationException()
		{
			throw new InvalidOperationException(System.SR.InvalidOperation_IncompleteAsyncOperation);
		}

		protected static void ThrowMultipleContinuations()
		{
			throw new InvalidOperationException(System.SR.InvalidOperation_MultipleContinuations);
		}

		protected static void ThrowIncorrectCurrentIdException()
		{
			throw new InvalidOperationException(System.SR.InvalidOperation_IncorrectToken);
		}
	}
	internal class AsyncOperation<TResult> : AsyncOperation, IValueTaskSource, IValueTaskSource<TResult>
	{
		private readonly CancellationTokenRegistration _registration;

		private readonly bool _pooled;

		private readonly bool _runContinuationsAsynchronously;

		private volatile int _completionReserved;

		private TResult _result;

		private ExceptionDispatchInfo _error;

		private Action<object> _continuation;

		private object _continuationState;

		private object _schedulingContext;

		private ExecutionContext _executionContext;

		private short _currentId;

		public AsyncOperation<TResult> Next { get; set; }

		public CancellationToken CancellationToken { get; }

		public ValueTask ValueTask => new ValueTask(this, _currentId);

		public ValueTask<TResult> ValueTaskOfT => new ValueTask<TResult>(this, _currentId);

		internal bool IsCompleted => (object)_continuation == AsyncOperation.s_completedSentinel;

		public AsyncOperation(bool runContinuationsAsynchronously, CancellationToken cancellationToken = default(CancellationToken), bool pooled = false)
		{
			_continuation = (pooled ? AsyncOperation.s_availableSentinel : null);
			_pooled = pooled;
			_runContinuationsAsynchronously = runContinuationsAsynchronously;
			if (cancellationToken.CanBeCanceled)
			{
				CancellationToken = cancellationToken;
				_registration = cancellationToken.Register(delegate(object s)
				{
					AsyncOperation<TResult> asyncOperation = (AsyncOperation<TResult>)s;
					asyncOperation.TrySetCanceled(asyncOperation.CancellationToken);
				}, this);
			}
		}

		public ValueTaskSourceStatus GetStatus(short token)
		{
			if (_currentId == token)
			{
				if (IsCompleted)
				{
					if (_error != null)
					{
						if (!(_error.SourceException is OperationCanceledException))
						{
							return ValueTaskSourceStatus.Faulted;
						}
						return ValueTaskSourceStatus.Canceled;
					}
					return ValueTaskSourceStatus.Succeeded;
				}
				return ValueTaskSourceStatus.Pending;
			}
			AsyncOperation.ThrowIncorrectCurrentIdException();
			return ValueTaskSourceStatus.Pending;
		}

		public TResult GetResult(short token)
		{
			if (_currentId != token)
			{
				AsyncOperation.ThrowIncorrectCurrentIdException();
			}
			if (!IsCompleted)
			{
				AsyncOperation.ThrowIncompleteOperationException();
			}
			ExceptionDispatchInfo error = _error;
			TResult result = _result;
			_currentId++;
			if (_pooled)
			{
				Volatile.Write(ref _continuation, AsyncOperation.s_availableSentinel);
			}
			error?.Throw();
			return result;
		}

		void IValueTaskSource.GetResult(short token)
		{
			if (_currentId != token)
			{
				AsyncOperation.ThrowIncorrectCurrentIdException();
			}
			if (!IsCompleted)
			{
				AsyncOperation.ThrowIncompleteOperationException();
			}
			ExceptionDispatchInfo error = _error;
			_currentId++;
			if (_pooled)
			{
				Volatile.Write(ref _continuation, AsyncOperation.s_availableSentinel);
			}
			error?.Throw();
		}

		public bool TryOwnAndReset()
		{
			if ((object)Interlocked.CompareExchange(ref _continuation, null, AsyncOperation.s_availableSentinel) == AsyncOperation.s_availableSentinel)
			{
				_continuationState = null;
				_result = default(TResult);
				_error = null;
				_schedulingContext = null;
				_executionContext = null;
				return true;
			}
			return false;
		}

		public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (_currentId != token)
			{
				AsyncOperation.ThrowIncorrectCurrentIdException();
			}
			if (_continuationState != null)
			{
				AsyncOperation.ThrowMultipleContinuations();
			}
			_continuationState = state;
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				_executionContext = ExecutionContext.Capture();
			}
			SynchronizationContext synchronizationContext = null;
			TaskScheduler taskScheduler = null;
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					_schedulingContext = synchronizationContext;
				}
				else
				{
					taskScheduler = TaskScheduler.Current;
					if (taskScheduler != TaskScheduler.Default)
					{
						_schedulingContext = taskScheduler;
					}
				}
			}
			Action<object> action = Interlocked.CompareExchange(ref _continuation, continuation, null);
			if (action == null)
			{
				return;
			}
			if ((object)action != AsyncOperation.s_completedSentinel)
			{
				AsyncOperation.ThrowMultipleContinuations();
			}
			if (synchronizationContext != null)
			{
				synchronizationContext.Post(delegate(object s)
				{
					Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
					tuple.Item1(tuple.Item2);
				}, Tuple.Create(continuation, state));
			}
			else
			{
				Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler ?? TaskScheduler.Default);
			}
		}

		public void UnregisterCancellation()
		{
			CancellationTokenRegistration registration = _registration;
			registration.Dispose();
		}

		public bool TrySetResult(TResult item)
		{
			UnregisterCancellation();
			if (TryReserveCompletionIfCancelable())
			{
				_result = item;
				SignalCompletion();
				return true;
			}
			return false;
		}

		public bool TrySetException(Exception exception)
		{
			UnregisterCancellation();
			if (TryReserveCompletionIfCancelable())
			{
				_error = ExceptionDispatchInfo.Capture(exception);
				SignalCompletion();
				return true;
			}
			return false;
		}

		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (TryReserveCompletionIfCancelable())
			{
				_error = ExceptionDispatchInfo.Capture(new OperationCanceledException(cancellationToken));
				SignalCompletion();
				return true;
			}
			return false;
		}

		private bool TryReserveCompletionIfCancelable()
		{
			if (CancellationToken.CanBeCanceled)
			{
				return Interlocked.CompareExchange(ref _completionReserved, 1, 0) == 0;
			}
			return true;
		}

		private void SignalCompletion()
		{
			if (_continuation == null && Interlocked.CompareExchange(ref _continuation, AsyncOperation.s_completedSentinel, null) == null)
			{
				return;
			}
			ExecutionContext executionContext = _executionContext;
			if (executionContext != null)
			{
				ExecutionContext.Run(executionContext, delegate(object s)
				{
					((AsyncOperation<TResult>)s).SignalCompletionCore();
				}, this);
			}
			else
			{
				SignalCompletionCore();
			}
		}

		private void SignalCompletionCore()
		{
			if (_schedulingContext == null)
			{
				if (_runContinuationsAsynchronously)
				{
					Task.Factory.StartNew(delegate(object s)
					{
						((AsyncOperation<TResult>)s).SetCompletionAndInvokeContinuation();
					}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
					return;
				}
			}
			else if (_schedulingContext is SynchronizationContext synchronizationContext)
			{
				if (_runContinuationsAsynchronously || synchronizationContext != SynchronizationContext.Current)
				{
					synchronizationContext.Post(delegate(object s)
					{
						((AsyncOperation<TResult>)s).SetCompletionAndInvokeContinuation();
					}, this);
					return;
				}
			}
			else
			{
				TaskScheduler taskScheduler = (TaskScheduler)_schedulingContext;
				if (_runContinuationsAsynchronously || taskScheduler != TaskScheduler.Current)
				{
					Task.Factory.StartNew(delegate(object s)
					{
						((AsyncOperation<TResult>)s).SetCompletionAndInvokeContinuation();
					}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
					return;
				}
			}
			SetCompletionAndInvokeContinuation();
		}

		private void SetCompletionAndInvokeContinuation()
		{
			Action<object> continuation = _continuation;
			_continuation = AsyncOperation.s_completedSentinel;
			continuation(_continuationState);
		}
	}
	internal sealed class VoidAsyncOperationWithData<TData> : AsyncOperation<VoidResult>
	{
		public TData Item { get; set; }

		public VoidAsyncOperationWithData(bool runContinuationsAsynchronously, CancellationToken cancellationToken = default(CancellationToken), bool pooled = false)
			: base(runContinuationsAsynchronously, cancellationToken, pooled)
		{
		}
	}
	[DebuggerDisplay("Items={ItemsCountForDebugger}, Closed={ChannelIsClosedForDebugger}")]
	[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
	internal sealed class SingleConsumerUnboundedChannel<T> : Channel<T>, IDebugEnumerable<T>
	{
		[DebuggerDisplay("Items={ItemsCountForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class UnboundedChannelReader : ChannelReader<T>, IDebugEnumerable<T>
		{
			internal readonly SingleConsumerUnboundedChannel<T> _parent;

			private readonly AsyncOperation<T> _readerSingleton;

			private readonly AsyncOperation<bool> _waiterSingleton;

			public override Task Completion => _parent._completion.Task;

			private int ItemsCountForDebugger => _parent._items.Count;

			internal UnboundedChannelReader(SingleConsumerUnboundedChannel<T> parent)
			{
				_parent = parent;
				_readerSingleton = new AsyncOperation<T>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
				_waiterSingleton = new AsyncOperation<bool>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
			}

			public override ValueTask<T> ReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<T>(Task.FromCanceled<T>(cancellationToken));
				}
				if (TryRead(out var item))
				{
					return new ValueTask<T>(item);
				}
				SingleConsumerUnboundedChannel<T> parent = _parent;
				AsyncOperation<T> asyncOperation;
				AsyncOperation<T> asyncOperation2;
				lock (parent.SyncObj)
				{
					if (TryRead(out item))
					{
						return new ValueTask<T>(item);
					}
					if (parent._doneWriting != null)
					{
						return ChannelUtilities.GetInvalidCompletionValueTask<T>(parent._doneWriting);
					}
					asyncOperation = parent._blockedReader;
					if (!cancellationToken.CanBeCanceled && _readerSingleton.TryOwnAndReset())
					{
						asyncOperation2 = _readerSingleton;
						if (asyncOperation2 == asyncOperation)
						{
							asyncOperation = null;
						}
					}
					else
					{
						asyncOperation2 = new AsyncOperation<T>(_parent._runContinuationsAsynchronously, cancellationToken);
					}
					parent._blockedReader = asyncOperation2;
				}
				asyncOperation?.TrySetCanceled();
				return asyncOperation2.ValueTaskOfT;
			}

			public override bool TryRead(out T item)
			{
				SingleConsumerUnboundedChannel<T> parent = _parent;
				if (parent._items.TryDequeue(out item))
				{
					if (parent._doneWriting != null && parent._items.IsEmpty)
					{
						ChannelUtilities.Complete(parent._completion, parent._doneWriting);
					}
					return true;
				}
				return false;
			}

			public override ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
				}
				if (!_parent._items.IsEmpty)
				{
					return new ValueTask<bool>(result: true);
				}
				SingleConsumerUnboundedChannel<T> parent = _parent;
				AsyncOperation<bool> asyncOperation = null;
				AsyncOperation<bool> asyncOperation2;
				lock (parent.SyncObj)
				{
					if (!parent._items.IsEmpty)
					{
						return new ValueTask<bool>(result: true);
					}
					if (parent._doneWriting != null)
					{
						return (parent._doneWriting != ChannelUtilities.s_doneWritingSentinel) ? new ValueTask<bool>(Task.FromException<bool>(parent._doneWriting)) : default(ValueTask<bool>);
					}
					asyncOperation = parent._waitingReader;
					if (!cancellationToken.CanBeCanceled && _waiterSingleton.TryOwnAndReset())
					{
						asyncOperation2 = _waiterSingleton;
						if (asyncOperation2 == asyncOperation)
						{
							asyncOperation = null;
						}
					}
					else
					{
						asyncOperation2 = new AsyncOperation<bool>(_parent._runContinuationsAsynchronously, cancellationToken);
					}
					parent._waitingReader = asyncOperation2;
				}
				asyncOperation?.TrySetCanceled();
				return asyncOperation2.ValueTaskOfT;
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		[DebuggerDisplay("Items={ItemsCountForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class UnboundedChannelWriter : ChannelWriter<T>, IDebugEnumerable<T>
		{
			internal readonly SingleConsumerUnboundedChannel<T> _parent;

			private int ItemsCountForDebugger => _parent._items.Count;

			internal UnboundedChannelWriter(SingleConsumerUnboundedChannel<T> parent)
			{
				_parent = parent;
			}

			public override bool TryComplete(Exception error)
			{
				AsyncOperation<T> asyncOperation = null;
				AsyncOperation<bool> asyncOperation2 = null;
				bool flag = false;
				SingleConsumerUnboundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return false;
					}
					parent._doneWriting = error ?? ChannelUtilities.s_doneWritingSentinel;
					if (parent._items.IsEmpty)
					{
						flag = true;
						if (parent._blockedReader != null)
						{
							asyncOperation = parent._blockedReader;
							parent._blockedReader = null;
						}
						if (parent._waitingReader != null)
						{
							asyncOperation2 = parent._waitingReader;
							parent._waitingReader = null;
						}
					}
				}
				if (flag)
				{
					ChannelUtilities.Complete(parent._completion, error);
				}
				if (asyncOperation != null)
				{
					error = ChannelUtilities.CreateInvalidCompletionException(error);
					asyncOperation.TrySetException(error);
				}
				if (asyncOperation2 != null)
				{
					if (error != null)
					{
						asyncOperation2.TrySetException(error);
					}
					else
					{
						asyncOperation2.TrySetResult(item: false);
					}
				}
				return true;
			}

			public override bool TryWrite(T item)
			{
				SingleConsumerUnboundedChannel<T> parent = _parent;
				AsyncOperation<T> asyncOperation;
				do
				{
					asyncOperation = null;
					AsyncOperation<bool> asyncOperation2 = null;
					lock (parent.SyncObj)
					{
						if (parent._doneWriting != null)
						{
							return false;
						}
						asyncOperation = parent._blockedReader;
						if (asyncOperation != null)
						{
							parent._blockedReader = null;
						}
						else
						{
							parent._items.Enqueue(item);
							asyncOperation2 = parent._waitingReader;
							if (asyncOperation2 == null)
							{
								return true;
							}
							parent._waitingReader = null;
						}
					}
					if (asyncOperation2 != null)
					{
						asyncOperation2.TrySetResult(item: true);
						return true;
					}
				}
				while (!asyncOperation.TrySetResult(item));
				return true;
			}

			public override ValueTask<bool> WaitToWriteAsync(CancellationToken cancellationToken)
			{
				Exception doneWriting = _parent._doneWriting;
				if (!cancellationToken.IsCancellationRequested)
				{
					if (doneWriting != null)
					{
						if (doneWriting == ChannelUtilities.s_doneWritingSentinel)
						{
							return default(ValueTask<bool>);
						}
						return new ValueTask<bool>(Task.FromException<bool>(doneWriting));
					}
					return new ValueTask<bool>(result: true);
				}
				return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
			}

			public override ValueTask WriteAsync(T item, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					if (!TryWrite(item))
					{
						return new ValueTask(Task.FromException(ChannelUtilities.CreateInvalidCompletionException(_parent._doneWriting)));
					}
					return default(ValueTask);
				}
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		private readonly TaskCompletionSource<VoidResult> _completion;

		private readonly System.Collections.Concurrent.SingleProducerSingleConsumerQueue<T> _items = new System.Collections.Concurrent.SingleProducerSingleConsumerQueue<T>();

		private readonly bool _runContinuationsAsynchronously;

		private volatile Exception _doneWriting;

		private AsyncOperation<T> _blockedReader;

		private AsyncOperation<bool> _waitingReader;

		private object SyncObj => _items;

		private int ItemsCountForDebugger => _items.Count;

		private bool ChannelIsClosedForDebugger => _doneWriting != null;

		internal SingleConsumerUnboundedChannel(bool runContinuationsAsynchronously)
		{
			_runContinuationsAsynchronously = runContinuationsAsynchronously;
			_completion = new TaskCompletionSource<VoidResult>(runContinuationsAsynchronously ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
			base.Reader = new UnboundedChannelReader(this);
			base.Writer = new UnboundedChannelWriter(this);
		}

		IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}
	[DebuggerDisplay("Items={ItemsCountForDebugger}, Closed={ChannelIsClosedForDebugger}")]
	[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
	internal sealed class UnboundedChannel<T> : Channel<T>, IDebugEnumerable<T>
	{
		[DebuggerDisplay("Items={ItemsCountForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class UnboundedChannelReader : ChannelReader<T>, IDebugEnumerable<T>
		{
			internal readonly UnboundedChannel<T> _parent;

			private readonly AsyncOperation<T> _readerSingleton;

			private readonly AsyncOperation<bool> _waiterSingleton;

			public override Task Completion => _parent._completion.Task;

			private int ItemsCountForDebugger => _parent._items.Count;

			internal UnboundedChannelReader(UnboundedChannel<T> parent)
			{
				_parent = parent;
				_readerSingleton = new AsyncOperation<T>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
				_waiterSingleton = new AsyncOperation<bool>(parent._runContinuationsAsynchronously, default(CancellationToken), pooled: true);
			}

			public override ValueTask<T> ReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<T>(Task.FromCanceled<T>(cancellationToken));
				}
				UnboundedChannel<T> parent = _parent;
				if (parent._items.TryDequeue(out var result))
				{
					CompleteIfDone(parent);
					return new ValueTask<T>(result);
				}
				lock (parent.SyncObj)
				{
					if (parent._items.TryDequeue(out result))
					{
						CompleteIfDone(parent);
						return new ValueTask<T>(result);
					}
					if (parent._doneWriting != null)
					{
						return ChannelUtilities.GetInvalidCompletionValueTask<T>(parent._doneWriting);
					}
					if (!cancellationToken.CanBeCanceled)
					{
						AsyncOperation<T> readerSingleton = _readerSingleton;
						if (readerSingleton.TryOwnAndReset())
						{
							parent._blockedReaders.EnqueueTail(readerSingleton);
							return readerSingleton.ValueTaskOfT;
						}
					}
					AsyncOperation<T> asyncOperation = new AsyncOperation<T>(parent._runContinuationsAsynchronously, cancellationToken);
					parent._blockedReaders.EnqueueTail(asyncOperation);
					return asyncOperation.ValueTaskOfT;
				}
			}

			public override bool TryRead(out T item)
			{
				UnboundedChannel<T> parent = _parent;
				if (parent._items.TryDequeue(out item))
				{
					CompleteIfDone(parent);
					return true;
				}
				item = default(T);
				return false;
			}

			private void CompleteIfDone(UnboundedChannel<T> parent)
			{
				if (parent._doneWriting != null && parent._items.IsEmpty)
				{
					ChannelUtilities.Complete(parent._completion, parent._doneWriting);
				}
			}

			public override ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
				}
				if (!_parent._items.IsEmpty)
				{
					return new ValueTask<bool>(result: true);
				}
				UnboundedChannel<T> parent = _parent;
				lock (parent.SyncObj)
				{
					if (!parent._items.IsEmpty)
					{
						return new ValueTask<bool>(result: true);
					}
					if (parent._doneWriting != null)
					{
						return (parent._doneWriting != ChannelUtilities.s_doneWritingSentinel) ? new ValueTask<bool>(Task.FromException<bool>(parent._doneWriting)) : default(ValueTask<bool>);
					}
					if (!cancellationToken.CanBeCanceled)
					{
						AsyncOperation<bool> waiterSingleton = _waiterSingleton;
						if (waiterSingleton.TryOwnAndReset())
						{
							ChannelUtilities.QueueWaiter(ref parent._waitingReadersTail, waiterSingleton);
							return waiterSingleton.ValueTaskOfT;
						}
					}
					AsyncOperation<bool> asyncOperation = new AsyncOperation<bool>(parent._runContinuationsAsynchronously, cancellationToken);
					ChannelUtilities.QueueWaiter(ref parent._waitingReadersTail, asyncOperation);
					return asyncOperation.ValueTaskOfT;
				}
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		[DebuggerDisplay("Items={ItemsCountForDebugger}")]
		[DebuggerTypeProxy(typeof(DebugEnumeratorDebugView<>))]
		private sealed class UnboundedChannelWriter : ChannelWriter<T>, IDebugEnumerable<T>
		{
			internal readonly UnboundedChannel<T> _parent;

			private int ItemsCountForDebugger => _parent._items.Count;

			internal UnboundedChannelWriter(UnboundedChannel<T> parent)
			{
				_parent = parent;
			}

			public override bool TryComplete(Exception error)
			{
				UnboundedChannel<T> parent = _parent;
				bool isEmpty;
				lock (parent.SyncObj)
				{
					if (parent._doneWriting != null)
					{
						return false;
					}
					parent._doneWriting = error ?? ChannelUtilities.s_doneWritingSentinel;
					isEmpty = parent._items.IsEmpty;
				}
				if (isEmpty)
				{
					ChannelUtilities.Complete(parent._completion, error);
				}
				ChannelUtilities.FailOperations<AsyncOperation<T>, T>(parent._blockedReaders, ChannelUtilities.CreateInvalidCompletionException(error));
				ChannelUtilities.WakeUpWaiters(ref parent._waitingReadersTail, result: false, error);
				return true;
			}

			public override bool TryWrite(T item)
			{
				UnboundedChannel<T> parent = _parent;
				AsyncOperation<bool> listTail;
				while (true)
				{
					AsyncOperation<T> asyncOperation = null;
					listTail = null;
					lock (parent.SyncObj)
					{
						if (parent._doneWriting != null)
						{
							return false;
						}
						if (parent._blockedReaders.IsEmpty)
						{
							parent._items.Enqueue(item);
							listTail = parent._waitingReadersTail;
							if (listTail == null)
							{
								return true;
							}
							parent._waitingReadersTail = null;
						}
						else
						{
							asyncOperation = parent._blockedReaders.DequeueHead();
						}
					}
					if (asyncOperation == null)
					{
						break;
					}
					if (asyncOperation.TrySetResult(item))
					{
						return true;
					}
				}
				ChannelUtilities.WakeUpWaiters(ref listTail, result: true);
				return true;
			}

			public override ValueTask<bool> WaitToWriteAsync(CancellationToken cancellationToken)
			{
				Exception doneWriting = _parent._doneWriting;
				if (!cancellationToken.IsCancellationRequested)
				{
					if (doneWriting != null)
					{
						if (doneWriting == ChannelUtilities.s_doneWritingSentinel)
						{
							return default(ValueTask<bool>);
						}
						return new ValueTask<bool>(Task.FromException<bool>(doneWriting));
					}
					return new ValueTask<bool>(result: true);
				}
				return new ValueTask<bool>(Task.FromCanceled<bool>(cancellationToken));
			}

			public override ValueTask WriteAsync(T item, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					if (!TryWrite(item))
					{
						return new ValueTask(Task.FromException(ChannelUtilities.CreateInvalidCompletionException(_parent._doneWriting)));
					}
					return default(ValueTask);
				}
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}

			IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
			{
				return _parent._items.GetEnumerator();
			}
		}

		private readonly TaskCompletionSource<VoidResult> _completion;

		private readonly ConcurrentQueue<T> _items = new ConcurrentQueue<T>();

		private readonly Dequeue<AsyncOperation<T>> _blockedReaders = new Dequeue<AsyncOperation<T>>();

		private readonly bool _runContinuationsAsynchronously;

		private AsyncOperation<bool> _waitingReadersTail;

		private Exception _doneWriting;

		private object SyncObj => _items;

		private int ItemsCountForDebugger => _items.Count;

		private bool ChannelIsClosedForDebugger => _doneWriting != null;

		internal UnboundedChannel(bool runContinuationsAsynchronously)
		{
			_runContinuationsAsynchronously = runContinuationsAsynchronously;
			_completion = new TaskCompletionSource<VoidResult>(runContinuationsAsynchronously ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
			base.Reader = new UnboundedChannelReader(this);
			base.Writer = new UnboundedChannelWriter(this);
		}

		[Conditional("DEBUG")]
		private void AssertInvariants()
		{
			if (!_items.IsEmpty)
			{
				_ = _runContinuationsAsynchronously;
			}
			if (!_blockedReaders.IsEmpty || _waitingReadersTail != null)
			{
				_ = _runContinuationsAsynchronously;
			}
			_ = _completion.Task.IsCompleted;
		}

		IEnumerator<T> IDebugEnumerable<T>.GetEnumerator()
		{
			return _items.GetEnumerator();
		}
	}
}
namespace System.Collections.Concurrent
{
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(System.Collections.Concurrent.SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			internal Segment _next;

			internal readonly T[] _array;

			internal SegmentState _state;

			internal Segment(int size)
			{
				_array = new T[size];
			}
		}

		private struct SegmentState
		{
			internal PaddingFor32 _pad0;

			internal volatile int _first;

			internal int _lastCopy;

			internal PaddingFor32 _pad1;

			internal int _firstCopy;

			internal volatile int _last;

			internal PaddingFor32 _pad2;
		}

		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			private readonly System.Collections.Concurrent.SingleProducerSingleConsumerQueue<T> _queue;

			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items => new List<T>(_queue).ToArray();

			public SingleProducerSingleConsumerQueue_DebugView(System.Collections.Concurrent.SingleProducerSingleConsumerQueue<T> queue)
			{
				_queue = queue;
			}
		}

		private const int InitialSegmentSize = 32;

		private const int MaxSegmentSize = 16777216;

		private volatile Segment _head;

		private volatile Segment _tail;

		public bool IsEmpty
		{
			get
			{
				Segment head = _head;
				if (head._state._first != head._state._lastCopy)
				{
					return false;
				}
				if (head._state._first != head._state._last)
				{
					return false;
				}
				return head._next == null;
			}
		}

		internal int Count
		{
			get
			{
				int num = 0;
				for (Segment segment = _head; segment != null; segment = segment._next)
				{
					int num2 = segment._array.Length;
					int first;
					int last;
					do
					{
						first = segment._state._first;
						last = segment._state._last;
					}
					while (first != segment._state._first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		public SingleProducerSingleConsumerQueue()
		{
			_head = (_tail = new Segment(32));
		}

		public void Enqueue(T item)
		{
			Segment segment = _tail;
			T[] array = segment._array;
			int last = segment._state._last;
			int num = (last + 1) & (array.Length - 1);
			if (num != segment._state._firstCopy)
			{
				array[last] = item;
				segment._state._last = num;
			}
			else
			{
				EnqueueSlow(item, ref segment);
			}
		}

		private void EnqueueSlow(T item, ref Segment segment)
		{
			if (segment._state._firstCopy != segment._state._first)
			{
				segment._state._firstCopy = segment._state._first;
				Enqueue(item);
				return;
			}
			int num = _tail._array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			Segment segment2 = new Segment(num);
			segment2._array[0] = item;
			segment2._state._last = 1;
			segment2._state._lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write(ref _tail._next, segment2);
				_tail = segment2;
			}
		}

		public bool TryDequeue(out T result)
		{
			Segment segment = _head;
			T[] array = segment._array;
			int first = segment._state._first;
			if (first != segment._state._lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				segment._state._first = (first + 1) & (array.Length - 1);
				return true;
			}
			return TryDequeueSlow(ref segment, ref array, out result);
		}

		private bool TryDequeueSlow(ref Segment segment, ref T[] array, out T result)
		{
			if (segment._state._last != segment._state._lastCopy)
			{
				segment._state._lastCopy = segment._state._last;
				return TryDequeue(out result);
			}
			if (segment._next != null && segment._state._first == segment._state._last)
			{
				segment = segment._next;
				array = segment._array;
				_head = segment;
			}
			int first = segment._state._first;
			if (first == segment._state._last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment._state._first = (first + 1) & (segment._array.Length - 1);
			segment._state._lastCopy = segment._state._last;
			return true;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (Segment segment = _head; segment != null; segment = segment._next)
			{
				for (int pt = segment._state._first; pt != segment._state._last; pt = (pt + 1) & (segment._array.Length - 1))
				{
					yield return segment._array[pt];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal static class PaddingHelpers
	{
		internal const int CACHE_LINE_SIZE = 128;
	}
	[StructLayout(LayoutKind.Explicit, Size = 124)]
	internal struct PaddingFor32
	{
	}
}
namespace System.Collections.Generic
{
	[DebuggerDisplay("Count = {_size}")]
	internal sealed class Dequeue<T>
	{
		private T[] _array = Array.Empty<T>();

		private int _head;

		private int _tail;

		private int _size;

		public int Count => _size;

		public bool IsEmpty => _size == 0;

		public void EnqueueTail(T item)
		{
			if (_size == _array.Length)
			{
				Grow();
			}
			_array[_tail] = item;
			if (++_tail == _array.Length)
			{
				_tail = 0;
			}
			_size++;
		}

		public T DequeueHead()
		{
			T result = _array[_head];
			_array[_head] = default(T);
			if (++_head == _array.Length)
			{
				_head = 0;
			}
			_size--;
			return result;
		}

		public T DequeueTail()
		{
			if (--_tail == -1)
			{
				_tail = _array.Length - 1;
			}
			T result = _array[_tail];
			_array[_tail] = default(T);
			_size--;
			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			int pos = _head;
			int count = _size;
			while (count-- > 0)
			{
				yield return _array[pos];
				pos = (pos + 1) % _array.Length;
			}
		}

		private void Grow()
		{
			int num = (int)((long)_array.Length * 2L);
			if (num < _array.Length + 4)
			{
				num = _array.Length + 4;
			}
			T[] array = new T[num];
			if (_head == 0)
			{
				Array.Copy(_array, 0, array, 0, _size);
			}
			else
			{
				Array.Copy(_array, _head, array, 0, _array.Length - _head);
				Array.Copy(_array, 0, array, _array.Length - _head, _tail);
			}
			_array = array;
			_head = 0;
			_tail = _size;
		}
	}
}
