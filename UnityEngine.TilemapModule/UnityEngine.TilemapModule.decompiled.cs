using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.U2D;

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
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
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
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomGridBrushAttribute : Attribute
	{
		private bool m_HideAssetInstances;

		private bool m_HideDefaultInstance;

		private bool m_DefaultBrush;

		private string m_DefaultName;

		public bool hideAssetInstances => m_HideAssetInstances;

		public bool hideDefaultInstance => m_HideDefaultInstance;

		public bool defaultBrush => m_DefaultBrush;

		public string defaultName => m_DefaultName;

		public CustomGridBrushAttribute()
		{
			m_HideAssetInstances = false;
			m_HideDefaultInstance = false;
			m_DefaultBrush = false;
			m_DefaultName = "";
		}

		public CustomGridBrushAttribute(bool hideAssetInstances, bool hideDefaultInstance, bool defaultBrush, string defaultName)
		{
			m_HideAssetInstances = hideAssetInstances;
			m_HideDefaultInstance = hideDefaultInstance;
			m_DefaultBrush = defaultBrush;
			m_DefaultName = defaultName;
		}
	}
	public abstract class GridBrushBase : ScriptableObject
	{
		public enum Tool
		{
			Select,
			Move,
			Paint,
			Box,
			Pick,
			Erase,
			FloodFill,
			Other
		}

		public enum RotationDirection
		{
			Clockwise,
			CounterClockwise
		}

		public enum FlipAxis
		{
			X,
			Y
		}

		public virtual void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		public virtual void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		public virtual void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
			for (int i = position.zMin; i < position.zMax; i++)
			{
				for (int j = position.yMin; j < position.yMax; j++)
				{
					for (int k = position.xMin; k < position.xMax; k++)
					{
						Paint(gridLayout, brushTarget, new Vector3Int(k, j, i));
					}
				}
			}
		}

		public virtual void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
			for (int i = position.zMin; i < position.zMax; i++)
			{
				for (int j = position.yMin; j < position.yMax; j++)
				{
					for (int k = position.xMin; k < position.xMax; k++)
					{
						Erase(gridLayout, brushTarget, new Vector3Int(k, j, i));
					}
				}
			}
		}

		public virtual void Select(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		public virtual void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
		{
		}

		public virtual void Rotate(RotationDirection direction, GridLayout.CellLayout layout)
		{
		}

		public virtual void Flip(FlipAxis flip, GridLayout.CellLayout layout)
		{
		}

		public virtual void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot)
		{
		}

		public virtual void Move(GridLayout gridLayout, GameObject brushTarget, BoundsInt from, BoundsInt to)
		{
		}

		public virtual void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		public virtual void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
		{
		}

		public virtual void ChangeZPosition(int change)
		{
		}

		public virtual void ResetZPosition()
		{
		}
	}
}
namespace UnityEngine.Tilemaps
{
	[RequiredByNativeCode]
	public class ITilemap
	{
		internal static ITilemap s_Instance;

		internal Tilemap m_Tilemap;

		internal bool m_AddToList;

		internal int m_RefreshCount;

		internal NativeArray<Vector3Int> m_RefreshPos;

		public Vector3Int origin => m_Tilemap.origin;

		public Vector3Int size => m_Tilemap.size;

		public Bounds localBounds => m_Tilemap.localBounds;

		public BoundsInt cellBounds => m_Tilemap.cellBounds;

		internal ITilemap()
		{
		}

		public ITilemap(Tilemap tilemap)
		{
			if (tilemap == null)
			{
				throw new ArgumentNullException("Argument tilemap cannot be null");
			}
			m_Tilemap = tilemap;
		}

		public static implicit operator ITilemap(Tilemap tilemap)
		{
			return new ITilemap(tilemap);
		}

		internal void SetTilemapInstance(Tilemap tilemap)
		{
			m_Tilemap = tilemap;
		}

		public virtual Sprite GetSprite(Vector3Int position)
		{
			return m_Tilemap.GetSprite(position);
		}

		public virtual Color GetColor(Vector3Int position)
		{
			return m_Tilemap.GetColor(position);
		}

		public virtual Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			return m_Tilemap.GetTransformMatrix(position);
		}

		public virtual TileFlags GetTileFlags(Vector3Int position)
		{
			return m_Tilemap.GetTileFlags(position);
		}

		public virtual TileBase GetTile(Vector3Int position)
		{
			return m_Tilemap.GetTile(position);
		}

		public virtual T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return m_Tilemap.GetTile<T>(position);
		}

		public void RefreshTile(Vector3Int position)
		{
			if (m_AddToList)
			{
				if (m_RefreshCount >= m_RefreshPos.Length)
				{
					NativeArray<Vector3Int> nativeArray = new NativeArray<Vector3Int>(Math.Max(1, m_RefreshCount * 2), Allocator.Temp);
					NativeArray<Vector3Int>.Copy(m_RefreshPos, nativeArray, m_RefreshPos.Length);
					m_RefreshPos.Dispose();
					m_RefreshPos = nativeArray;
				}
				m_RefreshPos[m_RefreshCount++] = position;
			}
			else
			{
				m_Tilemap.RefreshTile(position);
			}
		}

		public T GetComponent<T>()
		{
			if (typeof(T) == typeof(Tilemap))
			{
				return (T)(object)m_Tilemap;
			}
			return m_Tilemap.GetComponent<T>();
		}

		[RequiredByNativeCode]
		private static ITilemap CreateInstance()
		{
			s_Instance = new ITilemap();
			return s_Instance;
		}

		[RequiredByNativeCode]
		private unsafe static void FindAllRefreshPositions(ITilemap tilemap, int count, IntPtr oldTilesIntPtr, IntPtr newTilesIntPtr, IntPtr positionsIntPtr)
		{
			tilemap.m_AddToList = true;
			_ = tilemap.m_RefreshPos;
			if (!tilemap.m_RefreshPos.IsCreated || tilemap.m_RefreshPos.Length < count)
			{
				tilemap.m_RefreshPos = new NativeArray<Vector3Int>(Math.Max(16, count), Allocator.Temp);
			}
			tilemap.m_RefreshCount = 0;
			void* dataPointer = oldTilesIntPtr.ToPointer();
			void* dataPointer2 = newTilesIntPtr.ToPointer();
			void* dataPointer3 = positionsIntPtr.ToPointer();
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer, count, Allocator.Invalid);
			NativeArray<int> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer2, count, Allocator.Invalid);
			NativeArray<Vector3Int> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(dataPointer3, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				int num = nativeArray[i];
				int num2 = nativeArray2[i];
				Vector3Int position = nativeArray3[i];
				if (num != 0)
				{
					TileBase tileBase = (TileBase)Object.ForceLoadFromInstanceID(num);
					tileBase.RefreshTile(position, tilemap);
				}
				if (num2 != 0)
				{
					TileBase tileBase2 = (TileBase)Object.ForceLoadFromInstanceID(num2);
					tileBase2.RefreshTile(position, tilemap);
				}
			}
			tilemap.m_Tilemap.RefreshTilesNative(tilemap.m_RefreshPos.m_Buffer, tilemap.m_RefreshCount);
			tilemap.m_RefreshPos.Dispose();
			tilemap.m_AddToList = false;
		}

		[RequiredByNativeCode]
		private unsafe static void GetAllTileData(ITilemap tilemap, int count, IntPtr tilesIntPtr, IntPtr positionsIntPtr, IntPtr outTileDataIntPtr)
		{
			void* dataPointer = tilesIntPtr.ToPointer();
			void* dataPointer2 = positionsIntPtr.ToPointer();
			void* dataPointer3 = outTileDataIntPtr.ToPointer();
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(dataPointer, count, Allocator.Invalid);
			NativeArray<Vector3Int> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(dataPointer2, count, Allocator.Invalid);
			NativeArray<TileData> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<TileData>(dataPointer3, count, Allocator.Invalid);
			for (int i = 0; i < count; i++)
			{
				TileData tileData = TileData.Default;
				int num = nativeArray[i];
				if (num != 0)
				{
					TileBase tileBase = (TileBase)Object.ForceLoadFromInstanceID(num);
					tileBase.GetTileData(nativeArray2[i], tilemap, ref tileData);
				}
				nativeArray3[i] = tileData;
			}
		}
	}
	[Serializable]
	[RequiredByNativeCode]
	[HelpURL("https://docs.unity3d.com/Manual/Tilemap-TileAsset.html")]
	public class Tile : TileBase
	{
		public enum ColliderType
		{
			None,
			Sprite,
			Grid
		}

		[SerializeField]
		private Sprite m_Sprite;

		[SerializeField]
		private Color m_Color = Color.white;

		[SerializeField]
		private Matrix4x4 m_Transform = Matrix4x4.identity;

		[SerializeField]
		private GameObject m_InstancedGameObject;

		[SerializeField]
		private TileFlags m_Flags = TileFlags.LockColor;

		[SerializeField]
		private ColliderType m_ColliderType = ColliderType.Sprite;

		public Sprite sprite
		{
			get
			{
				return m_Sprite;
			}
			set
			{
				m_Sprite = value;
			}
		}

		public Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}

		public Matrix4x4 transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return m_InstancedGameObject;
			}
			set
			{
				m_InstancedGameObject = value;
			}
		}

		public TileFlags flags
		{
			get
			{
				return m_Flags;
			}
			set
			{
				m_Flags = value;
			}
		}

		public ColliderType colliderType
		{
			get
			{
				return m_ColliderType;
			}
			set
			{
				m_ColliderType = value;
			}
		}

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.sprite = m_Sprite;
			tileData.color = m_Color;
			tileData.transform = m_Transform;
			tileData.gameObject = m_InstancedGameObject;
			tileData.flags = m_Flags;
			tileData.colliderType = m_ColliderType;
		}
	}
	[RequiredByNativeCode]
	public abstract class TileBase : ScriptableObject
	{
		[RequiredByNativeCode]
		public virtual void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			tilemap.RefreshTile(position);
		}

		[RequiredByNativeCode]
		public virtual void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
		}

		private TileData GetTileDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileData tileData = default(TileData);
			GetTileData(position, tilemap, ref tileData);
			return tileData;
		}

		[RequiredByNativeCode]
		public virtual bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return false;
		}

		private TileAnimationData GetTileAnimationDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileAnimationData tileAnimationData = default(TileAnimationData);
			GetTileAnimationData(position, tilemap, ref tileAnimationData);
			return tileAnimationData;
		}

		[RequiredByNativeCode]
		private void GetTileAnimationDataRef(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData, ref bool hasAnimation)
		{
			hasAnimation = GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		[RequiredByNativeCode]
		public virtual bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			return false;
		}

		[RequiredByNativeCode]
		private void StartUpRef(Vector3Int position, ITilemap tilemap, GameObject go, ref bool startUpInvokedByUser)
		{
			startUpInvokedByUser = StartUp(position, tilemap, go);
		}
	}
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeHeader("Modules/Grid/Public/Grid.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapTile.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[NativeType(Header = "Modules/Tilemap/Public/Tilemap.h")]
	public sealed class Tilemap : GridLayout
	{
		public enum Orientation
		{
			XY,
			XZ,
			YX,
			YZ,
			ZX,
			ZY,
			Custom
		}

		[RequiredByNativeCode]
		public struct SyncTile
		{
			internal Vector3Int m_Position;

			internal TileBase m_Tile;

			internal TileData m_TileData;

			public Vector3Int position => m_Position;

			public TileBase tile => m_Tile;

			public TileData tileData => m_TileData;
		}

		internal struct SyncTileCallbackSettings
		{
			internal bool hasSyncTileCallback;

			internal bool hasPositionsChangedCallback;

			internal bool isBufferSyncTile;
		}

		private bool m_BufferSyncTile;

		internal bool bufferSyncTile
		{
			get
			{
				return m_BufferSyncTile;
			}
			set
			{
				if (!value && m_BufferSyncTile != value && HasSyncTileCallback())
				{
					SendAndClearSyncTileBuffer();
				}
				m_BufferSyncTile = value;
			}
		}

		public extern Grid layoutGrid
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetAttachedGrid")]
			get;
		}

		public BoundsInt cellBounds => new BoundsInt(origin, size);

		[NativeProperty("TilemapBoundsScripting")]
		public Bounds localBounds
		{
			get
			{
				get_localBounds_Injected(out var ret);
				return ret;
			}
		}

		[NativeProperty("TilemapFrameBoundsScripting")]
		internal Bounds localFrameBounds
		{
			get
			{
				get_localFrameBounds_Injected(out var ret);
				return ret;
			}
		}

		public extern float animationFrameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color color
		{
			get
			{
				get_color_Injected(out var ret);
				return ret;
			}
			set
			{
				set_color_Injected(ref value);
			}
		}

		public Vector3Int origin
		{
			get
			{
				get_origin_Injected(out var ret);
				return ret;
			}
			set
			{
				set_origin_Injected(ref value);
			}
		}

		public Vector3Int size
		{
			get
			{
				get_size_Injected(out var ret);
				return ret;
			}
			set
			{
				set_size_Injected(ref value);
			}
		}

		[NativeProperty(Name = "TileAnchorScripting")]
		public Vector3 tileAnchor
		{
			get
			{
				get_tileAnchor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_tileAnchor_Injected(ref value);
			}
		}

		public extern Orientation orientation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 orientationMatrix
		{
			[NativeMethod(Name = "GetTileOrientationMatrix")]
			get
			{
				get_orientationMatrix_Injected(out var ret);
				return ret;
			}
			[NativeMethod(Name = "SetOrientationMatrix")]
			set
			{
				set_orientationMatrix_Injected(ref value);
			}
		}

		public static event Action<Tilemap, SyncTile[]> tilemapTileChanged;

		public static event Action<Tilemap, NativeArray<Vector3Int>> tilemapPositionsChanged;

		internal static bool HasSyncTileCallback()
		{
			return Tilemap.tilemapTileChanged != null;
		}

		internal static bool HasPositionsChangedCallback()
		{
			return Tilemap.tilemapPositionsChanged != null;
		}

		private void HandleSyncTileCallback(SyncTile[] syncTiles)
		{
			if (Tilemap.tilemapTileChanged != null)
			{
				SendTilemapTileChangedCallback(syncTiles);
			}
		}

		private unsafe void HandlePositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			if (Tilemap.tilemapPositionsChanged != null)
			{
				void* dataPointer = positionsIntPtr.ToPointer();
				NativeArray<Vector3Int> positions = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(dataPointer, count, Allocator.Invalid);
				SendTilemapPositionsChangedCallback(positions);
			}
		}

		private void SendTilemapTileChangedCallback(SyncTile[] syncTiles)
		{
			try
			{
				Tilemap.tilemapTileChanged(this, syncTiles);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception, this);
			}
		}

		private void SendTilemapPositionsChangedCallback(NativeArray<Vector3Int> positions)
		{
			try
			{
				Tilemap.tilemapPositionsChanged(this, positions);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception, this);
			}
		}

		internal static void SetSyncTileCallback(Action<Tilemap, SyncTile[]> callback)
		{
			tilemapTileChanged += callback;
		}

		internal static void RemoveSyncTileCallback(Action<Tilemap, SyncTile[]> callback)
		{
			tilemapTileChanged -= callback;
		}

		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return CellToLocalInterpolated(position) + CellToLocalInterpolated(tileAnchor);
		}

		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return LocalToWorld(CellToLocalInterpolated(position) + CellToLocalInterpolated(tileAnchor));
		}

		internal Object GetTileAsset(Vector3Int position)
		{
			return GetTileAsset_Injected(ref position);
		}

		public TileBase GetTile(Vector3Int position)
		{
			return GetTileAsset(position) as TileBase;
		}

		public T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return GetTileAsset(position) as T;
		}

		internal Object[] GetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions)
		{
			return GetTileAssetsBlock_Injected(ref position, ref blockDimensions);
		}

		public TileBase[] GetTilesBlock(BoundsInt bounds)
		{
			Object[] tileAssetsBlock = GetTileAssetsBlock(bounds.min, bounds.size);
			TileBase[] array = new TileBase[tileAssetsBlock.Length];
			for (int i = 0; i < tileAssetsBlock.Length; i++)
			{
				array[i] = (TileBase)tileAssetsBlock[i];
			}
			return array;
		}

		[FreeFunction(Name = "TilemapBindings::GetTileAssetsBlockNonAlloc", HasExplicitThis = true)]
		internal int GetTileAssetsBlockNonAlloc(Vector3Int startPosition, Vector3Int endPosition, [Unmarshalled] Object[] tiles)
		{
			return GetTileAssetsBlockNonAlloc_Injected(ref startPosition, ref endPosition, tiles);
		}

		public int GetTilesBlockNonAlloc(BoundsInt bounds, TileBase[] tiles)
		{
			return GetTileAssetsBlockNonAlloc(bounds.min, bounds.size, tiles);
		}

		public int GetTilesRangeCount(Vector3Int startPosition, Vector3Int endPosition)
		{
			return GetTilesRangeCount_Injected(ref startPosition, ref endPosition);
		}

		[FreeFunction(Name = "TilemapBindings::GetTileAssetsRangeNonAlloc", HasExplicitThis = true)]
		internal int GetTileAssetsRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, [Unmarshalled] Vector3Int[] positions, [Unmarshalled] Object[] tiles)
		{
			return GetTileAssetsRangeNonAlloc_Injected(ref startPosition, ref endPosition, positions, tiles);
		}

		public int GetTilesRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, Vector3Int[] positions, TileBase[] tiles)
		{
			return GetTileAssetsRangeNonAlloc(startPosition, endPosition, positions, tiles);
		}

		internal void SetTileAsset(Vector3Int position, Object tile)
		{
			SetTileAsset_Injected(ref position, tile);
		}

		public void SetTile(Vector3Int position, TileBase tile)
		{
			SetTileAsset(position, tile);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTileAssets(Vector3Int[] positionArray, Object[] tileArray);

		public void SetTiles(Vector3Int[] positionArray, TileBase[] tileArray)
		{
			SetTileAssets(positionArray, tileArray);
		}

		[NativeMethod(Name = "SetTileAssetsBlock")]
		private void INTERNAL_CALL_SetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions, Object[] tileArray)
		{
			INTERNAL_CALL_SetTileAssetsBlock_Injected(ref position, ref blockDimensions, tileArray);
		}

		public void SetTilesBlock(BoundsInt position, TileBase[] tileArray)
		{
			INTERNAL_CALL_SetTileAssetsBlock(position.min, position.size, tileArray);
		}

		[NativeMethod(Name = "SetTileChangeData")]
		public void SetTile(TileChangeData tileChangeData, bool ignoreLockFlags)
		{
			SetTile_Injected(ref tileChangeData, ignoreLockFlags);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "SetTileChangeDataArray")]
		public extern void SetTiles(TileChangeData[] tileChangeDataArray, bool ignoreLockFlags);

		public bool HasTile(Vector3Int position)
		{
			return GetTileAsset(position) != null;
		}

		[NativeMethod(Name = "RefreshTileAsset")]
		public void RefreshTile(Vector3Int position)
		{
			RefreshTile_Injected(ref position);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TilemapBindings::RefreshTileAssetsNative", HasExplicitThis = true)]
		internal unsafe extern void RefreshTilesNative(void* positions, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "RefreshAllTileAssets")]
		public extern void RefreshAllTiles();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SwapTileAsset(Object changeTile, Object newTile);

		public void SwapTile(TileBase changeTile, TileBase newTile)
		{
			SwapTileAsset(changeTile, newTile);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool ContainsTileAsset(Object tileAsset);

		public bool ContainsTile(TileBase tileAsset)
		{
			return ContainsTileAsset(tileAsset);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUsedTilesCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetUsedSpritesCount();

		public int GetUsedTilesNonAlloc(TileBase[] usedTiles)
		{
			return Internal_GetUsedTilesNonAlloc(usedTiles);
		}

		public int GetUsedSpritesNonAlloc(Sprite[] usedSprites)
		{
			return Internal_GetUsedSpritesNonAlloc(usedSprites);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TilemapBindings::GetUsedTilesNonAlloc", HasExplicitThis = true)]
		internal extern int Internal_GetUsedTilesNonAlloc([Unmarshalled] Object[] usedTiles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TilemapBindings::GetUsedSpritesNonAlloc", HasExplicitThis = true)]
		internal extern int Internal_GetUsedSpritesNonAlloc([Unmarshalled] Object[] usedSprites);

		public Sprite GetSprite(Vector3Int position)
		{
			return GetSprite_Injected(ref position);
		}

		public Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			GetTransformMatrix_Injected(ref position, out var ret);
			return ret;
		}

		public void SetTransformMatrix(Vector3Int position, Matrix4x4 transform)
		{
			SetTransformMatrix_Injected(ref position, ref transform);
		}

		[NativeMethod(Name = "GetTileColor")]
		public Color GetColor(Vector3Int position)
		{
			GetColor_Injected(ref position, out var ret);
			return ret;
		}

		[NativeMethod(Name = "SetTileColor")]
		public void SetColor(Vector3Int position, Color color)
		{
			SetColor_Injected(ref position, ref color);
		}

		public TileFlags GetTileFlags(Vector3Int position)
		{
			return GetTileFlags_Injected(ref position);
		}

		public void SetTileFlags(Vector3Int position, TileFlags flags)
		{
			SetTileFlags_Injected(ref position, flags);
		}

		public void AddTileFlags(Vector3Int position, TileFlags flags)
		{
			AddTileFlags_Injected(ref position, flags);
		}

		public void RemoveTileFlags(Vector3Int position, TileFlags flags)
		{
			RemoveTileFlags_Injected(ref position, flags);
		}

		[NativeMethod(Name = "GetTileInstantiatedObject")]
		public GameObject GetInstantiatedObject(Vector3Int position)
		{
			return GetInstantiatedObject_Injected(ref position);
		}

		[NativeMethod(Name = "GetTileObjectToInstantiate")]
		public GameObject GetObjectToInstantiate(Vector3Int position)
		{
			return GetObjectToInstantiate_Injected(ref position);
		}

		[NativeMethod(Name = "SetTileColliderType")]
		public void SetColliderType(Vector3Int position, Tile.ColliderType colliderType)
		{
			SetColliderType_Injected(ref position, colliderType);
		}

		[NativeMethod(Name = "GetTileColliderType")]
		public Tile.ColliderType GetColliderType(Vector3Int position)
		{
			return GetColliderType_Injected(ref position);
		}

		[NativeMethod(Name = "GetTileAnimationFrameCount")]
		public int GetAnimationFrameCount(Vector3Int position)
		{
			return GetAnimationFrameCount_Injected(ref position);
		}

		[NativeMethod(Name = "GetTileAnimationFrame")]
		public int GetAnimationFrame(Vector3Int position)
		{
			return GetAnimationFrame_Injected(ref position);
		}

		[NativeMethod(Name = "SetTileAnimationFrame")]
		public void SetAnimationFrame(Vector3Int position, int frame)
		{
			SetAnimationFrame_Injected(ref position, frame);
		}

		[NativeMethod(Name = "GetTileAnimationTime")]
		public float GetAnimationTime(Vector3Int position)
		{
			return GetAnimationTime_Injected(ref position);
		}

		[NativeMethod(Name = "SetTileAnimationTime")]
		public void SetAnimationTime(Vector3Int position, float time)
		{
			SetAnimationTime_Injected(ref position, time);
		}

		public TileAnimationFlags GetTileAnimationFlags(Vector3Int position)
		{
			return GetTileAnimationFlags_Injected(ref position);
		}

		public void SetTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			SetTileAnimationFlags_Injected(ref position, flags);
		}

		public void AddTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			AddTileAnimationFlags_Injected(ref position, flags);
		}

		public void RemoveTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			RemoveTileAnimationFlags_Injected(ref position, flags);
		}

		public void FloodFill(Vector3Int position, TileBase tile)
		{
			FloodFillTileAsset(position, tile);
		}

		[NativeMethod(Name = "FloodFill")]
		private void FloodFillTileAsset(Vector3Int position, Object tile)
		{
			FloodFillTileAsset_Injected(ref position, tile);
		}

		public void BoxFill(Vector3Int position, TileBase tile, int startX, int startY, int endX, int endY)
		{
			BoxFillTileAsset(position, tile, startX, startY, endX, endY);
		}

		[NativeMethod(Name = "BoxFill")]
		private void BoxFillTileAsset(Vector3Int position, Object tile, int startX, int startY, int endX, int endY)
		{
			BoxFillTileAsset_Injected(ref position, tile, startX, startY, endX, endY);
		}

		public void InsertCells(Vector3Int position, Vector3Int insertCells)
		{
			InsertCells(position, insertCells.x, insertCells.y, insertCells.z);
		}

		public void InsertCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			InsertCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		public void DeleteCells(Vector3Int position, Vector3Int deleteCells)
		{
			DeleteCells(position, deleteCells.x, deleteCells.y, deleteCells.z);
		}

		public void DeleteCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			DeleteCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearAllTiles();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResizeBounds();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompressBounds();

		[RequiredByNativeCode]
		internal void GetSyncTileCallbackSettings(ref SyncTileCallbackSettings settings)
		{
			settings.hasSyncTileCallback = HasSyncTileCallback();
			settings.hasPositionsChangedCallback = HasPositionsChangedCallback();
			settings.isBufferSyncTile = bufferSyncTile;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SendAndClearSyncTileBuffer();

		[RequiredByNativeCode]
		private void DoSyncTileCallback(SyncTile[] syncTiles)
		{
			HandleSyncTileCallback(syncTiles);
		}

		[RequiredByNativeCode]
		private void DoPositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			HandlePositionsChangedCallback(count, positionsIntPtr);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_localBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_localFrameBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_origin_Injected(out Vector3Int ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_origin_Injected(ref Vector3Int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_size_Injected(out Vector3Int ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_size_Injected(ref Vector3Int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_tileAnchor_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_tileAnchor_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_orientationMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_orientationMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object GetTileAsset_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object[] GetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTileAssetsBlockNonAlloc_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition, Object[] tiles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTilesRangeCount_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTileAssetsRangeNonAlloc_Injected(ref Vector3Int startPosition, ref Vector3Int endPosition, Vector3Int[] positions, Object[] tiles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTileAsset_Injected(ref Vector3Int position, Object tile);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_CALL_SetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions, Object[] tileArray);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTile_Injected(ref TileChangeData tileChangeData, bool ignoreLockFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RefreshTile_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Sprite GetSprite_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTransformMatrix_Injected(ref Vector3Int position, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTransformMatrix_Injected(ref Vector3Int position, ref Matrix4x4 transform);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColor_Injected(ref Vector3Int position, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColor_Injected(ref Vector3Int position, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TileFlags GetTileFlags_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject GetInstantiatedObject_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject GetObjectToInstantiate_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColliderType_Injected(ref Vector3Int position, Tile.ColliderType colliderType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Tile.ColliderType GetColliderType_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAnimationFrameCount_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetAnimationFrame_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAnimationFrame_Injected(ref Vector3Int position, int frame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetAnimationTime_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAnimationTime_Injected(ref Vector3Int position, float time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TileAnimationFlags GetTileAnimationFlags_Injected(ref Vector3Int position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTileAnimationFlags_Injected(ref Vector3Int position, TileAnimationFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTileAnimationFlags_Injected(ref Vector3Int position, TileAnimationFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveTileAnimationFlags_Injected(ref Vector3Int position, TileAnimationFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void FloodFillTileAsset_Injected(ref Vector3Int position, Object tile);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void BoxFillTileAsset_Injected(ref Vector3Int position, Object tile, int startX, int startY, int endX, int endY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InsertCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DeleteCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);
	}
	[Flags]
	public enum TileFlags
	{
		None = 0,
		LockColor = 1,
		LockTransform = 2,
		InstantiateGameObjectRuntimeOnly = 4,
		KeepGameObjectRuntimeOnly = 8,
		LockAll = 3
	}
	[Flags]
	public enum TileAnimationFlags
	{
		None = 0,
		LoopOnce = 1,
		PauseAnimation = 2,
		UpdatePhysics = 4
	}
	[NativeHeader("Modules/Tilemap/TilemapRendererJobs.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[RequireComponent(typeof(Tilemap))]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	public sealed class TilemapRenderer : Renderer
	{
		public enum SortOrder
		{
			BottomLeft,
			BottomRight,
			TopLeft,
			TopRight
		}

		public enum Mode
		{
			Chunk,
			Individual
		}

		public enum DetectChunkCullingBounds
		{
			Auto,
			Manual
		}

		public Vector3Int chunkSize
		{
			get
			{
				get_chunkSize_Injected(out var ret);
				return ret;
			}
			set
			{
				set_chunkSize_Injected(ref value);
			}
		}

		public Vector3 chunkCullingBounds
		{
			[FreeFunction("TilemapRendererBindings::GetChunkCullingBounds", HasExplicitThis = true)]
			get
			{
				get_chunkCullingBounds_Injected(out var ret);
				return ret;
			}
			[FreeFunction("TilemapRendererBindings::SetChunkCullingBounds", HasExplicitThis = true)]
			set
			{
				set_chunkCullingBounds_Injected(ref value);
			}
		}

		public extern int maxChunkCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int maxFrameAge
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SortOrder sortOrder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("RenderMode")]
		public extern Mode mode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern DetectChunkCullingBounds detectChunkCullingBounds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		internal void RegisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered += OnSpriteAtlasRegistered;
		}

		[RequiredByNativeCode]
		internal void UnregisterSpriteAtlasRegistered()
		{
			SpriteAtlasManager.atlasRegistered -= OnSpriteAtlasRegistered;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void OnSpriteAtlasRegistered(SpriteAtlas atlas);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_chunkSize_Injected(out Vector3Int ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_chunkSize_Injected(ref Vector3Int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_chunkCullingBounds_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_chunkCullingBounds_Injected(ref Vector3 value);
	}
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileData
	{
		private int m_Sprite;

		private Color m_Color;

		private Matrix4x4 m_Transform;

		private int m_GameObject;

		private TileFlags m_Flags;

		private Tile.ColliderType m_ColliderType;

		internal static readonly TileData Default = CreateDefault();

		public Sprite sprite
		{
			get
			{
				return Object.ForceLoadFromInstanceID(m_Sprite) as Sprite;
			}
			set
			{
				m_Sprite = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		public Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}

		public Matrix4x4 transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return Object.ForceLoadFromInstanceID(m_GameObject) as GameObject;
			}
			set
			{
				m_GameObject = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		public TileFlags flags
		{
			get
			{
				return m_Flags;
			}
			set
			{
				m_Flags = value;
			}
		}

		public Tile.ColliderType colliderType
		{
			get
			{
				return m_ColliderType;
			}
			set
			{
				m_ColliderType = value;
			}
		}

		private static TileData CreateDefault()
		{
			return new TileData
			{
				color = Color.white,
				transform = Matrix4x4.identity,
				flags = TileFlags.None,
				colliderType = Tile.ColliderType.None
			};
		}
	}
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	internal struct TileDataNative
	{
		private int m_Sprite;

		private Color m_Color;

		private Matrix4x4 m_Transform;

		private int m_GameObject;

		private TileFlags m_Flags;

		private Tile.ColliderType m_ColliderType;

		public int sprite
		{
			get
			{
				return m_Sprite;
			}
			set
			{
				m_Sprite = value;
			}
		}

		public Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}

		public Matrix4x4 transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public int gameObject
		{
			get
			{
				return m_GameObject;
			}
			set
			{
				m_GameObject = value;
			}
		}

		public TileFlags flags
		{
			get
			{
				return m_Flags;
			}
			set
			{
				m_Flags = value;
			}
		}

		public Tile.ColliderType colliderType
		{
			get
			{
				return m_ColliderType;
			}
			set
			{
				m_ColliderType = value;
			}
		}

		public static implicit operator TileDataNative(TileData td)
		{
			return new TileDataNative
			{
				sprite = ((td.sprite != null) ? td.sprite.GetInstanceID() : 0),
				color = td.color,
				transform = td.transform,
				gameObject = ((td.gameObject != null) ? td.gameObject.GetInstanceID() : 0),
				flags = td.flags,
				colliderType = td.colliderType
			};
		}
	}
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileChangeData
	{
		private Vector3Int m_Position;

		private Object m_TileAsset;

		private Color m_Color;

		private Matrix4x4 m_Transform;

		public Vector3Int position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public TileBase tile
		{
			get
			{
				return (TileBase)m_TileAsset;
			}
			set
			{
				m_TileAsset = value;
			}
		}

		public Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				m_Color = value;
			}
		}

		public Matrix4x4 transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public TileChangeData(Vector3Int position, TileBase tile, Color color, Matrix4x4 transform)
		{
			m_Position = position;
			m_TileAsset = tile;
			m_Color = color;
			m_Transform = transform;
		}
	}
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileAnimationData
	{
		private Sprite[] m_AnimatedSprites;

		private float m_AnimationSpeed;

		private float m_AnimationStartTime;

		private TileAnimationFlags m_Flags;

		public Sprite[] animatedSprites
		{
			get
			{
				return m_AnimatedSprites;
			}
			set
			{
				m_AnimatedSprites = value;
			}
		}

		public float animationSpeed
		{
			get
			{
				return m_AnimationSpeed;
			}
			set
			{
				m_AnimationSpeed = value;
			}
		}

		public float animationStartTime
		{
			get
			{
				return m_AnimationStartTime;
			}
			set
			{
				m_AnimationStartTime = value;
			}
		}

		public TileAnimationFlags flags
		{
			get
			{
				return m_Flags;
			}
			set
			{
				m_Flags = value;
			}
		}
	}
	[NativeType(Header = "Modules/Tilemap/Public/TilemapCollider2D.h")]
	[RequireComponent(typeof(Tilemap))]
	public sealed class TilemapCollider2D : Collider2D
	{
		public extern bool useDelaunayMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint maximumTileChangeCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float extrusionFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasTilemapChanges
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasTilemapChanges")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProcessTileChangeQueue")]
		public extern void ProcessTilemapChanges();
	}
}
