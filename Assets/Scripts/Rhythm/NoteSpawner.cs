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
    public GameObject[] Lines;              // 리듬게임 오브젝트들이 다닐 라인

    public RhythmStorage storage;           // 리듬게임 오브젝트를 담는 클래스
    private RhythmManager manager;          // 리듬 매니저 캐싱

    private Sprite[] pizzaIngredientSprArr;         // 피자 만들기에서 선택한 재료를 불러올 배열(일반 노트)
    private Sprite[] pizzaIngredientSprArrGolden;   // 피자 만들기에서 선택한 재료를 불러올 배열(홀드 노트)

    private Vector2 end = new Vector2(-8f, 0f);     // 노트 및 마디가 향할 좌표

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;

        // 일반 노트 이미지 불러오기
        pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");

        // 홀드 노트 이미지 불러오기
        pizzaIngredientSprArrGolden = Resources.LoadAll<Sprite>("UI/Golden_Ingredients_120_120");

        // 초기화 함수 호출
        Init();
    }

    private void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;
    }

    /// <summary>
    /// 스포너를 초기화 하는 함수
    /// </summary>
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
    public void CreateNote()
    {
        // 리셋
        storage.NoteLoadReset();

        // 생성
        int ratio = Constant.ChoiceIngredientList.Count;        // 재료 개수
        int menuList = 0;                                       // 메뉴 리스트에 담긴 재료들 첫 인덱스 부터 탐색
        float nextList = manager.Data.Length / ratio;           // 노래 길이 / 재료 개수로 출력할 이미지 분기를 나눔

        // 모든 라인 반복
        for (int i = 0; i < Lines.Length; i++)
        {
            // 로드한 데이터에 해당라인에 담긴 노트 탐색
            foreach (var v in manager.Data.NoteLines[i])
            {
                Note note;

                // 리듬 저장소에서 빼온다
                note = storage.DequeueNote();

                // 데이터에 들어있는 타입으로 초기화
                note.Type = v.Value;

                // 노트 타이밍이 설정한 분기를 넘기면 다음 재료 이미지로 인덱스 이동
                if ((menuList + 1) * nextList < (float)(BitSlice * v.Key))
                    menuList++;

                // 넘겨 받은 재료 리스트가 있는 경우
                if (Constant.ChoiceIngredientList.Count > 0)
                {
                    // 노트 타입에 맞는 이미지 할당
                    if (note.Type == NoteType.Normal)
                        note.GetComponent<SpriteRenderer>().sprite =
                        pizzaIngredientSprArr[Constant.ChoiceIngredientList[menuList]];

                    else if (note.Type == NoteType.Hold)
                        note.GetComponent<SpriteRenderer>().sprite =
                        pizzaIngredientSprArrGolden[Constant.ChoiceIngredientList[menuList]];
                }
                // 해당 노트가 있는 라인의 y좌표로 초기화
                end.y = Lines[i].transform.position.y;

                // 노트 초기화
                note.Init(BitSlice * v.Key, end);

                // 모든 정보를 초기화 한 후 활성화
                note.gameObject.SetActive(true);

                // 노트를 NoteLoad(나와있는 노트 모음)에 추가
                storage.NoteLoad[i].Enqueue(note);
            }
            // 새로운 라인에 첫 재료부터 다시 탐색
            menuList = 0;
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
        // 2000개의 마디를 생성(추후에 곡 길이에 따른 마디로 변경)
        for (int i = 0; i < 2000; i++)
        {
            // 모든 라인 반복
            for (int j = 0; j < storage.BarLoad.Length; j++)    
            {
                Bar bar;
                // 리듬 저장소에서 빼온다
                bar = storage.DequeueBar();

                // 해당 마디가 있는 라인의 y좌표로 초기화
                end.y = Lines[j].transform.position.y;

                // 마디 초기화
                bar.Init(nextBar, end);

                // 모든 정보를 초기화 한 후 활성화
                bar.gameObject.SetActive(true);

                // 마디를 BarLoad(나와있는 마디 모음)에 추가
                storage.BarLoad[j].Enqueue(bar);           
            }

            // 마디 사이 간격 만큼 더해서 다음 마디 위치 표현
            nextBar += (oneBar / (decimal)BarInterval);
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
