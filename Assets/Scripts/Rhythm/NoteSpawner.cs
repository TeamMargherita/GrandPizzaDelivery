using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class NoteSpawner : MonoBehaviour
{
    public bool IsAuto;
    public AudioSource sound;

    Note n;
    void Start()
    {
        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        //for(int i = 0; i < times.Count; i++)
        //{
        //    int divide = (int)(times[i] / bitSlice);
        //    if (times[i] % bitSlice < bitSlice / 2m)
        //        times[i] = bitSlice * divide;
        //    else
        //        times[i] = bitSlice * (divide + 1);
        //}
        //
    }

    void Update()
    {
        // 마디 바 생성
        RhythmManager.Instance.CreateBar();

        //if (NoteLoad.Count > 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        // 노트 클리어
        //        if (NoteLoad.Peek().SendJudge() != Judge.None)
        //            NoteClear();
        //    }
        //    // 오토 클리어
        //    if (IsAuto)
        //    {
        //        if (NoteLoad.Peek().SendJudge() == Judge.Perfect)
        //        {
        //            Debug.Log(NoteLoad.Peek().Timing);
        //            NoteClear();
        //        }
        //    }
        //}

        // 나와있는 노트와 바를 다시 오브젝트 풀에 저장
        //if (BarLoad.Count > 0 && BarLoad.Peek().Timing < -0.12501m)
        //    QueueSwaping(BarLoad, Bars);

        //if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
        //    QueueSwaping(NoteLoad, Notes);
    }
}
