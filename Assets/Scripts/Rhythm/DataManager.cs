public static class DataManager
{
    private static DataStorage storage = new DataStorage();
    public static int killCount = 0;
    public static void SaveData()
    {
        storage.AddData(GameManager.Instance.Money + Constant.Dept, killCount);
        JsonManager<DataStorage>.Save(storage, "DataStorage");
    }
    public static void LoadData()
    {
        storage = new DataStorage(JsonManager<DataStorage>.Load("DataStorage"));
        killCount = 0;
    }
}
