using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleCount : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;

    public static int nowDate = 0;

    private int temPineapple = -1;
    // Start is called before the first frame update
    void Start()
    {
        //if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        //{
        //    nowDate = 0;
        //}

        if (nowDate != Constant.NowDate)
        {
            nowDate = Constant.NowDate;
            Constant.PineAppleCount += 5;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (temPineapple != Constant.PineAppleCount)
        {
            temPineapple = Constant.PineAppleCount;
            text.text = " : " + temPineapple;
        }
    }
}
