public class PlayerData
{
    public int Money;
    public int KillCount;
    public PlayerData()
    {
        Money = 0;
    }
    public PlayerData(PlayerData data)
    {
        // 맴버 변수 초기화
        Money = data.Money;
    }
    public PlayerData(int money, int count)
    {
        Money = money;
        KillCount = count;
    }
}
