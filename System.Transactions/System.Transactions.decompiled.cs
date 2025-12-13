using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Transactions.Configuration;
using Unity;

[assembly: BestFitMapping(false)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("System.Transactions.dll")]
[assembly: AssemblyDescription("System.Transactions.dll")]
[assembly: AssemblyDefaultAlias("System.Transactions.dll")]
[assembly: AssemblyCompany("Mono development team")]
[assembly: AssemblyProduct("Mono Common Language Infrastructure")]
[assembly: AssemblyCopyright("(c) Various Mono authors")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.6.57.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: ComVisible(false)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: CLSCompliant(true)]
[assembly: AssemblyDelaySign(true)]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyFileVersion("4.6.57.0")]
[assembly: AssemblyVersion("4.0.0.0")]
internal static class Consts
{
	public const string MonoVersion = "5.11.0.0";

	public const string MonoCompany = "Mono development team";

	public const string MonoProduct = "Mono Common Language Infrastructure";

	public const string MonoCopyright = "(c) Various Mono authors";

	public const int MonoCorlibVersion = 1051100001;

	public const string FxVersion = "4.0.0.0";

	public const string FxFileVersion = "4.6.57.0";

	public const string EnvironmentVersion = "4.0.30319.42000";

	public const string VsVersion = "0.0.0.0";

	public const string VsFileVersion = "11.0.0.0";

	private const string PublicKeyToken = "b77a5c561934e089";

	public const string AssemblyI18N = "I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_JScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_2_0 = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystemCore_3_5 = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string WindowsBase_3_0 = "WindowsBase, Version=3.0.0.0, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyWindowsBase = "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_3_5 = "PresentationCore, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_4_0 = "PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationFramework_3_5 = "PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblySystemServiceModel_3_0 = "System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
internal sealed class Locale
{
	private Locale()
	{
	}

	public static string GetText(string msg)
	{
		return msg;
	}

	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
namespace System
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		private string comment;

		public string Comment => comment;

		public MonoTODOAttribute()
		{
		}

		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : System.MonoTODOAttribute
	{
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : System.MonoTODOAttribute
	{
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : System.MonoTODOAttribute
	{
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : System.MonoTODOAttribute
	{
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : System.MonoTODOAttribute
	{
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
namespace System.Transactions
{
	/// <summary>Describes a committable transaction.</summary>
	[Serializable]
	public sealed class CommittableTransaction : Transaction, ISerializable, IDisposable, IAsyncResult
	{
		private TransactionOptions options;

		private AsyncCallback callback;

		private object user_defined_state;

		private IAsyncResult asyncResult;

		/// <summary>Gets the object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</summary>
		/// <returns>The object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</returns>
		object IAsyncResult.AsyncState => user_defined_state;

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		WaitHandle IAsyncResult.AsyncWaitHandle => asyncResult.AsyncWaitHandle;

		/// <summary>Gets an indication of whether the asynchronous commit operation completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the asynchronous commit operation completed synchronously; otherwise, <see langword="false" />. This property always returns <see langword="false" /> even if the operation completed synchronously.</returns>
		bool IAsyncResult.CompletedSynchronously => asyncResult.CompletedSynchronously;

		/// <summary>Gets an indication whether the asynchronous commit operation has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation is complete; otherwise, <see langword="false" />.</returns>
		bool IAsyncResult.IsCompleted => asyncResult.IsCompleted;

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		public CommittableTransaction()
			: this(default(TransactionOptions))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified <paramref name="timeout" /> value.</summary>
		/// <param name="timeout">The maximum amount of time the transaction can exist, before it is aborted.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		public CommittableTransaction(TimeSpan timeout)
		{
			options = default(TransactionOptions);
			options.Timeout = timeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified transaction options.</summary>
		/// <param name="options">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use for the new transaction.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is invalid.</exception>
		public CommittableTransaction(TransactionOptions options)
		{
			this.options = options;
		}

		/// <summary>Begins an attempt to commit the transaction asynchronously.</summary>
		/// <param name="asyncCallback">The <see cref="T:System.AsyncCallback" /> delegate that is invoked when the transaction completes. This parameter can be <see langword="null" />, in which case the application is not notified of the transaction's completion. Instead, the application must use the <see cref="T:System.IAsyncResult" /> interface to check for completion and wait accordingly, or call <see cref="M:System.Transactions.CommittableTransaction.EndCommit(System.IAsyncResult)" /> to wait for completion.</param>
		/// <param name="asyncState">An object, which might contain arbitrary state information, associated with the asynchronous commitment. This object is passed to the callback, and is not interpreted by <see cref="N:System.Transactions" />. A null reference is permitted.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> interface that can be used by the caller to check the status of the asynchronous operation, or to wait for the operation to complete.</returns>
		public IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState)
		{
			callback = asyncCallback;
			user_defined_state = asyncState;
			AsyncCallback asyncCallback2 = null;
			if (asyncCallback != null)
			{
				asyncCallback2 = CommitCallback;
			}
			asyncResult = BeginCommitInternal(asyncCallback2);
			return this;
		}

		/// <summary>Ends an attempt to commit the transaction asynchronously.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object associated with the asynchronous commitment.</param>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> is called and the transaction rolls back for the first time.</exception>
		public void EndCommit(IAsyncResult asyncResult)
		{
			if (asyncResult != this)
			{
				throw new ArgumentException("The IAsyncResult parameter must be the same parameter as returned by BeginCommit.", "asyncResult");
			}
			EndCommitInternal(this.asyncResult);
		}

		private void CommitCallback(IAsyncResult ar)
		{
			if (asyncResult == null && ar.CompletedSynchronously)
			{
				asyncResult = ar;
			}
			callback(this);
		}

		/// <summary>Attempts to commit the transaction.</summary>
		/// <exception cref="T:System.Transactions.TransactionInDoubtException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called on a transaction and the transaction becomes <see cref="F:System.Transactions.TransactionStatus.InDoubt" />.</exception>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called and the transaction rolls back for the first time.</exception>
		public void Commit()
		{
			CommitInternal();
		}

		[System.MonoTODO("Not implemented")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Provides a mechanism for the hosting environment to supply its own default notion of <see cref="P:System.Transactions.Transaction.Current" />.</summary>
	/// <returns>A <see cref="T:System.Transactions.Transaction" /> object.</returns>
	public delegate Transaction HostCurrentTransactionCallback();
	/// <summary>Represents the method that handles the <see cref="E:System.Transactions.Transaction.TransactionCompleted" /> event of a <see cref="T:System.Transactions.Transaction" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Transactions.TransactionEventArgs" /> that contains the event data.</param>
	public delegate void TransactionCompletedEventHandler(object sender, TransactionEventArgs e);
	/// <summary>Represents the method that will handle the <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" /> event of a <see cref="T:System.Transactions.TransactionManager" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Transactions.TransactionEventArgs" /> that contains the transaction from which transaction information can be retrieved.</param>
	public delegate void TransactionStartedEventHandler(object sender, TransactionEventArgs e);
	/// <summary>Controls what kind of dependent transaction to create.</summary>
	public enum DependentCloneOption
	{
		/// <summary>The dependent transaction blocks the commit process of the transaction until the parent transaction times out, or <see cref="M:System.Transactions.DependentTransaction.Complete" /> is called. In this case, additional work can be done on the transaction and new enlistments can be created.</summary>
		BlockCommitUntilComplete,
		/// <summary>The dependent transaction automatically aborts the transaction if Commit is called on the parent transaction before <see cref="M:System.Transactions.DependentTransaction.Complete" /> is called.</summary>
		RollbackIfNotComplete
	}
	/// <summary>Describes a clone of a transaction providing guarantee that the transaction cannot be committed until the application comes to rest regarding work on the transaction. This class cannot be inherited.</summary>
	[Serializable]
	[System.MonoTODO("Not supported yet")]
	public sealed class DependentTransaction : Transaction, ISerializable
	{
		private bool completed;

		internal bool Completed => completed;

		internal DependentTransaction(Transaction parent, DependentCloneOption option)
		{
		}

		/// <summary>Attempts to complete the dependent transaction.</summary>
		/// <exception cref="T:System.Transactions.TransactionException">Any attempt for additional work on the transaction after this method is called. These include invoking methods such as <see cref="Overload:System.Transactions.Transaction.EnlistVolatile" />, <see cref="Overload:System.Transactions.Transaction.EnlistDurable" />, <see cref="M:System.Transactions.Transaction.Clone" />, <see cref="M:System.Transactions.Transaction.DependentClone(System.Transactions.DependentCloneOption)" /> , or any serialization operations on the transaction.</exception>
		[System.MonoTODO]
		public void Complete()
		{
			throw new NotImplementedException();
		}

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			completed = info.GetBoolean("completed");
		}

		internal DependentTransaction()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the final phase of the transaction.</summary>
	public class Enlistment
	{
		internal bool done;

		internal Enlistment()
		{
			done = false;
		}

		/// <summary>Indicates that the transaction participant has completed its work.</summary>
		public void Done()
		{
			done = true;
			InternalOnDone();
		}

		internal virtual void InternalOnDone()
		{
		}
	}
	/// <summary>Determines whether the object should be enlisted during the prepare phase.</summary>
	[Flags]
	public enum EnlistmentOptions
	{
		/// <summary>The object does not require enlistment during the initial phase of the commitment process.</summary>
		None = 0,
		/// <summary>The object must enlist during the initial phase of the commitment process.</summary>
		EnlistDuringPrepareRequired = 1
	}
	/// <summary>Specifies how distributed transactions interact with COM+ transactions.</summary>
	public enum EnterpriseServicesInteropOption
	{
		/// <summary>There is no synchronization between <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" /> and <see cref="P:System.Transactions.Transaction.Current" />.</summary>
		None,
		/// <summary>Search for an existing COM+ context and synchronize with it if one exists.</summary>
		Automatic,
		/// <summary>The <see cref="N:System.EnterpriseServices" /> context (which can be retrieved by calling the static method <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" /> of the <see cref="T:System.EnterpriseServices.ContextUtil" /> class) and the <see cref="N:System.Transactions" /> ambient transaction (which can be retrieved by calling the static method <see cref="P:System.Transactions.Transaction.Current" /> of the <see cref="T:System.Transactions.Transaction" /> class) are always synchronized. This introduces a performance penalty because new <see cref="N:System.EnterpriseServices" /> contexts may need to be created.</summary>
		Full
	}
	/// <summary>Describes a DTC transaction.</summary>
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDtcTransaction
	{
		/// <summary>Aborts a transaction.</summary>
		/// <param name="reason">An optional <see cref="T:System.EnterpriseServices.BOID" /> that indicates why the transaction is being aborted. This parameter can be <see langword="null" />, indicating that no reason for the abort is provided.</param>
		/// <param name="retaining">This value must be <see langword="false" />.</param>
		/// <param name="async">When <paramref name="async" /> is <see langword="true" />, an asynchronous abort is performed and the caller must use <see langword="ITransactionOutcomeEvents" /> to learn about the outcome of the transaction.</param>
		void Abort(IntPtr reason, int retaining, int async);

		/// <summary>Commits a transaction.</summary>
		/// <param name="retaining">This value must be <see langword="false" />.</param>
		/// <param name="commitType">A value taken from the OLE DB enumeration <see langword="XACTTC" />.</param>
		/// <param name="reserved">This value must be zero.</param>
		void Commit(int retaining, int commitType, int reserved);

		/// <summary>Retrieves information about a transaction.</summary>
		/// <param name="transactionInformation">Pointer to the caller-allocated <see cref="T:System.EnterpriseServices.XACTTRANSINFO" /> structure that will receive information about the transaction. This value must not be <see langword="null" />.</param>
		void GetTransactionInfo(IntPtr transactionInformation);
	}
	/// <summary>Describes an interface that a resource manager should implement to provide two phase commit notification callbacks for the transaction manager upon enlisting for participation.</summary>
	public interface IEnlistmentNotification
	{
		/// <summary>Notifies an enlisted object that a transaction is being committed.</summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		void Commit(Enlistment enlistment);

		/// <summary>Notifies an enlisted object that the status of a transaction is in doubt.</summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		void InDoubt(Enlistment enlistment);

		/// <summary>Notifies an enlisted object that a transaction is being prepared for commitment.</summary>
		/// <param name="preparingEnlistment">A <see cref="T:System.Transactions.PreparingEnlistment" /> object used to send a response to the transaction manager.</param>
		void Prepare(PreparingEnlistment preparingEnlistment);

		/// <summary>Notifies an enlisted object that a transaction is being rolled back (aborted).</summary>
		/// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		void Rollback(Enlistment enlistment);
	}
	/// <summary>Describes an object that acts as a commit delegate for a non-distributed transaction internal to a resource manager.</summary>
	public interface IPromotableSinglePhaseNotification : ITransactionPromoter
	{
		/// <summary>Notifies a transaction participant that enlistment has completed successfully.</summary>
		/// <exception cref="T:System.Transactions.TransactionException">An attempt to enlist or serialize a transaction.</exception>
		void Initialize();

		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> object used to send a response to the transaction manager.</param>
		void Rollback(SinglePhaseEnlistment singlePhaseEnlistment);

		/// <summary>Notifies an enlisted object that the transaction is being committed.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface used to send a response to the transaction manager.</param>
		void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment);
	}
	/// <summary>Represents a transaction that is not a root transaction, but can be escalated to be managed by the MSDTC.</summary>
	public interface ISimpleTransactionSuperior : ITransactionPromoter
	{
		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		void Rollback();
	}
	/// <summary>Describes a resource object that supports single phase commit optimization to participate in a transaction.</summary>
	public interface ISinglePhaseNotification : IEnlistmentNotification
	{
		/// <summary>Represents the resource manager's implementation of the callback for the single phase commit optimization.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> used to send a response to the transaction manager.</param>
		void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment);
	}
	/// <summary>Describes a delegated transaction for an existing transaction that can be escalated to be managed by the MSDTC when needed.</summary>
	public interface ITransactionPromoter
	{
		/// <summary>Notifies an enlisted object that an escalation of the delegated transaction has been requested.</summary>
		/// <returns>A transmitter/receiver propagation token that marshals a distributed transaction. For more information, see <see cref="M:System.Transactions.TransactionInterop.GetTransactionFromTransmitterPropagationToken(System.Byte[])" />.</returns>
		byte[] Promote();
	}
	/// <summary>Specifies the isolation level of a transaction.</summary>
	public enum IsolationLevel
	{
		/// <summary>Volatile data can be read but not modified, and no new data can be added during the transaction.</summary>
		Serializable,
		/// <summary>Volatile data can be read but not modified during the transaction. New data can be added during the transaction.</summary>
		RepeatableRead,
		/// <summary>Volatile data cannot be read during the transaction, but can be modified.</summary>
		ReadCommitted,
		/// <summary>Volatile data can be read and modified during the transaction.</summary>
		ReadUncommitted,
		/// <summary>Volatile data can be read. Before a transaction modifies data, it verifies if another transaction has changed the data after it was initially read. If the data has been updated, an error is raised. This allows a transaction to get to the previously committed value of the data.</summary>
		Snapshot,
		/// <summary>The pending changes from more highly isolated transactions cannot be overwritten.</summary>
		Chaos,
		/// <summary>A different isolation level than the one specified is being used, but the level cannot be determined. An exception is thrown if this value is set.</summary>
		Unspecified
	}
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the Prepare phase of the transaction.</summary>
	public class PreparingEnlistment : Enlistment
	{
		private bool prepared;

		private Transaction tx;

		private IEnlistmentNotification enlisted;

		private WaitHandle waitHandle;

		private Exception ex;

		internal bool IsPrepared => prepared;

		internal WaitHandle WaitHandle => waitHandle;

		internal IEnlistmentNotification EnlistmentNotification => enlisted;

		internal Exception Exception
		{
			get
			{
				return ex;
			}
			set
			{
				ex = value;
			}
		}

		internal PreparingEnlistment(Transaction tx, IEnlistmentNotification enlisted)
		{
			this.tx = tx;
			this.enlisted = enlisted;
			waitHandle = new ManualResetEvent(initialState: false);
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		public void ForceRollback()
		{
			ForceRollback(null);
		}

		internal override void InternalOnDone()
		{
			Prepared();
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		/// <param name="e">An explanation of why a rollback is triggered.</param>
		[System.MonoTODO]
		public void ForceRollback(Exception e)
		{
			tx.Rollback(e, enlisted);
			((ManualResetEvent)waitHandle).Set();
		}

		/// <summary>Indicates that the transaction can be committed.</summary>
		[System.MonoTODO]
		public void Prepared()
		{
			prepared = true;
			((ManualResetEvent)waitHandle).Set();
		}

		/// <summary>Gets the recovery information of an enlistment.</summary>
		/// <returns>The recovery information of an enlistment.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to get recovery information inside a volatile enlistment, which does not generate any recovery information.</exception>
		[System.MonoTODO]
		public byte[] RecoveryInformation()
		{
			throw new NotImplementedException();
		}

		internal PreparingEnlistment()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
	/// <summary>Provides a set of callbacks that facilitate communication between a participant enlisted for Single Phase Commit and the transaction manager when the <see cref="M:System.Transactions.ISinglePhaseNotification.SinglePhaseCommit(System.Transactions.SinglePhaseEnlistment)" /> notification is received.</summary>
	public class SinglePhaseEnlistment : Enlistment
	{
		private Transaction tx;

		private object abortingEnlisted;

		internal SinglePhaseEnlistment()
		{
		}

		internal SinglePhaseEnlistment(Transaction tx, object abortingEnlisted)
		{
			this.tx = tx;
			this.abortingEnlisted = abortingEnlisted;
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back.</summary>
		public void Aborted()
		{
			Aborted(null);
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back, and provides an explanation.</summary>
		/// <param name="e">An explanation of why a rollback is initiated.</param>
		public void Aborted(Exception e)
		{
			if (tx != null)
			{
				tx.Rollback(e, abortingEnlisted);
			}
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the SinglePhaseCommit was successful.</summary>
		[System.MonoTODO]
		public void Committed()
		{
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt.</summary>
		[System.MonoTODO("Not implemented")]
		public void InDoubt()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt, and provides an explanation.</summary>
		/// <param name="e">An explanation of why the transaction is in doubt.</param>
		[System.MonoTODO("Not implemented")]
		public void InDoubt(Exception e)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Represents a non-rooted transaction that can be delegated. This class cannot be inherited.</summary>
	[Serializable]
	public sealed class SubordinateTransaction : Transaction
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.SubordinateTransaction" /> class.</summary>
		/// <param name="isoLevel">The isolation level of the transaction</param>
		/// <param name="superior">A <see cref="T:System.Transactions.ISimpleTransactionSuperior" /></param>
		public SubordinateTransaction(IsolationLevel isoLevel, ISimpleTransactionSuperior superior)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Represents a transaction.</summary>
	[Serializable]
	public class Transaction : IDisposable, ISerializable
	{
		private delegate void AsyncCommit();

		[ThreadStatic]
		private static Transaction ambient;

		private IsolationLevel level;

		private TransactionInformation info;

		private ArrayList dependents = new ArrayList();

		private List<IEnlistmentNotification> volatiles;

		private List<ISinglePhaseNotification> durables;

		private IPromotableSinglePhaseNotification pspe;

		private AsyncCommit asyncCommit;

		private bool committing;

		private bool committed;

		private bool aborted;

		private TransactionScope scope;

		private Exception innerException;

		private Guid tag = Guid.NewGuid();

		internal List<IEnlistmentNotification> Volatiles
		{
			get
			{
				if (volatiles == null)
				{
					volatiles = new List<IEnlistmentNotification>();
				}
				return volatiles;
			}
		}

		internal List<ISinglePhaseNotification> Durables
		{
			get
			{
				if (durables == null)
				{
					durables = new List<ISinglePhaseNotification>();
				}
				return durables;
			}
		}

		internal IPromotableSinglePhaseNotification Pspe => pspe;

		/// <summary>Gets or sets the ambient transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that describes the current transaction.</returns>
		public static Transaction Current
		{
			get
			{
				EnsureIncompleteCurrentScope();
				return CurrentInternal;
			}
			set
			{
				EnsureIncompleteCurrentScope();
				CurrentInternal = value;
			}
		}

		internal static Transaction CurrentInternal
		{
			get
			{
				return ambient;
			}
			set
			{
				ambient = value;
			}
		}

		/// <summary>Gets the isolation level of the transaction.</summary>
		/// <returns>One of the <see cref="T:System.Transactions.IsolationLevel" /> values that indicates the isolation level of the transaction.</returns>
		public IsolationLevel IsolationLevel
		{
			get
			{
				EnsureIncompleteCurrentScope();
				return level;
			}
		}

		/// <summary>Retrieves additional information about a transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionInformation" /> that contains additional information about the transaction.</returns>
		public TransactionInformation TransactionInformation
		{
			get
			{
				EnsureIncompleteCurrentScope();
				return info;
			}
		}

		/// <summary>Uniquely identifies the format of the byte[] returned by the Promote method when the transaction is promoted.</summary>
		/// <returns>A guid that uniquely identifies the format of the byte[] returned by the Promote method when the transaction is promoted.</returns>
		public Guid PromoterType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		private bool Aborted
		{
			get
			{
				return aborted;
			}
			set
			{
				aborted = value;
				if (aborted)
				{
					info.Status = TransactionStatus.Aborted;
				}
			}
		}

		internal TransactionScope Scope
		{
			get
			{
				return scope;
			}
			set
			{
				scope = value;
			}
		}

		/// <summary>Indicates that the transaction is completed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt to subscribe this event on a transaction that has been disposed.</exception>
		public event TransactionCompletedEventHandler TransactionCompleted;

		internal Transaction()
		{
			info = new TransactionInformation();
			level = IsolationLevel.Serializable;
		}

		internal Transaction(Transaction other)
		{
			level = other.level;
			info = other.info;
			dependents = other.dependents;
			volatiles = other.Volatiles;
			durables = other.Durables;
			pspe = other.Pspe;
		}

		/// <summary>Gets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize this transaction.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization.</param>
		[System.MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a clone of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that is a copy of the current transaction object.</returns>
		public Transaction Clone()
		{
			return new Transaction(this);
		}

		/// <summary>Releases the resources that are held by the object.</summary>
		public void Dispose()
		{
			if (TransactionInformation.Status == TransactionStatus.Active)
			{
				Rollback();
			}
		}

		/// <summary>Creates a dependent clone of the transaction.</summary>
		/// <param name="cloneOption">A <see cref="T:System.Transactions.DependentCloneOption" /> that controls what kind of dependent transaction to create.</param>
		/// <returns>A <see cref="T:System.Transactions.DependentTransaction" /> that represents the dependent clone.</returns>
		[System.MonoTODO]
		public DependentTransaction DependentClone(DependentCloneOption cloneOption)
		{
			DependentTransaction dependentTransaction = new DependentTransaction(this, cloneOption);
			dependents.Add(dependentTransaction);
			return dependentTransaction;
		}

		/// <summary>Enlists a durable resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		[System.MonoTODO("Only SinglePhase commit supported for durable resource managers.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment EnlistDurable(Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			throw new NotImplementedException("DTC unsupported, only SinglePhase commit supported for durable resource managers.");
		}

		/// <summary>Enlists a durable resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		[System.MonoTODO("Only Local Transaction Manager supported. Cannot have more than 1 durable resource per transaction. Only EnlistmentOptions.None supported yet.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment EnlistDurable(Guid resourceManagerIdentifier, ISinglePhaseNotification singlePhaseNotification, EnlistmentOptions enlistmentOptions)
		{
			EnsureIncompleteCurrentScope();
			if (pspe != null || Durables.Count > 0)
			{
				throw new NotImplementedException("DTC unsupported, multiple durable resource managers aren't supported.");
			}
			if (enlistmentOptions != EnlistmentOptions.None)
			{
				throw new NotImplementedException("EnlistmentOptions other than None aren't supported");
			}
			Durables.Add(singlePhaseNotification);
			return new Enlistment();
		}

		/// <summary>Enlists a resource manager that has an internal transaction using a promotable single phase enlistment (PSPE).</summary>
		/// <param name="promotableSinglePhaseNotification">A <see cref="T:System.Transactions.IPromotableSinglePhaseNotification" /> interface implemented by the participant.</param>
		/// <returns>A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface implementation that describes the enlistment.</returns>
		public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseNotification promotableSinglePhaseNotification)
		{
			EnsureIncompleteCurrentScope();
			if (pspe != null || Durables.Count > 0)
			{
				return false;
			}
			pspe = promotableSinglePhaseNotification;
			pspe.Initialize();
			return true;
		}

		/// <summary>Sets the distributed transaction identifier generated by the non-MSDTC promoter.</summary>
		/// <param name="promotableNotification">A <see cref="T:System.Transactions.IPromotableSinglePhaseNotification" /> interface implemented by the participant.</param>
		/// <param name="distributedTransactionIdentifier">The identifier for the transaction used by the distributed transaction manager.</param>
		public void SetDistributedTransactionIdentifier(IPromotableSinglePhaseNotification promotableNotification, Guid distributedTransactionIdentifier)
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists a resource manager that has an internal transaction using a promotable single phase enlistment (PSPE).</summary>
		/// <param name="promotableSinglePhaseNotification">A <see cref="T:System.Transactions.IPromotableSinglePhaseNotification" /> interface implemented by the participant.</param>
		/// <param name="promoterType">The type of the distributed transaction processor.</param>
		/// <returns>A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface implementation that describes the enlistment.</returns>
		public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Guid promoterType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the  byte[] returned by the Promote method when the transaction is promoted.</summary>
		/// <returns>The  byte[] returned by the Promote method when the transaction is promoted.</returns>
		public byte[] GetPromotedToken()
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists a volatile resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two-phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		[System.MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			return EnlistVolatileInternal(enlistmentNotification, enlistmentOptions);
		}

		/// <summary>Enlists a volatile resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		[System.MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(ISinglePhaseNotification singlePhaseNotification, EnlistmentOptions enlistmentOptions)
		{
			return EnlistVolatileInternal(singlePhaseNotification, enlistmentOptions);
		}

		private Enlistment EnlistVolatileInternal(IEnlistmentNotification notification, EnlistmentOptions options)
		{
			EnsureIncompleteCurrentScope();
			Volatiles.Add(notification);
			return new Enlistment();
		}

		/// <summary>Promotes and enlists a durable resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="promotableNotification">An object that acts as a commit delegate for a non-distributed transaction internal to a resource manager.</param>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		[System.MonoTODO("Only Local Transaction Manager supported. Cannot have more than 1 durable resource per transaction.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment PromoteAndEnlistDurable(Guid manager, IPromotableSinglePhaseNotification promotableNotification, ISinglePhaseNotification notification, EnlistmentOptions options)
		{
			throw new NotImplementedException("DTC unsupported, multiple durable resource managers aren't supported.");
		}

		/// <summary>Determines whether this transaction and the specified object are equal.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this transaction are identical; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as Transaction);
		}

		private bool Equals(Transaction t)
		{
			if ((object)t == this)
			{
				return true;
			}
			if ((object)t == null)
			{
				return false;
			}
			if (level == t.level)
			{
				return info == t.info;
			}
			return false;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.Transaction" /> instances are equivalent.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(Transaction x, Transaction y)
		{
			return x?.Equals(y) ?? ((object)y == null);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.Transaction" /> instances are not equal.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the inequality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(Transaction x, Transaction y)
		{
			return !(x == y);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return (int)((uint)level ^ (uint)info.GetHashCode()) ^ dependents.GetHashCode();
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		public void Rollback()
		{
			Rollback(null);
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		/// <param name="e">An explanation of why a rollback occurred.</param>
		public void Rollback(Exception e)
		{
			EnsureIncompleteCurrentScope();
			Rollback(e, null);
		}

		internal void Rollback(Exception ex, object abortingEnlisted)
		{
			if (aborted)
			{
				FireCompleted();
				return;
			}
			if (info.Status == TransactionStatus.Committed)
			{
				throw new TransactionException("Transaction has already been committed. Cannot accept any new work.");
			}
			innerException = ex;
			SinglePhaseEnlistment singlePhaseEnlistment = new SinglePhaseEnlistment();
			foreach (IEnlistmentNotification @volatile in Volatiles)
			{
				if (@volatile != abortingEnlisted)
				{
					@volatile.Rollback(singlePhaseEnlistment);
				}
			}
			List<ISinglePhaseNotification> list = Durables;
			if (list.Count > 0 && list[0] != abortingEnlisted)
			{
				list[0].Rollback(singlePhaseEnlistment);
			}
			if (pspe != null && pspe != abortingEnlisted)
			{
				pspe.Rollback(singlePhaseEnlistment);
			}
			Aborted = true;
			FireCompleted();
		}

		protected IAsyncResult BeginCommitInternal(AsyncCallback callback)
		{
			if (committed || committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			committing = true;
			asyncCommit = DoCommit;
			return asyncCommit.BeginInvoke(callback, null);
		}

		protected void EndCommitInternal(IAsyncResult ar)
		{
			asyncCommit.EndInvoke(ar);
		}

		internal void CommitInternal()
		{
			if (committed || committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			committing = true;
			try
			{
				DoCommit();
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new TransactionAbortedException("Transaction failed", ex2);
			}
		}

		private void DoCommit()
		{
			if (Scope != null)
			{
				Rollback(null, null);
				CheckAborted();
			}
			List<IEnlistmentNotification> list = Volatiles;
			List<ISinglePhaseNotification> list2 = Durables;
			if (list.Count == 1 && list2.Count == 0 && list[0] is ISinglePhaseNotification single)
			{
				DoSingleCommit(single);
				Complete();
				return;
			}
			if (list.Count > 0)
			{
				DoPreparePhase();
			}
			if (list2.Count > 0)
			{
				DoSingleCommit(list2[0]);
			}
			if (pspe != null)
			{
				DoSingleCommit(pspe);
			}
			if (list.Count > 0)
			{
				DoCommitPhase();
			}
			Complete();
		}

		private void Complete()
		{
			committing = false;
			committed = true;
			if (!aborted)
			{
				info.Status = TransactionStatus.Committed;
			}
			FireCompleted();
		}

		internal void InitScope(TransactionScope scope)
		{
			CheckAborted();
			if (committed)
			{
				throw new InvalidOperationException("Commit has already been called on this transaction.");
			}
			Scope = scope;
		}

		private static void PrepareCallbackWrapper(object state)
		{
			PreparingEnlistment preparingEnlistment = state as PreparingEnlistment;
			try
			{
				preparingEnlistment.EnlistmentNotification.Prepare(preparingEnlistment);
			}
			catch (Exception exception)
			{
				preparingEnlistment.Exception = exception;
				if (!preparingEnlistment.IsPrepared)
				{
					((ManualResetEvent)preparingEnlistment.WaitHandle).Set();
				}
			}
		}

		private void DoPreparePhase()
		{
			foreach (IEnlistmentNotification @volatile in Volatiles)
			{
				PreparingEnlistment preparingEnlistment = new PreparingEnlistment(this, @volatile);
				ThreadPool.QueueUserWorkItem(PrepareCallbackWrapper, preparingEnlistment);
				TimeSpan timeout = ((Scope != null) ? Scope.Timeout : TransactionManager.DefaultTimeout);
				if (!preparingEnlistment.WaitHandle.WaitOne(timeout, exitContext: true))
				{
					Aborted = true;
					throw new TimeoutException("Transaction timedout");
				}
				if (preparingEnlistment.Exception != null)
				{
					innerException = preparingEnlistment.Exception;
					Aborted = true;
					break;
				}
				if (!preparingEnlistment.IsPrepared)
				{
					Aborted = true;
					break;
				}
			}
			CheckAborted();
		}

		private void DoCommitPhase()
		{
			foreach (IEnlistmentNotification @volatile in Volatiles)
			{
				Enlistment enlistment = new Enlistment();
				@volatile.Commit(enlistment);
			}
		}

		private void DoSingleCommit(ISinglePhaseNotification single)
		{
			if (single != null)
			{
				single.SinglePhaseCommit(new SinglePhaseEnlistment(this, single));
				CheckAborted();
			}
		}

		private void DoSingleCommit(IPromotableSinglePhaseNotification single)
		{
			if (single != null)
			{
				single.SinglePhaseCommit(new SinglePhaseEnlistment(this, single));
				CheckAborted();
			}
		}

		private void CheckAborted()
		{
			if (aborted)
			{
				throw new TransactionAbortedException("Transaction has aborted", innerException);
			}
		}

		private void FireCompleted()
		{
			if (this.TransactionCompleted != null)
			{
				this.TransactionCompleted(this, new TransactionEventArgs(this));
			}
		}

		private static void EnsureIncompleteCurrentScope()
		{
			if (CurrentInternal == null || CurrentInternal.Scope == null || !CurrentInternal.Scope.IsComplete)
			{
				return;
			}
			throw new InvalidOperationException("The current TransactionScope is already complete");
		}
	}
	/// <summary>The exception that is thrown when an operation is attempted on a transaction that has already been rolled back, or an attempt is made to commit the transaction and the transaction aborts.</summary>
	[Serializable]
	public class TransactionAbortedException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class.</summary>
		public TransactionAbortedException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		public TransactionAbortedException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		public TransactionAbortedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		protected TransactionAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Provides data for the following transaction events: <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" />, <see cref="E:System.Transactions.Transaction.TransactionCompleted" />.</summary>
	public class TransactionEventArgs : EventArgs
	{
		private Transaction transaction;

		/// <summary>Gets the transaction for which event status is provided.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> for which event status is provided.</returns>
		public Transaction Transaction => transaction;

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionEventArgs" /> class.</summary>
		public TransactionEventArgs()
		{
		}

		internal TransactionEventArgs(Transaction transaction)
			: this()
		{
			this.transaction = transaction;
		}
	}
	/// <summary>The exception that is thrown when you attempt to do work on a transaction that cannot accept new work.</summary>
	[Serializable]
	public class TransactionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class.</summary>
		public TransactionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		public TransactionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		public TransactionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		protected TransactionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>The exception that is thrown when an operation is attempted on a transaction that is in doubt, or an attempt is made to commit the transaction and the transaction becomes InDoubt.</summary>
	[Serializable]
	public class TransactionInDoubtException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class.</summary>
		public TransactionInDoubtException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		public TransactionInDoubtException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		public TransactionInDoubtException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionInDoubtException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		protected TransactionInDoubtException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Provides additional information regarding a transaction.</summary>
	public class TransactionInformation
	{
		private string local_id;

		private Guid dtcId = Guid.Empty;

		private DateTime creation_time;

		private TransactionStatus status;

		/// <summary>Gets the creation time of the transaction.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the creation time of the transaction.</returns>
		public DateTime CreationTime => creation_time;

		/// <summary>Gets a unique identifier of the escalated transaction.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the unique identifier of the escalated transaction.</returns>
		public Guid DistributedIdentifier
		{
			get
			{
				return dtcId;
			}
			internal set
			{
				dtcId = value;
			}
		}

		/// <summary>Gets a unique identifier of the transaction.</summary>
		/// <returns>A unique identifier of the transaction.</returns>
		public string LocalIdentifier => local_id;

		/// <summary>Gets the status of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionStatus" /> that contains the status of the transaction.</returns>
		public TransactionStatus Status
		{
			get
			{
				return status;
			}
			internal set
			{
				status = value;
			}
		}

		internal TransactionInformation()
		{
			status = TransactionStatus.Active;
			creation_time = DateTime.Now.ToUniversalTime();
			local_id = Guid.NewGuid().ToString() + ":1";
		}

		private TransactionInformation(TransactionInformation other)
		{
			local_id = other.local_id;
			dtcId = other.dtcId;
			creation_time = other.creation_time;
			status = other.status;
		}

		internal TransactionInformation Clone(TransactionInformation other)
		{
			return new TransactionInformation(other);
		}
	}
	/// <summary>Facilitates interaction between <see cref="N:System.Transactions" /> and components that were previously written to interact with MSDTC, COM+, or <see cref="N:System.EnterpriseServices" />. This class cannot be inherited.</summary>
	[System.MonoTODO]
	public static class TransactionInterop
	{
		/// <summary>The type of the distributed transaction processor.</summary>
		public static readonly Guid PromoterTypeDtc = new Guid("14229753-FFE1-428D-82B7-DF73045CB8DA");

		/// <summary>Gets an <see cref="T:System.Transactions.IDtcTransaction" /> instance that represents a <see cref="T:System.Transactions.Transaction" />.</summary>
		/// <param name="transaction">A <see cref="T:System.Transactions.Transaction" /> instance to be marshaled.</param>
		/// <returns>An <see cref="T:System.Transactions.IDtcTransaction" /> instance that represents a <see cref="T:System.Transactions.Transaction" />.  The <see cref="T:System.Transactions.IDtcTransaction" /> instance is compatible with the unmanaged form of ITransaction used by MSDTC and with the Managed form of <see cref="T:System.EnterpriseServices.ITransaction" /> used by <see cref="N:System.EnterpriseServices" />.</returns>
		[System.MonoTODO]
		public static IDtcTransaction GetDtcTransaction(Transaction transaction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Transforms a transaction object into an export transaction cookie.</summary>
		/// <param name="transaction">The <see cref="T:System.Transactions.Transaction" /> object to be marshaled.</param>
		/// <param name="whereabouts">An address that describes the location of the destination transaction manager. This permits two transaction managers to communicate with one another and thereby propagate a transaction from one system to the other.</param>
		/// <returns>An export transaction cookie representing the specified <see cref="T:System.Transactions.Transaction" /> object.</returns>
		[System.MonoTODO]
		public static byte[] GetExportCookie(Transaction transaction, byte[] whereabouts)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a <see cref="T:System.Transactions.Transaction" /> from a specified <see cref="T:System.Transactions.IDtcTransaction" />.</summary>
		/// <param name="transactionNative">The <see cref="T:System.Transactions.IDtcTransaction" /> object to be marshaled.</param>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> instance that represents the given <see cref="T:System.Transactions.IDtcTransaction" />.</returns>
		[System.MonoTODO]
		public static Transaction GetTransactionFromDtcTransaction(IDtcTransaction transactionNative)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a <see cref="T:System.Transactions.Transaction" /> from the specified an export cookie.</summary>
		/// <param name="cookie">A marshaled form of the transaction object.</param>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> from the specified export cookie.</returns>
		[System.MonoTODO]
		public static Transaction GetTransactionFromExportCookie(byte[] cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a <see cref="T:System.Transactions.Transaction" /> instance from the specified transmitter propagation token.</summary>
		/// <param name="propagationToken">A propagation token representing a transaction.</param>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> from the specified transmitter propagation token.</returns>
		/// <exception cref="T:System.Transactions.TransactionManagerCommunicationException">The deserialization of a transaction fails because the transaction manager cannot be contacted. This may be caused by network firewall or security settings.</exception>
		[System.MonoTODO]
		public static Transaction GetTransactionFromTransmitterPropagationToken(byte[] propagationToken)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a propagation token for the specified <see cref="T:System.Transactions.Transaction" />.</summary>
		/// <param name="transaction">A transaction to be marshaled into a propagation token.</param>
		/// <returns>This method, together with the <see cref="M:System.Transactions.TransactionInterop.GetTransactionFromTransmitterPropagationToken(System.Byte[])" /> method, provide functionality for Transmitter/Receiver propagation, in which the transaction is "pulled" from the remote machine when the latter is called to unmarshal the transaction.  
		///  For more information on different propagation models, see <see cref="T:System.Transactions.TransactionInterop" /> class.</returns>
		[System.MonoTODO]
		public static byte[] GetTransmitterPropagationToken(Transaction transaction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the Whereabouts of the distributed transaction manager that <see cref="N:System.Transactions" /> uses.</summary>
		/// <returns>The Whereabouts of the distributed transaction manager that <see cref="N:System.Transactions" /> uses.</returns>
		[System.MonoTODO]
		public static byte[] GetWhereabouts()
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>Contains methods used for transaction management. This class cannot be inherited.</summary>
	public static class TransactionManager
	{
		private static DefaultSettingsSection defaultSettings;

		private static MachineSettingsSection machineSettings;

		private static TimeSpan defaultTimeout;

		private static TimeSpan maxTimeout;

		/// <summary>Gets the default timeout interval for new transactions.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout interval for new transactions.</returns>
		public static TimeSpan DefaultTimeout
		{
			get
			{
				if (defaultSettings != null)
				{
					return defaultSettings.Timeout;
				}
				return defaultTimeout;
			}
		}

		/// <summary>Gets or sets a custom transaction factory.</summary>
		/// <returns>A <see cref="T:System.Transactions.HostCurrentTransactionCallback" /> that contains a custom transaction factory.</returns>
		[System.MonoTODO("Not implemented")]
		public static HostCurrentTransactionCallback HostCurrentCallback
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the default maximum timeout interval for new transactions.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum timeout interval that is allowed when creating new transactions.</returns>
		public static TimeSpan MaximumTimeout
		{
			get
			{
				if (machineSettings != null)
				{
					return machineSettings.MaxTimeout;
				}
				return maxTimeout;
			}
		}

		/// <summary>Indicates that a distributed transaction has started.</summary>
		public static event TransactionStartedEventHandler DistributedTransactionStarted;

		static TransactionManager()
		{
			defaultTimeout = new TimeSpan(0, 1, 0);
			maxTimeout = new TimeSpan(0, 10, 0);
			defaultSettings = ConfigurationManager.GetSection("system.transactions/defaultSettings") as DefaultSettingsSection;
			machineSettings = ConfigurationManager.GetSection("system.transactions/machineSettings") as MachineSettingsSection;
		}

		/// <summary>Notifies the transaction manager that a resource manager recovering from failure has finished reenlisting in all unresolved transactions.</summary>
		/// <param name="resourceManagerIdentifier">A <see cref="T:System.Guid" /> that uniquely identifies the resource to be recovered from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="resourceManagerIdentifier" /> parameter is <see langword="null" />.</exception>
		[System.MonoTODO("Not implemented")]
		public static void RecoveryComplete(Guid resourceManagerIdentifier)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reenlists a durable participant in a transaction.</summary>
		/// <param name="resourceManagerIdentifier">A <see cref="T:System.Guid" /> that uniquely identifies the resource manager.</param>
		/// <param name="recoveryInformation">Contains additional information of recovery information.</param>
		/// <param name="enlistmentNotification">A resource object that implements <see cref="T:System.Transactions.IEnlistmentNotification" /> to receive notifications.</param>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> that describes the enlistment.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="recoveryInformation" /> is invalid.  
		/// -or-  
		/// Transaction Manager information in <paramref name="recoveryInformation" /> does not match the configured transaction manager.  
		/// -or-  
		/// <paramref name="RecoveryInformation" /> is not recognized by <see cref="N:System.Transactions" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Transactions.TransactionManager.RecoveryComplete(System.Guid)" /> has already been called for the specified <paramref name="resourceManagerIdentifier" />. The reenlistment is rejected.</exception>
		/// <exception cref="T:System.Transactions.TransactionException">The <paramref name="resourceManagerIdentifier" /> does not match the content of the specified recovery information in <paramref name="recoveryInformation" />.</exception>
		[System.MonoTODO("Not implemented")]
		public static Enlistment Reenlist(Guid resourceManagerIdentifier, byte[] recoveryInformation, IEnlistmentNotification enlistmentNotification)
		{
			throw new NotImplementedException();
		}
	}
	/// <summary>The exception that is thrown when a resource manager cannot communicate with the transaction manager.</summary>
	[Serializable]
	public class TransactionManagerCommunicationException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class.</summary>
		public TransactionManagerCommunicationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		public TransactionManagerCommunicationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		public TransactionManagerCommunicationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		protected TransactionManagerCommunicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Contains additional information that specifies transaction behaviors.</summary>
	public struct TransactionOptions
	{
		private IsolationLevel level;

		private TimeSpan timeout;

		/// <summary>Gets or sets the isolation level of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.IsolationLevel" /> enumeration that specifies the isolation level of the transaction.</returns>
		public IsolationLevel IsolationLevel
		{
			get
			{
				return level;
			}
			set
			{
				level = value;
			}
		}

		/// <summary>Gets or sets the timeout period for the transaction.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout period for the transaction.</returns>
		public TimeSpan Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
				timeout = value;
			}
		}

		internal TransactionOptions(IsolationLevel level, TimeSpan timeout)
		{
			this.level = level;
			this.timeout = timeout;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.TransactionOptions" /> instances are equivalent.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(TransactionOptions x, TransactionOptions y)
		{
			if (x.level == y.level)
			{
				return x.timeout == y.timeout;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.TransactionOptions" /> instances are not equal.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(TransactionOptions x, TransactionOptions y)
		{
			if (x.level == y.level)
			{
				return x.timeout != y.timeout;
			}
			return true;
		}

		/// <summary>Determines whether this <see cref="T:System.Transactions.TransactionOptions" /> instance and the specified object are equal.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this <see cref="T:System.Transactions.TransactionOptions" /> instance are identical; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is TransactionOptions))
			{
				return false;
			}
			return this == (TransactionOptions)obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return (int)level ^ timeout.GetHashCode();
		}
	}
	/// <summary>The exception that is thrown when a promotion fails.</summary>
	[Serializable]
	public class TransactionPromotionException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class.</summary>
		public TransactionPromotionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		public TransactionPromotionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		public TransactionPromotionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		protected TransactionPromotionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
	public sealed class TransactionScope : IDisposable
	{
		private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

		private Transaction transaction;

		private Transaction oldTransaction;

		private TransactionScope parentScope;

		private TimeSpan timeout;

		private int nested;

		private bool disposed;

		private bool completed;

		private bool isRoot;

		private bool asyncFlowEnabled;

		internal bool IsComplete => completed;

		internal TimeSpan Timeout => timeout;

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class.</summary>
		public TransactionScope()
			: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified asynchronous flow option.</summary>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(TransactionScopeAsyncFlowOption asyncFlowOption)
			: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout, asyncFlowOption)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		public TransactionScope(Transaction transactionToUse)
			: this(transactionToUse, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout)
			: this(transactionToUse, scopeTimeout, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		[System.MonoTODO("EnterpriseServicesInteropOption not supported.")]
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
		{
			Initialize(TransactionScopeOption.Required, transactionToUse, defaultOptions, interopOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		public TransactionScope(TransactionScopeOption scopeOption)
			: this(scopeOption, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
			: this(scopeOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(TransactionScopeOption option, TransactionScopeAsyncFlowOption asyncFlow)
			: this(option, TransactionManager.DefaultTimeout, asyncFlow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, requirements, and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			Initialize(scopeOption, null, defaultOptions, EnterpriseServicesInteropOption.None, scopeTimeout, asyncFlow);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
			: this(scopeOption, transactionOptions, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified scope and COM+ interoperability requirements, and transaction options.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		[System.MonoTODO("EnterpriseServicesInteropOption not supported")]
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
		{
			Initialize(scopeOption, null, transactionOptions, interopOption, TransactionManager.DefaultTimeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(Transaction transactionToUse, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		private void Initialize(TransactionScopeOption scopeOption, Transaction tx, TransactionOptions options, EnterpriseServicesInteropOption interop, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			completed = false;
			isRoot = false;
			nested = 0;
			asyncFlowEnabled = asyncFlow == TransactionScopeAsyncFlowOption.Enabled;
			if (scopeTimeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("scopeTimeout");
			}
			timeout = scopeTimeout;
			oldTransaction = Transaction.CurrentInternal;
			Transaction.CurrentInternal = (transaction = InitTransaction(tx, scopeOption));
			if (transaction != null)
			{
				transaction.InitScope(this);
			}
			if (parentScope != null)
			{
				parentScope.nested++;
			}
		}

		private Transaction InitTransaction(Transaction tx, TransactionScopeOption scopeOption)
		{
			if (tx != null)
			{
				return tx;
			}
			switch (scopeOption)
			{
			case TransactionScopeOption.Suppress:
				if (Transaction.CurrentInternal != null)
				{
					parentScope = Transaction.CurrentInternal.Scope;
				}
				return null;
			case TransactionScopeOption.Required:
				if (Transaction.CurrentInternal == null)
				{
					isRoot = true;
					return new Transaction();
				}
				parentScope = Transaction.CurrentInternal.Scope;
				return Transaction.CurrentInternal;
			default:
				if (Transaction.CurrentInternal != null)
				{
					parentScope = Transaction.CurrentInternal.Scope;
				}
				isRoot = true;
				return new Transaction();
			}
		}

		/// <summary>Indicates that all operations within the scope are completed successfully.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method has already been called once.</exception>
		public void Complete()
		{
			if (completed)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete. You should dispose the TransactionScope.");
			}
			completed = true;
		}

		/// <summary>Ends the transaction scope.</summary>
		public void Dispose()
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (parentScope != null)
			{
				parentScope.nested--;
			}
			if (nested > 0)
			{
				transaction.Rollback();
				throw new InvalidOperationException("TransactionScope nested incorrectly");
			}
			if (Transaction.CurrentInternal != transaction && !asyncFlowEnabled)
			{
				if (transaction != null)
				{
					transaction.Rollback();
				}
				if (Transaction.CurrentInternal != null)
				{
					Transaction.CurrentInternal.Rollback();
				}
				throw new InvalidOperationException("Transaction.Current has changed inside of the TransactionScope");
			}
			if (asyncFlowEnabled)
			{
				if (oldTransaction != null)
				{
					oldTransaction.Scope = parentScope;
				}
				Transaction currentInternal = Transaction.CurrentInternal;
				if (!(transaction == null) || !(currentInternal == null))
				{
					currentInternal.Scope = parentScope;
					Transaction.CurrentInternal = oldTransaction;
					transaction.Scope = null;
					if (!IsComplete)
					{
						transaction.Rollback();
						currentInternal.Rollback();
					}
					else if (isRoot)
					{
						currentInternal.CommitInternal();
						transaction.CommitInternal();
					}
				}
				return;
			}
			if (Transaction.CurrentInternal == oldTransaction && oldTransaction != null)
			{
				oldTransaction.Scope = parentScope;
			}
			Transaction.CurrentInternal = oldTransaction;
			if (!(transaction == null))
			{
				transaction.Scope = null;
				if (!IsComplete)
				{
					transaction.Rollback();
				}
				else if (isRoot)
				{
					transaction.CommitInternal();
				}
			}
		}
	}
	/// <summary>Specifies whether transaction flow across thread continuations is enabled for <see cref="T:System.Transactions.TransactionScope" />.</summary>
	public enum TransactionScopeAsyncFlowOption
	{
		/// <summary>Specifies that transaction flow across thread continuations is suppressed.</summary>
		Suppress,
		/// <summary>Specifies that transaction flow across thread continuations is enabled.</summary>
		Enabled
	}
	/// <summary>Provides additional options for creating a transaction scope.</summary>
	public enum TransactionScopeOption
	{
		/// <summary>A transaction is required by the scope. It uses an ambient transaction if one already exists. Otherwise, it creates a new transaction before entering the scope. This is the default value.</summary>
		Required,
		/// <summary>A new transaction is always created for the scope.</summary>
		RequiresNew,
		/// <summary>The ambient transaction context is suppressed when creating the scope. All operations within the scope are done without an ambient transaction context.</summary>
		Suppress
	}
	/// <summary>Describes the current status of a distributed transaction.</summary>
	public enum TransactionStatus
	{
		/// <summary>The status of the transaction is unknown, because some participants must still be polled.</summary>
		Active,
		/// <summary>The transaction has been committed.</summary>
		Committed,
		/// <summary>The transaction has been rolled back.</summary>
		Aborted,
		/// <summary>The status of the transaction is unknown.</summary>
		InDoubt
	}
}
namespace System.Transactions.Configuration
{
	/// <summary>Represents an XML section in a configuration file that contains default values of a transaction. This class cannot be inherited.</summary>
	public class DefaultSettingsSection : ConfigurationSection
	{
		/// <summary>Gets or sets a default time after which a transaction times out.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object. The default property is 00:01:00.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt to set this property to negative values.</exception>
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("timeout", DefaultValue = "00:01:00")]
		public TimeSpan Timeout
		{
			get
			{
				return (TimeSpan)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}

		/// <summary>Gets the name of the transaction manager.</summary>
		/// <returns>The name of the transaction manager. The default value is an empty string.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt to set this property to fully qualified domain names or IP addresses.</exception>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">An attempt to set this property to localhost.</exception>
		[ConfigurationProperty("distributedTransactionManagerName", DefaultValue = "")]
		public string DistributedTransactionManagerName
		{
			get
			{
				return base["distributedTransactionManagerName"] as string;
			}
			set
			{
				base["distributedTransactionManagerName"] = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> class.</summary>
		public DefaultSettingsSection()
		{
		}
	}
	/// <summary>Represents an XML section in a configuration file encapsulating all settings that can be modified only at the machine level. This class cannot be inherited.</summary>
	public class MachineSettingsSection : ConfigurationSection
	{
		/// <summary>Gets a maximum amount of time allowed before a transaction times out.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the maximum allowable time. The default value is 00:10:00.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt to set this property to negative values.</exception>
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("maxTimeout", DefaultValue = "00:10:00")]
		public TimeSpan MaxTimeout
		{
			get
			{
				return (TimeSpan)base["maxTimeout"];
			}
			set
			{
				base["maxTimeout"] = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> class.</summary>
		public MachineSettingsSection()
		{
		}
	}
	/// <summary>Represents a configuration section that encapsulates and allows traversal of all the transaction configuration XML elements and attributes that are within this configuration section. This class cannot be inherited.</summary>
	public class TransactionsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the default settings used to initialize the elements and attributes in a transactions section.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that represents the default settings. The default is a <see cref="T:System.Transactions.Configuration.DefaultSettingsSection" /> that is populated with default values.</returns>
		[ConfigurationProperty("defaultSettings")]
		public DefaultSettingsSection DefaultSettings => (DefaultSettingsSection)base.Sections["defaultSettings"];

		/// <summary>Gets the configuration settings set at the machine level.</summary>
		/// <returns>A <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that represents the configuration settings at the machine level. The default is a <see cref="T:System.Transactions.Configuration.MachineSettingsSection" /> that is populated with default values.</returns>
		[ConfigurationProperty("machineSettings")]
		public MachineSettingsSection MachineSettings => (MachineSettingsSection)base.Sections["machineSettings"];

		/// <summary>Provides static access to a <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" />.</summary>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> representing the configuration settings that apply to a particular computer, application, or resource.</param>
		/// <returns>A <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" /> object.</returns>
		public static TransactionsSectionGroup GetSectionGroup(System.Configuration.Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.transactions") as TransactionsSectionGroup;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.Configuration.TransactionsSectionGroup" /> class.</summary>
		public TransactionsSectionGroup()
		{
		}
	}
}
namespace System.Transactions
{
	/// <summary>The permission that is demanded by <see cref="N:System.Transactions" /> when management of a transaction is escalated to MSDTC. This class cannot be inherited.</summary>
	[Serializable]
	public sealed class DistributedTransactionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.DistributedTransactionPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		public DistributedTransactionPermission(PermissionState state)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		public override IPermission Copy()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the permission.</param>
		public override void FromXml(SecurityElement securityElement)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		public override IPermission Intersect(IPermission target)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a value that indicates whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.IPermission" /> is a subset of the specified <see cref="T:System.Security.IPermission" />; otherwise, <see langword="false" />.</returns>
		public override bool IsSubsetOf(IPermission target)
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns a value that indicates whether unrestricted access to the resource that is protected by the current permission is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if unrestricted use of the resource protected by the permission is allowed; otherwise, <see langword="false" />.</returns>
		public bool IsUnrestricted()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		public override SecurityElement ToXml()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
	/// <summary>Allows security actions for <see cref="T:System.Transactions.DistributedTransactionPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public sealed class DistributedTransactionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.DistributedTransactionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		public DistributedTransactionPermissionAttribute(SecurityAction action)
		{
		}

		/// <summary>Creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		public override IPermission CreatePermission()
		{
			Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
namespace Unity
{
	internal sealed class ThrowStub : ObjectDisposedException
	{
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
