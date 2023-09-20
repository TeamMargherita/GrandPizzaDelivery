using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStat : MonoBehaviour
{
    public int Career { get; set; } = -1;// 경력. 피자의 완성도가 낮아질 확률을 줄여준다.
    public int Handy { get; set; } = 20; // 기본 손재주
    public int Creativity { get; set; } = -1;// 창의력. 피자의 완성도가 높아질 확률을 높여준다.
    public int Agility { get; set; } = -1; // 순발력. 피자의 완성 속도를 높여준다.
    public int Pay { get; set; } // 주급.

    // 스텟 단계별 수치(경력, 창의력)
    public int bad { get; } = -1;
    public int normal { get; } = 1;
    public int good { get; } = 3;
    public int perfect { get; } = 6;

    public string[] CreativityStat { get; } = new string[4] { "낮음", "보통", "높음", "천재" };
    public string[] CareerStat { get; } = new string[4] { "신입", "경력직", "베테랑", "달인" };
    public string[] AgilityStat { get; } = new string[4] { "느림", "보통", "조금 빠름", "빠름" };

    private void Awake()
    {
        Pay = Handy - Agility + Creativity + Career + Random.Range(-10, 11);
    }

    private void Update()
    {
        Debug.Log(Handy);
    }
}