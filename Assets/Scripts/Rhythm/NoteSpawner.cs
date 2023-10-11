using UnityEngine;

/// <summary>
/// 노트 와 바를 생성시키는 클래스
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Sync;                      // 곡 싱크 (추후 로직 수정 필요)
    public static decimal BitSlice;         // 1 비트를 8 등분
    public float BarInterval = 1f;          // 바 간격

    private decimal oneBar;                 // 4 비트당 1 마디
    private decimal nextBar;                // 현재 마디
    private int barCycle = 0;               // 마디 색 변경을 위한 임시 변수

    private Sprite[] pizzaIngredientSprArr;

    private RhythmManager manager;
    private RhythmStorage storage;
    void Start()
    {
        manager = RhythmManager.Instance;
        storage = manager.Storage;
        pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");

        Init();
    }

    void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;

        // 나와있는 노트와 바를 다시 오브젝트 풀에 저장
        storage.ReturnNote();
    }
    public void Init()
    {
        // 판정 초기화
        manager.Init();

        // 데이터 값 연산
        DataCalculator();

        // 노트 생성
        CreateNote();

        // 바 생성
        CreateBar();
    }

    /// <summary>
    /// 노트를 생성하는 함수
    /// </summary>
    private void CreateNote()
    {
        // 리셋
        storage.NoteLoadReset();
        // 생성
        int ratio = Constant.ChoiceIngredientList.Count;
        int curList = 0;
        float nextList = manager.Data.Length / ratio;
        foreach (var v in manager.Data.Notes)
        {
            // 노트가 존재함
            Note note;

            note = storage.DequeueNote();

            note.Type = v.Value;
            // 노트 초기화

            if ((curList + 1) * nextList < (float)(BitSlice * v.Key))
                curList++;

            if (Constant.ChoiceIngredientList.Count > 0)
                note.GetComponent<SpriteRenderer>().sprite =
                pizzaIngredientSprArr[Constant.ChoiceIngredientList[curList]];

            note.Init(BitSlice * v.Key);
            Debug.Log(BitSlice * v.Key);
            note.gameObject.SetActive(true);
            // 노트를 NoteLoad(나와있는 노트 모음)에 추가
            storage.NoteLoad.Enqueue(note);
        }
    }

    /// <summary>
    /// 마디를 생성하는 함수
    /// </summary>
    private void CreateBar()
    {
        // 리셋
        storage.BarLoadReset();

        // 생성
        barCycle = 0;
        // 5000개의 마디를 생성(추후에 곡 길이에 따른 마디로 변경)
        for (int i = 0; i < 5000; i++)
        {
            Bar bar;

            bar = storage.DequeueBar();

            // 마디 초기화
            bar.Init(nextBar);
            bar.gameObject.SetActive(true);

            // 마디를 BarLoad(나와있는 마디 모음)에 추가
            storage.BarLoad.Enqueue(bar);

            nextBar += (oneBar / (decimal)BarInterval);
            barCycle++;
        }
    }

    /// <summary>
    /// 데이터를 기반으로 변수 값 연산하는 함수
    /// </summary>
    private void DataCalculator()
    {
        // 60m / (decimal)data.BPM = 1 비트
        // 1 마디 = 4 비트
        oneBar = 60m / (decimal)manager.Data.BPM * 4m;
        Sync = manager.Data.Sync;
        nextBar = 0;

        // BitSlice = 비트 / 8 = 마디 / 32
        BitSlice = oneBar / 32m;
    }
}
