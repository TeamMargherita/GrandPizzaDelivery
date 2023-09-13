using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStat : MonoBehaviour
{
    public int career { get; set; }// 경력. 피자의 완성도가 낮아질 확률을 줄여준다.
    public int handy { get; set; } // 기본 손재주
    public int creativity { get; set; } // 창의력. 피자의 완성도가 높아질 확률을 높여준다.
    public int agility { get; set; } // 순발력. 피자의 완성 속도를 높여준다.
    public int pay { get; set; } // 주급.

    // 스텟 수치(경력, 창의력)
    public int bad { get; } = -1;
    public int normal { get; } = 1;
    public int good { get; } = 3;
    public int perfect { get; } = 6;

    public string[] creativityStat { get; } = new string[4] { "낮음", "보통", "높음", "천재" };
    public string[] careerStat { get; } = new string[4] { "신입", "경력직", "베테랑", "달인" };

    private void Start()
    {
        handy = 20;
        agility = -1;
        creativity = -1;
        career = -1;
        pay = handy + agility + creativity + career + Random.Range(-10, 11);

        Debug.Log(handy);
        Debug.Log(agility);
        Debug.Log(creativity);
        Debug.Log(career);
        Debug.Log(pay);
    }
}