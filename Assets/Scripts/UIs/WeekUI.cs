using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class WeekUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    public void Awake()
    {
        switch (Constant.NowDay)
        {
            case DayNS.DayEnum.MONDAY:
                text.text = $"{Constant.NowDate}��,������";
                break;
            case DayNS.DayEnum.TUESDAY:
                text.text = $"{Constant.NowDate}��,ȭ����";
                break;
            case DayNS.DayEnum.WENDESDAY:
                text.text = $"{Constant.NowDate}��,������";
                break;
            case DayNS.DayEnum.THURSDAY:
                text.text = $"{Constant.NowDate}��,�����";
                break;
            case DayNS.DayEnum.FRIDAY:
                text.text = $"{Constant.NowDate}��,�ݿ���";
                break;
            case DayNS.DayEnum.SATURDAY:
                text.text = $"{Constant.NowDate}��,�����";
                break;
            case DayNS.DayEnum.SUNDAY:
                text.text = $"{Constant.NowDate}��,�Ͽ���";
                break;
        }
    }
}
