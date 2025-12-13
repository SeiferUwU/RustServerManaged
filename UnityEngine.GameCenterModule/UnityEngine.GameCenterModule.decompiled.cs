using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	public static class Social
	{
		public static ISocialPlatform Active
		{
			get
			{
				return ActivePlatform.Instance;
			}
			set
			{
				ActivePlatform.Instance = value;
			}
		}

		public static ILocalUser localUser => Active.localUser;

		public static void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			Active.LoadUsers(userIDs, callback);
		}

		public static void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			Active.ReportProgress(achievementID, progress, callback);
		}

		public static void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			Active.LoadAchievementDescriptions(callback);
		}

		public static void LoadAchievements(Action<IAchievement[]> callback)
		{
			Active.LoadAchievements(callback);
		}

		public static void ReportScore(long score, string board, Action<bool> callback)
		{
			Active.ReportScore(score, board, callback);
		}

		public static void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			Active.LoadScores(leaderboardID, callback);
		}

		public static ILeaderboard CreateLeaderboard()
		{
			return Active.CreateLeaderboard();
		}

		public static IAchievement CreateAchievement()
		{
			return Active.CreateAchievement();
		}

		public static void ShowAchievementsUI()
		{
			Active.ShowAchievementsUI();
		}

		public static void ShowLeaderboardUI()
		{
			Active.ShowLeaderboardUI();
		}
	}
}
namespace UnityEngine.SocialPlatforms
{
	public class Local : ISocialPlatform
	{
		private static LocalUser m_LocalUser;

		private List<UserProfile> m_Friends = new List<UserProfile>();

		private List<UserProfile> m_Users = new List<UserProfile>();

		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		private List<Achievement> m_Achievements = new List<Achievement>();

		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		private Texture2D m_DefaultTexture;

		public ILocalUser localUser
		{
			get
			{
				if (m_LocalUser == null)
				{
					m_LocalUser = new LocalUser();
				}
				return m_LocalUser;
			}
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			LocalUser localUser = (LocalUser)user;
			m_DefaultTexture = CreateDummyTexture(32, 32);
			PopulateStaticData();
			localUser.SetAuthenticated(value: true);
			localUser.SetUnderage(value: false);
			localUser.SetUserID("1000");
			localUser.SetUserName("Lerpz");
			localUser.SetImage(m_DefaultTexture);
			callback?.Invoke(obj: true);
		}

		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool, string> callback)
		{
			((ISocialPlatform)this).Authenticate(user, (Action<bool>)delegate(bool success)
			{
				callback(success, null);
			});
		}

		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			if (VerifyUser())
			{
				LocalUser obj = (LocalUser)user;
				IUserProfile[] friends = m_Friends.ToArray();
				obj.SetFriends(friends);
				callback?.Invoke(obj: true);
			}
		}

		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			List<UserProfile> list = new List<UserProfile>();
			if (!VerifyUser())
			{
				return;
			}
			foreach (string text in userIDs)
			{
				foreach (UserProfile user in m_Users)
				{
					if (user.id == text)
					{
						list.Add(user);
					}
				}
				foreach (UserProfile friend in m_Friends)
				{
					if (friend.id == text)
					{
						list.Add(friend);
					}
				}
			}
			IUserProfile[] obj = list.ToArray();
			callback(obj);
		}

		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			if (!VerifyUser())
			{
				return;
			}
			foreach (Achievement achievement in m_Achievements)
			{
				if (achievement.id == id && achievement.percentCompleted <= progress)
				{
					if (progress >= 100.0)
					{
						achievement.SetCompleted(value: true);
					}
					achievement.SetHidden(value: false);
					achievement.SetLastReportedDate(DateTime.Now);
					achievement.percentCompleted = progress;
					callback?.Invoke(obj: true);
					return;
				}
			}
			foreach (AchievementDescription achievementDescription in m_AchievementDescriptions)
			{
				if (achievementDescription.id == id)
				{
					bool completed = progress >= 100.0;
					Achievement item = new Achievement(id, progress, completed, hidden: false, DateTime.Now);
					m_Achievements.Add(item);
					callback?.Invoke(obj: true);
					return;
				}
			}
			Debug.LogError("Achievement ID not found");
			callback?.Invoke(obj: false);
		}

		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			if (VerifyUser() && callback != null)
			{
				IAchievementDescription[] obj = m_AchievementDescriptions.ToArray();
				callback(obj);
			}
		}

		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			if (VerifyUser() && callback != null)
			{
				IAchievement[] obj = m_Achievements.ToArray();
				callback(obj);
			}
		}

		public void ReportScore(long score, string board, Action<bool> callback)
		{
			if (!VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in m_Leaderboards)
			{
				if (leaderboard.id == board)
				{
					List<Score> list = new List<Score>((Score[])leaderboard.scores);
					list.Add(new Score(board, score, localUser.id, DateTime.Now, score + " points", 0));
					IScore[] scores = list.ToArray();
					leaderboard.SetScores(scores);
					callback?.Invoke(obj: true);
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			callback?.Invoke(obj: false);
		}

		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			if (!VerifyUser())
			{
				return;
			}
			foreach (Leaderboard leaderboard in m_Leaderboards)
			{
				if (leaderboard.id == leaderboardID)
				{
					SortScores(leaderboard);
					callback?.Invoke(leaderboard.scores);
					return;
				}
			}
			Debug.LogError("Leaderboard not found");
			if (callback != null)
			{
				IScore[] obj = new Score[0];
				callback(obj);
			}
		}

		void ISocialPlatform.LoadScores(ILeaderboard board, Action<bool> callback)
		{
			if (!VerifyUser())
			{
				return;
			}
			Leaderboard leaderboard = (Leaderboard)board;
			foreach (Leaderboard leaderboard2 in m_Leaderboards)
			{
				if (leaderboard2.id == leaderboard.id)
				{
					leaderboard.SetTitle(leaderboard2.title);
					leaderboard.SetScores(leaderboard2.scores);
					leaderboard.SetMaxRange((uint)leaderboard2.scores.Length);
				}
			}
			SortScores(leaderboard);
			SetLocalPlayerScore(leaderboard);
			callback?.Invoke(obj: true);
		}

		bool ISocialPlatform.GetLoading(ILeaderboard board)
		{
			if (!VerifyUser())
			{
				return false;
			}
			return ((Leaderboard)board).loading;
		}

		private void SortScores(Leaderboard board)
		{
			List<Score> list = new List<Score>((Score[])board.scores);
			list.Sort((Score s1, Score s2) => s2.value.CompareTo(s1.value));
			for (int num = 0; num < list.Count; num++)
			{
				list[num].SetRank(num + 1);
			}
		}

		private void SetLocalPlayerScore(Leaderboard board)
		{
			IScore[] scores = board.scores;
			for (int i = 0; i < scores.Length; i++)
			{
				Score score = (Score)scores[i];
				if (score.userID == localUser.id)
				{
					board.SetLocalUserScore(score);
					break;
				}
			}
		}

		public void ShowAchievementsUI()
		{
			Debug.Log("ShowAchievementsUI not implemented");
		}

		public void ShowLeaderboardUI()
		{
			Debug.Log("ShowLeaderboardUI not implemented");
		}

		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		private bool VerifyUser()
		{
			if (!localUser.authenticated)
			{
				Debug.LogError("Must authenticate first");
				return false;
			}
			return true;
		}

		private void PopulateStaticData()
		{
			m_Friends.Add(new UserProfile("Fred", "1001", friend: true, UserState.Online, m_DefaultTexture));
			m_Friends.Add(new UserProfile("Julia", "1002", friend: true, UserState.Online, m_DefaultTexture));
			m_Friends.Add(new UserProfile("Jeff", "1003", friend: true, UserState.Online, m_DefaultTexture));
			m_Users.Add(new UserProfile("Sam", "1004", friend: false, UserState.Offline, m_DefaultTexture));
			m_Users.Add(new UserProfile("Max", "1005", friend: false, UserState.Offline, m_DefaultTexture));
			m_AchievementDescriptions.Add(new AchievementDescription("Achievement01", "First achievement", m_DefaultTexture, "Get first achievement", "Received first achievement", hidden: false, 10));
			m_AchievementDescriptions.Add(new AchievementDescription("Achievement02", "Second achievement", m_DefaultTexture, "Get second achievement", "Received second achievement", hidden: false, 20));
			m_AchievementDescriptions.Add(new AchievementDescription("Achievement03", "Third achievement", m_DefaultTexture, "Get third achievement", "Received third achievement", hidden: false, 15));
			Leaderboard leaderboard = new Leaderboard();
			leaderboard.SetTitle("High Scores");
			leaderboard.id = "Leaderboard01";
			List<Score> list = new List<Score>();
			list.Add(new Score("Leaderboard01", 300L, "1001", DateTime.Now.AddDays(-1.0), "300 points", 1));
			list.Add(new Score("Leaderboard01", 255L, "1002", DateTime.Now.AddDays(-1.0), "255 points", 2));
			list.Add(new Score("Leaderboard01", 55L, "1003", DateTime.Now.AddDays(-1.0), "55 points", 3));
			list.Add(new Score("Leaderboard01", 10L, "1004", DateTime.Now.AddDays(-1.0), "10 points", 4));
			IScore[] scores = list.ToArray();
			leaderboard.SetScores(scores);
			m_Leaderboards.Add(leaderboard);
		}

		private Texture2D CreateDummyTexture(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = (((j & i) > 0) ? Color.white : Color.gray);
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}
	}
	internal static class ActivePlatform
	{
		private static ISocialPlatform _active;

		internal static ISocialPlatform Instance
		{
			get
			{
				if (_active == null)
				{
					_active = SelectSocialPlatform();
				}
				return _active;
			}
			set
			{
				_active = value;
			}
		}

		private static ISocialPlatform SelectSocialPlatform()
		{
			return new Local();
		}
	}
	public interface ISocialPlatform
	{
		ILocalUser localUser { get; }

		void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback);

		void ReportProgress(string achievementID, double progress, Action<bool> callback);

		void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback);

		void LoadAchievements(Action<IAchievement[]> callback);

		IAchievement CreateAchievement();

		void ReportScore(long score, string board, Action<bool> callback);

		void LoadScores(string leaderboardID, Action<IScore[]> callback);

		ILeaderboard CreateLeaderboard();

		void ShowAchievementsUI();

		void ShowLeaderboardUI();

		void Authenticate(ILocalUser user, Action<bool> callback);

		void Authenticate(ILocalUser user, Action<bool, string> callback);

		void LoadFriends(ILocalUser user, Action<bool> callback);

		void LoadScores(ILeaderboard board, Action<bool> callback);

		bool GetLoading(ILeaderboard board);
	}
	public interface ILocalUser : IUserProfile
	{
		IUserProfile[] friends { get; }

		bool authenticated { get; }

		bool underage { get; }

		void Authenticate(Action<bool> callback);

		void Authenticate(Action<bool, string> callback);

		void LoadFriends(Action<bool> callback);
	}
	public enum UserState
	{
		Online,
		OnlineAndAway,
		OnlineAndBusy,
		Offline,
		Playing
	}
	public interface IUserProfile
	{
		string userName { get; }

		string id { get; }

		bool isFriend { get; }

		UserState state { get; }

		Texture2D image { get; }
	}
	public interface IAchievement
	{
		string id { get; set; }

		double percentCompleted { get; set; }

		bool completed { get; }

		bool hidden { get; }

		DateTime lastReportedDate { get; }

		void ReportProgress(Action<bool> callback);
	}
	public interface IAchievementDescription
	{
		string id { get; set; }

		string title { get; }

		Texture2D image { get; }

		string achievedDescription { get; }

		string unachievedDescription { get; }

		bool hidden { get; }

		int points { get; }
	}
	public interface IScore
	{
		string leaderboardID { get; set; }

		long value { get; set; }

		DateTime date { get; }

		string formattedValue { get; }

		string userID { get; }

		int rank { get; }

		void ReportScore(Action<bool> callback);
	}
	public enum UserScope
	{
		Global,
		FriendsOnly
	}
	public enum TimeScope
	{
		Today,
		Week,
		AllTime
	}
	public struct Range
	{
		public int from;

		public int count;

		public Range(int fromValue, int valueCount)
		{
			from = fromValue;
			count = valueCount;
		}
	}
	public interface ILeaderboard
	{
		bool loading { get; }

		string id { get; set; }

		UserScope userScope { get; set; }

		Range range { get; set; }

		TimeScope timeScope { get; set; }

		IScore localUserScore { get; }

		uint maxRange { get; }

		IScore[] scores { get; }

		string title { get; }

		void SetUserFilter(string[] userIDs);

		void LoadScores(Action<bool> callback);
	}
}
namespace UnityEngine.SocialPlatforms.Impl
{
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		private IUserProfile[] m_Friends;

		private bool m_Authenticated;

		private bool m_Underage;

		public IUserProfile[] friends => m_Friends;

		public bool authenticated => m_Authenticated;

		public bool underage => m_Underage;

		public LocalUser()
		{
			IUserProfile[] array = new UserProfile[0];
			m_Friends = array;
			m_Authenticated = false;
			m_Underage = false;
		}

		public void Authenticate(Action<bool> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		public void Authenticate(Action<bool, string> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		public void LoadFriends(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadFriends(this, callback);
		}

		public void SetFriends(IUserProfile[] friends)
		{
			m_Friends = friends;
		}

		public void SetAuthenticated(bool value)
		{
			m_Authenticated = value;
		}

		public void SetUnderage(bool value)
		{
			m_Underage = value;
		}
	}
	public class UserProfile : IUserProfile
	{
		protected string m_UserName;

		protected string m_ID;

		private string m_legacyID;

		protected bool m_IsFriend;

		protected UserState m_State;

		protected Texture2D m_Image;

		private string m_gameID;

		private const string legacyIdObsoleteMessage = "legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead";

		public string userName => m_UserName;

		public string id => m_ID;

		[Obsolete("legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead (UnityUpgradable) -> id", true)]
		public string legacyId
		{
			get
			{
				throw new NotSupportedException("legacyId returns playerID from GKPlayer, which became obsolete in iOS 12.4 . id returns playerID for devices running versions before iOS 12.4, and the newer teamPlayerID for later versions. Please use IUserProfile.id or UserProfile.id instead");
			}
		}

		public string gameId => m_gameID;

		public bool isFriend => m_IsFriend;

		public UserState state => m_State;

		public Texture2D image => m_Image;

		public UserProfile()
		{
			m_UserName = "Uninitialized";
			m_ID = "0";
			m_legacyID = "0";
			m_IsFriend = false;
			m_State = UserState.Offline;
			m_Image = new Texture2D(32, 32);
		}

		public UserProfile(string name, string id, bool friend)
			: this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image)
			: this(name, id, id, friend, state, image)
		{
		}

		public UserProfile(string name, string teamId, string gameId, bool friend, UserState state, Texture2D image)
		{
			m_UserName = name;
			m_ID = teamId;
			m_gameID = gameId;
			m_IsFriend = friend;
			m_State = state;
			m_Image = image;
		}

		public override string ToString()
		{
			return id + " - " + userName + " - " + isFriend + " - " + state;
		}

		public void SetUserName(string name)
		{
			m_UserName = name;
		}

		public void SetUserID(string id)
		{
			m_ID = id;
		}

		public void SetLegacyUserID(string id)
		{
			m_legacyID = id;
		}

		public void SetUserGameID(string id)
		{
			m_gameID = id;
		}

		public void SetImage(Texture2D image)
		{
			m_Image = image;
		}

		public void SetIsFriend(bool value)
		{
			m_IsFriend = value;
		}

		public void SetState(UserState state)
		{
			m_State = state;
		}
	}
	public class Achievement : IAchievement
	{
		private bool m_Completed;

		private bool m_Hidden;

		private DateTime m_LastReportedDate;

		public string id { get; set; }

		public double percentCompleted { get; set; }

		public bool completed => m_Completed;

		public bool hidden => m_Hidden;

		public DateTime lastReportedDate => m_LastReportedDate;

		public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
		{
			this.id = id;
			this.percentCompleted = percentCompleted;
			m_Completed = completed;
			m_Hidden = hidden;
			m_LastReportedDate = lastReportedDate;
		}

		public Achievement(string id, double percent)
		{
			this.id = id;
			percentCompleted = percent;
			m_Hidden = false;
			m_Completed = false;
			m_LastReportedDate = DateTime.MinValue;
		}

		public Achievement()
			: this("unknown", 0.0)
		{
		}

		public override string ToString()
		{
			return id + " - " + percentCompleted + " - " + completed + " - " + hidden + " - " + lastReportedDate;
		}

		public void ReportProgress(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportProgress(id, percentCompleted, callback);
		}

		public void SetCompleted(bool value)
		{
			m_Completed = value;
		}

		public void SetHidden(bool value)
		{
			m_Hidden = value;
		}

		public void SetLastReportedDate(DateTime date)
		{
			m_LastReportedDate = date;
		}
	}
	public class AchievementDescription : IAchievementDescription
	{
		private string m_Title;

		private Texture2D m_Image;

		private string m_AchievedDescription;

		private string m_UnachievedDescription;

		private bool m_Hidden;

		private int m_Points;

		public string id { get; set; }

		public string title => m_Title;

		public Texture2D image => m_Image;

		public string achievedDescription => m_AchievedDescription;

		public string unachievedDescription => m_UnachievedDescription;

		public bool hidden => m_Hidden;

		public int points => m_Points;

		public AchievementDescription(string id, string title, Texture2D image, string achievedDescription, string unachievedDescription, bool hidden, int points)
		{
			this.id = id;
			m_Title = title;
			m_Image = image;
			m_AchievedDescription = achievedDescription;
			m_UnachievedDescription = unachievedDescription;
			m_Hidden = hidden;
			m_Points = points;
		}

		public override string ToString()
		{
			return id + " - " + title + " - " + achievedDescription + " - " + unachievedDescription + " - " + points + " - " + hidden;
		}

		public void SetImage(Texture2D image)
		{
			m_Image = image;
		}
	}
	public class Score : IScore
	{
		private DateTime m_Date;

		private string m_FormattedValue;

		private string m_UserID;

		private int m_Rank;

		public string leaderboardID { get; set; }

		public long value { get; set; }

		public DateTime date => m_Date;

		public string formattedValue => m_FormattedValue;

		public string userID => m_UserID;

		public int rank => m_Rank;

		public Score()
			: this("unkown", -1L)
		{
		}

		public Score(string leaderboardID, long value)
			: this(leaderboardID, value, "0", DateTime.Now, "", -1)
		{
		}

		public Score(string leaderboardID, long value, string userID, DateTime date, string formattedValue, int rank)
		{
			this.leaderboardID = leaderboardID;
			this.value = value;
			m_UserID = userID;
			m_Date = date;
			m_FormattedValue = formattedValue;
			m_Rank = rank;
		}

		public override string ToString()
		{
			return "Rank: '" + m_Rank + "' Value: '" + value + "' Category: '" + leaderboardID + "' PlayerID: '" + m_UserID + "' Date: '" + m_Date;
		}

		public void ReportScore(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportScore(value, leaderboardID, callback);
		}

		public void SetDate(DateTime date)
		{
			m_Date = date;
		}

		public void SetFormattedValue(string value)
		{
			m_FormattedValue = value;
		}

		public void SetUserID(string userID)
		{
			m_UserID = userID;
		}

		public void SetRank(int rank)
		{
			m_Rank = rank;
		}
	}
	public class Leaderboard : ILeaderboard
	{
		private bool m_Loading;

		private IScore m_LocalUserScore;

		private uint m_MaxRange;

		private IScore[] m_Scores;

		private string m_Title;

		private string[] m_UserIDs;

		public bool loading => ActivePlatform.Instance.GetLoading(this);

		public string id { get; set; }

		public UserScope userScope { get; set; }

		public Range range { get; set; }

		public TimeScope timeScope { get; set; }

		public IScore localUserScore => m_LocalUserScore;

		public uint maxRange => m_MaxRange;

		public IScore[] scores => m_Scores;

		public string title => m_Title;

		public Leaderboard()
		{
			id = "Invalid";
			range = new Range(1, 10);
			userScope = UserScope.Global;
			timeScope = TimeScope.AllTime;
			m_Loading = false;
			m_LocalUserScore = new Score("Invalid", 0L);
			m_MaxRange = 0u;
			IScore[] array = new Score[0];
			m_Scores = array;
			m_Title = "Invalid";
			m_UserIDs = new string[0];
		}

		public void SetUserFilter(string[] userIDs)
		{
			m_UserIDs = userIDs;
		}

		public override string ToString()
		{
			return "ID: '" + id + "' Title: '" + m_Title + "' Loading: '" + m_Loading + "' Range: [" + range.from + "," + range.count + "] MaxRange: '" + m_MaxRange + "' Scores: '" + m_Scores.Length + "' UserScope: '" + userScope.ToString() + "' TimeScope: '" + timeScope.ToString() + "' UserFilter: '" + m_UserIDs.Length;
		}

		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		public void SetLocalUserScore(IScore score)
		{
			m_LocalUserScore = score;
		}

		public void SetMaxRange(uint maxRange)
		{
			m_MaxRange = maxRange;
		}

		public void SetScores(IScore[] scores)
		{
			m_Scores = scores;
		}

		public void SetTitle(string title)
		{
			m_Title = title;
		}

		public string[] GetUserFilter()
		{
			return m_UserIDs;
		}
	}
}
