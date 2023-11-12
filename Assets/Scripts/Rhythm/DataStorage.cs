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

    public void AddData(int money)
    {
        PlayerData player = new PlayerData(money);
        PlayerData.Add(Id, player);

        int total = 0;
        foreach (var value in PlayerData.Values)
        {
            total += value.Money;
        }
        TotalData.TotalMoney = total;
    }
}