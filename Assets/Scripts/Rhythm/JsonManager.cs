using Newtonsoft.Json;
using System.IO;
using UnityEngine;

/// <summary>
/// Json 데이터를 관리하기 위한 정적 클래스
/// </summary>
/// <typeparam name="T">관리하고자 하는 데이터의 타입</typeparam>
public static class JsonManager<T>
{
    private static string path = Application.dataPath + "/StreamingAssets/Json";    // Json 저장 경로

    /// <summary>
    /// Json 데이터를 저장하는 함수
    /// </summary>
    /// <param name="userData">해당 타입의 데이터</param>
    /// <param name="fileName">저장할 파일 이름</param>
    public static void Save(T userData, string fileName)
    {
        // 해당 경로가 없으면 새로 경로 생성
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // 해당 타입의 데이터를 Json 포맷으로 변경 후 문자열로 담음
        string _saveJson = JsonConvert.SerializeObject(userData);

        // 파일경로/파일이름.json 으로 경로 지정
        string _filePath = path + "/" + fileName + ".json";

        // 해당 정보를 바탕으로 파일 작성
        File.WriteAllText(_filePath, _saveJson);
    }

    /// <summary>
    /// Json 데이터를 불러오는 함수
    /// </summary>
    /// <param name="_fileName">불러올 파일 이름</param>
    /// <returns>불러온 데이터</returns>
    public static T Load(string _fileName)
    {
        // 파일경로/파일이름.json 으로 경로 지정
        string _filePath = path + "/" + _fileName + ".json";

        // 해당 경로가 없으면 에러 표시 후 제네릭 타입 default 반환
        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return default(T);
        }

        // 파일 텍스트 불러오기
        string saveFile = File.ReadAllText(_filePath);

        // 불러온 문자열을 해당 타입으로 변환 후 반환
        T _userData = JsonConvert.DeserializeObject<T>(saveFile);
        return _userData;
    }

    /// <summary>
    /// Json 데이터를 제거하는 함수
    /// </summary>
    /// <param name="_fileName">제거할 파일 이름</param>
    public static void Delete(string _fileName)
    {
        // 파일경로/파일이름.json 으로 경로 지정
        string _filePath = path + "/" + _fileName + ".json";

        // 해당 경로가 없으면 에러 표시 후 함수 종료
        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return;
        }

        // 파일 제거
        File.Delete(_filePath);
    }
}