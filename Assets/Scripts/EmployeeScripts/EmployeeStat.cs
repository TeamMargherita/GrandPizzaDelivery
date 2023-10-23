using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClerkNS;

public class EmployeeStat : MonoBehaviour
{
    public Tier Career { get; set; } = Tier.ONE;// 경력. 피자의 완성도가 낮아질 확률을 줄여준다.
    public int Handy { get; set; } = 20; // 기본 손재주
    public Tier Creativity { get; set; } = Tier.ONE;// 창의력. 피자의 완성도가 높아질 확률을 높여준다.
    public Tier Agility { get; set; } = Tier.ONE; // 순발력. 피자의 완성 속도를 높여준다.
    public int Pay { get; set; } // 주급.
    public int Stress { get; set; } = 0;// 스트레스 지수

    public string[] CreativityStat { get; } = new string[4] { "낮음", "보통", "높음", "천재" };
    public string[] CareerStat { get; } = new string[4] { "신입", "경력직", "베테랑", "달인" };
    public string[] AgilityStat { get; } = new string[4] { "느림", "보통", "조금 빠름", "빠름" };

    private void Awake()
    {
        Pay = Handy - (int)Agility + (int)Creativity + (int)Career + Random.Range(-10, 11);
    }
}