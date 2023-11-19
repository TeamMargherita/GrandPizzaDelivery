using UnityEngine;

// 茄籍龋 累己
public class WeekUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    public void Awake()
    {
        switch (Constant.NowDay)
        {
            case DayNS.DayEnum.MONDAY:
                text.text = $"{Constant.NowDate}老,岿夸老";
                break;
            case DayNS.DayEnum.TUESDAY:
                text.text = $"{Constant.NowDate}老,拳夸老";
                break;
            case DayNS.DayEnum.WENDESDAY:
                text.text = $"{Constant.NowDate}老,荐夸老";
                break;
            case DayNS.DayEnum.THURSDAY:
                text.text = $"{Constant.NowDate}老,格夸老";
                break;
            case DayNS.DayEnum.FRIDAY:
                text.text = $"{Constant.NowDate}老,陛夸老";
                break;
            case DayNS.DayEnum.SATURDAY:
                text.text = $"{Constant.NowDate}老,配夸老";
                break;
            case DayNS.DayEnum.SUNDAY:
                text.text = $"{Constant.NowDate}老,老夸老";
                break;
        }
    }
}
