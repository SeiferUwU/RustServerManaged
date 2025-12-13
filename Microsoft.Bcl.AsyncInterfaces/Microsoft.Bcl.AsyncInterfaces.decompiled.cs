using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyDefaultAlias("Microsoft.Bcl.AsyncInterfaces")]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: AssemblyMetadata("PreferInbox", "True")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Microsoft.Bcl.AsyncInterfaces")]
[assembly: AssemblyFileVersion("4.700.20.21406")]
[assembly: AssemblyInformationalVersion("3.1.4+c4164928b270ee2369808ab347d33423ef765216")]
[assembly: AssemblyProduct("Microsoft® .NET Core")]
[assembly: AssemblyTitle("Microsoft.Bcl.AsyncInterfaces")]
[assembly: CLSCompliant(true)]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class IsReadOnlyAttribute : Attribute
	{
	}
}
namespace System
{
	public interface IAsyncDisposable
	{
		ValueTask DisposeAsync();
	}
}
namespace System.Collections.Generic
{
	public interface IAsyncEnumerable<out T>
	{
		IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
	public interface IAsyncEnumerator<out T> : IAsyncDisposable
	{
		T Current { get; }

		ValueTask<bool> MoveNextAsync();
	}
}
namespace System.Runtime.CompilerServices
{
	[StructLayout(LayoutKind.Auto)]
	public struct AsyncIteratorMethodBuilder
	{
		private AsyncTaskMethodBuilder _methodBuilder;

		private object _id;

		internal object ObjectIdForDebugger => _id ?? Interlocked.CompareExchange(ref _id, new object(), null) ?? _id;

		public static AsyncIteratorMethodBuilder Create()
		{
			return new AsyncIteratorMethodBuilder
			{
				_methodBuilder = AsyncTaskMethodBuilder.Create()
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveNext<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			_methodBuilder.Start(ref stateMachine);
		}

		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			_methodBuilder.AwaitOnCompleted(ref awaiter, ref stateMachine);
		}

		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			_methodBuilder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
		}

		public void Complete()
		{
			_methodBuilder.SetResult();
		}
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		public AsyncIteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		private readonly IAsyncDisposable _source;

		private readonly bool _continueOnCapturedContext;

		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			_source = source;
			_continueOnCapturedContext = continueOnCapturedContext;
		}

		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return _source.DisposeAsync().ConfigureAwait(_continueOnCapturedContext);
		}
	}
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredCancelableAsyncEnumerable<T>
	{
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			private readonly IAsyncEnumerator<T> _enumerator;

			private readonly bool _continueOnCapturedContext;

			public T Current => _enumerator.Current;

			internal Enumerator(IAsyncEnumerator<T> enumerator, bool continueOnCapturedContext)
			{
				_enumerator = enumerator;
				_continueOnCapturedContext = continueOnCapturedContext;
			}

			public ConfiguredValueTaskAwaitable<bool> MoveNextAsync()
			{
				return _enumerator.MoveNextAsync().ConfigureAwait(_continueOnCapturedContext);
			}

			public ConfiguredValueTaskAwaitable DisposeAsync()
			{
				return _enumerator.DisposeAsync().ConfigureAwait(_continueOnCapturedContext);
			}
		}

		private readonly IAsyncEnumerable<T> _enumerable;

		private readonly CancellationToken _cancellationToken;

		private readonly bool _continueOnCapturedContext;

		internal ConfiguredCancelableAsyncEnumerable(IAsyncEnumerable<T> enumerable, bool continueOnCapturedContext, CancellationToken cancellationToken)
		{
			_enumerable = enumerable;
			_continueOnCapturedContext = continueOnCapturedContext;
			_cancellationToken = cancellationToken;
		}

		public ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(_enumerable, continueOnCapturedContext, _cancellationToken);
		}

		public ConfiguredCancelableAsyncEnumerable<T> WithCancellation(CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(_enumerable, _continueOnCapturedContext, cancellationToken);
		}

		public Enumerator GetAsyncEnumerator()
		{
			return new Enumerator(_enumerable.GetAsyncEnumerator(_cancellationToken), _continueOnCapturedContext);
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class EnumeratorCancellationAttribute : Attribute
	{
	}
}
namespace System.Threading.Tasks
{
	public static class TaskAsyncEnumerableExtensions
	{
		public static ConfiguredAsyncDisposable ConfigureAwait(this IAsyncDisposable source, bool continueOnCapturedContext)
		{
			return new ConfiguredAsyncDisposable(source, continueOnCapturedContext);
		}

		public static ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<T>(this IAsyncEnumerable<T> source, bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext, default(CancellationToken));
		}

		public static ConfiguredCancelableAsyncEnumerable<T> WithCancellation<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext: true, cancellationToken);
		}
	}
}
namespace System.Threading.Tasks.Sources
{
	[StructLayout(LayoutKind.Auto)]
	public struct ManualResetValueTaskSourceCore<TResult>
	{
		private Action<object> _continuation;

		private object _continuationState;

		private ExecutionContext _executionContext;

		private object _capturedContext;

		private bool _completed;

		private TResult _result;

		private ExceptionDispatchInfo _error;

		private short _version;

		public bool RunContinuationsAsynchronously { get; set; }

		public short Version => _version;

		public void Reset()
		{
			_version++;
			_completed = false;
			_result = default(TResult);
			_error = null;
			_executionContext = null;
			_capturedContext = null;
			_continuation = null;
			_continuationState = null;
		}

		public void SetResult(TResult result)
		{
			_result = result;
			SignalCompletion();
		}

		public void SetException(Exception error)
		{
			_error = ExceptionDispatchInfo.Capture(error);
			SignalCompletion();
		}

		public ValueTaskSourceStatus GetStatus(short token)
		{
			ValidateToken(token);
			if (_continuation != null && _completed)
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

		public TResult GetResult(short token)
		{
			ValidateToken(token);
			if (!_completed)
			{
				throw new InvalidOperationException();
			}
			_error?.Throw();
			return _result;
		}

		public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			ValidateToken(token);
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				_executionContext = ExecutionContext.Capture();
			}
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				SynchronizationContext current = SynchronizationContext.Current;
				if (current != null && current.GetType() != typeof(SynchronizationContext))
				{
					_capturedContext = current;
				}
				else
				{
					TaskScheduler current2 = TaskScheduler.Current;
					if (current2 != TaskScheduler.Default)
					{
						_capturedContext = current2;
					}
				}
			}
			object obj = _continuation;
			if (obj == null)
			{
				_continuationState = state;
				obj = Interlocked.CompareExchange(ref _continuation, continuation, null);
			}
			if (obj == null)
			{
				return;
			}
			if (obj != System.Threading.Tasks.Sources.ManualResetValueTaskSourceCoreShared.s_sentinel)
			{
				throw new InvalidOperationException();
			}
			object capturedContext = _capturedContext;
			if (capturedContext != null)
			{
				if (!(capturedContext is SynchronizationContext synchronizationContext))
				{
					if (capturedContext is TaskScheduler scheduler)
					{
						Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, scheduler);
					}
				}
				else
				{
					synchronizationContext.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create(continuation, state));
				}
			}
			else
			{
				Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
		}

		private void ValidateToken(short token)
		{
			if (token != _version)
			{
				throw new InvalidOperationException();
			}
		}

		private void SignalCompletion()
		{
			if (_completed)
			{
				throw new InvalidOperationException();
			}
			_completed = true;
			if (_continuation == null && Interlocked.CompareExchange(ref _continuation, System.Threading.Tasks.Sources.ManualResetValueTaskSourceCoreShared.s_sentinel, null) == null)
			{
				return;
			}
			if (_executionContext != null)
			{
				ExecutionContext.Run(_executionContext, delegate(object s)
				{
					((ManualResetValueTaskSourceCore<TResult>)s).InvokeContinuation();
				}, this);
			}
			else
			{
				InvokeContinuation();
			}
		}

		private void InvokeContinuation()
		{
			object capturedContext = _capturedContext;
			if (capturedContext != null)
			{
				if (!(capturedContext is SynchronizationContext synchronizationContext))
				{
					if (capturedContext is TaskScheduler scheduler)
					{
						Task.Factory.StartNew(_continuation, _continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, scheduler);
					}
				}
				else
				{
					synchronizationContext.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create(_continuation, _continuationState));
				}
			}
			else if (RunContinuationsAsynchronously)
			{
				Task.Factory.StartNew(_continuation, _continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				_continuation(_continuationState);
			}
		}
	}
	internal static class ManualResetValueTaskSourceCoreShared
	{
		internal static readonly Action<object> s_sentinel = CompletionSentinel;

		private static void CompletionSentinel(object _)
		{
			throw new InvalidOperationException();
		}
	}
}
