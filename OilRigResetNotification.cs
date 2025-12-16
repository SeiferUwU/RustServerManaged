using ConVar;
using Network;
using UnityEngine;

public class OilRigResetNotification : BaseEntity
{
	private enum OilRigType
	{
		Undefined,
		Small,
		Large
	}

	[SerializeField]
	private OilRigType oilRigType;

	[SerializeField]
	private Translate.Phrase smallRigResetPhrase;

	[SerializeField]
	private Translate.Phrase largeRigResetPhrase;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("OilRigResetNotification.OnRpcMessage"))
		{
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public void OnPuzzleReset()
	{
		Server_HandlePuzzleReset();
	}

	public void Server_HandlePuzzleReset()
	{
		if (Global.legacymonumentnotifications)
		{
			ClientRPC(RpcTarget.NetworkGroup("Client_DoLegacyMonumentNotification"), (byte)oilRigType);
		}
		else
		{
			ClientRPC(RpcTarget.NetworkGroup("Client_PuzzleResetRPC"), (byte)oilRigType);
		}
	}
}
