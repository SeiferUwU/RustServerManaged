namespace Facepunch.Models.Leaderboard;

[JsonModel]
public class Add
{
	public string Parent;

	public float Score;

	public string Extra;

	public bool ReplaceIfHigher;

	public bool ReplaceIfLower;

	public Auth Auth;

	public int Version => 2;
}
