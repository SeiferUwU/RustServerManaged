using Rust.UI;
using UnityEngine;

public class UIParty : BaseMonoBehaviour
{
	public GameObjectRef PartyMemberPrefab;

	public GameObjectRef InvitePrefab;

	public int MaxPartyMembersToRender = 6;

	public GameObject HiddenPartyMemberContainer;

	public RustText HiddenPartyMemberCountText;

	public RectTransform PartyMemberContainer;

	public GameObject PartySection;

	public RustText PartyMemberCount;

	public RectTransform InvitesContainer;

	public GameObject InvitesSection;

	public RustText InviteCountLabel;

	public FriendStyleDef Style;

	public UIFriendsList FriendsList;

	public bool RenderParty = true;

	public bool ShouldShowInvites = true;

	public void LeavePartyClicked()
	{
	}
}
