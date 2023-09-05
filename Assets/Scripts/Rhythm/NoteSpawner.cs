using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject Note;         // 노트
    public GameObject Bar;          // 마디
    public double BPM = 84f;        // 곡 BPM
    public double OneBit = 0f;      // 1비트당 생성 속도
    public double OneBar = 0f;      // 1 마디 = 4비트
    public double CurBit = 0f;      // 현재 비트
    public double CurBar = 0f;      // 현재 마디
    public double StartTime = 0f;   // 곡 재생 시간

    void Start()
    {
        // OneBit 연산
        OneBit = 60f / BPM;

        // OneBar 연산
        OneBar = OneBit * 4f;
        StartTime = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurBar <= AudioSettings.dspTime - StartTime)
        {
            GameObject b = Instantiate(Bar, transform);
            b.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            CurBar += OneBar;
        }

        //if (CurBit <= AudioSettings.dspTime - StartTime)
        //{
        //    Instantiate(Bar);
        //    CurBit += OneBit;
        //}
    }
}
