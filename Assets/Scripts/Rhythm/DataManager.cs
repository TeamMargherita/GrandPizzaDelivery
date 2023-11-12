public static class DataManager
{
    private static DataStorage storage = new DataStorage();

    public static void SaveData()
    {
        storage.AddData(GameManager.Instance.Money + Constant.Dept);
        JsonManager<DataStorage>.Save(storage, "DataStorage");
    }
    public static void LoadData()
    {
        storage = new DataStorage(JsonManager<DataStorage>.Load("DataStorage"));
    }
}
