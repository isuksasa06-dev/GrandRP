using UnityEngine;

public class SimpleJobPoint : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    [Header("Награда за работу")]
    public int moneyReward = 150;
    public int expReward = 25;

    [Header("Задержка (в секундах)")]
    public float cooldown = 2f;
    private float nextWorkTime = 0f;

    public void DoWork()
    {
        if (Time.time < nextWorkTime)
        {
            Debug.Log("Подождите перед следующим действием!");
            return;
        }

        if (dataManager == null)
        {
            Debug.LogWarning("Не назначен PlayerDataManager!");
            return;
        }

        nextWorkTime = Time.time + cooldown;

        dataManager.AddMoney(moneyReward);
        dataManager.AddExperience(expReward);

        Debug.Log($"Вы заработали {moneyReward}$ и {expReward} опыта!");
    }
}
