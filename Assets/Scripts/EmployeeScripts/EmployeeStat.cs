using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClerkNS;

//추후 변경 예정~
public enum Day
{
    Monday = 0,
    Tuesday = 1,
    Wednesday = 2, 
    Thursday = 3,
    Friday = 4,
    Saturday = 5,
    Sunday = 6
}

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

    public string[] RanName { get; } = new string[40] { "엠마", "리암", "올리비아", "노아", "에바", "이사벨라", "소피아", "미아", "잭슨", "에이든", "루카스", "아멜리아", 
        "벤자민", "하퍼", "에단", "샤를로트", "아비게일", "제임스", "릴리", "헨리", "알렉산더", "윌리엄", "사무엘", "다니엘", "칼리브", "그레이스", "미셸", "스칼렛", 
        "엘리자", "엘라", "이사벨라", "샘", "스미스", "랄프", "르미에", "앤", "샬롯", "샤를", "아론", "아벨"};

    private void Awake()
    {
        Pay = (Handy - (int)Agility + (int)Creativity + (int)Career) * 100 + Random.Range(-1000, 1001);
    }
}