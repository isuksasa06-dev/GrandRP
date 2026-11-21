
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;

    private void Awake()
    {
        LoadPlayer();
    }

    public void LoadPlayer()
    {
        playerData = PlayerSaveSystem.Load();
    }

    public void SavePlayer()
    {
        PlayerSaveSystem.Save(playerData);
    }

    public void AddMoney(int amount)
{
    if (amount <= 0 || playerData == null)
        return;

    playerData.money += amount;
    SavePlayer();
}
    }

    public void AddExperience(int amount)
{
    if (amount <= 0 || playerData == null)
        return;

    playerData.experience += amount;

    while (playerData.experience >=
           100 + (playerData.level - 1) * 50)
    {
        int requiredXP =
            100 + (playerData.level - 1) * 50;

        playerData.experience -= requiredXP;
        playerData.level++;

        Debug.Log("Новый уровень: " + playerData.level);
    }

    SavePlayer();
}

    private void OnApplicationPause(bool paused)
    {
        if (paused && playerData != null)
            SavePlayer();
    }

    private void OnApplicationQuit()
    {
        SavePlayer();
    }
}
