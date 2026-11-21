
using UnityEngine;
using System.IO;

public static class PlayerSaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "player.json");

    public static void Save(PlayerData data)
    {
        if (data == null) return;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Данные игрока сохранены!");
    }

    public static PlayerData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("Сохранение не найдено.");
            return new PlayerData();
        }

        string json = File.ReadAllText(SavePath);

        PlayerData data =
            JsonUtility.FromJson<PlayerData>(json);

        if (data == null)
            return new PlayerData();

        Debug.Log("Данные игрока загружены!");

        return data;
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }
}