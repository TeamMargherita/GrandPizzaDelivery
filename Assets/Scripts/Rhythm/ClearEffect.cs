using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public float Speed = 3f;            // 변경된 색상이 돌아오는 데에 걸리는 시간 = 1 / Speed;
    public GameObject Ring;             // 색 변경되면서 출력할 이펙트 링
    private SpriteRenderer render;      // SpriteRenderer 캐싱
    private Color[] color;              // 판정에 따라 변경할 색을 담는 배열 
    private Color startColor;           // 처음 갖고있는 색
    private Color endColor;             // 변경할 색
    private float timer = 0f;           // 선형 보간용 타이머 변수
    void Start()
    {
        // 렌더러 캐싱
        render = GetComponent<SpriteRenderer>();

        // 색상 지정
        color = new Color[] { Color.cyan, Color.green, Color.yellow };

        // 시작 색상 저장
        startColor = render.color;
    }

    void Update()
    {
        // 타이머가 음수면 연산x
        if (timer <= 0)
            return;

        // 시작 색상과 종료 색상을 선형 보간한 값으로 색상 초기화
        render.color = Color.Lerp(startColor, endColor, timer);

        // 시간이 지남에 따른 타이머 연산
        timer -= Time.deltaTime * Speed;
    }

    /// <summary>
    /// 받아온 판정을 통해 색상을 지정 하는 함수
    /// </summary>
    /// <param name="judge"></param>
    public void GetJudge(Judge judge)
    {
        switch (judge)
        {
            case Judge.PERFECT:
                Init(0);
                break;
            case Judge.GREAT:
                Init(1);
                break;
            case Judge.GOOD:
                Init(2);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 이펙트 초기화 함수
    /// </summary>
    /// <param name="index">판정에 따른 색상</param>
    private void Init(int index)
    {
        //목표 색상 초기화
        endColor = color[index];

        // 이펙트 링 인스턴싱
        GameObject ring = Instantiate(Ring, transform);

        // 링 색상, 좌표 초기화
        ring.GetComponent<SpriteRenderer>().color = color[index];
        ring.transform.localPosition = Vector2.zero;
        timer = 1f;
    }
}
