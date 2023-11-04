using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정확도 측정을 위한 변수를 담는 클래스
/// </summary>
public class JudgeStorage
{
    public float Accuracy;      // 정확도
    public int Attractive;      // 매력도
    public int Perfect;         // 100%
    public int Great;           // 70%
    public int Good;            // 50%
    public int Miss;            // 0%

    public JudgeStorage()
    {
        Init();
    }

    /// <summary>
    /// 변수를 리셋하는 초기화 함수
    /// </summary>
    public void Init()
    {
        Accuracy = 0;
        Attractive = 0;
        Perfect = 0;
        Great = 0;
        Good = 0;
        Miss = 0;
    }

    /// <summary>
    /// 정확도를 측정하고 그에 맞는 매력도를 설정하는 함수
    /// </summary>
    public void SetAttractive()
    {
        // 정확도 = 정확도 보정한 판정들의 합 / 전체 판정들의 수
        // Perfect = 1, Great = 0.7, Good = 0.5, Miss = 0
        // 전체 개수 = Perfect + Great + Good + Miss

        // 판정된 노트가 하나라도 있을 시
        if (Perfect + Great + Good + Miss > 0)
            Accuracy = (float)(Perfect + Great * 0.7f + Good * 0.5f) / (Perfect + Great + Good + Miss) * 100f;
        // 하나도 없을 시
        else
            Accuracy = 100;

        // 정확도를 통한 매력도 환산
        Attractive = (int)(Constant.PizzaAttractiveness * (Accuracy / 100));
    }
}
