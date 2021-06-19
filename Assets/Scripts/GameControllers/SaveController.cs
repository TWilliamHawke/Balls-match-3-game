using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static T Load<T>(string key)
    {
        var jsonVel = PlayerPrefs.GetString(key);

        var jsonData = JsonUtility.FromJson<GameData<T>>(jsonVel);
        if (jsonData != null)
        {
            return jsonData.data;
        }
        else return default(T);
    }

    public static void Save<T>(string key, T stringifyData)
    {
        var gameData = new GameData<T>();
        var jsonData = JsonUtility.ToJson(stringifyData);

        gameData.data = stringifyData;

        var json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(key, json);
    }

    public static void Clear(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static bool DataIsExist(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}

