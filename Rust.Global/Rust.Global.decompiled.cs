#define UNITY_ASSERTIONS
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using Facepunch;
using Facepunch.Extend;
using Newtonsoft.Json;
using Rust;
using Rust.Components;
using Rust.Components.Camera;
using Rust.ImageEffects;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
public class ArrayIndexIsEnum : PropertyAttribute
{
	public Type enumType;
}
public class ArrayIndexIsEnumRanged : ArrayIndexIsEnum
{
	public float min;

	public float max;
}
public static class AssetPool
{
	public class Pool
	{
		public Stack<UnityEngine.Object> stack = new Stack<UnityEngine.Object>();

		public int allocated;

		public int available;

		public string name;

		public Pool(string name)
		{
			this.name = name;
		}

		public T Pop<T>() where T : UnityEngine.Object, new()
		{
			if (stack.Count > 0)
			{
				available--;
				return stack.Pop() as T;
			}
			allocated++;
			return new T
			{
				name = name
			};
		}

		public void Push<T>(ref T instance) where T : UnityEngine.Object, new()
		{
			available++;
			stack.Push(instance);
			instance = null;
		}

		public void Clear()
		{
			foreach (UnityEngine.Object item in stack)
			{
				UnityEngine.Object.Destroy(item);
			}
			available -= stack.Count;
			allocated -= stack.Count;
			stack.Clear();
		}
	}

	public static Dictionary<Type, Pool> storage = new Dictionary<Type, Pool>();

	public static T Get<T>() where T : UnityEngine.Object, new()
	{
		return GetPool<T>().Pop<T>();
	}

	public static void Free(ref Mesh mesh)
	{
		mesh.Clear();
		GetPool<Mesh>().Push(ref mesh);
	}

	private static Pool GetPool<T>() where T : UnityEngine.Object, new()
	{
		if (!storage.TryGetValue(typeof(T), out var value))
		{
			value = new Pool("Pooled " + typeof(T).Name);
			storage.Add(typeof(T), value);
		}
		return value;
	}

	public static void Clear(string filter = null)
	{
		if (string.IsNullOrEmpty(filter))
		{
			foreach (KeyValuePair<Type, Pool> item in storage)
			{
				item.Value.Clear();
			}
			return;
		}
		foreach (KeyValuePair<Type, Pool> item2 in storage)
		{
			if (item2.Key.FullName.Contains(filter, CompareOptions.IgnoreCase))
			{
				item2.Value.Clear();
			}
		}
	}
}
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
public abstract class PersistentObjectWorkQueue
{
	public static readonly List<PersistentObjectWorkQueue> All = new List<PersistentObjectWorkQueue>();

	public string Name = "Untitled PersistentObjectWorkQueue";

	public TimeSpan WarningThreshold = TimeSpan.FromMilliseconds(200.0);

	public TimeSpan TotalExecutionTime;

	public TimeSpan LastExecutionTime;

	public int LastProcessedCount;

	public abstract int ListLength { get; }
}
public abstract class PersistentObjectWorkQueue<T> : PersistentObjectWorkQueue
{
	protected ListHashSet<T> workList = new ListHashSet<T>();

	private int currentIndex;

	private Stopwatch stopwatch = new Stopwatch();

	public override int ListLength => workList.Count;

	public PersistentObjectWorkQueue()
	{
		Name = GetType().FullName;
		PersistentObjectWorkQueue.All.Add(this);
	}

	public void Clear()
	{
		workList.Clear();
	}

	public void RunList(double maximumMilliseconds)
	{
		if (workList.Count == 0)
		{
			return;
		}
		using (TimeWarning.New(Name, (int)WarningThreshold.TotalMilliseconds))
		{
			stopwatch.Reset();
			stopwatch.Start();
			int count = workList.Count;
			T[] buffer = workList.Values.Buffer;
			if (currentIndex >= workList.Count)
			{
				currentIndex = 0;
			}
			int num = currentIndex;
			LastProcessedCount = 0;
			while (LastProcessedCount < count)
			{
				LastProcessedCount++;
				T val = buffer[currentIndex];
				if (val != null)
				{
					RunJob(val);
				}
				currentIndex++;
				if (currentIndex >= count)
				{
					currentIndex = 0;
				}
				if (currentIndex == num || stopwatch.Elapsed.TotalMilliseconds >= maximumMilliseconds)
				{
					break;
				}
			}
			LastExecutionTime = stopwatch.Elapsed;
			TotalExecutionTime += LastExecutionTime;
		}
	}

	public void Add(T entity)
	{
		if (!Contains(entity) && ShouldAdd(entity))
		{
			workList.Add(entity);
			OnAdded(entity);
		}
	}

	protected virtual void OnAdded(T entity)
	{
	}

	public void Remove(T entity)
	{
		workList.Remove(entity);
		OnRemoved(entity);
	}

	protected virtual void OnRemoved(T entity)
	{
	}

	public bool Contains(T entity)
	{
		return workList.Contains(entity);
	}

	protected virtual bool ShouldAdd(T entity)
	{
		return true;
	}

	protected abstract void RunJob(T entity);

	public string Info()
	{
		return $"{ListLength:n0}, lastCount: {LastProcessedCount:n0}, lastMS: {LastExecutionTime.TotalMilliseconds:R}, totMS: {TotalExecutionTime.TotalMilliseconds:n0}";
	}

	public void RunOnAll(Action<T> target)
	{
		foreach (T work in workList)
		{
			target(work);
		}
	}
}
public class WorldSpaceGrid<T>
{
	public T[] Cells;

	public float CellSize;

	public float CellSizeHalf;

	public float CellSizeInverse;

	public float CellArea;

	public int CellCount;

	public float CellCountOffset;

	public T this[Vector3 worldPos]
	{
		get
		{
			return this[WorldToGridCoords(worldPos)];
		}
		set
		{
			this[WorldToGridCoords(worldPos)] = value;
		}
	}

	public T this[Vector2i cellCoords]
	{
		get
		{
			return this[cellCoords.x, cellCoords.y];
		}
		set
		{
			this[cellCoords.x, cellCoords.y] = value;
		}
	}

	public T this[int x, int y]
	{
		get
		{
			return Cells[y * CellCount + x];
		}
		set
		{
			Cells[y * CellCount + x] = value;
		}
	}

	public T this[int index]
	{
		get
		{
			return Cells[index];
		}
		set
		{
			Cells[index] = value;
		}
	}

	public WorldSpaceGrid(float gridSize, float cellSize, WorldSpaceGrid.RoundingMode rounding = WorldSpaceGrid.RoundingMode.Up)
	{
		CellSize = cellSize;
		CellSizeHalf = cellSize * 0.5f;
		CellSizeInverse = 1f / cellSize;
		CellArea = cellSize * cellSize;
		CellCount = WorldSpaceGrid.CalculateCellCount(gridSize, cellSize, rounding);
		CellCountOffset = (float)CellCount * 0.5f - 0.5f;
		Cells = new T[CellCount * CellCount];
	}

	public Vector2i IndexToGridCoords(int index)
	{
		int num = index / CellCount;
		return new Vector2i(index - num * CellCount, num);
	}

	public Vector3 IndexToWorldCoords(int index)
	{
		return GridToWorldCoords(IndexToGridCoords(index));
	}

	public Vector2i WorldToGridCoords(Vector3 worldPos)
	{
		int v = Mathf.RoundToInt(worldPos.x * CellSizeInverse + CellCountOffset);
		int v2 = Mathf.RoundToInt(worldPos.z * CellSizeInverse + CellCountOffset);
		int x = Mathx.Clamp(v, 0, CellCount - 1);
		v2 = Mathx.Clamp(v2, 0, CellCount - 1);
		return new Vector2i(x, v2);
	}

	public Vector3 GridToWorldCoords(Vector2i cellPos)
	{
		float x = ((float)cellPos.x - CellCountOffset) * CellSize;
		float z = ((float)cellPos.y - CellCountOffset) * CellSize;
		return new Vector3(x, 0f, z);
	}

	public void Copy(WorldSpaceGrid<T> other)
	{
		for (int i = 0; i < CellCount; i++)
		{
			for (int j = 0; j < CellCount; j++)
			{
				this[i, j] = other[i, j];
			}
		}
	}
}
public class WorldSpaceGrid
{
	public enum RoundingMode
	{
		Up,
		Down
	}

	public static int CalculateCellCount(float gridSize, float cellSize, RoundingMode rounding = RoundingMode.Up)
	{
		float num = 1f / cellSize;
		float f = Mathf.Max(gridSize, 1000f) * num;
		if (rounding == RoundingMode.Up)
		{
			return Mathf.CeilToInt(f);
		}
		return Mathf.FloorToInt(f);
	}

	public static Vector3 ClosestGridCell(Vector3 worldPos, float gridSize, float cellSize, RoundingMode rounding = RoundingMode.Up)
	{
		float num = 1f / cellSize;
		int num2 = CalculateCellCount(gridSize, cellSize, rounding);
		float num3 = (float)num2 * 0.5f - 0.5f;
		int v = Mathf.RoundToInt(worldPos.x * num + num3);
		int v2 = Mathf.RoundToInt(worldPos.z * num + num3);
		int num4 = Mathx.Clamp(v, 0, num2 - 1);
		v2 = Mathx.Clamp(v2, 0, num2 - 1);
		float x = ((float)num4 - num3) * cellSize;
		float z = ((float)v2 - num3) * cellSize;
		return new Vector3(x, 0f, z);
	}
}
public class CRC
{
	private static byte[] byteBuffer = new byte[1024];

	private static readonly uint[] crc32_tab = new uint[256]
	{
		0u, 1996959894u, 3993919788u, 2567524794u, 124634137u, 1886057615u, 3915621685u, 2657392035u, 249268274u, 2044508324u,
		3772115230u, 2547177864u, 162941995u, 2125561021u, 3887607047u, 2428444049u, 498536548u, 1789927666u, 4089016648u, 2227061214u,
		450548861u, 1843258603u, 4107580753u, 2211677639u, 325883990u, 1684777152u, 4251122042u, 2321926636u, 335633487u, 1661365465u,
		4195302755u, 2366115317u, 997073096u, 1281953886u, 3579855332u, 2724688242u, 1006888145u, 1258607687u, 3524101629u, 2768942443u,
		901097722u, 1119000684u, 3686517206u, 2898065728u, 853044451u, 1172266101u, 3705015759u, 2882616665u, 651767980u, 1373503546u,
		3369554304u, 3218104598u, 565507253u, 1454621731u, 3485111705u, 3099436303u, 671266974u, 1594198024u, 3322730930u, 2970347812u,
		795835527u, 1483230225u, 3244367275u, 3060149565u, 1994146192u, 31158534u, 2563907772u, 4023717930u, 1907459465u, 112637215u,
		2680153253u, 3904427059u, 2013776290u, 251722036u, 2517215374u, 3775830040u, 2137656763u, 141376813u, 2439277719u, 3865271297u,
		1802195444u, 476864866u, 2238001368u, 4066508878u, 1812370925u, 453092731u, 2181625025u, 4111451223u, 1706088902u, 314042704u,
		2344532202u, 4240017532u, 1658658271u, 366619977u, 2362670323u, 4224994405u, 1303535960u, 984961486u, 2747007092u, 3569037538u,
		1256170817u, 1037604311u, 2765210733u, 3554079995u, 1131014506u, 879679996u, 2909243462u, 3663771856u, 1141124467u, 855842277u,
		2852801631u, 3708648649u, 1342533948u, 654459306u, 3188396048u, 3373015174u, 1466479909u, 544179635u, 3110523913u, 3462522015u,
		1591671054u, 702138776u, 2966460450u, 3352799412u, 1504918807u, 783551873u, 3082640443u, 3233442989u, 3988292384u, 2596254646u,
		62317068u, 1957810842u, 3939845945u, 2647816111u, 81470997u, 1943803523u, 3814918930u, 2489596804u, 225274430u, 2053790376u,
		3826175755u, 2466906013u, 167816743u, 2097651377u, 4027552580u, 2265490386u, 503444072u, 1762050814u, 4150417245u, 2154129355u,
		426522225u, 1852507879u, 4275313526u, 2312317920u, 282753626u, 1742555852u, 4189708143u, 2394877945u, 397917763u, 1622183637u,
		3604390888u, 2714866558u, 953729732u, 1340076626u, 3518719985u, 2797360999u, 1068828381u, 1219638859u, 3624741850u, 2936675148u,
		906185462u, 1090812512u, 3747672003u, 2825379669u, 829329135u, 1181335161u, 3412177804u, 3160834842u, 628085408u, 1382605366u,
		3423369109u, 3138078467u, 570562233u, 1426400815u, 3317316542u, 2998733608u, 733239954u, 1555261956u, 3268935591u, 3050360625u,
		752459403u, 1541320221u, 2607071920u, 3965973030u, 1969922972u, 40735498u, 2617837225u, 3943577151u, 1913087877u, 83908371u,
		2512341634u, 3803740692u, 2075208622u, 213261112u, 2463272603u, 3855990285u, 2094854071u, 198958881u, 2262029012u, 4057260610u,
		1759359992u, 534414190u, 2176718541u, 4139329115u, 1873836001u, 414664567u, 2282248934u, 4279200368u, 1711684554u, 285281116u,
		2405801727u, 4167216745u, 1634467795u, 376229701u, 2685067896u, 3608007406u, 1308918612u, 956543938u, 2808555105u, 3495958263u,
		1231636301u, 1047427035u, 2932959818u, 3654703836u, 1088359270u, 936918000u, 2847714899u, 3736837829u, 1202900863u, 817233897u,
		3183342108u, 3401237130u, 1404277552u, 615818150u, 3134207493u, 3453421203u, 1423857449u, 601450431u, 3009837614u, 3294710456u,
		1567103746u, 711928724u, 3020668471u, 3272380065u, 1510334235u, 755167117u
	};

	private static readonly ulong[] crc64_tab = new ulong[256]
	{
		0uL, 8851949072701294969uL, 17703898145402589938uL, 10333669153493130123uL, 13851072938616403599uL, 13465927519055396854uL, 3857338458010461309uL, 5715195658523061508uL, 12333367839138578037uL, 15127763206205961996uL,
		6816212484437830791uL, 2612226237385041406uL, 7714676916020922618uL, 1281407202545942915uL, 11430391317046123016uL, 16463076249205199729uL, 9009731685717012353uL, 563108230357313272uL, 9851657908567506291uL, 17465080730062222346uL,
		13632424968875661582uL, 14404880506683019383uL, 5224452474770082812uL, 3627802401766982277uL, 15429353832041845236uL, 12463821128841762957uL, 2562814405091885830uL, 6433535930597116543uL, 1592294032496338811uL, 7836410910743637506uL,
		16404387395731993993uL, 11056451039949864176uL, 18019463371434024706uL, 9280105458721969787uL, 1126216460714626544uL, 8464919223366468745uL, 4190910634541279629uL, 4679640014836523252uL, 14959263154764675967uL, 13060872525739979270uL,
		5852729821509460343uL, 3161916214005835790uL, 11856275032257016709uL, 16019730051968187132uL, 10448904949540165624uL, 16994763621833383553uL, 7255604803533964554uL, 2191395843288271987uL, 9734813498046853251uL, 18285020776702097914uL,
		8262382231073956465uL, 608425843627928328uL, 5125628810183771660uL, 4465764294926438261uL, 12867071861194233086uL, 14432195567501024647uL, 3184588064992677622uL, 6262709589572306831uL, 15672821821487275012uL, 11770576130456212861uL,
		17008134862606432377uL, 10867599606483677440uL, 1853769023980628619uL, 7161174014982448114uL, 16103423924954344815uL, 11935289383220651030uL, 3083341959784644509uL, 5769757520242456292uL, 2252432921429253088uL, 7321251034957484697uL,
		16929838446732937490uL, 10388307452745547883uL, 8381821269082559258uL, 1047727658635319907uL, 9359280029673046504uL, 18102965619612993681uL, 13000435797616977301uL, 14894146905688698092uL, 4745161141923116903uL, 4252033715651608094uL,
		11705459643018920686uL, 15612384854998895511uL, 6323832428011671580uL, 3250108949404244325uL, 7082685524280996961uL, 1770671381070249240uL, 10951102161764411027uL, 17087309740654948330uL, 674072313427442843uL, 8323419547594995170uL,
		18224423522563763817uL, 9669888565606754064uL, 14511209607067929108uL, 12950765422787986285uL, 4382791686576543974uL, 5047054248884015519uL, 2696289253709771373uL, 6895947823530343188uL, 15049839570318909599uL, 12250835051042597350uL,
		16524764462147912930uL, 11496477575961038235uL, 1216851687255856656uL, 7654800921679748969uL, 10251257620367543320uL, 17625884659327141217uL, 8931528589852876522uL, 84259039178430355uL, 5655163293556783767uL, 3792978414742418414uL,
		13532134484260726885uL, 13912670750543257884uL, 6369176129985355244uL, 2502782282785952917uL, 12525419179144613662uL, 15495561035627234919uL, 10978437246791527267uL, 16321975555527844378uL, 7920669638525335953uL, 1671873238255513832uL,
		17531166746306175897uL, 9913345878835194592uL, 503231997654823275uL, 8945175932061546514uL, 3707538047961257238uL, 5308515798192249967uL, 14322348029964896228uL, 13554501644362141341uL, 10785157014839085493uL, 17254666630495879372uL,
		6925536469308201799uL, 1928669229005230654uL, 6166683919569289018uL, 3408106242218915395uL, 11539515040484912584uL, 15779741191858611377uL, 4504865842858506176uL, 4925828954283753145uL, 14642502069914969394uL, 12820884771576065099uL,
		18355716529793696079uL, 9540007361421969462uL, 796147016248169405uL, 8202193697865996996uL, 16763642538165118516uL, 10555343349626187597uL, 2095455317270639814uL, 7479631577382337983uL, 2926364910754730171uL, 5928137516128508354uL,
		15937228569359352393uL, 12102324735718361904uL, 4867406749023426625uL, 4131191115536978232uL, 13131477498808912563uL, 14763945261529023434uL, 9490322283846233806uL, 17972763431062038455uL, 8504067431303216188uL, 926884511990314309uL,
		8051711962477172407uL, 1541670979892322254uL, 11100683476643087429uL, 16201132341218348348uL, 12647664856023343160uL, 15374718365700663617uL, 6500217898808488650uL, 2372580570961558451uL, 14165371048561993922uL, 13712881572587659707uL,
		3541342762140498480uL, 5475551080882205513uL, 337036156713721421uL, 9112211761281881908uL, 17374189211922025663uL, 10071726351451997638uL, 1348144626854885686uL, 7524919785159454799uL, 16646839095189990340uL, 11375251796044276413uL,
		15171913658969673657uL, 12129609824107054784uL, 2827581646778391883uL, 6766067242130363442uL, 13374985906044110659uL, 14070668113165684282uL, 5489218623395763633uL, 3960334819262667976uL, 8765583373153087948uL, 251615998827411637uL,
		10094108497768031038uL, 17783882574922426951uL, 5392578507419542746uL, 3462768234654100899uL, 13791895647060686376uL, 14249064643987996497uL, 10011129131143811669uL, 17309264314385947436uL, 9177858264896848039uL, 398073508124084702uL,
		16284634862666717871uL, 11179858319785628630uL, 1463182455377365085uL, 7968614284679676196uL, 2433703374511713312uL, 6565738749404456281uL, 15309601843359497938uL, 12587227855704700843uL, 4025855981238586203uL, 5550341738321543714uL,
		14010231419946703273uL, 13309869690798280912uL, 17863057179705753044uL, 10177610780853122221uL, 168518078356860710uL, 8687094605961012831uL, 11310326587113567534uL, 16586241563491499095uL, 7585956829484836828uL, 1413790823389195941uL,
		6687492953022055329uL, 2744609311697881816uL, 12213303662187237715uL, 15250927976100943914uL, 12738352259970710488uL, 14564578711588090529uL, 5005564565571905834uL, 4588929132448424019uL, 8142317431333358935uL, 731591227688682542uL,
		9606093343850471333uL, 18417404465172059868uL, 2012927990619293101uL, 7005115709973351636uL, 17176652871151048543uL, 10702745209522052646uL, 15841339277050671906uL, 11605722277885901403uL, 3343746476511027664uL, 6106651831093618857uL,
		14830152191845028953uL, 13193075276920315168uL, 4071158715666679467uL, 4803046671925235666uL, 1006463995309646550uL, 8588326435575524271uL, 17890351864123093028uL, 9412308762883553629uL, 7415076095922514476uL, 2035579357833339733uL,
		10617031596384499934uL, 16829728831969243559uL, 12024401134718426275uL, 15854695815076877786uL, 6012200567359213137uL, 3006100283679606568uL
	};

	public static uint Compute32(uint crc, int i)
	{
		Union32 union = new Union32
		{
			i = i
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC32(crc, byteBuffer, 4uL);
	}

	public static uint Compute32(uint crc, uint u)
	{
		Union32 union = new Union32
		{
			u = u
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC32(crc, byteBuffer, 4uL);
	}

	public static uint Compute32(uint crc, float f)
	{
		Union32 union = new Union32
		{
			f = f
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC32(crc, byteBuffer, 4uL);
	}

	public static uint Compute32(uint crc, long i)
	{
		Union64 union = new Union64
		{
			i = i
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC32(crc, byteBuffer, 8uL);
	}

	public static uint Compute32(uint crc, ulong u)
	{
		Union64 union = new Union64
		{
			u = u
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC32(crc, byteBuffer, 8uL);
	}

	public static uint Compute32(uint crc, double f)
	{
		Union64 union = new Union64
		{
			f = f
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC32(crc, byteBuffer, 8uL);
	}

	public static uint Compute32(uint crc, string str)
	{
		int num = GrowByteBuffer(Encoding.UTF8.GetByteCount(str));
		Encoding.UTF8.GetBytes(str, 0, str.Length, byteBuffer, 0);
		return CRC32(crc, byteBuffer, (ulong)num);
	}

	public static uint Compute32(string[] strs)
	{
		uint num = 0u;
		for (int i = 0; i < strs.Length; i++)
		{
			int num2 = GrowByteBuffer(Encoding.UTF8.GetByteCount(strs[i]));
			Encoding.UTF8.GetBytes(strs[i], 0, strs[i].Length, byteBuffer, 0);
			num = CRC32(num, byteBuffer, (ulong)num2);
		}
		return num;
	}

	public static ulong Compute64(ulong crc, int i)
	{
		Union32 union = new Union32
		{
			i = i
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC64(crc, byteBuffer, 4uL);
	}

	public static ulong Compute64(ulong crc, uint u)
	{
		Union32 union = new Union32
		{
			u = u
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC64(crc, byteBuffer, 4uL);
	}

	public static ulong Compute64(ulong crc, float f)
	{
		Union32 union = new Union32
		{
			f = f
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		return CRC64(crc, byteBuffer, 4uL);
	}

	public static ulong Compute64(ulong crc, long i)
	{
		Union64 union = new Union64
		{
			i = i
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC64(crc, byteBuffer, 8uL);
	}

	public static ulong Compute64(ulong crc, ulong u)
	{
		Union64 union = new Union64
		{
			u = u
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC64(crc, byteBuffer, 8uL);
	}

	public static ulong Compute64(ulong crc, double f)
	{
		Union64 union = new Union64
		{
			f = f
		};
		byteBuffer[0] = union.b1;
		byteBuffer[1] = union.b2;
		byteBuffer[2] = union.b3;
		byteBuffer[3] = union.b4;
		byteBuffer[4] = union.b5;
		byteBuffer[5] = union.b6;
		byteBuffer[6] = union.b7;
		byteBuffer[7] = union.b8;
		return CRC64(crc, byteBuffer, 8uL);
	}

	public static ulong Compute64(ulong crc, string str)
	{
		int num = GrowByteBuffer(Encoding.UTF8.GetByteCount(str));
		Encoding.UTF8.GetBytes(str, 0, str.Length, byteBuffer, 0);
		return CRC64(crc, byteBuffer, (ulong)num);
	}

	public static ulong Compute64(string[] strs)
	{
		ulong num = 0uL;
		for (int i = 0; i < strs.Length; i++)
		{
			int num2 = GrowByteBuffer(Encoding.UTF8.GetByteCount(strs[i]));
			Encoding.UTF8.GetBytes(strs[i], 0, strs[i].Length, byteBuffer, 0);
			num = CRC64(num, byteBuffer, (ulong)num2);
		}
		return num;
	}

	private static int GrowByteBuffer(int count)
	{
		if (count > byteBuffer.Length)
		{
			byteBuffer = new byte[count];
		}
		return count;
	}

	private static uint CRC32(uint value, byte[] buf, ulong size)
	{
		uint num = 0u;
		uint num2 = value ^ 0xFFFFFFFFu;
		while (size-- != 0)
		{
			num2 = crc32_tab[(num2 ^ buf[num++]) & 0xFF] ^ (num2 >> 8);
		}
		return num2 ^ 0xFFFFFFFFu;
	}

	private static ulong CRC64(ulong value, byte[] buf, ulong size)
	{
		uint num = 0u;
		ulong num2 = value ^ 0xFFFFFFFFFFFFFFFFuL;
		while (size-- != 0)
		{
			num2 = crc64_tab[(num2 ^ buf[num++]) & 0xFF] ^ (num2 >> 8);
		}
		return num2 ^ 0xFFFFFFFFFFFFFFFFuL;
	}
}
public static class FloatEx
{
	public static bool IsNaNOrInfinity(this float f)
	{
		if (!float.IsNaN(f))
		{
			return float.IsInfinity(f);
		}
		return true;
	}

	public static bool IsNaNOrInfinity(this double d)
	{
		if (!double.IsNaN(d))
		{
			return double.IsInfinity(d);
		}
		return true;
	}

	public static bool IsNaN(this float f)
	{
		return float.IsNaN(f);
	}

	public static bool IsNaN(this double d)
	{
		return double.IsNaN(d);
	}

	public static bool IsInfinity(this float f)
	{
		return float.IsInfinity(f);
	}

	public static bool IsInfinity(this double d)
	{
		return double.IsInfinity(d);
	}
}
public static class ListEx
{
	public static void Populate<T>(T[] arr, T value)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = value;
		}
	}

	public static T GetRandom<T>(this List<T> list)
	{
		if (list == null || list.Count == 0)
		{
			return default(T);
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T GetRandom<T>(this List<T> list, uint seed)
	{
		if (list == null || list.Count == 0)
		{
			return default(T);
		}
		return list[SeedRandom.Range(ref seed, 0, list.Count)];
	}

	public static T GetRandom<T>(this List<T> list, ref uint seed)
	{
		if (list == null || list.Count == 0)
		{
			return default(T);
		}
		return list[SeedRandom.Range(ref seed, 0, list.Count)];
	}

	public static void Shuffle<T>(this List<T> list, uint seed)
	{
		list.Shuffle(ref seed);
	}

	public static void Shuffle<T>(this List<T> list, ref uint seed)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int index = SeedRandom.Range(ref seed, 0, list.Count);
			int index2 = SeedRandom.Range(ref seed, 0, list.Count);
			T value = list[index];
			list[index] = list[index2];
			list[index2] = value;
		}
	}

	public static void BubbleSort<T>(this List<T> list) where T : IComparable<T>
	{
		for (int i = 1; i < list.Count; i++)
		{
			T value = list[i];
			for (int num = i - 1; num >= 0; num--)
			{
				T val = list[num];
				if (value.CompareTo(val) >= 0)
				{
					break;
				}
				list[num + 1] = val;
				list[num] = value;
			}
		}
	}

	public static void RemoveUnordered<T>(this List<T> list, int index)
	{
		list[index] = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
	}

	public static double TruncatedAverage(this List<double> list, float pct)
	{
		int num = (int)Math.Floor((float)list.Count * pct);
		return list.OrderBy((double x) => x).Skip(num).Take(list.Count - num * 2)
			.Average();
	}

	public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
	{
		return (from x in source.Select((T x, int i) => new
			{
				Index = i,
				Value = x
			})
			group x by x.Index / chunkSize into x
			select x.Select(v => v.Value).ToList()).ToList();
	}
}
public static class StringBuilderEx
{
	public static void AppendPadded(this StringBuilder sb, int n, int pad)
	{
		sb.Append(' ', Mathf.Max(pad - n.Digits(), 0));
		sb.Append(n);
	}

	public static void AppendPadded(this StringBuilder sb, uint n, int pad)
	{
		sb.Append(' ', Mathf.Max(pad - n.Digits(), 0));
		sb.Append(n);
	}

	public static void AppendPadded(this StringBuilder sb, long n, int pad)
	{
		sb.AppendPadded((int)n, pad);
	}

	public static void AppendPadded(this StringBuilder sb, ulong n, int pad)
	{
		sb.AppendPadded((uint)n, pad);
	}

	public static void AppendPadded(this StringBuilder sb, string s, int pad)
	{
		sb.Append(s);
		sb.Append(' ', Mathf.Max(pad - s.Length, 0));
	}

	public static void Clear(this StringBuilder value)
	{
		value.Length = 0;
	}
}
public class HorizontalAttribute : PropertyAttribute
{
	public int count;

	public int label;

	public HorizontalAttribute(int count, int label = -1)
	{
		this.count = count;
		this.label = label;
	}
}
public interface ICustomMaterialReplacer
{
}
public class InspectorFlagsAttribute : PropertyAttribute
{
}
public interface IEditorComponent
{
}
public interface IServerComponent
{
}
public interface IClientComponent
{
}
public interface IPrefabPreProcess
{
	bool CanRunDuringBundling { get; }

	void PreProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling);
}
public interface IPrefabPostProcess
{
	void PostProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling);
}
public interface IPrefabProcessor
{
	void RemoveComponent(UnityEngine.Component component);

	void NominateForDeletion(GameObject obj);

	void MarkPropertiesDirty(UnityEngine.Object obj);
}
public interface IServerComponentEx
{
	void PreServerComponentCull(IPrefabProcessor p);
}
public interface IClientComponentEx
{
	void PreClientComponentCull(IPrefabProcessor p);
}
public interface IPropRenderNotify
{
}
public class MaterialReplacement : MonoBehaviour
{
	private bool initialized;

	public Material[] Default;

	public Material[] Override;

	public Renderer Renderer;

	public static void ReplaceRecursive(GameObject go, Material mat)
	{
		List<Renderer> obj = Pool.Get<List<Renderer>>();
		go.transform.GetComponentsInChildren(includeInactive: true, obj);
		foreach (Renderer item in obj)
		{
			if (!(item is ParticleSystemRenderer) && !item.gameObject.CompareTag("IgnoreSkinReplacement"))
			{
				MaterialReplacement orAddComponent = item.transform.GetOrAddComponent<MaterialReplacement>();
				orAddComponent.Init();
				orAddComponent.Replace(mat);
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	public static void ReplaceRecursive(GameObject obj, Material[] find, Material[] replace)
	{
		if (find.Length != replace.Length)
		{
			UnityEngine.Debug.LogWarning($"Incorrect material array length: {find.Length} != {replace.Length} {obj.name}");
			return;
		}
		List<Renderer> obj2 = Pool.Get<List<Renderer>>();
		obj.GetComponentsInChildren(includeInactive: true, obj2);
		foreach (Renderer item in obj2)
		{
			if (!(item is ParticleSystemRenderer) && MaterialsContainAny(item.sharedMaterials, find) && !item.gameObject.CompareTag("IgnoreSkinReplacement"))
			{
				MaterialReplacement orAddComponent = item.transform.GetOrAddComponent<MaterialReplacement>();
				orAddComponent.Init();
				orAddComponent.Revert();
				orAddComponent.Replace(find, replace);
			}
		}
		Pool.FreeUnmanaged(ref obj2);
	}

	public static void Prepare(GameObject go)
	{
		List<Renderer> obj = Pool.Get<List<Renderer>>();
		go.GetComponentsInChildren(includeInactive: true, obj);
		foreach (Renderer item in obj)
		{
			if (!(item is ParticleSystemRenderer) && !item.gameObject.CompareTag("IgnoreSkinReplacement"))
			{
				item.transform.GetOrAddComponent<MaterialReplacement>().Init();
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	public static bool MaterialsContainAny(Material[] source, Material[] find)
	{
		for (int i = 0; i < source.Length; i++)
		{
			if (find.Contains(source[i]))
			{
				return true;
			}
		}
		return false;
	}

	public static void Reset(GameObject go)
	{
		List<MaterialReplacement> obj = Pool.Get<List<MaterialReplacement>>();
		go.GetComponentsInChildren(includeInactive: true, obj);
		foreach (MaterialReplacement item in obj)
		{
			item.Revert();
		}
		Pool.FreeUnmanaged(ref obj);
	}

	public void Init()
	{
		if (!initialized)
		{
			initialized = true;
			Renderer = GetComponent<Renderer>();
			Default = Renderer.sharedMaterials;
			Override = new Material[Default.Length];
			Array.Copy(Default, Override, Default.Length);
		}
	}

	private void Replace(Material mat)
	{
		if ((bool)Renderer)
		{
			for (int i = 0; i < Override.Length; i++)
			{
				Override[i] = mat;
			}
			Renderer.sharedMaterials = Override;
		}
	}

	private void Replace(Material find, Material replace)
	{
		if (!Renderer)
		{
			return;
		}
		for (int i = 0; i < Default.Length; i++)
		{
			if (find == Default[i])
			{
				Override[i] = replace;
			}
		}
		Renderer.sharedMaterials = Override;
	}

	private void Replace(Material[] find, Material[] replace)
	{
		if (!Renderer)
		{
			return;
		}
		int num = Mathf.Min(find.Length, replace.Length);
		for (int i = 0; i < Default.Length; i++)
		{
			for (int j = 0; j < num; j++)
			{
				if (find[j] == Default[i])
				{
					Override[i] = replace[j];
				}
			}
		}
		Renderer.sharedMaterials = Override;
		if (Renderer.enabled)
		{
			Renderer.enabled = false;
			Renderer.enabled = true;
		}
	}

	private void Revert()
	{
		if ((object)Renderer != null)
		{
			Renderer.sharedMaterials = Default;
			Array.Copy(Default, Override, Default.Length);
		}
	}
}
public struct AABB
{
	public Vector3 position;

	public Vector3 extents;

	public AABB(Vector3 position, Vector3 size)
	{
		this.position = position;
		extents = size * 0.5f;
	}

	public bool Contains(Vector3 target)
	{
		return ClosestPoint(target) == target;
	}

	public Vector3 ClosestPoint(Vector3 target)
	{
		Vector3 result = target;
		float num = position.x - extents.x;
		float num2 = position.x + extents.x;
		if (target.x < num)
		{
			result.x = num;
		}
		else if (target.x > num2)
		{
			result.x = num2;
		}
		float num3 = position.y - extents.y;
		float num4 = position.y + extents.y;
		if (target.y < num3)
		{
			result.y = num3;
		}
		else if (target.y > num4)
		{
			result.y = num4;
		}
		float num5 = position.z - extents.z;
		float num6 = position.z + extents.z;
		if (target.z < num5)
		{
			result.z = num5;
		}
		else if (target.z > num6)
		{
			result.z = num6;
		}
		return result;
	}

	public bool Trace(Ray ray, out RaycastHit hit, float radius = 0f, float maxDistance = float.PositiveInfinity)
	{
		Vector3 vector = position - (extents + Vector3.one * radius);
		Vector3 vector2 = position + (extents + Vector3.one * radius);
		hit = default(RaycastHit);
		float num = (vector.x - ray.origin.x) / ray.direction.x;
		float num2 = (vector2.x - ray.origin.x) / ray.direction.x;
		float num3 = (vector.y - ray.origin.y) / ray.direction.y;
		float num4 = (vector2.y - ray.origin.y) / ray.direction.y;
		float num5 = (vector.z - ray.origin.z) / ray.direction.z;
		float num6 = (vector2.z - ray.origin.z) / ray.direction.z;
		float num7 = ((num < num2) ? num : num2);
		float num8 = ((num3 < num4) ? num3 : num4);
		float num9 = ((num5 < num6) ? num5 : num6);
		float num10 = ((num > num2) ? num : num2);
		float num11 = ((num3 > num4) ? num3 : num4);
		float num12 = ((num5 > num6) ? num5 : num6);
		float num13 = ((num7 > num8) ? num7 : num8);
		float num14 = ((num10 < num11) ? num10 : num11);
		float num15 = ((num13 > num9) ? num13 : num9);
		float num16 = ((num14 < num12) ? num14 : num12);
		float num17 = ((num16 < 0f || num15 > num16) ? (-1f) : num15);
		if (num17 < 0f)
		{
			return false;
		}
		if (num17 == num)
		{
			hit.normal = new Vector3(-1f, 0f, 0f);
		}
		else if (num17 == num2)
		{
			hit.normal = new Vector3(1f, 0f, 0f);
		}
		else if (num17 == num3)
		{
			hit.normal = new Vector3(0f, -1f, 0f);
		}
		else if (num17 == num4)
		{
			hit.normal = new Vector3(0f, 1f, 0f);
		}
		else if (num17 == num5)
		{
			hit.normal = new Vector3(0f, 0f, -1f);
		}
		else
		{
			hit.normal = new Vector3(0f, 0f, 1f);
		}
		hit.point = ray.origin + ray.direction * num17 - hit.normal * radius;
		Vector3 vector3 = hit.point - position;
		vector3.x = Mathf.Clamp(vector3.x, 0f - extents.x, extents.x);
		vector3.y = Mathf.Clamp(vector3.y, 0f - extents.y, extents.y);
		vector3.z = Mathf.Clamp(vector3.z, 0f - extents.z, extents.z);
		hit.point = position + vector3;
		hit.distance = Vector3.Distance(ray.origin, hit.point);
		return hit.distance < maxDistance;
	}
}
public struct Capsule
{
	public Vector3 position;

	public float radius;

	public float extent;

	public Capsule(Vector3 position, float radius, float extent)
	{
		this.position = position;
		this.radius = radius;
		this.extent = extent;
	}

	public bool Contains(Vector3 target)
	{
		return ClosestPoint(target) == target;
	}

	public Vector3 ClosestPoint(Vector3 target)
	{
		Vector3 result = target;
		Vector3 v = target - position;
		float num = v.Magnitude2D();
		if (num > radius)
		{
			float num2 = radius / num;
			result.x = position.x + v.x * num2;
			result.z = position.z + v.z * num2;
		}
		result.y = Mathf.Clamp(target.y, position.y - extent, position.y + extent);
		return result;
	}

	public void Move(Vector3 direction, float distance, int layerMask = 0)
	{
		Vector3 point = position + Vector3.up * (extent - radius);
		Vector3 point2 = position - Vector3.up * (extent - radius);
		if (layerMask != 0 && Physics.CapsuleCast(point, point2, radius, direction, out var hitInfo, distance, layerMask))
		{
			position += direction * hitInfo.distance;
		}
		else
		{
			position += direction * distance;
		}
	}

	public bool Trace(Ray ray, out RaycastHit hit, float rayRadius = 0f, float maxDistance = float.PositiveInfinity)
	{
		hit = default(RaycastHit);
		float num = radius + rayRadius;
		float num2 = extent + rayRadius - num;
		Vector3 vector = position + Vector3.down * num2;
		Vector3 vector2 = position + Vector3.up * num2;
		if (num2 < Mathf.Epsilon * 2f)
		{
			return new Sphere(position, radius).Trace(ray, out hit, maxDistance);
		}
		Vector3 vector3 = vector2 - vector;
		Vector3 vector4 = ray.origin - vector;
		float num3 = Vector3.Dot(vector3, vector3);
		float num4 = Vector3.Dot(vector3, ray.direction);
		float num5 = Vector3.Dot(vector3, vector4);
		float num6 = Vector3.Dot(ray.direction, vector4);
		float num7 = Vector3.Dot(vector4, vector4);
		float num8 = num3 - num4 * num4;
		float num9 = num3 * num6 - num5 * num4;
		float num10 = num3 * num7 - num5 * num5 - num * num * num3;
		float num11 = num9 * num9 - num8 * num10;
		if ((double)num11 >= 0.0)
		{
			float num12 = (0f - num9 - Mathf.Sqrt(num11)) / num8;
			float num13 = num5 + num12 * num4;
			if ((double)num13 > 0.0 && num13 < num3)
			{
				hit.distance = num12;
				if (hit.distance < 0f || hit.distance > maxDistance)
				{
					return false;
				}
				hit.point = ray.origin + ray.direction * num12;
				Vector3 vector5 = hit.point - vector;
				float num14 = Mathf.Clamp(Vector3.Dot(vector5, vector3) / Vector3.Dot(vector3, vector3), 0f, 1f);
				hit.normal = (vector5 - num14 * vector3) / num;
				hit.point -= hit.normal * rayRadius;
				return true;
			}
			Vector3 vector6 = (((double)num13 <= 0.0) ? vector4 : (ray.origin - vector2));
			num9 = Vector3.Dot(ray.direction, vector6);
			num10 = Vector3.Dot(vector6, vector6) - num * num;
			num11 = num9 * num9 - num10;
			if ((double)num11 > 0.0)
			{
				hit.distance = 0f - num9 - Mathf.Sqrt(num11);
				if (hit.distance < 0f || hit.distance > maxDistance)
				{
					return false;
				}
				hit.point = ray.origin + ray.direction * hit.distance;
				Vector3 vector7 = hit.point - vector;
				float num15 = Mathf.Clamp(Vector3.Dot(vector7, vector3) / Vector3.Dot(vector3, vector3), 0f, 1f);
				hit.normal = (vector7 - num15 * vector3) / num;
				hit.point -= hit.normal * rayRadius;
				hit.distance = hit.distance;
				return true;
			}
		}
		return false;
	}
}
public struct Cylinder
{
	public Vector3 position;

	public float radius;

	public float extent;

	public Cylinder(Vector3 position, float radius, float extent)
	{
		this.position = position;
		this.radius = radius;
		this.extent = extent;
	}

	public bool Contains(Vector3 target)
	{
		return ClosestPoint(target) == target;
	}

	public Vector3 ClosestPoint(Vector3 target)
	{
		Vector3 result = target;
		Vector3 v = target - position;
		float num = v.Magnitude2D();
		if (num > radius)
		{
			float num2 = radius / num;
			result.x = position.x + v.x * num2;
			result.z = position.z + v.z * num2;
		}
		result.y = Mathf.Clamp(target.y, position.y - extent, position.y + extent);
		return result;
	}
}
public static class GJK
{
	private static readonly ProfilerMarker p_Distance = new ProfilerMarker("GJK.Distance");

	private static readonly ProfilerMarker p_SolveDistance = new ProfilerMarker("Simplex.SolveDistance");

	private static readonly ProfilerMarker p_GetSupportingVertex = new ProfilerMarker("GetSupportingVertex");

	private const float MaxIterations = 32f;

	public static float Distance(OBB a, OBB b)
	{
		using (p_Distance.Auto())
		{
			Span<Vector3> span = stackalloc Vector3[8];
			Span<Vector3> span2 = stackalloc Vector3[8];
			PopulateVertices(a, span);
			PopulateVertices(b, span2);
			return DistanceInternal((ReadOnlySpan<Vector3>)span, (ReadOnlySpan<Vector3>)span2);
		}
	}

	public static float Distance2(OBB a, OBB b)
	{
		using (p_Distance.Auto())
		{
			Span<Vector3> span = stackalloc Vector3[8];
			Span<Vector3> span2 = stackalloc Vector3[8];
			PopulateVertices(a, span);
			PopulateVertices(b, span2);
			return Distance2Internal((ReadOnlySpan<Vector3>)span, (ReadOnlySpan<Vector3>)span2);
		}
	}

	private static float DistanceInternal(in ReadOnlySpan<Vector3> a, in ReadOnlySpan<Vector3> b)
	{
		SolveDistanceSimplex(in a, in b, out var simplex);
		float num = Vector3.SqrMagnitude(simplex.Direction);
		float num2 = 1f / Mathf.Sqrt(num);
		if (num != 0f)
		{
			return simplex.ScaledDistance * num2;
		}
		return 0f;
	}

	private static float Distance2Internal(in ReadOnlySpan<Vector3> a, in ReadOnlySpan<Vector3> b)
	{
		SolveDistanceSimplex(in a, in b, out var simplex);
		float num = Vector3.SqrMagnitude(simplex.Direction);
		if (num != 0f)
		{
			return simplex.ScaledDistance * simplex.ScaledDistance / num;
		}
		return 0f;
	}

	private static void SolveDistanceSimplex(in ReadOnlySpan<Vector3> a, in ReadOnlySpan<Vector3> b, out Simplex simplex)
	{
		Assert.IsTrue(a.Length > 0, "Distance function called with empty LHS collider");
		Assert.IsTrue(b.Length > 0, "Distance function called with empty RHS collider");
		Simplex simplex2 = new Simplex
		{
			size = 1
		};
		Vector3 direction = Vector3.up;
		simplex2.a = GetSupportingVertex(in a, in b, in direction);
		simplex = simplex2;
		simplex.Direction = simplex.a;
		simplex.ScaledDistance = Vector3.SqrMagnitude(simplex.a);
		float num = simplex.ScaledDistance;
		for (int i = 0; (float)i < 32f; i++)
		{
			direction = -simplex.Direction;
			Vector3 supportingVertex = GetSupportingVertex(in a, in b, in direction);
			float num2 = Vector3.Dot(simplex.a - supportingVertex, simplex.Direction);
			if (!(num2 * Mathf.Abs(num2) < 1E-08f * num))
			{
				switch (simplex.size++)
				{
				case 1:
					simplex.b = supportingVertex;
					break;
				case 2:
					simplex.c = supportingVertex;
					break;
				default:
					simplex.d = supportingVertex;
					break;
				}
				using (p_SolveDistance.Auto())
				{
					simplex.SolveDistance();
				}
				num = Vector3.SqrMagnitude(simplex.Direction);
				if (simplex.size == 4)
				{
					simplex.ScaledDistance = 0f;
					break;
				}
				continue;
			}
			break;
		}
	}

	private static Vector3 Support(in ReadOnlySpan<Vector3> vertices, in Vector3 direction)
	{
		float num = float.MinValue;
		Vector3 result = Vector3.zero;
		ReadOnlySpan<Vector3> readOnlySpan = vertices;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			Vector3 vector = readOnlySpan[i];
			float num2 = Vector3.Dot(vector, direction);
			if (num2 > num)
			{
				num = num2;
				result = vector;
			}
		}
		return result;
	}

	private static Vector3 GetSupportingVertex(in ReadOnlySpan<Vector3> verticesA, in ReadOnlySpan<Vector3> verticesB, in Vector3 direction)
	{
		using (p_GetSupportingVertex.Auto())
		{
			Vector3 vector = Support(in verticesA, in direction);
			Vector3 direction2 = -direction;
			Vector3 vector2 = Support(in verticesB, in direction2);
			direction2 = vector - vector2;
			return direction2;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void PopulateVertices(OBB obb, Span<Vector3> vertices)
	{
		Assert.IsTrue(vertices.Length >= 8);
		Vector3 position = obb.position;
		Vector3 right = obb.right;
		Vector3 up = obb.up;
		Vector3 forward = obb.forward;
		Vector3 extents = obb.extents;
		vertices[0] = position + right * extents.x + up * extents.y + forward * extents.z;
		vertices[1] = position + right * extents.x + up * extents.y - forward * extents.z;
		vertices[2] = position + right * extents.x - up * extents.y + forward * extents.z;
		vertices[3] = position + right * extents.x - up * extents.y - forward * extents.z;
		vertices[4] = position - right * extents.x + up * extents.y + forward * extents.z;
		vertices[5] = position - right * extents.x + up * extents.y - forward * extents.z;
		vertices[6] = position - right * extents.x - up * extents.y + forward * extents.z;
		vertices[7] = position - right * extents.x - up * extents.y - forward * extents.z;
	}
}
internal struct Simplex
{
	public Vector3 a;

	public Vector3 b;

	public Vector3 c;

	public Vector3 d;

	public int size;

	public Vector3 Direction;

	public float ScaledDistance;

	public void SolveDistance()
	{
		switch (size)
		{
		case 1:
			Point();
			break;
		case 2:
			Line();
			break;
		case 3:
			Triangle();
			break;
		case 4:
			Tetrahedron();
			break;
		}
	}

	private void DebugDraw()
	{
		Gizmos.color = size switch
		{
			1 => Color.blue, 
			2 => Color.red, 
			3 => Color.green, 
			4 => Color.magenta, 
			_ => Color.black, 
		};
		switch (size)
		{
		case 1:
			Gizmos.DrawWireSphere(a, 0.03f);
			break;
		case 2:
			Gizmos.DrawWireSphere(a, 0.03f);
			Gizmos.DrawWireSphere(b, 0.03f);
			Gizmos.DrawLine(a, b);
			break;
		case 3:
			Gizmos.DrawWireSphere(a, 0.03f);
			Gizmos.DrawWireSphere(b, 0.03f);
			Gizmos.DrawWireSphere(c, 0.03f);
			Gizmos.DrawLine(a, b);
			Gizmos.DrawLine(b, c);
			Gizmos.DrawLine(c, a);
			break;
		case 4:
			Gizmos.DrawWireSphere(a, 0.03f);
			Gizmos.DrawWireSphere(b, 0.03f);
			Gizmos.DrawWireSphere(c, 0.03f);
			Gizmos.DrawWireSphere(d, 0.03f);
			Gizmos.DrawLine(a, b);
			Gizmos.DrawLine(a, c);
			Gizmos.DrawLine(a, d);
			Gizmos.DrawLine(b, c);
			Gizmos.DrawLine(b, d);
			Gizmos.DrawLine(d, c);
			break;
		}
	}

	private void Point()
	{
		Direction = a;
		ScaledDistance = Vector3.SqrMagnitude(Direction);
	}

	private void Line()
	{
		Vector3 vector = b - a;
		float num = Vector3.Dot(vector, vector);
		if (Vector3.Dot(-a, vector) >= num)
		{
			size = 1;
			a = b;
			Point();
		}
		else
		{
			Direction = Vector3.Cross(Vector3.Cross(vector, a), vector);
			ScaledDistance = Vector3.Dot(Direction, a);
		}
	}

	private void Triangle()
	{
		Vector3 rhs = a - c;
		Vector3 lhs = b - c;
		Vector3 vector = Vector3.Cross(lhs, rhs);
		Vector3 lhs2 = Vector3.Cross(lhs, vector);
		Vector3 lhs3 = Vector3.Cross(vector, rhs);
		float num = Vector3.Dot(lhs2, b);
		float num2 = Vector3.Dot(lhs3, c);
		if (num < 0f)
		{
			if (num2 >= 0f || Det(vector, lhs2, c) < 0f)
			{
				a = b;
			}
		}
		else if (num2 >= 0f)
		{
			float num3 = Vector3.Dot(c, vector);
			if (num3 < 0f)
			{
				Vector3 vector2 = b;
				Vector3 vector3 = a;
				a = vector2;
				b = vector3;
				vector = -vector;
				num3 = 0f - num3;
			}
			Direction = vector;
			ScaledDistance = num3;
			return;
		}
		b = c;
		size = 2;
		Line();
	}

	private void Tetrahedron()
	{
		Vector3 lhs = Vector3.Cross(a, b);
		Vector3 lhs2 = Vector3.Cross(b, c);
		Vector3 lhs3 = Vector3.Cross(c, a);
		Vector3 vector = new Vector3(Vector3.Dot(lhs, d), Vector3.Dot(lhs2, d), Vector3.Dot(lhs3, d));
		if (vector.x >= -1E-05f && vector.y >= -1E-05f && vector.z >= -1E-05f)
		{
			Direction = Vector3.zero;
			return;
		}
		bool flag = Vector3.Dot(lhs, d) >= 0f;
		bool flag2 = Vector3.Dot(lhs2, d) >= 0f;
		bool flag3 = Vector3.Dot(lhs3, d) >= 0f;
		Vector3 vector2 = d - a;
		Vector3 vector3 = d - b;
		Vector3 vector4 = d - c;
		Vector3 vector5 = Vector3.Cross(vector2, vector3);
		Vector3 vector6 = Vector3.Cross(vector3, vector4);
		Vector3 vector7 = Vector3.Cross(vector4, vector2);
		bool flag4 = Det(vector5, vector2, d) >= 0f;
		bool flag5 = Det(vector6, vector3, d) >= 0f;
		bool flag6 = Det(vector7, vector4, d) >= 0f;
		bool flag7 = Det(vector3, vector5, d) >= 0f;
		bool flag8 = Det(vector4, vector6, d) >= 0f;
		bool flag9 = Det(vector2, vector7, d) >= 0f;
		bool num = flag4 && flag7 && !flag;
		bool flag10 = flag5 && flag8 && !flag2;
		bool flag11 = flag6 && flag9 && !flag3;
		if (num || flag10 || flag11)
		{
			if (flag10)
			{
				a = b;
				b = c;
			}
			else if (flag11)
			{
				b = c;
			}
		}
		else
		{
			bool flag12 = Vector3.Dot(vector2, d) >= 0f;
			bool flag13 = Vector3.Dot(vector3, d) >= 0f;
			bool flag14 = Vector3.Dot(vector4, d) >= 0f;
			bool num2 = !flag4 && !flag9 && flag12;
			bool flag15 = !flag5 && !flag7 && flag13;
			bool flag16 = !flag6 && !flag8 && flag14;
			if (num2 || flag15 || flag16)
			{
				a = b;
				b = c;
			}
		}
		c = d;
		size = 3;
		Triangle();
	}

	private static float Det(Vector3 a, Vector3 b, Vector3 c)
	{
		return Vector3.Dot(Vector3.Cross(a, b), c);
	}
}
public struct Line
{
	public Vector3 point0;

	public Vector3 point1;

	public float Length => Vector3.Distance(point0, point1);

	public float LengthSqr => Vector3.SqrMagnitude(point1 - point0);

	public Line(Vector3 point0, Vector3 point1)
	{
		this.point0 = point0;
		this.point1 = point1;
	}

	public Line(Vector3 origin, Vector3 direction, float length)
	{
		point0 = origin;
		point1 = origin + direction * length;
	}

	public bool Trace(Ray ray, float radius, out RaycastHit hit, float maxDistance = float.PositiveInfinity)
	{
		hit = default(RaycastHit);
		if (radius <= 0f)
		{
			return false;
		}
		Vector3 vector = point1 - point0;
		Vector3 direction = ray.direction;
		Vector3 rhs = point0 - ray.origin;
		float num = Vector3.Dot(vector, vector);
		float num2 = Vector3.Dot(vector, direction);
		float num3 = Vector3.Dot(direction, rhs);
		float num4 = num - num2 * num2;
		float num5 = 0f;
		float num6 = num3;
		if (num4 >= Mathf.Epsilon)
		{
			float num7 = Vector3.Dot(vector, rhs);
			float num8 = 1f / num4;
			num5 = num8 * (num2 * num3 - num7);
			num6 = num8 * (num * num3 - num2 * num7);
			num5 = Mathf.Clamp01(num5);
		}
		if (num6 < 0f || num6 > maxDistance)
		{
			return false;
		}
		Vector3 vector2 = point0 + num5 * vector;
		Vector3 vector3 = ray.origin + num6 * direction - vector2;
		float magnitude = vector3.magnitude;
		if (magnitude > radius)
		{
			return false;
		}
		hit.point = vector2;
		hit.normal = vector3 / magnitude;
		hit.distance = Vector3.Distance(ray.origin, hit.point);
		return true;
	}

	public Vector3 ClosestPoint(Vector3 pos)
	{
		Vector3 vector = point1 - point0;
		float magnitude = vector.magnitude;
		if (magnitude < 0.001f)
		{
			return point0;
		}
		Vector3 vector2 = vector / magnitude;
		return point0 + Mathf.Clamp(Vector3.Dot(pos - point0, vector2), 0f, magnitude) * vector2;
	}

	public Vector3 ClosestPoint2D(Vector3 pos)
	{
		Vector3 vector = point1 - point0;
		float magnitude = vector.magnitude;
		if (magnitude < 0.001f)
		{
			return point0;
		}
		float num = vector.Magnitude2D();
		Vector3 vector2 = vector / magnitude;
		return point0 + Mathf.Clamp(Vector3.Dot(pos.XZ3D() - point0.XZ3D(), vector2.XZ3D()), 0f, num) * (magnitude / num) * vector2;
	}

	public float Distance(Vector3 pos)
	{
		return (pos - ClosestPoint(pos)).magnitude;
	}

	public float SqrDistance(Vector3 pos)
	{
		return (pos - ClosestPoint(pos)).sqrMagnitude;
	}

	public Vector3 ClosestPoint(in Line other)
	{
		ClosestPoints(in other, out var closestThis, out var _);
		return closestThis;
	}

	public void ClosestPoints(in Line other, out Vector3 closestThis, out Vector3 closestOther)
	{
		Vector3 vector = point1 - point0;
		Vector3 vector2 = other.point1 - other.point0;
		Vector3 rhs = point0 - other.point0;
		float sqrMagnitude = vector.sqrMagnitude;
		float sqrMagnitude2 = vector2.sqrMagnitude;
		float num = Vector3.Dot(vector2, rhs);
		if (sqrMagnitude <= Mathf.Epsilon && sqrMagnitude2 <= Mathf.Epsilon)
		{
			closestThis = point0;
			closestOther = other.point0;
			return;
		}
		float num2 = 0f;
		float num3 = 0f;
		if (sqrMagnitude <= Mathf.Epsilon)
		{
			num2 = 0f;
			num3 = Mathf.Clamp01(num / sqrMagnitude2);
		}
		else
		{
			float num4 = Vector3.Dot(vector, rhs);
			if (sqrMagnitude2 <= Mathf.Epsilon)
			{
				num3 = 0f;
				num2 = Mathf.Clamp01((0f - num4) / sqrMagnitude);
			}
			else
			{
				float num5 = Vector3.Dot(vector, vector2);
				float num6 = sqrMagnitude * sqrMagnitude2 - num5 * num5;
				num2 = ((num6 == 0f) ? 0f : Mathf.Clamp01((num5 * num - num4 * sqrMagnitude2) / num6));
				num3 = num5 * num2 + num;
				if (num3 < 0f)
				{
					num3 = 0f;
					num2 = Mathf.Clamp01((0f - num4) / sqrMagnitude);
				}
				else if (num3 > sqrMagnitude2)
				{
					num3 = 1f;
					num2 = Mathf.Clamp01((num5 - num4) / sqrMagnitude);
				}
				else
				{
					num3 /= sqrMagnitude2;
				}
			}
		}
		closestThis = point0 + vector * num2;
		closestOther = other.point0 + vector2 * num3;
	}

	public float Distance(Line other)
	{
		ClosestPoints(in other, out var closestThis, out var closestOther);
		return Vector3.Distance(closestThis, closestOther);
	}

	public float SqrDistance(Line other)
	{
		ClosestPoints(in other, out var closestThis, out var closestOther);
		return Vector3.SqrMagnitude(closestThis - closestOther);
	}
}
public static class Mathx
{
	public unsafe static float Increment(float f)
	{
		if (float.IsNaN(f))
		{
			return f;
		}
		if (f == 0f)
		{
			return float.Epsilon;
		}
		int num = *(int*)(&f);
		num = ((f > 0f) ? (num + 1) : (num - 1));
		return *(float*)(&num);
	}

	public unsafe static float Decrement(float f)
	{
		if (float.IsNaN(f))
		{
			return f;
		}
		if (f == 0f)
		{
			return -1E-45f;
		}
		int num = *(int*)(&f);
		num = ((f > 0f) ? (num - 1) : (num + 1));
		return *(float*)(&num);
	}

	public static float Above(float latitude, float lower, float fade = 0.1f)
	{
		latitude -= fade * 0.5f;
		return Mathf.Clamp01((latitude - lower + fade) / fade);
	}

	public static float Tween(float latitude, float lower, float upper, float fade = 0.1f)
	{
		latitude -= fade * 0.5f;
		return Mathf.Clamp01((latitude - lower + fade) / fade) * Mathf.Clamp01((upper - latitude) / fade);
	}

	public static float Below(float latitude, float upper, float fade = 0.1f)
	{
		latitude -= fade * 0.5f;
		return Mathf.Clamp01((upper - latitude) / fade);
	}

	public static Color Lerp3(float f1, Color c1, float f2, Color c2, float f3, Color c3)
	{
		if (f1 == 1f)
		{
			return c1;
		}
		if (f2 == 1f)
		{
			return c2;
		}
		if (f3 == 1f)
		{
			return c3;
		}
		if (f3 == 0f)
		{
			return f1 * c1 + f2 * c2;
		}
		if (f1 == 0f)
		{
			return f2 * c2 + f3 * c3;
		}
		return f1 * c1 + f2 * c2 + f3 * c3;
	}

	public static int Clamp(int v, int min, int max)
	{
		if (v >= min)
		{
			if (v <= max)
			{
				return v;
			}
			return max;
		}
		return min;
	}

	public static int Sign(int v)
	{
		return Math.Sign(v);
	}

	public static float SmoothMax(float a, float b, float fade = 0.1f)
	{
		return Mathf.SmoothStep(a, b, 0.5f + (b - a) / fade);
	}

	public static float Discretize01(float v, int steps)
	{
		return (float)Mathf.RoundToInt(Mathf.Clamp01(v) * (float)steps) / (float)steps;
	}

	public static float Min(float f1, float f2, float f3)
	{
		return Mathf.Min(Mathf.Min(f1, f2), f3);
	}

	public static float Min(float f1, float f2, float f3, float f4)
	{
		return Mathf.Min(Mathf.Min(f1, f2), Mathf.Min(f3, f4));
	}

	public static int Min(int f1, int f2, int f3)
	{
		return Mathf.Min(Mathf.Min(f1, f2), f3);
	}

	public static int Min(int f1, int f2, int f3, int f4)
	{
		return Mathf.Min(Mathf.Min(f1, f2), Mathf.Min(f3, f4));
	}

	public static float Max(float f1, float f2, float f3)
	{
		return Mathf.Max(Mathf.Max(f1, f2), f3);
	}

	public static float Max(float f1, float f2, float f3, float f4)
	{
		return Mathf.Max(Mathf.Max(f1, f2), Mathf.Max(f3, f4));
	}

	public static int Max(int f1, int f2, int f3)
	{
		return Mathf.Max(Mathf.Max(f1, f2), f3);
	}

	public static int Max(int f1, int f2, int f3, int f4)
	{
		return Mathf.Max(Mathf.Max(f1, f2), Mathf.Max(f3, f4));
	}

	public static uint Min(uint i1, uint i2)
	{
		if (i1 >= i2)
		{
			return i2;
		}
		return i1;
	}

	public static uint Max(uint i1, uint i2)
	{
		if (i1 <= i2)
		{
			return i2;
		}
		return i1;
	}

	public static float fsel(float c, float x, float y)
	{
		if (c >= 0f)
		{
			return x;
		}
		return y;
	}

	public static float RemapValClamped(float val, float A, float B, float C, float D)
	{
		if (A == B)
		{
			return fsel(val - B, D, C);
		}
		float value = (val - A) / (B - A);
		value = Mathf.Clamp(value, 0f, 1f);
		return C + (D - C) * value;
	}

	public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
	{
		Vector3 rhs = b - a;
		Vector3 lhs = value - a;
		float sqrMagnitude = rhs.sqrMagnitude;
		if (sqrMagnitude == 0f)
		{
			return 0f;
		}
		return Mathf.Clamp01(Vector3.Dot(lhs, rhs) / sqrMagnitude);
	}

	public static bool HasSignFlipped(int signA, int signB)
	{
		if (signA == 0 || signB == 0)
		{
			return false;
		}
		if (signA == signB || signB == signA)
		{
			return false;
		}
		return true;
	}

	public static int GetSignZero(float input, bool useApproximate = true)
	{
		if (useApproximate)
		{
			if (Mathf.Approximately(input, 0f))
			{
				return 0;
			}
			if (input == 0f)
			{
				return 0;
			}
		}
		if (!(input < 0f))
		{
			return 1;
		}
		return -1;
	}

	public static float Lerp(float from, float to, float speed, float deltaTime)
	{
		return Mathf.Lerp(to, from, Mathf.Pow(2f, (0f - speed) * deltaTime));
	}

	public static float Lerp(float from, float to, float speed)
	{
		return Lerp(from, to, speed, Time.deltaTime);
	}

	public static Vector3 Lerp(Vector3 from, Vector3 to, float speed, float deltaTime)
	{
		return Vector3.Lerp(to, from, Mathf.Pow(2f, (0f - speed) * deltaTime));
	}

	public static Vector3 Lerp(Vector3 from, Vector3 to, float speed)
	{
		return Lerp(from, to, speed, Time.deltaTime);
	}

	public static Quaternion Lerp(Quaternion from, Quaternion to, float speed, float deltaTime)
	{
		return Quaternion.Lerp(to, from, Mathf.Pow(2f, (0f - speed) * deltaTime));
	}

	public static Quaternion Lerp(Quaternion from, Quaternion to, float speed)
	{
		return Lerp(from, to, speed, Time.deltaTime);
	}

	public static int Mod(int x, int m)
	{
		return (x % m + m) % m;
	}

	public static Vector3 UniformVec(float value)
	{
		return new Vector3(value, value, value);
	}
}
public struct OBB
{
	public Quaternion rotation;

	public Vector3 position;

	public Vector3 extents;

	public Vector3 forward;

	public Vector3 right;

	public Vector3 up;

	public float reject;

	public OBB(Bounds bounds)
		: this(Vector3.zero, Vector3.one, Quaternion.identity, bounds)
	{
	}

	public OBB(Transform transform, Bounds bounds)
		: this(transform.position, transform.lossyScale, transform.rotation, bounds)
	{
	}

	public OBB(Vector3 position, Vector3 scale, Quaternion rotation, Bounds bounds)
	{
		this.rotation = rotation;
		this.position = position + rotation * Vector3.Scale(scale, bounds.center);
		extents = Vector3.Scale(scale, bounds.extents);
		forward = rotation * Vector3.forward;
		right = rotation * Vector3.right;
		up = rotation * Vector3.up;
		reject = extents.sqrMagnitude;
	}

	public OBB(Vector3 position, Quaternion rotation, Bounds bounds)
	{
		this.rotation = rotation;
		this.position = position + rotation * bounds.center;
		extents = bounds.extents;
		forward = rotation * Vector3.forward;
		right = rotation * Vector3.right;
		up = rotation * Vector3.up;
		reject = extents.sqrMagnitude;
	}

	public OBB(Vector3 position, Vector3 size, Quaternion rotation)
	{
		this.rotation = rotation;
		this.position = position;
		extents = size * 0.5f;
		forward = rotation * Vector3.forward;
		right = rotation * Vector3.right;
		up = rotation * Vector3.up;
		reject = extents.sqrMagnitude;
	}

	public void Transform(Vector3 position, Vector3 scale, Quaternion rotation)
	{
		this.rotation *= rotation;
		this.position = position + rotation * Vector3.Scale(scale, this.position);
		extents = Vector3.Scale(scale, extents);
	}

	public Vector3 GetPoint(float x, float y, float z)
	{
		return position + x * extents.x * right + y * extents.y * up + z * extents.z * forward;
	}

	public Bounds ToBounds()
	{
		Vector3 vector = extents.x * right;
		Vector3 vector2 = extents.y * up;
		Vector3 vector3 = extents.z * forward;
		Bounds result = new Bounds(position, Vector3.zero);
		result.Encapsulate(position + vector2 + vector + vector3);
		result.Encapsulate(position + vector2 + vector - vector3);
		result.Encapsulate(position + vector2 - vector + vector3);
		result.Encapsulate(position + vector2 - vector - vector3);
		result.Encapsulate(position - vector2 + vector + vector3);
		result.Encapsulate(position - vector2 + vector - vector3);
		result.Encapsulate(position - vector2 - vector + vector3);
		result.Encapsulate(position - vector2 - vector - vector3);
		return result;
	}

	public bool Contains(Vector3 target)
	{
		if ((target - position).sqrMagnitude > reject)
		{
			return false;
		}
		return ClosestPoint(target) == target;
	}

	public bool Intersects(OBB target)
	{
		Matrix4x4 matrix4x = Matrix4x4.Rotate(rotation);
		Matrix4x4 matrix4x2 = Matrix4x4.Rotate(target.rotation);
		Vector3 vector = matrix4x.inverse.MultiplyPoint3x4(target.position - position);
		Matrix4x4 matrix4x3 = matrix4x.transpose * matrix4x2;
		Matrix4x4 identity = Matrix4x4.identity;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				identity[i, j] = Mathf.Abs(matrix4x3[i, j]) + Mathf.Epsilon;
			}
		}
		if (Mathf.Abs(vector.x) > extents.x + target.extents.x * identity[0, 0] + target.extents.y * identity[0, 1] + target.extents.z * identity[0, 2])
		{
			return false;
		}
		if (Mathf.Abs(vector.y) > extents.y + target.extents.x * identity[1, 0] + target.extents.y * identity[1, 1] + target.extents.z * identity[1, 2])
		{
			return false;
		}
		if (Mathf.Abs(vector.z) > extents.z + target.extents.x * identity[2, 0] + target.extents.y * identity[2, 1] + target.extents.z * identity[2, 2])
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[0, 0] + vector.y * matrix4x3[1, 0] + vector.z * matrix4x3[2, 0]) > extents.x * identity[0, 0] + extents.y * identity[1, 0] + extents.z * identity[2, 0] + target.extents.x)
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[0, 1] + vector.y * matrix4x3[1, 1] + vector.z * matrix4x3[2, 1]) > extents.x * identity[0, 1] + extents.y * identity[1, 1] + extents.z * identity[2, 1] + target.extents.y)
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[0, 2] + vector.y * matrix4x3[1, 2] + vector.z * matrix4x3[2, 2]) > extents.x * identity[0, 2] + extents.y * identity[1, 2] + extents.z * identity[2, 2] + target.extents.z)
		{
			return false;
		}
		if (Mathf.Abs(vector.z * matrix4x3[1, 0] - vector.y * matrix4x3[2, 0]) > extents.y * identity[2, 0] + extents.z * identity[1, 0] + target.extents.y * identity[0, 2] + target.extents.z * identity[0, 1])
		{
			return false;
		}
		if (Mathf.Abs(vector.z * matrix4x3[1, 1] - vector.y * matrix4x3[2, 1]) > extents.y * identity[2, 1] + extents.z * identity[1, 1] + target.extents.x * identity[0, 2] + target.extents.z * identity[0, 0])
		{
			return false;
		}
		if (Mathf.Abs(vector.z * matrix4x3[1, 2] - vector.y * matrix4x3[2, 2]) > extents.y * identity[2, 2] + extents.z * identity[1, 2] + target.extents.x * identity[0, 1] + target.extents.y * identity[0, 0])
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[2, 0] - vector.z * matrix4x3[0, 0]) > extents.x * identity[2, 0] + extents.z * identity[0, 0] + target.extents.y * identity[1, 2] + target.extents.z * identity[1, 1])
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[2, 1] - vector.z * matrix4x3[0, 1]) > extents.x * identity[2, 1] + extents.z * identity[0, 1] + target.extents.x * identity[1, 2] + target.extents.z * identity[1, 0])
		{
			return false;
		}
		if (Mathf.Abs(vector.x * matrix4x3[2, 2] - vector.z * matrix4x3[0, 2]) > extents.x * identity[2, 2] + extents.z * identity[0, 2] + target.extents.x * identity[1, 1] + target.extents.y * identity[1, 0])
		{
			return false;
		}
		if (Mathf.Abs(vector.y * matrix4x3[0, 0] - vector.x * matrix4x3[1, 0]) > extents.x * identity[1, 0] + extents.y * identity[0, 0] + target.extents.y * identity[2, 2] + target.extents.z * identity[2, 1])
		{
			return false;
		}
		if (Mathf.Abs(vector.y * matrix4x3[0, 1] - vector.x * matrix4x3[1, 1]) > extents.x * identity[1, 1] + extents.y * identity[0, 1] + target.extents.x * identity[2, 2] + target.extents.z * identity[2, 0])
		{
			return false;
		}
		if (Mathf.Abs(vector.y * matrix4x3[0, 2] - vector.x * matrix4x3[1, 2]) > extents.x * identity[1, 2] + extents.y * identity[0, 2] + target.extents.x * identity[2, 1] + target.extents.y * identity[2, 0])
		{
			return false;
		}
		return true;
	}

	public bool Intersects2D(OBB target)
	{
		target.position.y = position.y;
		return Intersects(target);
	}

	public bool Intersects(Ray ray)
	{
		RaycastHit hit;
		return Trace(ray, out hit);
	}

	public bool Trace(Ray ray, out RaycastHit hit, float maxDistance = float.PositiveInfinity)
	{
		hit = default(RaycastHit);
		Vector3 rhs = right;
		Vector3 rhs2 = up;
		Vector3 rhs3 = forward;
		float x = extents.x;
		float y = extents.y;
		float z = extents.z;
		Vector3 lhs = ray.origin - position;
		Vector3 direction = ray.direction;
		float num = Vector3.Dot(direction, rhs);
		float num2 = Vector3.Dot(direction, rhs2);
		float num3 = Vector3.Dot(direction, rhs3);
		float num4 = Vector3.Dot(lhs, rhs);
		float num5 = Vector3.Dot(lhs, rhs2);
		float num6 = Vector3.Dot(lhs, rhs3);
		float f;
		float f2;
		if (num > 0f)
		{
			f = (0f - x - num4) / num;
			f2 = (x - num4) / num;
		}
		else if (num < 0f)
		{
			f = (x - num4) / num;
			f2 = (0f - x - num4) / num;
		}
		else
		{
			f = float.MinValue;
			f2 = float.MaxValue;
		}
		float f3;
		float f4;
		if (num2 > 0f)
		{
			f3 = (0f - y - num5) / num2;
			f4 = (y - num5) / num2;
		}
		else if (num2 < 0f)
		{
			f3 = (y - num5) / num2;
			f4 = (0f - y - num5) / num2;
		}
		else
		{
			f3 = float.MinValue;
			f4 = float.MaxValue;
		}
		float f5;
		float f6;
		if (num3 > 0f)
		{
			f5 = (0f - z - num6) / num3;
			f6 = (z - num6) / num3;
		}
		else if (num3 < 0f)
		{
			f5 = (z - num6) / num3;
			f6 = (0f - z - num6) / num3;
		}
		else
		{
			f5 = float.MinValue;
			f6 = float.MaxValue;
		}
		float num7 = Mathx.Min(f2, f4, f6);
		if (num7 < 0f)
		{
			return false;
		}
		float num8 = Mathx.Max(f, f3, f5);
		if (num8 > num7)
		{
			return false;
		}
		float num9 = Mathf.Clamp(0f, num8, num7);
		if (num9 > maxDistance)
		{
			return false;
		}
		hit.point = ray.origin + ray.direction * num9;
		hit.distance = num9;
		return true;
	}

	public Vector3 ClosestPoint(Vector3 target)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		Vector3 result = position;
		Vector3 lhs = target - position;
		float num = Vector3.Dot(lhs, right);
		if (num > extents.x)
		{
			result += right * extents.x;
		}
		else if (num < 0f - extents.x)
		{
			result -= right * extents.x;
		}
		else
		{
			flag = true;
			result += right * num;
		}
		float num2 = Vector3.Dot(lhs, up);
		if (num2 > extents.y)
		{
			result += up * extents.y;
		}
		else if (num2 < 0f - extents.y)
		{
			result -= up * extents.y;
		}
		else
		{
			flag2 = true;
			result += up * num2;
		}
		float num3 = Vector3.Dot(lhs, forward);
		if (num3 > extents.z)
		{
			result += forward * extents.z;
		}
		else if (num3 < 0f - extents.z)
		{
			result -= forward * extents.z;
		}
		else
		{
			flag3 = true;
			result += forward * num3;
		}
		if (flag && flag2 && flag3)
		{
			return target;
		}
		return result;
	}

	public float Distance(OBB other)
	{
		return GJK.Distance(this, other);
	}

	public float Distance(Vector3 position)
	{
		return Vector3.Distance(position, ClosestPoint(position));
	}

	public float SqrDistance(OBB other)
	{
		return GJK.Distance2(this, other);
	}

	public float SqrDistance(Vector3 position)
	{
		return (position - ClosestPoint(position)).sqrMagnitude;
	}

	public void DebugDraw(Color colour, float duration)
	{
		UnityEngine.Debug.DrawLine(GetPoint(-1f, -1f, -1f), GetPoint(1f, -1f, -1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, -1f, 1f), GetPoint(1f, -1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, -1f, -1f), GetPoint(-1f, -1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(1f, -1f, -1f), GetPoint(1f, -1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, -1f, -1f), GetPoint(-1f, 1f, -1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, -1f, 1f), GetPoint(-1f, 1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(1f, -1f, -1f), GetPoint(1f, 1f, -1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(1f, -1f, 1f), GetPoint(1f, 1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, 1f, -1f), GetPoint(1f, 1f, -1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, 1f, 1f), GetPoint(1f, 1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(-1f, 1f, -1f), GetPoint(-1f, 1f, 1f), colour, duration);
		UnityEngine.Debug.DrawLine(GetPoint(1f, -1f, -1f), GetPoint(1f, 1f, 1f), colour, duration);
	}
}
public static class SeedEx
{
	public static uint Seed(this Vector2 v, uint baseSeed)
	{
		return baseSeed + (uint)(v.x * 10f + v.y * 100f);
	}

	public static uint Seed(this Vector3 v, uint baseSeed)
	{
		return baseSeed + (uint)(v.x * 10f + v.y * 100f + v.z * 1000f);
	}

	public static uint Seed(this Vector4 v, uint baseSeed)
	{
		return baseSeed + (uint)(v.x * 10f + v.y * 100f + v.z * 1000f + v.w * 10000f);
	}
}
public class SeedRandom
{
	public uint Seed;

	public SeedRandom(uint seed = 0u)
	{
		if (seed != 0)
		{
			Seed = seed;
		}
		else
		{
			Seed = (uint)UnityEngine.Random.Range(1, int.MaxValue);
		}
	}

	public int Range(int min, int max)
	{
		return Range(ref Seed, min, max);
	}

	public static int Range(uint seed, int min, int max)
	{
		return Range(ref seed, min, max);
	}

	public static int Range(ref uint seed, int min, int max)
	{
		uint num = (uint)(max - min);
		return min + (int)(Xorshift(ref seed) % num);
	}

	public float Range(float min, float max)
	{
		return Range(ref Seed, min, max);
	}

	public static float Range(uint seed, float min, float max)
	{
		return Range(ref seed, min, max);
	}

	public static float Range(ref uint seed, float min, float max)
	{
		return min + Xorshift01(ref seed) * (max - min);
	}

	public int Sign()
	{
		if (Xorshift(ref Seed) % 2 != 0)
		{
			return -1;
		}
		return 1;
	}

	public static int Sign(uint seed)
	{
		if (Xorshift(ref seed) % 2 != 0)
		{
			return -1;
		}
		return 1;
	}

	public static int Sign(ref uint seed)
	{
		if (Xorshift(ref seed) % 2 != 0)
		{
			return -1;
		}
		return 1;
	}

	public float Value()
	{
		return Xorshift01(ref Seed);
	}

	public static float Value(uint seed)
	{
		return Xorshift01(ref seed);
	}

	public static float Value(ref uint seed)
	{
		return Xorshift01(ref seed);
	}

	public Vector2 Value2D()
	{
		return Value2D(ref Seed);
	}

	public static Vector2 Value2D(uint seed)
	{
		return Value2D(ref seed);
	}

	public static Vector2 Value2D(ref uint seed)
	{
		float f = Value(ref seed) * MathF.PI * 2f;
		return new Vector2(Mathf.Cos(f), Mathf.Sin(f));
	}

	public static uint Wanghash(ref uint x)
	{
		x = x ^ 0x3D ^ (x >> 16);
		x *= 9u;
		x ^= x >> 4;
		x *= 668265261u;
		x ^= x >> 15;
		return x;
	}

	public static ulong Wanghash(ref ulong x)
	{
		x ^= x >> 30;
		x *= 13787848793156543929uL;
		x ^= x >> 27;
		x *= 10723151780598845931uL;
		x ^= x >> 31;
		return x;
	}

	public static float Wanghash01(ref uint x)
	{
		return (float)Wanghash(ref x) * 2.3283064E-10f;
	}

	public static uint Xorshift(ref uint x)
	{
		x ^= x << 13;
		x ^= x >> 17;
		x ^= x << 5;
		return x;
	}

	public static ulong Xorshift(ref ulong x)
	{
		x ^= x << 13;
		x ^= x >> 7;
		x ^= x << 17;
		return x;
	}

	public static float Xorshift01(ref uint x)
	{
		return (float)Xorshift(ref x) * 2.3283064E-10f;
	}
}
public struct Sphere
{
	public Vector3 position;

	public float radius;

	public Sphere(Vector3 position, float radius)
	{
		this.position = position;
		this.radius = radius;
	}

	public bool Contains(Vector3 target)
	{
		return ClosestPoint(target) == target;
	}

	public Vector3 ClosestPoint(Vector3 target)
	{
		Vector3 result = target;
		Vector3 vector = target - position;
		float magnitude = vector.magnitude;
		if (magnitude <= radius)
		{
			return result;
		}
		float num = radius / magnitude;
		result.x = position.x + vector.x * num;
		result.y = position.y + vector.y * num;
		result.z = position.z + vector.z * num;
		return result;
	}

	public void Move(Vector3 direction, float distance, int layerMask = 0)
	{
		RaycastHit hitInfo;
		bool flag = Physics.SphereCast(position, radius, direction, out hitInfo, distance, layerMask);
		if ((!(hitInfo.collider != null) || ((int)hitInfo.collider.excludeLayers & 0x1000) == 0) && layerMask != 0 && flag)
		{
			position += direction * hitInfo.distance;
		}
		else
		{
			position += direction * distance;
		}
	}

	public bool Trace(Ray ray, out RaycastHit hit, float maxDistance = float.PositiveInfinity)
	{
		hit = default(RaycastHit);
		if (radius <= 0f)
		{
			return false;
		}
		float num = 1f;
		float num2 = 2f * Vector3.Dot(ray.direction, ray.origin - position);
		float num3 = (ray.origin - position).sqrMagnitude - radius * radius;
		float num4 = num2 * num2 - 4f * num * num3;
		if (num4 < 0f)
		{
			return false;
		}
		float num5 = Mathf.Sqrt(num4);
		float num6 = 2f * num;
		float num7 = 0f - num2;
		float num8 = (num7 - num5) / num6;
		if (num8 >= 0f)
		{
			if (num8 <= maxDistance)
			{
				hit.point = ray.origin + num8 * ray.direction;
				hit.normal = (hit.point - position) / radius;
				hit.distance = num8;
				return true;
			}
			return false;
		}
		if ((num7 + num5) / num6 >= 0f)
		{
			hit.point = ray.origin;
			hit.normal = (hit.point - position).normalized;
			hit.distance = 0f;
			return true;
		}
		return false;
	}
}
public struct Triangle
{
	public Vector3 point0;

	public Vector3 point1;

	public Vector3 point2;

	public Vector3 Normal
	{
		get
		{
			Vector3 lhs = point1 - point0;
			Vector3 rhs = point2 - point0;
			return Vector3.Cross(lhs, rhs).normalized;
		}
	}

	public Vector3 Center => (point0 + point1 + point2) / 3f;

	public Triangle(Vector3 point0, Vector3 point1, Vector3 point2)
	{
		this.point0 = point0;
		this.point1 = point1;
		this.point2 = point2;
	}

	public bool Trace(Ray ray, float radius, out RaycastHit hit, float maxDistance = float.PositiveInfinity)
	{
		hit = default(RaycastHit);
		Vector3 vector = point1 - point0;
		Vector3 vector2 = point2 - point0;
		Vector3 rhs = Vector3.Cross(ray.direction, vector2);
		float num = Vector3.Dot(vector, rhs);
		if (num > 0f - Mathf.Epsilon && num < Mathf.Epsilon)
		{
			return false;
		}
		float num2 = 1f / num;
		Vector3 lhs = ray.origin - point0;
		float num3 = Vector3.Dot(lhs, rhs) * num2;
		if (num3 < 0f)
		{
			return LineTest(point0, point2, ray, radius, out hit, maxDistance);
		}
		if (num3 > 1f)
		{
			return LineTest(point1, point2, ray, radius, out hit, maxDistance);
		}
		Vector3 rhs2 = Vector3.Cross(lhs, vector);
		float num4 = Vector3.Dot(ray.direction, rhs2) * num2;
		if (num4 < 0f)
		{
			return LineTest(point0, point1, ray, radius, out hit, maxDistance);
		}
		if (num3 + num4 > 1f)
		{
			return LineTest(point1, point2, ray, radius, out hit, maxDistance);
		}
		float num5 = Vector3.Dot(vector2, rhs2) * num2;
		if (num5 < 0f || num5 > maxDistance)
		{
			return false;
		}
		Vector3 point = ray.origin + num5 * ray.direction;
		hit.point = point;
		hit.distance = num5;
		hit.normal = Vector3.Cross(vector, vector2).normalized;
		return true;
	}

	private bool LineTest(Vector3 a, Vector3 b, Ray ray, float radius, out RaycastHit hit, float maxDistance)
	{
		if (new Line(point0, point2).Trace(ray, radius, out hit, maxDistance))
		{
			hit.normal = Normal;
			return true;
		}
		return false;
	}

	public Vector3 ClosestPoint(Vector3 pos)
	{
		Vector3 rhs = point0 - pos;
		Vector3 vector = point1 - point0;
		Vector3 vector2 = point2 - point0;
		float num = Vector3.Dot(vector, vector);
		float num2 = Vector3.Dot(vector, vector2);
		float num3 = Vector3.Dot(vector2, vector2);
		float num4 = Vector3.Dot(vector, rhs);
		float num5 = Vector3.Dot(vector2, rhs);
		float num6 = num * num3 - num2 * num2;
		float num7 = num2 * num5 - num3 * num4;
		float num8 = num2 * num4 - num * num5;
		if (num7 + num8 < num6)
		{
			if (num7 < 0f)
			{
				if (num8 < 0f)
				{
					if (num4 < 0f)
					{
						num7 = Mathf.Clamp01((0f - num4) / num);
						num8 = 0f;
					}
					else
					{
						num7 = 0f;
						num8 = Mathf.Clamp01((0f - num5) / num3);
					}
				}
				else
				{
					num7 = 0f;
					num8 = Mathf.Clamp01((0f - num5) / num3);
				}
			}
			else if (num8 < 0f)
			{
				num7 = Mathf.Clamp01((0f - num4) / num);
				num8 = 0f;
			}
			else
			{
				float num9 = 1f / num6;
				num7 *= num9;
				num8 *= num9;
			}
		}
		else if (num7 < 0f)
		{
			float num10 = num2 + num4;
			float num11 = num3 + num5;
			if (num11 > num10)
			{
				float num12 = num11 - num10;
				float num13 = num - 2f * num2 + num3;
				num7 = Mathf.Clamp01(num12 / num13);
				num8 = 1f - num7;
			}
			else
			{
				num8 = Mathf.Clamp01((0f - num5) / num3);
				num7 = 0f;
			}
		}
		else if (num8 < 0f)
		{
			if (num + num4 > num2 + num5)
			{
				float num14 = num3 + num5 - num2 - num4;
				float num15 = num - 2f * num2 + num3;
				num7 = Mathf.Clamp01(num14 / num15);
				num8 = 1f - num7;
			}
			else
			{
				num7 = Mathf.Clamp01((0f - num5) / num3);
				num8 = 0f;
			}
		}
		else
		{
			float num16 = num3 + num5 - num2 - num4;
			float num17 = num - 2f * num2 + num3;
			num7 = Mathf.Clamp01(num16 / num17);
			num8 = 1f - num7;
		}
		return point0 + num7 * vector + num8 * vector2;
	}

	public float Distance(Vector3 pos)
	{
		return (pos - ClosestPoint(pos)).magnitude;
	}

	public float SqrDistance(Vector3 pos)
	{
		return (pos - ClosestPoint(pos)).sqrMagnitude;
	}

	public float Area()
	{
		float num = Vector3.Distance(point0, point1);
		float num2 = Vector3.Distance(point1, point2);
		float num3 = Vector3.Distance(point2, point0);
		return 0.25f * Mathf.Sqrt((num + num2 + num3) * (0f - num + num2 + num3) * (num - num2 + num3) * (num + num2 - num3));
	}
}
public class TextBuffer
{
	private Queue<string> buffer;

	private StringBuilder builder;

	private string text = string.Empty;

	private bool dirty;

	private int curlines;

	private int maxlines;

	private int curchars;

	private int maxchars;

	public int Count => curlines;

	public TextBuffer(int maxlines, int maxchars = int.MaxValue)
	{
		buffer = new Queue<string>(maxlines + 1);
		builder = new StringBuilder();
		this.maxlines = maxlines;
		this.maxchars = maxchars;
	}

	public void Add(string text)
	{
		foreach (string item in text.SplitToLines())
		{
			buffer.Enqueue(item);
			curlines++;
			curchars += item.Length;
			while (curlines > maxlines || curchars > maxchars)
			{
				Remove();
			}
		}
		dirty = true;
	}

	public void Remove()
	{
		if (buffer.Count != 0)
		{
			string text = buffer.Dequeue();
			curlines--;
			curchars -= text.Length;
		}
	}

	public void Clear()
	{
		buffer.Clear();
		curlines = 0;
		curchars = 0;
		text = string.Empty;
		dirty = true;
	}

	public string Get(int index)
	{
		if (index < 0 || index > buffer.Count - 1)
		{
			return string.Empty;
		}
		return buffer.ElementAt(buffer.Count - 1 - index);
	}

	public override string ToString()
	{
		if (dirty)
		{
			builder.Clear();
			foreach (string item in buffer)
			{
				builder.AppendLine(item);
			}
			text = builder.ToString();
			dirty = false;
		}
		return text;
	}
}
public class TextTable : Pool.IPooled, IDisposable
{
	[StructLayout(LayoutKind.Explicit)]
	private struct RowValueUnion
	{
		[FieldOffset(0)]
		public bool Bool;

		[FieldOffset(0)]
		public int Int;

		[FieldOffset(0)]
		public uint UInt;

		[FieldOffset(0)]
		public long Long;

		[FieldOffset(0)]
		public ulong ULong;

		[FieldOffset(0)]
		public float Float;

		[FieldOffset(0)]
		public double Double;

		[FieldOffset(0)]
		public Vector3 Vec3;

		[FieldOffset(0)]
		public string String;
	}

	private enum ValueType
	{
		Bool,
		Int,
		UInt,
		Long,
		ULong,
		Float,
		Double,
		Vec3,
		String,
		NextRow
	}

	private struct RowValue
	{
		public RowValueUnion Value;

		public ValueType ValueType;

		public void WriteTo(StringBuilder builder)
		{
			switch (ValueType)
			{
			case ValueType.Bool:
				builder.Append(Value.Bool);
				break;
			case ValueType.Int:
			{
				Span<char> destination7 = stackalloc char[32];
				Value.Int.TryFormat(destination7, out var charsWritten7);
				builder.Append(destination7.Slice(0, charsWritten7));
				break;
			}
			case ValueType.UInt:
			{
				Span<char> destination6 = stackalloc char[32];
				Value.UInt.TryFormat(destination6, out var charsWritten6);
				builder.Append(destination6.Slice(0, charsWritten6));
				break;
			}
			case ValueType.Long:
			{
				Span<char> destination5 = stackalloc char[32];
				Value.Long.TryFormat(destination5, out var charsWritten5);
				builder.Append(destination5.Slice(0, charsWritten5));
				break;
			}
			case ValueType.ULong:
			{
				Span<char> destination4 = stackalloc char[32];
				Value.ULong.TryFormat(destination4, out var charsWritten4);
				builder.Append(destination4.Slice(0, charsWritten4));
				break;
			}
			case ValueType.Float:
			{
				Span<char> destination3 = stackalloc char[32];
				Value.Float.TryFormat(destination3, out var charsWritten3);
				builder.Append(destination3.Slice(0, charsWritten3));
				break;
			}
			case ValueType.Double:
			{
				Span<char> destination2 = stackalloc char[32];
				Value.Double.TryFormat(destination2, out var charsWritten2);
				builder.Append(destination2.Slice(0, charsWritten2));
				break;
			}
			case ValueType.Vec3:
			{
				Span<char> destination = stackalloc char[32];
				builder.Append('(');
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				Value.Vec3.x.TryFormat(destination, out var charsWritten, "F2", numberFormat);
				builder.Append(destination.Slice(0, charsWritten));
				builder.Append(", ");
				Value.Vec3.y.TryFormat(destination, out charsWritten, "F2", numberFormat);
				builder.Append(destination.Slice(0, charsWritten));
				builder.Append(", ");
				Value.Vec3.z.TryFormat(destination, out charsWritten, "F2", numberFormat);
				builder.Append(destination.Slice(0, charsWritten));
				builder.Append(')');
				break;
			}
			case ValueType.String:
				builder.Append(Value.String);
				break;
			}
		}

		public void WriteTo(JsonTextWriter writer, TextWriter textWriter, bool stringify)
		{
			if (stringify)
			{
				switch (ValueType)
				{
				case ValueType.Bool:
					writer.WriteRaw("\"");
					writer.WriteRawValue(Value.Bool ? "True" : "False");
					writer.WriteRaw("\"");
					break;
				case ValueType.Int:
					writer.WriteRaw("\"");
					writer.WriteValue(Value.Int);
					writer.WriteRaw("\"");
					break;
				case ValueType.UInt:
					writer.WriteRaw("\"");
					writer.WriteValue(Value.UInt);
					writer.WriteRaw("\"");
					break;
				case ValueType.Long:
					writer.WriteRaw("\"");
					writer.WriteValue(Value.Long);
					writer.WriteRaw("\"");
					break;
				case ValueType.ULong:
					writer.WriteRaw("\"");
					writer.WriteValue(Value.ULong);
					writer.WriteRaw("\"");
					break;
				case ValueType.Float:
				{
					Span<char> destination2 = stackalloc char[32];
					Value.Float.TryFormat(destination2, out var charsWritten2);
					writer.WriteRaw("\"");
					for (int l = 0; l < charsWritten2; l++)
					{
						textWriter.Write(destination2[l]);
					}
					writer.WriteRaw("\"");
					writer.WriteRawValue(null);
					break;
				}
				case ValueType.Double:
				{
					Span<char> destination3 = stackalloc char[32];
					Value.Double.TryFormat(destination3, out var charsWritten3);
					writer.WriteRaw("\"");
					for (int m = 0; m < charsWritten3; m++)
					{
						textWriter.Write(destination3[m]);
					}
					writer.WriteRaw("\"");
					writer.WriteRawValue(null);
					break;
				}
				case ValueType.Vec3:
				{
					writer.WriteRaw("\"(");
					Span<char> destination = stackalloc char[32];
					NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
					Value.Vec3.x.TryFormat(destination, out var charsWritten, "F2", numberFormat);
					for (int i = 0; i < charsWritten; i++)
					{
						textWriter.Write(destination[i]);
					}
					writer.WriteRaw(", ");
					Value.Vec3.y.TryFormat(destination, out charsWritten, "F2", numberFormat);
					for (int j = 0; j < charsWritten; j++)
					{
						textWriter.Write(destination[j]);
					}
					writer.WriteRaw(", ");
					Value.Vec3.z.TryFormat(destination, out charsWritten, "F2", numberFormat);
					for (int k = 0; k < charsWritten; k++)
					{
						textWriter.Write(destination[k]);
					}
					writer.WriteRaw(")\"");
					writer.WriteRawValue(null);
					break;
				}
				case ValueType.String:
					writer.WriteValue(Value.String);
					break;
				}
				return;
			}
			switch (ValueType)
			{
			case ValueType.Bool:
				writer.WriteValue(Value.Bool);
				break;
			case ValueType.Int:
				writer.WriteValue(Value.Int);
				break;
			case ValueType.UInt:
				writer.WriteValue(Value.UInt);
				break;
			case ValueType.Long:
				writer.WriteValue(Value.Long);
				break;
			case ValueType.ULong:
				writer.WriteValue(Value.ULong);
				break;
			case ValueType.Float:
			{
				Span<char> destination5 = stackalloc char[32];
				Value.Float.TryFormat(destination5, out var charsWritten5);
				for (int num3 = 0; num3 < charsWritten5; num3++)
				{
					textWriter.Write(destination5[num3]);
				}
				writer.WriteRawValue(null);
				break;
			}
			case ValueType.Double:
			{
				Span<char> destination6 = stackalloc char[32];
				Value.Double.TryFormat(destination6, out var charsWritten6);
				for (int num4 = 0; num4 < charsWritten6; num4++)
				{
					textWriter.Write(destination6[num4]);
				}
				writer.WriteRawValue(null);
				break;
			}
			case ValueType.Vec3:
			{
				writer.WriteStartArray();
				Span<char> destination4 = stackalloc char[32];
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				Value.Vec3.x.TryFormat(destination4, out var charsWritten4, "F2", numberFormat2);
				for (int n = 0; n < charsWritten4; n++)
				{
					textWriter.Write(destination4[n]);
				}
				textWriter.Write(',');
				Value.Vec3.y.TryFormat(destination4, out charsWritten4, "F2", numberFormat2);
				for (int num = 0; num < charsWritten4; num++)
				{
					textWriter.Write(destination4[num]);
				}
				textWriter.Write(',');
				Value.Vec3.z.TryFormat(destination4, out charsWritten4, "F2", numberFormat2);
				for (int num2 = 0; num2 < charsWritten4; num2++)
				{
					textWriter.Write(destination4[num2]);
				}
				writer.WriteEndArray();
				break;
			}
			case ValueType.String:
				writer.WriteValue(Value.String);
				break;
			}
		}
	}

	private struct Column
	{
		public string title;

		public int width;

		public Column(string title)
		{
			this.title = title;
			width = title.Length;
		}
	}

	private static Encoding utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

	private BufferList<RowValue> rowValues;

	private BufferList<Column> columns;

	private string text;

	private string jsonText;

	private bool wasPooled;

	public bool ShouldPadColumns;

	public TextTable()
		: this(shouldPadColumns: true)
	{
	}

	public TextTable(bool shouldPadColumns)
	{
		ShouldPadColumns = shouldPadColumns;
	}

	void Pool.IPooled.EnterPool()
	{
		Pool.FreeUnmanaged(ref columns);
		Pool.FreeUnmanaged(ref rowValues);
		ShouldPadColumns = true;
	}

	void Pool.IPooled.LeavePool()
	{
		columns = Pool.Get<BufferList<Column>>();
		rowValues = Pool.Get<BufferList<RowValue>>();
		wasPooled = true;
	}

	void IDisposable.Dispose()
	{
		if (wasPooled)
		{
			TextTable obj = this;
			Pool.Free(ref obj);
		}
	}

	public void Clear()
	{
		columns?.Clear();
		rowValues?.Clear();
		MarkDirty();
	}

	public void ResizeColumns(int count)
	{
		if (columns == null)
		{
			columns = new BufferList<Column>();
		}
		columns.Resize(count);
	}

	public void AddColumns(params string[] values)
	{
		ResizeColumns(values.Length);
		for (int i = 0; i < values.Length; i++)
		{
			columns.Add(new Column(values[i]));
		}
		MarkDirty();
	}

	public void AddColumn(string title)
	{
		if (columns == null)
		{
			columns = new BufferList<Column>();
		}
		columns.Add(new Column(title));
		MarkDirty();
	}

	public void ResizeRows(int count)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		rowValues.Resize(count * columns.Count);
	}

	public void AddRow(params string[] values)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		int num = Mathf.Min(columns.Count, values.Length);
		for (int i = 0; i < num; i++)
		{
			if (ShouldPadColumns)
			{
				columns.Buffer[i].width = Mathf.Max(columns[i].width, values[i].Length);
			}
			RowValue element = new RowValue
			{
				Value = new RowValueUnion
				{
					String = values[i]
				},
				ValueType = ValueType.String
			};
			rowValues.Add(element);
		}
		if (num < columns.Count)
		{
			RowValue element2 = new RowValue
			{
				ValueType = ValueType.NextRow
			};
			rowValues.Add(element2);
		}
		MarkDirty();
	}

	public void AddValue(bool value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Bool = value
			},
			ValueType = ValueType.Bool
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = (value ? 4 : 5);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(int value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Int = value
			},
			ValueType = ValueType.Int
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(uint value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				UInt = value
			},
			ValueType = ValueType.UInt
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(long value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Long = value
			},
			ValueType = ValueType.Long
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(ulong value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				ULong = value
			},
			ValueType = ValueType.ULong
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(float value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Float = value
			},
			ValueType = ValueType.Float
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(double value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Double = value
			},
			ValueType = ValueType.Double
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(Vector3 value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				Vec3 = value
			},
			ValueType = ValueType.Vec3
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			int val = LengthOf(value);
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, val);
		}
		rowValues.Add(element);
	}

	public void AddValue(string value)
	{
		if (rowValues == null)
		{
			rowValues = new BufferList<RowValue>();
		}
		RowValue element = new RowValue
		{
			Value = new RowValueUnion
			{
				String = value
			},
			ValueType = ValueType.String
		};
		if (ShouldPadColumns)
		{
			int num = rowValues.Count % columns.Count;
			ref Column reference = ref columns.Buffer[num];
			reference.width = Math.Max(reference.width, value.Length);
		}
		rowValues.Add(element);
	}

	public string ToJson(bool stringify = true)
	{
		if (jsonText == null)
		{
			if (columns == null || rowValues == null)
			{
				jsonText = "[]";
				return jsonText;
			}
			using MemoryStream memoryStream = new MemoryStream();
			using (StreamWriter textWriter = new StreamWriter(memoryStream, utf8NoBom, 1024, leaveOpen: true))
			{
				using JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter);
				jsonTextWriter.WriteStartArray();
				int num = 0;
				while (num < rowValues.Count)
				{
					jsonTextWriter.WriteStartObject();
					for (int i = 0; i < columns.Count; i++)
					{
						if (num >= rowValues.Count)
						{
							break;
						}
						RowValue rowValue = rowValues[num++];
						if (rowValue.ValueType == ValueType.NextRow)
						{
							break;
						}
						jsonTextWriter.WritePropertyName(columns[i].title);
						rowValue.WriteTo(jsonTextWriter, textWriter, stringify);
					}
					jsonTextWriter.WriteEndObject();
				}
				jsonTextWriter.WriteEndArray();
			}
			jsonText = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
		}
		return jsonText;
	}

	public override string ToString()
	{
		if (text == null)
		{
			if (columns == null)
			{
				text = string.Empty;
				return text;
			}
			StringBuilder obj = Pool.Get<StringBuilder>();
			for (int i = 0; i < columns.Count; i++)
			{
				obj.Append(columns[i].title);
				int length = columns[i].title.Length;
				int num = columns[i].width + 1;
				for (int j = length; j < num; j++)
				{
					obj.Append(' ');
				}
			}
			obj.AppendLine();
			if (rowValues != null)
			{
				int num2 = 0;
				while (num2 < rowValues.Count)
				{
					for (int k = 0; k < columns.Count; k++)
					{
						if (num2 >= rowValues.Count)
						{
							break;
						}
						RowValue rowValue = rowValues[num2++];
						if (rowValue.ValueType == ValueType.NextRow)
						{
							break;
						}
						int length2 = obj.Length;
						rowValue.WriteTo(obj);
						int num3 = obj.Length - length2;
						int num4 = columns[k].width + 1;
						for (int l = num3; l < num4; l++)
						{
							obj.Append(' ');
						}
					}
					obj.AppendLine();
				}
			}
			text = obj.ToString();
			Pool.FreeUnmanaged(ref obj);
		}
		return text;
	}

	private void MarkDirty()
	{
		jsonText = null;
		text = null;
	}

	private static int LengthOf(int i)
	{
		return ((i < 0) ? 1 : 0) + LengthOf((uint)Math.Abs(i));
	}

	private static int LengthOf(uint u)
	{
		if (u < 100000)
		{
			if (u < 100)
			{
				return (u < 10) ? 1 : 2;
			}
			if (u < 10000)
			{
				return (u < 1000) ? 3 : 4;
			}
			return 5;
		}
		if (u < 100000000)
		{
			if (u < 10000000)
			{
				return (u < 1000000) ? 6 : 7;
			}
			return 8;
		}
		return (u < 1000000000) ? 9 : 10;
	}

	private static int LengthOf(long l)
	{
		return ((l < 0) ? 1 : 0) + LengthOf((ulong)Math.Abs(l));
	}

	private static int LengthOf(ulong ul)
	{
		if (ul < 10000000000L)
		{
			if (ul < 100000)
			{
				if (ul < 100)
				{
					return (ul < 10) ? 1 : 2;
				}
				if (ul < 10000)
				{
					return (ul < 1000) ? 3 : 4;
				}
				return 5;
			}
			if (ul < 100000000)
			{
				if (ul < 10000000)
				{
					return (ul < 1000000) ? 6 : 7;
				}
				return 8;
			}
			return (ul < 1000000000) ? 9 : 10;
		}
		if (ul < 1000000000000000L)
		{
			if (ul < 1000000000000L)
			{
				return (ul < 100000000000L) ? 11 : 12;
			}
			if (ul < 100000000000000L)
			{
				return (ul < 10000000000000L) ? 13 : 14;
			}
			return 15;
		}
		if (ul < 1000000000000000000L)
		{
			if (ul < 100000000000000000L)
			{
				return (ul < 10000000000000000L) ? 16 : 17;
			}
			return 18;
		}
		return (ul < 10000000000000000000uL) ? 19 : 20;
	}

	private static int LengthOf(float f)
	{
		Span<char> destination = stackalloc char[32];
		f.TryFormat(destination, out var charsWritten);
		return charsWritten;
	}

	private static int LengthOf(double d)
	{
		Span<char> destination = stackalloc char[32];
		d.TryFormat(destination, out var charsWritten);
		return charsWritten;
	}

	private static int LengthOf(Vector3 v)
	{
		int num = 6;
		Span<char> destination = stackalloc char[32];
		NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
		v.x.TryFormat(destination, out var charsWritten, "F2", numberFormat);
		num += charsWritten;
		v.y.TryFormat(destination, out charsWritten, "F2", numberFormat);
		num += charsWritten;
		v.z.TryFormat(destination, out charsWritten, "F2", numberFormat);
		return num + charsWritten;
	}
}
public static class UnsafeListAccess
{
	[Preserve]
	private class ListPrivateFieldAccess<T>
	{
		internal T[] _items;

		internal int _size;

		internal int _version;
	}

	private static ListPrivateFieldAccess<T> GetPrivateFieldsUnsafe<T>(this List<T> list)
	{
		UnityEngine.Debug.Assert(list != null);
		return Unsafe.As<ListPrivateFieldAccess<T>>(list);
	}

	private static T[] GetInternalArrayUnsafe<T>(this List<T> list)
	{
		return list.GetPrivateFieldsUnsafe()._items;
	}

	public static Span<T> ListAsSpan<T>(this List<T> list)
	{
		return list.GetInternalArrayUnsafe().AsSpan(0, list.Count);
	}

	public static ReadOnlySpan<T> ListAsReadOnlySpan<T>(this List<T> list)
	{
		return list.GetInternalArrayUnsafe().AsSpan(0, list.Count);
	}

	public static ReadOnlySpan<U> ListAsReadOnlySpanOf<T, U>(this List<T> list) where T : class, U
	{
		U[] internalArrayUnsafe = list.GetInternalArrayUnsafe();
		return new ReadOnlySpan<U>(internalArrayUnsafe, 0, list.Count);
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
			FilePathsData = new byte[2994]
			{
				0, 0, 0, 1, 0, 0, 0, 42, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 65, 112, 112, 108,
				105, 99, 97, 116, 105, 111, 110, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 47, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 65, 114, 114, 97,
				121, 73, 110, 100, 101, 120, 73, 115, 69, 110,
				117, 109, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 40, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 65, 115, 115, 101, 116, 80, 111, 111, 108,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				58, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 71, 108, 111, 98, 97, 108, 92, 67,
				111, 108, 108, 101, 99, 116, 105, 111, 110, 115,
				92, 79, 98, 106, 101, 99, 116, 87, 111, 114,
				107, 81, 117, 101, 117, 101, 46, 99, 115, 0,
				0, 0, 2, 0, 0, 0, 68, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 71, 108,
				111, 98, 97, 108, 92, 67, 111, 108, 108, 101,
				99, 116, 105, 111, 110, 115, 92, 80, 101, 114,
				115, 105, 115, 116, 101, 110, 116, 79, 98, 106,
				101, 99, 116, 87, 111, 114, 107, 81, 117, 101,
				117, 101, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 57, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 67, 111, 108, 108, 101, 99, 116, 105, 111,
				110, 115, 92, 87, 111, 114, 108, 100, 83, 112,
				97, 99, 101, 71, 114, 105, 100, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 71, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 67, 111, 109, 112,
				111, 110, 101, 110, 116, 115, 92, 67, 97, 109,
				101, 114, 97, 92, 68, 101, 112, 116, 104, 79,
				102, 70, 105, 101, 108, 100, 70, 111, 99, 117,
				115, 80, 111, 105, 110, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 65, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 71, 108,
				111, 98, 97, 108, 92, 67, 111, 109, 112, 111,
				110, 101, 110, 116, 115, 92, 67, 97, 109, 101,
				114, 97, 92, 76, 105, 103, 104, 116, 105, 110,
				103, 79, 118, 101, 114, 114, 105, 100, 101, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 68,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 71, 108, 111, 98, 97, 108, 92, 67, 111,
				109, 112, 111, 110, 101, 110, 116, 115, 92, 73,
				110, 116, 101, 114, 110, 97, 108, 92, 78, 117,
				108, 108, 77, 111, 110, 111, 66, 101, 104, 97,
				118, 105, 111, 117, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 65, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 67, 111, 109, 112, 111, 110,
				101, 110, 116, 115, 92, 85, 116, 105, 108, 105,
				116, 121, 92, 79, 110, 79, 98, 106, 101, 99,
				116, 68, 105, 115, 97, 98, 108, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 64, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 67, 111, 109,
				112, 111, 110, 101, 110, 116, 115, 92, 85, 116,
				105, 108, 105, 116, 121, 92, 79, 110, 79, 98,
				106, 101, 99, 116, 69, 110, 97, 98, 108, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				34, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 71, 108, 111, 98, 97, 108, 92, 67,
				82, 67, 46, 99, 115, 0, 0, 0, 4, 0,
				0, 0, 38, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 68, 101, 102, 105, 110, 101, 115, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 49, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 69, 120, 116,
				101, 110, 115, 105, 111, 110, 115, 92, 70, 108,
				111, 97, 116, 69, 120, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 47, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 69, 120, 116, 101, 110, 115,
				105, 111, 110, 115, 92, 73, 110, 116, 69, 120,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				48, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 71, 108, 111, 98, 97, 108, 92, 69,
				120, 116, 101, 110, 115, 105, 111, 110, 115, 92,
				76, 105, 115, 116, 69, 120, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 48, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 71, 108,
				111, 98, 97, 108, 92, 69, 120, 116, 101, 110,
				115, 105, 111, 110, 115, 92, 77, 97, 116, 104,
				69, 120, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 57, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 69, 120, 116, 101, 110, 115, 105, 111, 110,
				115, 92, 83, 116, 114, 105, 110, 103, 66, 117,
				105, 108, 100, 101, 114, 69, 120, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 50, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 69, 120, 116, 101,
				110, 115, 105, 111, 110, 115, 92, 83, 116, 114,
				105, 110, 103, 69, 120, 46, 99, 115, 0, 0,
				0, 3, 0, 0, 0, 50, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 69, 120, 116, 101, 110, 115,
				105, 111, 110, 115, 92, 86, 101, 99, 116, 111,
				114, 69, 120, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 37, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 71, 108, 111, 98, 97,
				108, 92, 71, 108, 111, 98, 97, 108, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 41, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 72, 111, 114,
				105, 122, 111, 110, 116, 97, 108, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 54, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 73, 67, 117, 115,
				116, 111, 109, 77, 97, 116, 101, 114, 105, 97,
				108, 82, 101, 112, 108, 97, 99, 101, 114, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 38,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 71, 108, 111, 98, 97, 108, 92, 73, 69,
				110, 116, 105, 116, 121, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 55, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 73, 109, 97, 103, 101, 69,
				102, 102, 101, 99, 116, 115, 92, 73, 99, 111,
				110, 79, 117, 116, 108, 105, 110, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 45, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 73, 110, 115,
				112, 101, 99, 116, 111, 114, 70, 108, 97, 103,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 42, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 71, 108, 111, 98, 97, 108, 92,
				73, 110, 115, 116, 97, 110, 116, 105, 97, 116,
				101, 46, 99, 115, 0, 0, 0, 8, 0, 0,
				0, 52, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 71, 108, 111, 98, 97, 108, 92,
				73, 110, 116, 101, 114, 102, 97, 99, 101, 115,
				92, 80, 114, 101, 80, 114, 111, 99, 101, 115,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 58, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 71, 108, 111, 98, 97, 108, 92,
				73, 110, 116, 101, 114, 102, 97, 99, 101, 115,
				92, 80, 114, 111, 112, 82, 101, 110, 100, 101,
				114, 78, 111, 116, 105, 102, 121, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 56, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 76, 105, 103, 104,
				116, 105, 110, 103, 92, 73, 110, 100, 105, 114,
				101, 99, 116, 76, 105, 103, 104, 116, 105, 110,
				103, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 50, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 71, 108, 111, 98, 97, 108, 92,
				77, 97, 116, 101, 114, 105, 97, 108, 82, 101,
				112, 108, 97, 99, 101, 109, 101, 110, 116, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 40,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 71, 108, 111, 98, 97, 108, 92, 77, 97,
				116, 104, 92, 65, 65, 66, 66, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 43, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 77, 97, 116, 104,
				92, 67, 97, 112, 115, 117, 108, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 44, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 77, 97, 116,
				104, 92, 67, 121, 108, 105, 110, 100, 101, 114,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				39, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 71, 108, 111, 98, 97, 108, 92, 77,
				97, 116, 104, 92, 71, 74, 75, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 40, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 77, 97, 116, 104,
				92, 76, 105, 110, 101, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 41, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 77, 97, 116, 104, 92, 77,
				97, 116, 104, 120, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 39, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 71, 108, 111, 98,
				97, 108, 92, 77, 97, 116, 104, 92, 79, 66,
				66, 46, 99, 115, 0, 0, 0, 2, 0, 0,
				0, 46, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 71, 108, 111, 98, 97, 108, 92,
				77, 97, 116, 104, 92, 83, 101, 101, 100, 82,
				97, 110, 100, 111, 109, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 42, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 77, 97, 116, 104, 92, 83,
				112, 104, 101, 114, 101, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 44, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 77, 97, 116, 104, 92, 84,
				114, 105, 97, 110, 103, 108, 101, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 41, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 79, 98, 106, 101,
				99, 116, 80, 111, 111, 108, 46, 99, 115, 0,
				0, 0, 2, 0, 0, 0, 43, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 71, 108,
				111, 98, 97, 108, 92, 80, 114, 111, 112, 82,
				101, 110, 100, 101, 114, 101, 114, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 48, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 71,
				108, 111, 98, 97, 108, 92, 80, 114, 111, 112,
				82, 101, 110, 100, 101, 114, 101, 114, 68, 101,
				98, 117, 103, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 39, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 71, 108, 111, 98, 97,
				108, 92, 80, 114, 111, 116, 111, 99, 111, 108,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				46, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 71, 108, 111, 98, 97, 108, 92, 82,
				101, 103, 105, 115, 116, 114, 121, 92, 69, 110,
				116, 105, 116, 121, 46, 99, 115, 0, 0, 0,
				2, 0, 0, 0, 40, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 71, 108, 111, 98,
				97, 108, 92, 84, 105, 99, 107, 92, 84, 105,
				99, 107, 46, 99, 115, 0, 0, 0, 3, 0,
				0, 0, 45, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 84, 105, 99, 107, 92, 84, 105, 99, 107,
				46, 76, 105, 115, 116, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 49, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 84, 105, 99, 107, 92, 84,
				105, 99, 107, 67, 111, 109, 112, 111, 110, 101,
				110, 116, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 55, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 85, 116, 105, 108, 105, 116, 121, 92, 65,
				115, 121, 110, 99, 84, 101, 120, 116, 117, 114,
				101, 76, 111, 97, 100, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 49, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 71, 108, 111,
				98, 97, 108, 92, 85, 116, 105, 108, 105, 116,
				121, 92, 84, 101, 120, 116, 66, 117, 102, 102,
				101, 114, 46, 99, 115, 0, 0, 0, 4, 0,
				0, 0, 48, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 71, 108, 111, 98, 97, 108,
				92, 85, 116, 105, 108, 105, 116, 121, 92, 84,
				101, 120, 116, 84, 97, 98, 108, 101, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 55, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				71, 108, 111, 98, 97, 108, 92, 85, 116, 105,
				108, 105, 116, 121, 92, 85, 110, 115, 97, 102,
				101, 76, 105, 115, 116, 65, 99, 99, 101, 115,
				115, 46, 99, 115
			},
			TypesData = new byte[1898]
			{
				0, 0, 0, 0, 16, 82, 117, 115, 116, 124,
				65, 112, 112, 108, 105, 99, 97, 116, 105, 111,
				110, 0, 0, 0, 0, 17, 124, 65, 114, 114,
				97, 121, 73, 110, 100, 101, 120, 73, 115, 69,
				110, 117, 109, 0, 0, 0, 0, 23, 124, 65,
				114, 114, 97, 121, 73, 110, 100, 101, 120, 73,
				115, 69, 110, 117, 109, 82, 97, 110, 103, 101,
				100, 0, 0, 0, 0, 10, 124, 65, 115, 115,
				101, 116, 80, 111, 111, 108, 0, 0, 0, 0,
				14, 65, 115, 115, 101, 116, 80, 111, 111, 108,
				124, 80, 111, 111, 108, 1, 0, 0, 0, 16,
				124, 79, 98, 106, 101, 99, 116, 87, 111, 114,
				107, 81, 117, 101, 117, 101, 1, 0, 0, 0,
				16, 124, 79, 98, 106, 101, 99, 116, 87, 111,
				114, 107, 81, 117, 101, 117, 101, 1, 0, 0,
				0, 26, 124, 80, 101, 114, 115, 105, 115, 116,
				101, 110, 116, 79, 98, 106, 101, 99, 116, 87,
				111, 114, 107, 81, 117, 101, 117, 101, 1, 0,
				0, 0, 26, 124, 80, 101, 114, 115, 105, 115,
				116, 101, 110, 116, 79, 98, 106, 101, 99, 116,
				87, 111, 114, 107, 81, 117, 101, 117, 101, 1,
				0, 0, 0, 15, 124, 87, 111, 114, 108, 100,
				83, 112, 97, 99, 101, 71, 114, 105, 100, 1,
				0, 0, 0, 15, 124, 87, 111, 114, 108, 100,
				83, 112, 97, 99, 101, 71, 114, 105, 100, 0,
				0, 0, 0, 45, 82, 117, 115, 116, 46, 67,
				111, 109, 112, 111, 110, 101, 110, 116, 115, 46,
				67, 97, 109, 101, 114, 97, 124, 68, 101, 112,
				116, 104, 79, 102, 70, 105, 101, 108, 100, 70,
				111, 99, 117, 115, 80, 111, 105, 110, 116, 0,
				0, 0, 0, 39, 82, 117, 115, 116, 46, 67,
				111, 109, 112, 111, 110, 101, 110, 116, 115, 46,
				67, 97, 109, 101, 114, 97, 124, 76, 105, 103,
				104, 116, 105, 110, 103, 79, 118, 101, 114, 114,
				105, 100, 101, 0, 0, 0, 0, 33, 82, 117,
				115, 116, 46, 67, 111, 109, 112, 111, 110, 101,
				110, 116, 115, 124, 78, 117, 108, 108, 77, 111,
				110, 111, 66, 101, 104, 97, 118, 105, 111, 117,
				114, 0, 0, 0, 0, 39, 82, 117, 115, 116,
				46, 67, 111, 109, 112, 111, 110, 101, 110, 116,
				115, 46, 85, 116, 105, 108, 105, 116, 121, 124,
				79, 110, 79, 98, 106, 101, 99, 116, 68, 105,
				115, 97, 98, 108, 101, 0, 0, 0, 0, 38,
				82, 117, 115, 116, 46, 67, 111, 109, 112, 111,
				110, 101, 110, 116, 115, 46, 85, 116, 105, 108,
				105, 116, 121, 124, 79, 110, 79, 98, 106, 101,
				99, 116, 69, 110, 97, 98, 108, 101, 0, 0,
				0, 0, 4, 124, 67, 82, 67, 0, 0, 0,
				0, 11, 82, 117, 115, 116, 124, 76, 97, 121,
				101, 114, 115, 0, 0, 0, 0, 18, 82, 117,
				115, 116, 46, 76, 97, 121, 101, 114, 115, 124,
				83, 101, 114, 118, 101, 114, 0, 0, 0, 0,
				18, 82, 117, 115, 116, 46, 76, 97, 121, 101,
				114, 115, 124, 67, 108, 105, 101, 110, 116, 0,
				0, 0, 0, 16, 82, 117, 115, 116, 46, 76,
				97, 121, 101, 114, 115, 124, 77, 97, 115, 107,
				0, 0, 0, 0, 8, 124, 70, 108, 111, 97,
				116, 69, 120, 0, 0, 0, 0, 17, 85, 110,
				105, 116, 121, 69, 110, 103, 105, 110, 101, 124,
				73, 110, 116, 69, 120, 0, 0, 0, 0, 7,
				124, 76, 105, 115, 116, 69, 120, 0, 0, 0,
				0, 18, 85, 110, 105, 116, 121, 69, 110, 103,
				105, 110, 101, 124, 77, 97, 116, 104, 69, 120,
				0, 0, 0, 0, 16, 124, 83, 116, 114, 105,
				110, 103, 66, 117, 105, 108, 100, 101, 114, 69,
				120, 0, 0, 0, 0, 20, 85, 110, 105, 116,
				121, 69, 110, 103, 105, 110, 101, 124, 83, 116,
				114, 105, 110, 103, 69, 120, 0, 0, 0, 0,
				21, 85, 110, 105, 116, 121, 69, 110, 103, 105,
				110, 101, 124, 86, 101, 99, 116, 111, 114, 50,
				69, 120, 0, 0, 0, 0, 21, 85, 110, 105,
				116, 121, 69, 110, 103, 105, 110, 101, 124, 86,
				101, 99, 116, 111, 114, 51, 69, 120, 0, 0,
				0, 0, 21, 85, 110, 105, 116, 121, 69, 110,
				103, 105, 110, 101, 124, 86, 101, 99, 116, 111,
				114, 52, 69, 120, 0, 0, 0, 0, 11, 82,
				117, 115, 116, 124, 71, 108, 111, 98, 97, 108,
				0, 0, 0, 0, 20, 124, 72, 111, 114, 105,
				122, 111, 110, 116, 97, 108, 65, 116, 116, 114,
				105, 98, 117, 116, 101, 0, 0, 0, 0, 24,
				124, 73, 67, 117, 115, 116, 111, 109, 77, 97,
				116, 101, 114, 105, 97, 108, 82, 101, 112, 108,
				97, 99, 101, 114, 0, 0, 0, 0, 12, 82,
				117, 115, 116, 124, 73, 69, 110, 116, 105, 116,
				121, 0, 0, 0, 0, 29, 82, 117, 115, 116,
				46, 73, 109, 97, 103, 101, 69, 102, 102, 101,
				99, 116, 115, 124, 73, 99, 111, 110, 79, 117,
				116, 108, 105, 110, 101, 0, 0, 0, 0, 24,
				124, 73, 110, 115, 112, 101, 99, 116, 111, 114,
				70, 108, 97, 103, 115, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 0, 0, 0, 0, 21, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 124, 73,
				110, 115, 116, 97, 110, 116, 105, 97, 116, 101,
				0, 0, 0, 0, 17, 124, 73, 69, 100, 105,
				116, 111, 114, 67, 111, 109, 112, 111, 110, 101,
				110, 116, 0, 0, 0, 0, 17, 124, 73, 83,
				101, 114, 118, 101, 114, 67, 111, 109, 112, 111,
				110, 101, 110, 116, 0, 0, 0, 0, 17, 124,
				73, 67, 108, 105, 101, 110, 116, 67, 111, 109,
				112, 111, 110, 101, 110, 116, 0, 0, 0, 0,
				18, 124, 73, 80, 114, 101, 102, 97, 98, 80,
				114, 101, 80, 114, 111, 99, 101, 115, 115, 0,
				0, 0, 0, 19, 124, 73, 80, 114, 101, 102,
				97, 98, 80, 111, 115, 116, 80, 114, 111, 99,
				101, 115, 115, 0, 0, 0, 0, 17, 124, 73,
				80, 114, 101, 102, 97, 98, 80, 114, 111, 99,
				101, 115, 115, 111, 114, 0, 0, 0, 0, 19,
				124, 73, 83, 101, 114, 118, 101, 114, 67, 111,
				109, 112, 111, 110, 101, 110, 116, 69, 120, 0,
				0, 0, 0, 19, 124, 73, 67, 108, 105, 101,
				110, 116, 67, 111, 109, 112, 111, 110, 101, 110,
				116, 69, 120, 0, 0, 0, 0, 18, 124, 73,
				80, 114, 111, 112, 82, 101, 110, 100, 101, 114,
				78, 111, 116, 105, 102, 121, 0, 0, 0, 0,
				21, 82, 117, 115, 116, 124, 73, 110, 100, 105,
				114, 101, 99, 116, 76, 105, 103, 104, 116, 105,
				110, 103, 0, 0, 0, 0, 40, 82, 117, 115,
				116, 46, 73, 110, 100, 105, 114, 101, 99, 116,
				76, 105, 103, 104, 116, 105, 110, 103, 124, 65,
				109, 98, 105, 101, 110, 116, 80, 114, 111, 98,
				101, 80, 97, 114, 97, 109, 115, 0, 0, 0,
				0, 20, 124, 77, 97, 116, 101, 114, 105, 97,
				108, 82, 101, 112, 108, 97, 99, 101, 109, 101,
				110, 116, 0, 0, 0, 0, 5, 124, 65, 65,
				66, 66, 0, 0, 0, 0, 8, 124, 67, 97,
				112, 115, 117, 108, 101, 0, 0, 0, 0, 9,
				124, 67, 121, 108, 105, 110, 100, 101, 114, 0,
				0, 0, 0, 4, 124, 71, 74, 75, 0, 0,
				0, 0, 8, 124, 83, 105, 109, 112, 108, 101,
				120, 0, 0, 0, 0, 5, 124, 76, 105, 110,
				101, 0, 0, 0, 0, 6, 124, 77, 97, 116,
				104, 120, 0, 0, 0, 0, 4, 124, 79, 66,
				66, 0, 0, 0, 0, 7, 124, 83, 101, 101,
				100, 69, 120, 0, 0, 0, 0, 11, 124, 83,
				101, 101, 100, 82, 97, 110, 100, 111, 109, 0,
				0, 0, 0, 7, 124, 83, 112, 104, 101, 114,
				101, 0, 0, 0, 0, 9, 124, 84, 114, 105,
				97, 110, 103, 108, 101, 0, 0, 0, 0, 20,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 124,
				79, 98, 106, 101, 99, 116, 80, 111, 111, 108,
				0, 0, 0, 0, 24, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 124, 71, 97, 109, 101, 79,
				98, 106, 101, 99, 116, 80, 111, 111, 108, 0,
				0, 0, 0, 17, 82, 117, 115, 116, 124, 80,
				114, 111, 112, 82, 101, 110, 100, 101, 114, 101,
				114, 0, 0, 0, 0, 36, 82, 117, 115, 116,
				46, 80, 114, 111, 112, 82, 101, 110, 100, 101,
				114, 101, 114, 124, 83, 107, 105, 110, 86, 105,
				101, 119, 101, 114, 83, 101, 116, 116, 105, 110,
				103, 115, 0, 0, 0, 0, 22, 82, 117, 115,
				116, 124, 80, 114, 111, 112, 82, 101, 110, 100,
				101, 114, 101, 114, 68, 101, 98, 117, 103, 0,
				0, 0, 0, 13, 82, 117, 115, 116, 124, 80,
				114, 111, 116, 111, 99, 111, 108, 0, 0, 0,
				0, 20, 82, 117, 115, 116, 46, 82, 101, 103,
				105, 115, 116, 114, 121, 124, 69, 110, 116, 105,
				116, 121, 1, 0, 0, 0, 14, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 124, 84, 105, 99,
				107, 1, 0, 0, 0, 20, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 84, 105, 99, 107,
				124, 69, 110, 116, 114, 121, 1, 0, 0, 0,
				14, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				124, 84, 105, 99, 107, 1, 0, 0, 0, 20,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				84, 105, 99, 107, 124, 69, 110, 116, 114, 121,
				0, 0, 0, 0, 20, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 84, 105, 99, 107, 43,
				124, 76, 105, 115, 116, 0, 0, 0, 0, 23,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 124,
				84, 105, 99, 107, 67, 111, 109, 112, 111, 110,
				101, 110, 116, 0, 0, 0, 0, 21, 82, 117,
				115, 116, 124, 65, 115, 121, 110, 99, 84, 101,
				120, 116, 117, 114, 101, 76, 111, 97, 100, 0,
				0, 0, 0, 11, 124, 84, 101, 120, 116, 66,
				117, 102, 102, 101, 114, 0, 0, 0, 0, 10,
				124, 84, 101, 120, 116, 84, 97, 98, 108, 101,
				0, 0, 0, 0, 23, 84, 101, 120, 116, 84,
				97, 98, 108, 101, 124, 82, 111, 119, 86, 97,
				108, 117, 101, 85, 110, 105, 111, 110, 0, 0,
				0, 0, 18, 84, 101, 120, 116, 84, 97, 98,
				108, 101, 124, 82, 111, 119, 86, 97, 108, 117,
				101, 0, 0, 0, 0, 16, 84, 101, 120, 116,
				84, 97, 98, 108, 101, 124, 67, 111, 108, 117,
				109, 110, 0, 0, 0, 0, 17, 124, 85, 110,
				115, 97, 102, 101, 76, 105, 115, 116, 65, 99,
				99, 101, 115, 115, 0, 0, 0, 0, 39, 85,
				110, 115, 97, 102, 101, 76, 105, 115, 116, 65,
				99, 99, 101, 115, 115, 124, 76, 105, 115, 116,
				80, 114, 105, 118, 97, 116, 101, 70, 105, 101,
				108, 100, 65, 99, 99, 101, 115, 115
			},
			TotalFiles = 53,
			TotalTypes = 82,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch
{
	public static class Instantiate
	{
		public static GameObject GameObject(GameObject go, Transform parent = null)
		{
			return UnityEngine.Object.Instantiate(go, parent);
		}

		public static GameObject GameObject(GameObject go, Vector3 pos, Quaternion rot)
		{
			return UnityEngine.Object.Instantiate(go, pos, rot);
		}
	}
	public class ObjectPool<T>
	{
		public List<T> list = new List<T>();

		public virtual void AddToPool(T t)
		{
			list.Add(t);
		}

		public T TakeFromPool()
		{
			if (list.Count == 0)
			{
				return default(T);
			}
			T result = list[0];
			list.RemoveAt(0);
			return result;
		}
	}
	public class GameObjectPool<T> : ObjectPool<T> where T : UnityEngine.Component
	{
		private GameObject poolRoot;

		public override void AddToPool(T t)
		{
			if (!Rust.Application.isQuitting)
			{
				if (poolRoot == null)
				{
					poolRoot = new GameObject("GameObjectPool - " + typeof(T).Name);
					poolRoot.SetActive(value: false);
					UnityEngine.Object.DontDestroyOnLoad(poolRoot);
				}
				base.AddToPool(t);
				t.gameObject.SetActive(value: false);
				t.transform.SetParent(poolRoot.transform, worldPositionStays: false);
			}
		}

		public void AddChildrenToPool(Transform t)
		{
			T[] array = (from Transform x in t
				select x.GetComponent<T>() into x
				where x != null
				select x).ToArray();
			foreach (T val in array)
			{
				if (!val.CompareTag("persist"))
				{
					AddToPool(val);
				}
			}
		}

		public T TakeOrInstantiate(GameObject prefabSource)
		{
			T val = TakeFromPool();
			if (val != null)
			{
				val.gameObject.SetActive(value: true);
				return val;
			}
			GameObject gameObject = Instantiate.GameObject(prefabSource);
			UnityEngine.Debug.Assert(gameObject != null, "GameObjectPool - passed prefab didn't have a valid component!");
			gameObject.transform.SetParent(null, worldPositionStays: false);
			gameObject.SetActive(value: true);
			return gameObject.GetComponent<T>();
		}
	}
	public static class Tick
	{
		public struct Entry
		{
			public class List : List<Entry>
			{
				public void Remove(UnityEngine.Object obj)
				{
					for (int i = 0; i < base.Count; i++)
					{
						if (base[i].TargetObject == obj || base[i].Errored)
						{
							RemoveAt(i);
							i--;
						}
					}
				}

				internal void Tick()
				{
					int i = 0;
					try
					{
						for (i = 0; i < base.Count; i++)
						{
							base[i].Function();
						}
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
						Entry value = base[i];
						value.Errored = true;
						base[i] = value;
					}
				}

				internal void TickTimed()
				{
					float time = Time.time;
					int i = 0;
					try
					{
						for (i = 0; i < base.Count; i++)
						{
							Entry value = base[i];
							if (!(value.NextCall > time))
							{
								value.Function();
								value.NextCall = time + value.MinDelay + value.RandDelay * UnityEngine.Random.Range(0f, 1f);
								base[i] = value;
							}
						}
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
						Entry value2 = base[i];
						value2.Errored = true;
						base[i] = value2;
					}
				}
			}

			public UnityEngine.Object TargetObject;

			public float MinDelay;

			public float RandDelay;

			public float NextCall;

			public Action Function;

			private bool Errored;

			public string DebugName;
		}

		private static Entry.List Timed = new Entry.List();

		private static Entry.List Update = new Entry.List();

		private static Entry.List Late = new Entry.List();

		private static List<UnityEngine.Object> RemoveList = new List<UnityEngine.Object>(32);

		public static void AddTimed(UnityEngine.Object obj, float minDelay, float maxDelay, Action action, string DebugName)
		{
			TickComponent.Init();
			Timed.Add(new Entry
			{
				TargetObject = obj,
				MinDelay = minDelay,
				RandDelay = maxDelay - minDelay,
				Function = action,
				DebugName = $"{DebugName} - {obj.name}"
			});
		}

		public static void Add(UnityEngine.Object obj, Action action, string DebugName)
		{
			TickComponent.Init();
			Update.Add(new Entry
			{
				TargetObject = obj,
				Function = action,
				DebugName = $"{DebugName} - {obj.name}"
			});
		}

		public static void AddLateUpdate(UnityEngine.Object obj, Action action, string DebugName)
		{
			TickComponent.Init();
			Late.Add(new Entry
			{
				TargetObject = obj,
				Function = action,
				DebugName = $"{DebugName} - {obj.name}"
			});
		}

		public static void RemoveAll(UnityEngine.Object obj)
		{
			RemoveList.Add(obj);
		}

		private static void Cleanup()
		{
			if (RemoveList.Count != 0)
			{
				for (int i = 0; i < RemoveList.Count; i++)
				{
					UnityEngine.Object obj = RemoveList[i];
					Timed.Remove(obj);
					Update.Remove(obj);
					Late.Remove(obj);
				}
				RemoveList.Clear();
			}
		}

		internal static void OnFrame()
		{
			Cleanup();
			Update.Tick();
			Cleanup();
			Timed.TickTimed();
		}

		internal static void OnFrameLate()
		{
			Cleanup();
			Late.Tick();
		}
	}
	public class TickComponent : MonoBehaviour
	{
		public static TickComponent Instance;

		public static void Init()
		{
			if (!(Instance != null))
			{
				new GameObject("Tick Manager").AddComponent<TickComponent>();
			}
		}

		private void OnEnable()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Instance = this;
		}

		private void Update()
		{
			Tick.OnFrame();
		}

		private void LateUpdate()
		{
			Tick.OnFrameLate();
		}
	}
}
namespace UnityEngine
{
	public static class IntEx
	{
		public static int Digits(this int n)
		{
			if (n >= 0)
			{
				if (n < 10)
				{
					return 1;
				}
				if (n < 100)
				{
					return 2;
				}
				if (n < 1000)
				{
					return 3;
				}
				if (n < 10000)
				{
					return 4;
				}
				if (n < 100000)
				{
					return 5;
				}
				if (n < 1000000)
				{
					return 6;
				}
				if (n < 10000000)
				{
					return 7;
				}
				if (n < 100000000)
				{
					return 8;
				}
				if (n < 1000000000)
				{
					return 9;
				}
				return 10;
			}
			if (n > -10)
			{
				return 2;
			}
			if (n > -100)
			{
				return 3;
			}
			if (n > -1000)
			{
				return 4;
			}
			if (n > -10000)
			{
				return 5;
			}
			if (n > -100000)
			{
				return 6;
			}
			if (n > -1000000)
			{
				return 7;
			}
			if (n > -10000000)
			{
				return 8;
			}
			if (n > -100000000)
			{
				return 9;
			}
			if (n > -1000000000)
			{
				return 10;
			}
			return 11;
		}

		public static int Digits(this uint n)
		{
			if (n < 10)
			{
				return 1;
			}
			if (n < 100)
			{
				return 2;
			}
			if (n < 1000)
			{
				return 3;
			}
			if (n < 10000)
			{
				return 4;
			}
			if (n < 100000)
			{
				return 5;
			}
			if (n < 1000000)
			{
				return 6;
			}
			if (n < 10000000)
			{
				return 7;
			}
			if (n < 100000000)
			{
				return 8;
			}
			if (n < 1000000000)
			{
				return 9;
			}
			return 10;
		}

		public static string ToZeroPaddedString(this int value, int noOfDigits)
		{
			switch (noOfDigits)
			{
			case 1:
				return value.ToString("0");
			case 2:
				return value.ToString("00");
			case 3:
				return value.ToString("000");
			case 4:
				return value.ToString("0000");
			case 5:
				return value.ToString("00000");
			case 6:
				return value.ToString("000000");
			case 7:
				return value.ToString("0000000");
			case 8:
				return value.ToString("00000000");
			case 9:
				return value.ToString("000000000");
			case 10:
				return value.ToString("0000000000");
			default:
			{
				string text = value.ToString();
				Debug.LogError($"Number of digits {noOfDigits} is unsupported, returning {text}");
				return text;
			}
			}
		}

		public static string ToZeroPaddedString(this uint value, int noOfDigits)
		{
			switch (noOfDigits)
			{
			case 1:
				return value.ToString("0");
			case 2:
				return value.ToString("00");
			case 3:
				return value.ToString("000");
			case 4:
				return value.ToString("0000");
			case 5:
				return value.ToString("00000");
			case 6:
				return value.ToString("000000");
			case 7:
				return value.ToString("0000000");
			case 8:
				return value.ToString("00000000");
			case 9:
				return value.ToString("000000000");
			case 10:
				return value.ToString("0000000000");
			default:
			{
				string text = value.ToString();
				Debug.LogError($"Number of digits {noOfDigits} is unsupported, returning {text}");
				return text;
			}
			}
		}
	}
	public static class MathEx
	{
		public static float SnapTo(this float val, float snapValue)
		{
			if (snapValue == 0f)
			{
				return val;
			}
			return Mathf.Round(val / snapValue) * snapValue;
		}

		public static bool QuadTest(this Ray ray, Vector3 planeCenter, Quaternion planeRot, Vector2 planeSize, out Vector3 hitPosition, float gridSize = 0f)
		{
			Plane plane = new Plane(planeRot * Vector3.forward, planeCenter);
			hitPosition = Vector3.zero;
			float enter = 0f;
			if (!plane.Raycast(ray, out enter))
			{
				return false;
			}
			hitPosition = ray.origin + ray.direction * enter;
			Vector3 lhs = hitPosition - planeCenter;
			float num = Vector3.Dot(lhs, planeRot * Vector3.left);
			float num2 = Vector3.Dot(lhs, planeRot * Vector3.up);
			if (Mathf.Abs(num) > planeSize.x / 2f)
			{
				num = ((num < 0f) ? (0f - planeSize.x) : planeSize.x) / 2f;
			}
			if (Mathf.Abs(num2) > planeSize.y / 2f)
			{
				num2 = ((num2 < 0f) ? (0f - planeSize.y) : planeSize.y) / 2f;
			}
			if (gridSize > 0f)
			{
				num = num.SnapTo(gridSize);
				num2 = num2.SnapTo(gridSize);
			}
			hitPosition = planeCenter;
			hitPosition += planeRot * Vector3.left * num;
			hitPosition += planeRot * Vector3.up * num2;
			return true;
		}

		public static float BiasedLerp(float x, float bias)
		{
			float num = ((!(bias <= 0.5f)) ? (1f - Bias(1f - Mathf.Abs(x), 1f - bias)) : Bias(Mathf.Abs(x), bias));
			if (!(x < 0f))
			{
				return num;
			}
			return 0f - num;
		}

		public static float Bias(float x, float bias)
		{
			if (x <= 0f || bias <= 0f)
			{
				return 0f;
			}
			if (x >= 1f || bias >= 1f)
			{
				return 1f;
			}
			if (bias == 0.5f)
			{
				return x;
			}
			float p = Mathf.Log(bias) * -1.4427f;
			return Mathf.Pow(x, p);
		}
	}
	public static class StringEx
	{
		private static readonly Regex regexNumeric = new Regex("^[0-9]+$");

		private static readonly Regex regexAlphaNumeric = new Regex("^[a-zA-Z0-9]+$");

		public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
		{
			int startIndex = 0;
			while (true)
			{
				startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
				if (startIndex == -1)
				{
					break;
				}
				originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);
				startIndex += newValue.Length;
			}
			return originalString;
		}

		public static bool Contains(this string haystack, string needle, CompareOptions options)
		{
			return CultureInfo.InvariantCulture.CompareInfo.IndexOf(haystack, needle, options) >= 0;
		}

		public static bool IsLower(this string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (char.IsUpper(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static string ToPrintable(this string str, int maxLength = int.MaxValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(str))
			{
				TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(str);
				int num = Mathf.Min(str.Length, maxLength);
				for (int i = 0; i < num; i++)
				{
					if (!textElementEnumerator.MoveNext())
					{
						break;
					}
					string textElement = textElementEnumerator.GetTextElement();
					if (!char.IsControl(textElement, 0))
					{
						stringBuilder.Append(textElement);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public static bool IsNumeric(this string str)
		{
			return regexNumeric.IsMatch(str);
		}

		public static bool IsAlphaNumeric(this string str)
		{
			return regexAlphaNumeric.IsMatch(str);
		}

		public static string FilterRichText(this string str, bool invert, params string[] tags)
		{
			if (tags == null || tags.Length == 0)
			{
				return str;
			}
			if (!str.Contains("<") && !str.Contains(">"))
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = str.Split('<');
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				if (string.IsNullOrWhiteSpace(array[i]))
				{
					if (i == array.Length - 1 || string.IsNullOrWhiteSpace(array[i + 1]))
					{
						stringBuilder.Append("<");
					}
					continue;
				}
				if (i == 0)
				{
					stringBuilder.Append(array[i]);
					continue;
				}
				foreach (string obj in tags)
				{
					int num = ((array[i][0] == '/') ? 1 : 0);
					int k;
					for (k = num; k < array[i].Length - num; k++)
					{
						char c = array[i][k];
						if (c != '-' && (uint)((c | 0x20) - 97) > 25u)
						{
							break;
						}
					}
					if (new StringView(array[i]).Substring(num, k - num).Equals(obj, StringComparison.CurrentCultureIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (flag == invert)
				{
					stringBuilder.Append("<");
					stringBuilder.Append(array[i]);
				}
				else
				{
					stringBuilder.Append("<\u200b");
					stringBuilder.Append(array[i].Replace(">", "\u200b>"));
				}
			}
			return stringBuilder.ToString();
		}

		public static string EscapeRichText(this string str, bool altMode = false)
		{
			if (str.Contains("<"))
			{
				str = ((!altMode) ? str.Replace("<", "<\u200b") : str.Replace("<", "〈"));
			}
			if (str.Contains(">"))
			{
				str = ((!altMode) ? str.Replace(">", "\u200b>") : str.Replace(">", "〉"));
			}
			return str;
		}

		public static IEnumerable<string> SplitToLines(this string input)
		{
			if (input == null)
			{
				yield break;
			}
			using StringReader reader = new StringReader(input);
			string text;
			while ((text = reader.ReadLine()) != null)
			{
				yield return text;
			}
		}

		public static IEnumerable<string> SplitToChunks(this string str, int chunkLength)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("string cannot be null");
			}
			if (chunkLength < 1)
			{
				throw new ArgumentException("chunk length needs to be more than 0");
			}
			for (int i = 0; i < str.Length; i += chunkLength)
			{
				if (chunkLength + i >= str.Length)
				{
					chunkLength = str.Length - i;
				}
				yield return str.Substring(i, chunkLength);
			}
		}

		public static uint ManifestHash(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return 0u;
			}
			if (!str.IsLower())
			{
				str = str.ToLower();
			}
			return BitConverter.ToUInt32(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str)), 0);
		}

		public static byte[] Sha256(this string str)
		{
			if (str == null)
			{
				throw new NullReferenceException("Input string cannot be null");
			}
			return new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
		}

		public static string HexString(this byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		public static bool StartsWithAny(this string str, string[] values)
		{
			foreach (string value in values)
			{
				if (str.StartsWith(value))
				{
					return true;
				}
			}
			return false;
		}
	}
	public static class Vector2Ex
	{
		public static Vector2 WithX(this Vector2 v, float x)
		{
			return new Vector2(x, v.y);
		}

		public static Vector2 WithY(this Vector2 v, float y)
		{
			return new Vector2(v.x, y);
		}

		public static Vector2 Parse(string p)
		{
			string[] array = p.Split(' ');
			if (array.Length == 2 && float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2))
			{
				return new Vector2(result, result2);
			}
			return Vector2.zero;
		}

		public static Vector2 Rotate(this Vector2 v, float degrees)
		{
			float f = degrees * (MathF.PI / 180f);
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			return new Vector2(v.x * num2 - v.y * num, v.y * num2 + v.x * num);
		}

		public static float Length(float x, float y)
		{
			return Mathf.Sqrt(x * x + y * y);
		}

		public static float Length(Vector2 vec)
		{
			return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
		}

		public static Vector2 X(this Vector2 v, float x)
		{
			return new Vector2(x, v.y);
		}

		public static Vector2 Y(this Vector2 v, float y)
		{
			return new Vector2(v.x, y);
		}

		public static Vector2 ToCanvas(this Vector2 v, RectTransform target, Camera cam = null)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(target, v, cam, out var localPoint);
			return localPoint;
		}

		public static float AngleFromTo(Vector2 from, Vector2 to)
		{
			return Mathf.Atan2(to.y - from.y, to.x - from.x) * (180f / MathF.PI);
		}
	}
	public static class Vector3Ex
	{
		public static Vector3 WithX(this Vector3 v, float x)
		{
			return new Vector3(x, v.y, v.z);
		}

		public static Vector3 WithY(this Vector3 v, float y)
		{
			return new Vector3(v.x, y, v.z);
		}

		public static Vector3 WithZ(this Vector3 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		public static Vector3 WithXY(this Vector3 v, float x, float y)
		{
			return new Vector3(x, y, v.z);
		}

		public static Vector3 WithXY(this Vector3 v, Vector2 other)
		{
			return new Vector3(other.x, other.y, v.z);
		}

		public static Vector3 WithXZ(this Vector3 v, float x, float z)
		{
			return new Vector3(x, v.y, z);
		}

		public static Vector3 WithXZ(this Vector3 v, Vector2 other)
		{
			return new Vector3(other.x, v.y, other.y);
		}

		public static Vector3 XZ(Vector3 v)
		{
			return new Vector3(v.x, 0f, v.z);
		}

		public static float Distance2D(Vector3 a, Vector3 b)
		{
			return Vector3.Distance(new Vector3(a.x, 0f, a.z), new Vector3(b.x, 0f, b.z));
		}

		public static float HeightDelta(Vector3 a, Vector3 b)
		{
			return Mathf.Abs(a.y - b.y);
		}

		public static Vector3 Direction2D(Vector3 aimAt, Vector3 aimFrom)
		{
			return (new Vector3(aimAt.x, 0f, aimAt.z) - new Vector3(aimFrom.x, 0f, aimFrom.z)).normalized;
		}

		public static Vector3 Direction(Vector3 aimAt, Vector3 aimFrom)
		{
			return (aimAt - aimFrom).normalized;
		}

		public static Vector3 Range(float x, float y)
		{
			return new Vector3(Random.Range(x, y), Random.Range(x, y), Random.Range(x, y));
		}

		public static Vector3 Scale(this Vector3 vector, float x, float y, float z)
		{
			return new Vector3(vector.x * x, vector.y * y, vector.z * z);
		}

		public static Vector3 SnapTo(this Vector3 vector, float snapValue)
		{
			return new Vector3(vector.x.SnapTo(snapValue), vector.y.SnapTo(snapValue), vector.z.SnapTo(snapValue));
		}

		public static Vector3 Inverse(this Vector3 v)
		{
			return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
		}

		public static float SignedAngle(this Vector3 v1, Vector3 v2, Vector3 n)
		{
			float num = Vector3.Angle(v1, v2);
			float num2 = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(v1, v2)));
			return num * num2;
		}

		public static void FastRenormalize(this Vector3 n, float scale = 1f)
		{
			float num = n.x * n.x + n.y * n.y + n.z * n.z;
			float num2 = (1.875f + -1.25f * num + 0.375f * num * num) * scale;
			n.x *= num2;
			n.y *= num2;
			n.z *= num2;
		}

		public static void ToDirectionAndMagnitude(this Vector3 value, out Vector3 direction, out float magnitude)
		{
			magnitude = value.magnitude;
			direction = (((double)magnitude > 0.0001) ? (value / magnitude) : Vector3.zero);
		}

		public static Vector3 BlendNormals(Vector3 n1, Vector3 n2)
		{
			return Vector3.Normalize(new Vector3(n1.x + n2.x, n1.y * n2.y, n1.z + n2.z));
		}

		public static bool IsNaNOrInfinity(this Vector3 v)
		{
			if (float.IsNaN(v.x))
			{
				return true;
			}
			if (float.IsNaN(v.y))
			{
				return true;
			}
			if (float.IsNaN(v.z))
			{
				return true;
			}
			if (float.IsInfinity(v.x))
			{
				return true;
			}
			if (float.IsInfinity(v.y))
			{
				return true;
			}
			if (float.IsInfinity(v.z))
			{
				return true;
			}
			return false;
		}

		public static float DotRadians(this Vector3 a, Vector3 b)
		{
			float num = Vector3.Dot(a.normalized, b.normalized);
			if (float.IsNaN(num))
			{
				Vector3 vector = a;
				string text = vector.ToString();
				vector = b;
				Debug.LogWarning("DotRadians NAN: " + text + " -> " + vector.ToString());
			}
			return Mathf.Acos(Mathf.Clamp(num, -1f, 1f));
		}

		public static float DotDegrees(this Vector3 a, Vector3 b)
		{
			return a.DotRadians(b) * 57.29578f;
		}

		public static float Magnitude2D(this Vector3 v)
		{
			return v.MagnitudeXZ();
		}

		public static float SqrMagnitude2D(this Vector3 v)
		{
			return v.SqrMagnitudeXZ();
		}

		public static float MagnitudeXY(this Vector3 v)
		{
			return Mathf.Sqrt(v.x * v.x + v.y * v.y);
		}

		public static float SqrMagnitudeXY(this Vector3 v)
		{
			return v.x * v.x + v.y * v.y;
		}

		public static float MagnitudeXZ(this Vector3 v)
		{
			return Mathf.Sqrt(v.x * v.x + v.z * v.z);
		}

		public static float SqrMagnitudeXZ(this Vector3 v)
		{
			return v.x * v.x + v.z * v.z;
		}

		public static float MagnitudeYZ(this Vector3 v)
		{
			return Mathf.Sqrt(v.y * v.y + v.z * v.z);
		}

		public static float MagnitudeXPositiveYZ(this Vector3 v)
		{
			return Mathf.Sqrt(v.x * v.x + ((v.y > 0f) ? (v.y * v.y) : 0f) + v.z * v.z);
		}

		public static float SqrMagnitudeYZ(this Vector3 v)
		{
			return v.y * v.y + v.z * v.z;
		}

		public static Vector3 XY3D(this Vector3 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		public static Vector3 XZ3D(this Vector3 v)
		{
			return new Vector3(v.x, 0f, v.z);
		}

		public static Vector3 YZ3D(this Vector3 v)
		{
			return new Vector3(0f, v.y, v.z);
		}

		public static Vector2 XY2D(this Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		public static Vector2 XZ2D(this Vector3 v)
		{
			return new Vector2(v.x, v.z);
		}

		public static Vector2 YZ2D(this Vector3 v)
		{
			return new Vector2(v.y, v.z);
		}

		public static Vector2 YX2D(this Vector3 v)
		{
			return new Vector2(v.y, v.x);
		}

		public static Vector2 ZX2D(this Vector3 v)
		{
			return new Vector2(v.z, v.x);
		}

		public static Vector2 ZY2D(this Vector3 v)
		{
			return new Vector2(v.z, v.y);
		}

		public static Vector3 XZ3D(this Vector2 v)
		{
			return new Vector3(v.x, 0f, v.y);
		}

		public static float Max(this Vector4 v)
		{
			return Mathf.Max(Mathf.Max(v.x, v.y), Mathf.Max(v.z, v.w));
		}

		public static float Max(this Vector3 v)
		{
			return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
		}

		public static float Max(this Vector2 v)
		{
			return Mathf.Max(v.x, v.y);
		}

		public static Vector4 Abs(this Vector4 v)
		{
			return new Vector4(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z), Mathf.Abs(v.w));
		}

		public static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		public static Vector2 Abs(this Vector2 v)
		{
			return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		public static Vector3 Parse(string p)
		{
			string[] array = p.Split(' ');
			if (array.Length == 3 && float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2) && float.TryParse(array[2], out var result3))
			{
				return new Vector3(result, result2, result3);
			}
			return Vector3.zero;
		}

		public static Vector3 GetWithInaccuracy(this Vector3 o, float maxAngle)
		{
			return Quaternion.AngleAxis(Random.Range(0f - maxAngle, maxAngle), Vector3.up) * o;
		}

		public static Vector3 NormalizeXZ(this Vector3 v)
		{
			return new Vector2(v.x, v.z).normalized.XZ3D();
		}
	}
	public static class Vector4Ex
	{
		public static Vector4 Parse(string p)
		{
			string[] array = p.Split(' ');
			if (array.Length == 4 && float.TryParse(array[0], out var result) && float.TryParse(array[1], out var result2) && float.TryParse(array[2], out var result3) && float.TryParse(array[3], out var result4))
			{
				return new Vector4(result, result2, result3, result4);
			}
			return Vector4.zero;
		}
	}
}
namespace Rust
{
	public static class Application
	{
		public static bool isPlaying;

		public static bool isQuitting;

		public static bool isLoading;

		public static bool isReceiving;

		public static bool isLoadingSave;

		public static bool isLoadingPrefabs;

		public static bool isReturningToMenu;

		public static bool isServerStarted;

		public static bool isUnloadingWorld
		{
			get
			{
				if (!isQuitting)
				{
					return isReturningToMenu;
				}
				return true;
			}
		}

		public static string installPath
		{
			get
			{
				if (UnityEngine.Application.platform == RuntimePlatform.OSXPlayer)
				{
					return UnityEngine.Application.dataPath + "/../..";
				}
				return UnityEngine.Application.dataPath + "/..";
			}
		}

		public static string dataPath => UnityEngine.Application.dataPath;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Startup()
		{
			isPlaying = true;
		}

		public static void Quit()
		{
			isQuitting = true;
			UnityEngine.Application.Quit();
		}
	}
	public static class Layers
	{
		public static class Server
		{
			public const int VehiclesSimple = 32768;

			public const int Players = 131072;

			public const int NPCs = 2048;

			public const int Buildings = 2097152;

			public const int Bullet = 1220225809;

			public const int Projectile = 1237003025;

			public const int ProjectileStaticSolid = 1084293393;

			public const int Deployed = 256;

			public const int Stability = 2097408;

			public const int Decay = 2097408;

			public const int PlayerMovement = 1503731969;

			public const int PlayerGrounded = 1503764737;

			public const int GroundWatch = 161546240;

			public const int Targets = 133120;

			public const int VehicleCollision = 1235583233;

			public const int RagdollCollision = -910884607;
		}

		public static class Client
		{
			public const int Melee = 1270440705;

			public const int Bullet = 1270440721;

			public const int PlayerUse = -1917228287;

			public const int PlayerMovement = 1537286401;

			public const int PlayerStepDetection = 455155969;

			public const int Footstep = 144777217;

			public const int EntTrace = -877042943;
		}

		public static class Mask
		{
			public const int Default = 1;

			public const int TransparentFX = 2;

			public const int Ignore_Raycast = 4;

			public const int Reserved1 = 8;

			public const int Water = 16;

			public const int UI = 32;

			public const int Reserved2 = 64;

			public const int Reserved3 = 128;

			public const int Deployed = 256;

			public const int Ragdoll = 512;

			public const int Invisible = 1024;

			public const int AI = 2048;

			public const int Player_Movement = 4096;

			public const int Vehicle_Detailed = 8192;

			public const int Game_Trace = 16384;

			public const int Vehicle_World = 32768;

			public const int World = 65536;

			public const int Player_Server = 131072;

			public const int Trigger = 262144;

			public const int Harvestable = 524288;

			public const int Physics_Projectile = 1048576;

			public const int Construction = 2097152;

			public const int Construction_Socket = 4194304;

			public const int Terrain = 8388608;

			public const int Transparent = 16777216;

			public const int Clutter = 33554432;

			public const int Bush = 67108864;

			public const int Vehicle_Large = 134217728;

			public const int Prevent_Movement = 268435456;

			public const int Prevent_Building = 536870912;

			public const int Tree = 1073741824;

			public const int Physics_Debris = int.MinValue;
		}

		public const int Terrain = 8388608;

		public const int World = 65536;

		public const int Ragdolls = 512;

		public const int Construction = 2097152;

		public const int ConstructionSocket = 4194304;

		public const int Craters = 1;

		public const int GameTrace = 16384;

		public const int Trigger = 262144;

		public const int VehiclesLarge = 134217728;

		public const int VehiclesDetailed = 8192;

		public const int Tree = 1073741824;

		public const int Bush = 67108864;

		public const int Harvestable = 524288;

		public const int Deployed = 256;

		public const int AI = 2048;

		public const int PhysicsDebris = int.MinValue;

		public const int RainFall = 161546513;

		public const int Deploy = 1235288065;

		public const int DefaultDeployVolumeCheck = 537001984;

		public const int PreventBuilding = 536870912;

		public const int BuildLineOfSightCheck = 2097152;

		public const int ProjectileLineOfSightCheck = 1075904512;

		public const int MeleeLineOfSightCheck = 1075904512;

		public const int EyeLineOfSightCheck = 2162688;

		public const int EntityLineOfSightCheck = 1218519041;

		public const int PlayerBuildings = 153092352;

		public const int PlannerPlacement = 161546496;

		public const int IndustrialPipeObstruction = 2162944;

		public const int Solid = 1218652417;

		public const int StaticSolid = 1084293377;

		public const int StaticSolidAndVehicleLarge = 1218511105;

		public const int VisCulling = 10551297;

		public const int HABGroundEffect = 1218511105;

		public const int AILineOfSight = 1218519297;

		public const int DismountCheck = 1486946561;

		public const int AIPlacement = 429981697;

		public const int WheelRay = 1235321089;

		public const int VisualWheelRay = 1235296513;

		public const int Water = 16;

		public const int Sprays = 2;

		public const int InactivePhysicsStuff = 524288;

		public const int InactivePhysicsStuffLayer = 19;

		public const int Physics_Projectile = 1048576;
	}
	public enum Rarity
	{
		None,
		Common,
		Uncommon,
		Rare,
		VeryRare
	}
	public enum Era
	{
		None = 0,
		Any = 1,
		Primitive = 10,
		Medieval = 20,
		Frontier = 30,
		Modern = 1000
	}
	[Flags]
	public enum EraRestriction
	{
		Default = 0,
		Vending = 1,
		Loot = 2,
		Craft = 4,
		Mission = 8,
		Recycle = 0x10,
		MetalDetector = 0x20,
		None = 0x40
	}
	[Flags]
	public enum AmmoTypes
	{
		PISTOL_9MM = 1,
		RIFLE_556MM = 2,
		SHOTGUN_12GUAGE = 4,
		BOW_ARROW = 8,
		HANDMADE_SHELL = 0x10,
		ROCKET = 0x20,
		NAILS = 0x40,
		AMMO_40MM = 0x80,
		SNOWBALL = 0x100,
		SPEARGUN_BOLT = 0x200,
		TORPEDO = 0x400,
		MLRS_ROCKET = 0x800,
		MISSILE_SEEKING = 0x1000,
		CATAPULT_BOULDER = 0x2000,
		BALLISTA_ARROW = 0x4000,
		DART = 0x8000
	}
	public enum Layer
	{
		Default,
		TransparentFX,
		Ignore_Raycast,
		Reserved1,
		Water,
		UI,
		Reserved2,
		Reserved3,
		Deployed,
		Ragdoll,
		Invisible,
		AI,
		Player_Movement,
		Vehicle_Detailed,
		Game_Trace,
		Vehicle_World,
		World,
		Player_Server,
		Trigger,
		Harvestable,
		Physics_Projectile,
		Construction,
		Construction_Socket,
		Terrain,
		Transparent,
		Clutter,
		Bush,
		Vehicle_Large,
		Prevent_Movement,
		Prevent_Building,
		Tree,
		Physics_Debris
	}
	public static class Global
	{
		public static Func<string, GameObject> LoadPrefab;

		public static Func<string, GameObject> FindPrefab;

		public static Func<string, GameObject> CreatePrefab;

		public static Action OpenMainMenu;

		public static Func<bool> IsLoadingScreenActive;

		private static FacepunchBehaviour _runner;

		public static FacepunchBehaviour Runner
		{
			get
			{
				if (_runner == null)
				{
					GameObject gameObject = new GameObject("Coroutine Runner");
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					_runner = gameObject.AddComponent<NullMonoBehaviour>();
				}
				return _runner;
			}
		}

		public static int LaunchCountThisVersion { get; private set; }

		public static void Init()
		{
			LaunchCountThisVersion = PlayerPrefs.GetInt($"launch{2615}", 0) + 1;
			PlayerPrefs.SetInt($"launch{2615}", LaunchCountThisVersion);
		}
	}
	public interface IEntity
	{
		bool IsDestroyed { get; }
	}
	public static class IndirectLighting
	{
		public struct AmbientProbeParams
		{
			public SphericalHarmonicsL2 sh;

			public float intensity;
		}

		public static readonly Dictionary<Camera, AmbientProbeParams> ambientProbeCache = new Dictionary<Camera, AmbientProbeParams>();

		private static SphericalHarmonicsL2[] lightProbe = new SphericalHarmonicsL2[1];

		public static SphericalHarmonicsL2[] LightProbe => lightProbe;

		public static void UpdateLightProbe()
		{
			LightProbes.GetInterpolatedProbe(Vector3.zero, null, out lightProbe[0]);
		}

		public static void UpdateAmbientProbe(Camera camera)
		{
			SphericalHarmonicsL2 sh = RenderSettings.ambientProbe;
			switch (RenderSettings.ambientMode)
			{
			case AmbientMode.Flat:
				sh = default(SphericalHarmonicsL2);
				sh.AddAmbientLight(RenderSettings.ambientSkyColor.linear * RenderSettings.ambientIntensity);
				break;
			case AmbientMode.Trilight:
			{
				Color color = RenderSettings.ambientSkyColor.linear * RenderSettings.ambientIntensity;
				Color color2 = RenderSettings.ambientEquatorColor.linear * RenderSettings.ambientIntensity;
				Color color3 = RenderSettings.ambientGroundColor.linear * RenderSettings.ambientIntensity;
				sh = default(SphericalHarmonicsL2);
				sh.AddAmbientLight(color2);
				sh.AddDirectionalLight(Vector3.up, color - color2, 0.5f);
				sh.AddDirectionalLight(Vector3.down, color3 - color2, 0.5f);
				break;
			}
			default:
				throw new ArgumentOutOfRangeException();
			case AmbientMode.Skybox:
			case AmbientMode.Custom:
				break;
			}
			AmbientProbeParams value = new AmbientProbeParams
			{
				sh = sh,
				intensity = RenderSettings.ambientIntensity
			};
			ambientProbeCache[camera] = value;
		}
	}
	public class PropRenderer : MonoBehaviour, IClientComponent
	{
		[Serializable]
		public class SkinViewerSettings
		{
			[Tooltip("If non-zero, will be used as a pivot point instead of the centre of the enclosing bounds")]
			public Vector3 customLocalPivot = Vector3.zero;

			[Tooltip("Additional camera offset only used in the skin viewer (as opposed to icon generation)")]
			public Vector3 camPosOffset;

			[Tooltip("For objects that rotate weirdly in other skin viewer pivot modes")]
			public bool forceCamUpPivot;

			public bool HasCustomPivot => customLocalPivot != Vector3.zero;
		}

		public delegate float LightIntensityScale(float intensity);

		public bool HideLowLods = true;

		public bool HideUnskinnable = true;

		public bool Outline = true;

		[Header("Rotation")]
		public Vector3 Rotation = Vector3.zero;

		public Vector3 PostRotation = Vector3.zero;

		[Header("Position Offset")]
		public Vector3 PositionalTweak = Vector3.zero;

		[Header("Misc")]
		public float FieldOfView = 40f;

		public float farClipPlane = 100f;

		public bool ForceLookAtProp;

		public Vector3 LookDirection = new Vector3(0.2f, 0.4f, 1f);

		public Vector3 UpDirection = Vector3.up;

		public GameObject[] HideDuringRender = new GameObject[0];

		[Range(0f, 1f)]
		public float Light1IntensityMultiplier = 1f;

		[Range(-360f, 360f)]
		public float Light1Rotation = 60f;

		[Range(0f, 1f)]
		public float Light2IntensityMultiplier = 1f;

		[Range(-360f, 360f)]
		public float Light2Rotation = -35f;

		public SkinViewerSettings skinViewerSettings;

		private static void GetTransformsRootToChild(List<Transform> transforms, Transform start)
		{
			Transform transform = start;
			while (transform != null)
			{
				transforms.Add(transform);
				transform = transform.parent;
			}
			transforms.Reverse();
		}

		private static Transform FindMatchingTransforms(List<Transform> sourceHierarchy, Transform target)
		{
			Transform transform = target;
			if (sourceHierarchy == null || sourceHierarchy.Count == 0)
			{
				return null;
			}
			if (target.name != sourceHierarchy[0].name)
			{
				UnityEngine.Debug.LogWarning("PropRenderer: Target object " + target.name + " does not match the first object in the source hierarchy " + sourceHierarchy[0].name + ". Skipping.");
				return null;
			}
			for (int i = 1; i < sourceHierarchy.Count; i++)
			{
				Transform transform2 = sourceHierarchy[i];
				Transform transform3 = transform.Find(transform2.name);
				if (transform3 == null)
				{
					return null;
				}
				transform = transform3;
			}
			return transform;
		}

		private static GameObject[] MatchGameObjectsBetweenObjects(GameObject[] sourceObjects, GameObject target)
		{
			List<GameObject> list = Pool.Get<List<GameObject>>();
			foreach (GameObject gameObject in sourceObjects)
			{
				if (gameObject == null)
				{
					list.Add(null);
					continue;
				}
				List<Transform> obj = Pool.Get<List<Transform>>();
				GetTransformsRootToChild(obj, gameObject.transform);
				Transform transform = FindMatchingTransforms(obj, target.transform);
				if (transform != null)
				{
					list.Add(transform.gameObject);
				}
				else
				{
					UnityEngine.Debug.LogWarning("PropRenderer: Could not find matching hierarchy for " + gameObject.name + " in target object " + target.name + ". Skipping.");
				}
				Pool.FreeUnmanaged(ref obj);
			}
			return list.ToArray();
		}

		public void CopySettingsTo(PropRenderer target)
		{
			target.farClipPlane = farClipPlane;
			target.FieldOfView = FieldOfView;
			target.ForceLookAtProp = ForceLookAtProp;
			target.HideLowLods = HideLowLods;
			target.HideUnskinnable = HideUnskinnable;
			target.Light1IntensityMultiplier = Light1IntensityMultiplier;
			target.Light1Rotation = Light1Rotation;
			target.Light2IntensityMultiplier = Light2IntensityMultiplier;
			target.Light2Rotation = Light2Rotation;
			target.LookDirection = LookDirection;
			target.Outline = Outline;
			target.PositionalTweak = PositionalTweak;
			target.PostRotation = PostRotation;
			target.Rotation = Rotation;
			target.UpDirection = UpDirection;
			if (HideDuringRender != null && HideDuringRender.Length != 0)
			{
				GameObject[] source = MatchGameObjectsBetweenObjects(HideDuringRender, target.gameObject);
				target.HideDuringRender = source.ToArray();
			}
		}

		public void DebugAlign()
		{
			PreRender();
			Camera main = Camera.main;
			main.fieldOfView = FieldOfView;
			PositionCamera(main, isSkinViewer: true);
			PostRender();
		}

		public void PositionCamera(Camera cam, bool isSkinViewer = false)
		{
			Vector3 vector = Quaternion.Euler(Rotation) * LookDirection.normalized;
			Vector3 vector2 = Quaternion.Euler(Rotation) * UpDirection.normalized;
			vector = Quaternion.Euler(PostRotation) * vector;
			vector2 = Quaternion.Euler(PostRotation) * vector2;
			cam.fieldOfView = FieldOfView;
			cam.nearClipPlane = 0.01f;
			cam.farClipPlane = farClipPlane;
			cam.FocusOnRenderer(base.gameObject, vector, vector2, 2048);
			cam.transform.position += PositionalTweak.x * cam.transform.right * 0.01f;
			cam.transform.position += PositionalTweak.y * cam.transform.up * 0.01f;
			cam.transform.position += PositionalTweak.z * cam.transform.forward * 0.1f;
			if (isSkinViewer)
			{
				cam.transform.position += skinViewerSettings.camPosOffset.x * cam.transform.right * 0.01f;
				cam.transform.position += skinViewerSettings.camPosOffset.y * cam.transform.up * 0.01f;
				cam.transform.position += skinViewerSettings.camPosOffset.z * cam.transform.forward * 0.1f;
			}
		}

		public void PreRender()
		{
			PreRender(base.gameObject, HideLowLods, HideUnskinnable, HideDuringRender);
		}

		public static void PreRender(GameObject gameObject, bool hideLowLODs, bool hideUnskinnable, GameObject[] hideDuringRender = null)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer is ParticleSystemRenderer) && !renderer.gameObject.CompareTag("StripFromStorePreview") && (!hideLowLODs || (!renderer.gameObject.name.EndsWith("lod01", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod02", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod03", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod04", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod1", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod2", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod3", StringComparison.InvariantCultureIgnoreCase) && !renderer.gameObject.name.EndsWith("lod4", StringComparison.InvariantCultureIgnoreCase))))
				{
					renderer.gameObject.layer = 11;
					SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
					if ((bool)skinnedMeshRenderer)
					{
						skinnedMeshRenderer.updateWhenOffscreen = true;
					}
				}
			}
		}

		public void PostRender()
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer is ParticleSystemRenderer))
				{
					renderer.gameObject.layer = 0;
					SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
					if ((bool)skinnedMeshRenderer)
					{
						skinnedMeshRenderer.updateWhenOffscreen = false;
					}
				}
			}
		}

		[ContextMenu("Create 512x512 Icon")]
		public void CreateIcon()
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Debug.Log("Saving To " + UnityEngine.Application.dataPath + "/" + gameObject.name + ".icon.png");
			ScreenshotToDisk(UnityEngine.Application.dataPath + "/" + gameObject.name + ".icon.png", 512, 512, 4);
		}

		[ContextMenu("Create Large Render")]
		public void CreateRender()
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Debug.Log("Saving To " + UnityEngine.Application.dataPath + "/" + gameObject.name + ".large.png");
			ScreenshotToDisk(UnityEngine.Application.dataPath + "/" + gameObject.name + ".large.png", 4096, 4096);
		}

		public static float DefaultLightIntensityScale(float intensity)
		{
			return Mathf.GammaToLinearSpace(intensity) * MathF.PI;
		}

		public Texture2D ScreenshotToTexture(int width, int height, int superSampleSize, LightIntensityScale lightIntensityScale = null)
		{
			bool streamingTextureForceLoadAll = Texture.streamingTextureForceLoadAll;
			Texture.streamingTextureForceLoadAll = true;
			GameObject gameObject = new GameObject("Temporary Camera");
			Camera camera = gameObject.AddComponent<Camera>();
			camera.clearFlags = CameraClearFlags.Depth;
			camera.backgroundColor = new Color(1f, 1f, 1f, 0f);
			camera.allowHDR = true;
			camera.allowMSAA = false;
			Type type = Type.GetType("DeferredIndirectLightingPass,Assembly-CSharp");
			if (type != null)
			{
				gameObject.AddComponent(type);
			}
			gameObject.AddComponent<StreamingController>();
			Type type2 = Type.GetType("DeferredExtension,Assembly-CSharp");
			if (type2 != null)
			{
				gameObject.AddComponent(type2);
			}
			Type type3 = Type.GetType("DeferredDecalRenderer,Assembly-CSharp");
			if (type2 != null)
			{
				gameObject.AddComponent(type3);
			}
			if (ReflectionProbe.defaultTexture != null)
			{
				Shader.SetGlobalTexture("global_SkyReflection", ReflectionProbe.defaultTexture);
				Shader.SetGlobalVector("global_SkyReflection_HDR", new Vector2(0.2f, 0.01f));
			}
			if (Outline)
			{
				gameObject.AddComponent<IconOutline>();
			}
			LightingOverride lightingOverride = gameObject.AddComponent<LightingOverride>();
			lightingOverride.overrideAmbientLight = true;
			lightingOverride.ambientMode = AmbientMode.Flat;
			lightingOverride.ambientLight = new Color(0.4f, 0.4f, 0.4f, 1f);
			lightingOverride.overrideSkyReflection = true;
			GameObject obj = new GameObject("Temporary Light");
			obj.transform.SetParent(camera.transform);
			CreateSunLight(obj, this, lightIntensityScale);
			GameObject obj2 = new GameObject("Temporary Light");
			obj2.transform.SetParent(camera.transform);
			CreateGeneralLight(obj2, this, lightIntensityScale);
			PreRender();
			try
			{
				camera.cullingMask = 2048;
				PositionCamera(camera);
				return camera.ScreenshotToTexture(width, height, transparent: true, superSampleSize, camera.backgroundColor);
			}
			finally
			{
				Texture.streamingTextureForceLoadAll = streamingTextureForceLoadAll;
				UnityEngine.Object.DestroyImmediate(gameObject);
				PostRender();
			}
		}

		public void ScreenshotToDisk(string filename, int width, int height, int superSampleSize = 1, LightIntensityScale lightIntensityScale = null)
		{
			Texture2D texture2D = ScreenshotToTexture(width, height, superSampleSize, lightIntensityScale);
			CameraEx.SavePNG(filename, texture2D);
			UnityEngine.Object.DestroyImmediate(texture2D);
		}

		public static Light CreateSunLight(GameObject lightgo, PropRenderer propRenderer, LightIntensityScale lightIntensityScale = null)
		{
			if (lightIntensityScale == null)
			{
				lightIntensityScale = DefaultLightIntensityScale;
			}
			Light light = lightgo.GetComponent<Light>();
			if (light == null)
			{
				light = lightgo.AddComponent<Light>();
			}
			lightgo.transform.localRotation = Quaternion.Euler(115f, propRenderer.Light2Rotation, 0f);
			light.type = LightType.Directional;
			light.color = new Color(1f, 1f, 0.96f);
			light.cullingMask = 2048;
			light.shadows = LightShadows.Soft;
			light.shadowBias = 0.01f;
			light.shadowNormalBias = 0.01f;
			light.shadowStrength = 0.9f;
			light.intensity = 2f * lightIntensityScale(propRenderer.Light2IntensityMultiplier);
			return light;
		}

		public static Light CreateGeneralLight(GameObject lightgo, PropRenderer propRenderer, LightIntensityScale lightIntensityScale = null)
		{
			if (lightIntensityScale == null)
			{
				lightIntensityScale = DefaultLightIntensityScale;
			}
			Light light = lightgo.GetComponent<Light>();
			if (light == null)
			{
				light = lightgo.AddComponent<Light>();
			}
			lightgo.transform.localRotation = Quaternion.Euler(5f, propRenderer.Light1Rotation, 0f);
			light.type = LightType.Directional;
			light.color = new Color(1f, 1f, 1f);
			light.cullingMask = 2048;
			light.shadows = LightShadows.Soft;
			light.shadowBias = 0.01f;
			light.shadowNormalBias = 0.01f;
			light.shadowStrength = 0.9f;
			light.intensity = lightIntensityScale(propRenderer.Light1IntensityMultiplier);
			return light;
		}

		public static bool RenderScreenshot(GameObject prefab, string filename, int width, int height, int SuperSampleSize = 1)
		{
			if (prefab == null)
			{
				UnityEngine.Debug.Log("RenderScreenshot - prefab is null", prefab);
				return false;
			}
			PropRenderer propRenderer = null;
			PropRenderer propRenderer2 = prefab.GetComponent<PropRenderer>();
			if (propRenderer2 == null)
			{
				propRenderer = prefab.AddComponent<PropRenderer>();
				propRenderer2 = propRenderer;
			}
			propRenderer2.ScreenshotToDisk(filename, width, height, SuperSampleSize);
			if (propRenderer != null)
			{
				UnityEngine.Object.DestroyImmediate(propRenderer);
			}
			return true;
		}
	}
	[ExecuteInEditMode]
	public class PropRendererDebug : MonoBehaviour
	{
		public void Update()
		{
			PropRenderer[] array = UnityEngine.Object.FindObjectsOfType<PropRenderer>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].DebugAlign();
			}
		}
	}
	public static class Protocol
	{
		public const int network = 2615;

		public const int save = 274;

		public const int report = 1;

		public const int persistance = 8;

		public const int analytics_db = 1;

		public const int ping = 1;

		public static string printable => 2615 + "." + 274 + "." + 1;
	}
	public class AsyncTextureLoad : CustomYieldInstruction
	{
		private IntPtr buffer = IntPtr.Zero;

		private byte[] uncompressedTextureBytes;

		private int size;

		private int width;

		private int height;

		private int format;

		public string filename;

		public bool normal;

		public bool dither;

		public bool hqmode;

		public bool cache;

		public bool compressOnLoad;

		private Action worker;

		public override bool keepWaiting => worker != null;

		public bool isDone => worker == null;

		public bool isValid
		{
			get
			{
				if (buffer == IntPtr.Zero && uncompressedTextureBytes == null)
				{
					return false;
				}
				if (size == 0)
				{
					return false;
				}
				if (format == 0)
				{
					return false;
				}
				if (width < 32 || width > 8192 || !Mathf.IsPowerOfTwo(width))
				{
					return false;
				}
				if (height < 32 || height > 8192 || !Mathf.IsPowerOfTwo(height))
				{
					return false;
				}
				if (format != 12 && format != 10 && format != 4)
				{
					return false;
				}
				return true;
			}
		}

		public Texture2D texture
		{
			get
			{
				if (!isValid)
				{
					return null;
				}
				Texture2D texture2D = new Texture2D(width, height, (TextureFormat)format, mipChain: true, normal);
				if (compressOnLoad)
				{
					texture2D.LoadRawTextureData(buffer, size);
					texture2D.Apply(updateMipmaps: false);
					FreeTexture(ref buffer);
					return texture2D;
				}
				if (!texture2D.LoadImage(uncompressedTextureBytes))
				{
					UnityEngine.Debug.LogError("Failed to load uncompressedTextureBytes into image!");
				}
				return texture2D;
			}
		}

		public void LoadIntoTexture(Texture2D tex)
		{
			if (isValid && tex.width == width && tex.height == height && tex.format == (TextureFormat)format)
			{
				if (compressOnLoad)
				{
					tex.LoadRawTextureData(buffer, size);
				}
				else if (!tex.LoadImage(uncompressedTextureBytes))
				{
					UnityEngine.Debug.LogError("Failed to load uncompressedTextureBytes into image!");
					return;
				}
				tex.Apply(updateMipmaps: false);
				FreeTexture(ref buffer);
			}
		}

		public void WriteToCache(string cachename)
		{
			SaveTextureToCache(cachename, buffer, size, width, height, format);
		}

		[DllImport("RustNative", EntryPoint = "free_texture")]
		private static extern void FreeTexture(ref IntPtr buffer);

		[DllImport("RustNative", EntryPoint = "load_texture_from_file")]
		private static extern void LoadTextureFromFile(string filename, ref IntPtr buffer, ref int size, ref int width, ref int height, ref int channels, bool normal, bool dither, bool hqmode);

		[DllImport("RustNative", EntryPoint = "load_texture_from_cache")]
		private static extern void LoadTextureFromCache(string filename, ref IntPtr buffer, ref int size, ref int width, ref int height, ref int format);

		[DllImport("RustNative", EntryPoint = "save_texture_to_cache")]
		private static extern void SaveTextureToCache(string filename, IntPtr buffer, int size, int width, int height, int format);

		public AsyncTextureLoad(string filename, bool normal, bool dither, bool hqmode, bool cache, bool compressOnLoad = true)
		{
			this.filename = filename;
			this.normal = normal;
			this.dither = dither;
			this.hqmode = hqmode;
			this.cache = cache;
			this.compressOnLoad = compressOnLoad;
			Invoke();
		}

		private void DoWork()
		{
			if (cache)
			{
				LoadTextureFromCache(filename, ref buffer, ref size, ref width, ref height, ref format);
				return;
			}
			int channels = 0;
			if (compressOnLoad)
			{
				LoadTextureFromFile(filename, ref buffer, ref size, ref width, ref height, ref channels, normal, dither, hqmode);
				format = ((channels > 3) ? 12 : 10);
				return;
			}
			LoadTextureFromFile(filename, ref buffer, ref size, ref width, ref height, ref channels, normal, dither, hqmode);
			FreeTexture(ref buffer);
			uncompressedTextureBytes = File.ReadAllBytes(filename);
			if (uncompressedTextureBytes == null)
			{
				throw new Exception("Failed to load uncompressed texture data!");
			}
			format = 4;
			size = width * height * 32;
		}

		private void Invoke()
		{
			worker = DoWork;
			worker.BeginInvoke(Callback, null);
		}

		private void Callback(IAsyncResult result)
		{
			worker.EndInvoke(result);
			worker = null;
		}
	}
}
namespace Rust.Registry
{
	public static class Entity
	{
		private static Dictionary<Transform, IEntity> _dict = new Dictionary<Transform, IEntity>();

		public static void Register(Transform t, IEntity entity)
		{
			_dict[t] = entity;
		}

		public static IEntity Get(Transform t)
		{
			_dict.TryGetValue(t, out var value);
			return value;
		}

		public static void Unregister(Transform t)
		{
			_dict.Remove(t);
		}

		public static void Register(GameObject obj, IEntity entity)
		{
			Register(obj.transform, entity);
		}

		public static IEntity Get(GameObject obj)
		{
			return Get(obj.transform);
		}

		public static void Unregister(GameObject obj)
		{
			Unregister(obj.transform);
		}
	}
}
namespace Rust.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Icon Outline")]
	public class IconOutline : MonoBehaviour
	{
		public Material Material;

		private void OnEnable()
		{
			if (Material == null)
			{
				Shader shader = Shader.Find("UI/IconOutline");
				if (shader == null)
				{
					throw new Exception("UI/IconOutline - Missing!");
				}
				Material = new Material(shader);
				Material.hideFlags = HideFlags.DontSave;
			}
		}

		private void OnDisable()
		{
			if (!Application.isQuitting)
			{
				UnityEngine.Object.DestroyImmediate(Material);
				Material = null;
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, Material);
		}
	}
}
namespace Rust.Components
{
	public class NullMonoBehaviour : FacepunchBehaviour
	{
	}
}
namespace Rust.Components.Utility
{
	internal class OnObjectDisable : MonoBehaviour
	{
		public UnityEvent Action;

		private void OnDisable()
		{
			if (!Application.isQuitting)
			{
				Action.Invoke();
			}
		}
	}
	internal class OnObjectEnable : MonoBehaviour
	{
		public UnityEvent Action;

		private void OnEnable()
		{
			Action.Invoke();
		}
	}
}
namespace Rust.Components.Camera
{
	public class DepthOfFieldFocusPoint : ListComponent<DepthOfFieldFocusPoint>
	{
		private Renderer cachedRenderer;

		public Vector3 FocusPoint
		{
			get
			{
				Vector3 result = base.transform.position;
				if (cachedRenderer != null)
				{
					result = cachedRenderer.bounds.center;
				}
				return result;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			cachedRenderer = GetComponentInChildren<MeshRenderer>(includeInactive: true);
			if (cachedRenderer == null)
			{
				cachedRenderer = GetComponentInChildren<SkinnedMeshRenderer>(includeInactive: true);
			}
		}

		public static DepthOfFieldFocusPoint Evaluate(UnityEngine.Camera cam)
		{
			if (ListComponent<DepthOfFieldFocusPoint>.InstanceList == null || ListComponent<DepthOfFieldFocusPoint>.InstanceList.Count == 0)
			{
				return null;
			}
			DepthOfFieldFocusPoint result = null;
			float num = float.MaxValue;
			for (int i = 0; i < ListComponent<DepthOfFieldFocusPoint>.InstanceList.Count; i++)
			{
				float num2 = ListComponent<DepthOfFieldFocusPoint>.InstanceList[i].Score(cam);
				if (num2 < num)
				{
					num = num2;
					result = ListComponent<DepthOfFieldFocusPoint>.InstanceList[i];
				}
			}
			return result;
		}

		private float Score(UnityEngine.Camera cam)
		{
			Vector3 vector = cam.WorldToScreenPoint(FocusPoint);
			if (vector.z < 0f)
			{
				return float.MaxValue;
			}
			return (new Vector2(vector.x, vector.y) - new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)).sqrMagnitude + vector.z * 128f;
		}
	}
	[ExecuteInEditMode]
	public class LightingOverride : MonoBehaviour
	{
		public bool overrideAmbientLight;

		public AmbientMode ambientMode;

		public Color ambientGroundColor;

		public Color ambientEquatorColor;

		public Color ambientLight;

		public float ambientIntensity;

		internal Color old_ambientLight;

		internal Color old_ambientGroundColor;

		internal Color old_ambientEquatorColor;

		internal float old_ambientIntensity;

		internal AmbientMode old_ambientMode;

		public float aspect;

		public bool overrideSkyReflection;

		public ReflectionProbe reflectionProbe;

		private static readonly int global_SkyReflection = Shader.PropertyToID("global_SkyReflection");

		private static readonly int global_SkyReflection_HDR = Shader.PropertyToID("global_SkyReflection_HDR");

		private UnityEngine.Camera camera;

		private void OnPreRender()
		{
			if (overrideAmbientLight)
			{
				old_ambientLight = RenderSettings.ambientLight;
				old_ambientIntensity = RenderSettings.ambientIntensity;
				old_ambientMode = RenderSettings.ambientMode;
				old_ambientGroundColor = RenderSettings.ambientGroundColor;
				old_ambientEquatorColor = RenderSettings.ambientEquatorColor;
				old_ambientGroundColor = RenderSettings.ambientGroundColor;
				RenderSettings.ambientMode = ambientMode;
				RenderSettings.ambientLight = ambientLight;
				RenderSettings.ambientIntensity = ambientIntensity;
				RenderSettings.ambientGroundColor = ambientLight;
				RenderSettings.ambientEquatorColor = ambientEquatorColor;
				RenderSettings.ambientGroundColor = ambientGroundColor;
			}
			if (camera == null)
			{
				camera = GetComponent<UnityEngine.Camera>();
			}
			if (aspect > 0f)
			{
				camera.aspect = aspect;
			}
			if (overrideSkyReflection)
			{
				Texture value = null;
				Vector4 value2 = new Vector4(1f, 1f, 0f, 0f);
				if (reflectionProbe != null)
				{
					value = reflectionProbe.texture;
					value2 = new Vector2(reflectionProbe.intensity, 1f);
				}
				Shader.SetGlobalTexture(global_SkyReflection, value);
				Shader.SetGlobalVector(global_SkyReflection_HDR, value2);
			}
			IndirectLighting.UpdateLightProbe();
			IndirectLighting.UpdateAmbientProbe(camera);
		}

		private void OnPostRender()
		{
			if (overrideAmbientLight)
			{
				RenderSettings.ambientMode = ambientMode;
				RenderSettings.ambientLight = old_ambientLight;
				RenderSettings.ambientIntensity = old_ambientIntensity;
				RenderSettings.ambientMode = old_ambientMode;
				RenderSettings.ambientGroundColor = old_ambientGroundColor;
				RenderSettings.ambientEquatorColor = old_ambientEquatorColor;
				RenderSettings.ambientGroundColor = old_ambientGroundColor;
			}
		}
	}
}
