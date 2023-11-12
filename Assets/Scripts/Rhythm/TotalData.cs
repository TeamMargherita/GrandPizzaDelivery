public class TotalData
{
    public int TotalMoney;

    public TotalData()
    {
        TotalMoney = 0;
    }

    public TotalData(TotalData data)
    {
        // 맴버 변수 초기화
        TotalMoney = data.TotalMoney;
    }

    public TotalData(int totalMoney)
    {
        TotalMoney = totalMoney;
    }
}
