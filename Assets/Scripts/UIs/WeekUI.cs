using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class WeekUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    public void Awake()
    {
        switch(Constant.NowDay)
        {
            case DayNS.DayEnum.MONDAY:
                text.text = "월요일";
                break;
            case DayNS.DayEnum.TUESDAY:
                text.text = "화요일";
                break;
            case DayNS.DayEnum.WENDESDAY:
                text.text = "수요일";
                break;
            case DayNS.DayEnum.THURSDAY:
                text.text = "목요일";
                break;
            case DayNS.DayEnum.FRIDAY:
                text.text = "금요일";
                break;
            case DayNS.DayEnum.SATURDAY:
                text.text = "토요일";
                break;
            case DayNS.DayEnum.SUNDAY:
                text.text = "일요일";
                break;
        }
    }
}
