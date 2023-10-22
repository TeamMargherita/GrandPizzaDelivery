using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeStorage
{
    public float Accuracy;                          // 정확도
    public int Attractive;                          // 매력도
    public int Perfect;
    public int Great;
    public int Good;
    public int Miss;

    public JudgeStorage()
    {
        Init();
    }

    public void Init()
    {
        Accuracy = 0;
        Attractive = 0;
        Perfect = 0;
        Great = 0;
        Good = 0;
        Miss = 0;
    }

    public void SetAttractive()
    {
        if (Perfect + Great + Good + Miss > 0)
            Accuracy = (float)(Perfect + Great * 0.7f + Good * 0.5f) / (Perfect + Great + Good + Miss) * 100f;
        else
            Accuracy = 100;
        Attractive = (int)(Constant.PizzaAttractiveness * (Accuracy / 100));
    }
}
