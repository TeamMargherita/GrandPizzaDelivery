using System.Collections.Generic;

public class DataStorage
{
    public TotalData TotalData;
    public Dictionary<int, PlayerData> PlayerData;
    public static int Id = 0;
    public DataStorage()
    {
        TotalData = new TotalData();
        PlayerData = new Dictionary<int, PlayerData>();
        Id = 0;
    }

    public DataStorage(DataStorage storage)
    {
        TotalData = new TotalData(storage.TotalData);
        PlayerData = new Dictionary<int, PlayerData>(storage.PlayerData);
        Id = PlayerData.Count;
    }

    public void AddData(int money, int count)
    {
        PlayerData player = new PlayerData(money, count);
        PlayerData.Add(Id, player);

        int totalMoney = 0;
        foreach (var value in PlayerData.Values)
        {
            totalMoney += value.Money;
        }

        int totalKill = 0;
        foreach (var value in PlayerData.Values)
        {
            totalKill += value.KillCount;
        }
        TotalData.TotalMoney = totalMoney;
        TotalData.TotalKill = totalKill;
    }
}