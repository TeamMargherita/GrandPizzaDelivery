using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 노트 패턴 편집 클래스
/// </summary>
public class NoteEditor : MonoBehaviour
{
    public Button FrontButton;      // 되감기 버튼
    public Button BackButton;       // 감기 버튼
    public AudioSource BgSound;     // 배경 음악
    public Transform Line1;         // 노트가 다닐 라인 1
    public Transform Line2;         // 노트가 다닐 라인 2

    private RhythmManager manager;  // 리1듬매니저 캐싱
    private decimal calculator;     // 노트 배열 인덱스 계산용

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        // 음악이 재생중이 아니면 에디터 작동x
        if (manager.CurrentTime < 0m)
            return;

        // 노트 추가
        AddNote();

        // 노트 제거
        RemoveNote();

        // 방향키로 음악 감기
        Wind();

        // 음악 배속
        SetPitch();

        // 스페이스바를 통한 음악 재생 및 일시정지
        Playing();

        // 마우스 입력을 통해 노트 생성 및 제거
        MouseInput();
    }

    /// <summary>
    /// 앞으로 감기
    /// </summary>
    /// <param name="second">감을 시간</param>
    public void Front(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time - second, 0f, BgSound.clip.length);
    }

    /// <summary>
    /// 뒤로 감기
    /// </summary>
    /// <param name="second">감을 시간</param>
    public void Back(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time + second, 0f, (int)BgSound.clip.length);
    }

    /// <summary>
    /// 음악 재생상태에 따른 플레이 함수
    /// </summary>
    public void Play()
    {
        // 음악 멈춰있을 때에만 플레이 함
        if (!BgSound.isPlaying)
            BgSound.Play();
    }

    /// <summary>
    /// 음악 일시정지
    /// </summary>
    public void Pause()
    {
        BgSound.Pause();
    }

    /// <summary>
    /// 음악 정지
    /// </summary>
    public void Stop()
    {
        BgSound.time = 0;
        BgSound.Stop();
    }

    /// <summary>
    /// 노트 추가
    /// </summary>
    private void AddNote()
    {
        // 1 라인
        // Q = 일반 노트 생성, W = 홀드 노트 생성
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.W))
        {
            // 인덱스 = 현재시간 / 최소단위 비트
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // 반올림을 통해 더욱 근접한 인덱스로 초기화
            calculator = decimal.Round(calculator);

            // 해당 키와 매칭된 데이터가 없으면 새로 생성
            if (!manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                // 입력받은 키에 따른 노트 타입 설정 후 추가
                manager.Data.NoteLines[0].Add((int)calculator, (Input.GetKeyDown(KeyCode.Q)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }

        // 2 라인
        // O = 일반 노트 생성, P = 홀드 노트 생성
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKey(KeyCode.P))
        {
            // 인덱스 = 현재시간 / 최소단위 비트
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // 반올림을 통해 더욱 근접한 인덱스로 초기화
            calculator = decimal.Round(calculator);

            // 해당 키와 매칭된 데이터가 없으면 새로 생성
            if (!manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                // 입력받은 키에 따른 노트 타입 설정 후 추가
                manager.Data.NoteLines[1].Add((int)calculator, (Input.GetKeyDown(KeyCode.O)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }
    }

    /// <summary>
    /// 노트 제거
    /// </summary>
    private void RemoveNote()
    {
        // 1 라인
        // LeftAlt = 제거
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // 인덱스 = 현재시간 / 최소단위 비트
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // 반올림을 통해 더욱 근접한 인덱스로 초기화
            calculator = decimal.Round(calculator);

            // 해당 키와 매칭된 데이터가 있으면 데이터 제거
            if (manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[0].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }

        // 2 라인
        // M = 제거(임시 매핑)
        if (Input.GetKey(KeyCode.M))
        {
            // 인덱스 = 현재시간 / 최소단위 비트
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // 반올림을 통해 더욱 근접한 인덱스로 초기화
            calculator = decimal.Round(calculator);

            // 해당 키와 매칭된 데이터가 있으면 데이터 제거
            if (manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[1].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
    }

    /// <summary>
    /// 방향키 입력을 통한 음악 감기
    /// </summary>
    private void Wind()
    {
        // LeftArrow = 앞으로 감기
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontButton.onClick.Invoke();
        }

        // RightArrow = 뒤로 감기
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BackButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// 음악 배속
    /// </summary>
    private void SetPitch()
    {
        // UpArrow = 배속 증가
        if (Input.GetKey(KeyCode.UpArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch + 0.01f, 0f, 2f);
        }

        // DownArrow = 배속 감소
        if (Input.GetKey(KeyCode.DownArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch - 0.01f, 0f, 2f);
        }

        // Backspace = 배속 초기화
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BgSound.pitch = 1;
        }
    }

    /// <summary>
    /// 스페이스바를 통한 음악 재생 및 일시정지를 하는 함수
    /// </summary>
    private void Playing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 재생 상태에 따른 재생 일시정지 여부 결정
            if (BgSound.isPlaying)
                Pause();
            else
                Play();
        }
    }

    /// <summary>
    /// 마우스 입력을 통한 노트 에디터 기능을 구현한 함수
    /// </summary>
    private void MouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            // 화면상의 마우스 좌표 입력
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int line;
            // 마우스의 y좌표에 따른 line 지정(지정 불가 시 함수 종료)
            if (Mathf.Abs(Line1.position.y - vector.y) < 0.4f)
                line = 0;
            else if (Mathf.Abs(Line2.position.y - vector.y) < 0.4f)
                line = 1;
            else
                return;

            // Bar의 위치를 잡는 로직 역산
            // 노트 위치 = 목적지 + (right * timing * speed * 보정)
            // => timing = (노트 위치 - 목적지) / (right * speed * 보정)
            // right = 1, 보정 = 5
            //노트 위치 = 마우스 위치로 변환시 마우스 좌표에 따른 타이밍 반환
            // timing = (마우스 위치 - 목적지) / (Speed * 5)
            float timing = (vector.x + 8) / (manager.Speed * 5f);

            // timing / BitSlice = 남은 노트 중 현재 마우스 위치의 인덱스
            // CurrentTime / BitSlice = 지나간 노트들중 마지막 인덱스
            // timing / BitSlice + CurrentTime / BitSlice = 마우스가 위치한 좌표의 최종 인덱스
            // 곱셈공식으로 최종 calculator = (timing + CurretTime) / BitSlice
            calculator = ((decimal)timing + manager.CurrentTime) / NoteSpawner.BitSlice;

            // 반올림을 통해 더욱 근접한 인덱스로 초기화
            calculator = decimal.Round(calculator);

            // 매칭된 키가 없으면 노트 추가
            if (!manager.Data.NoteLines[line].ContainsKey((int)calculator))
            {
                // 좌클릭 = 일반 노트, 우클릭 = 홀드 노트
                manager.Data.NoteLines[line].Add((int)calculator, (Input.GetKeyDown(KeyCode.Mouse0)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }

            // 매칭된 키가 있으면 노트 제거
            else
            {
                manager.Data.NoteLines[line].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
    }
}