using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleCount : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;

    private static int nowDate = 0;

    private int temPineapple = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        {
            nowDate = 0;
        }

        if (nowDate != Constant.NowDate)
        {
            nowDate = Constant.NowDate;
            Constant.PineappleCount += 5;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (temPineapple != Constant.PineappleCount)
        {
            temPineapple = Constant.PineappleCount;
            text.text = " : " + temPineapple;
        }
    }
}
