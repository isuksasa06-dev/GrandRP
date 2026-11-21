using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    [Header("Скорость траты (в секунду)")]
    public float hungerDrainRate = 0.2f;
    public float thirstDrainRate = 0.35f;
    public float starvationDamage = 1.5f;

    private void Update()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;

        data.hunger = Mathf.Clamp(data.hunger - hungerDrainRate * Time.deltaTime, 0f, 100f);
        data.thirst = Mathf.Clamp(data.thirst - thirstDrainRate * Time.deltaTime, 0f, 100f);

        if (data.hunger <= 0f || data.thirst <= 0f)
        {
            TakeDamage(starvationDamage * Time.deltaTime);
        }
    }

    public void Eat(float amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        dataManager.playerData.hunger = Mathf.Clamp(dataManager.playerData.hunger + amount, 0f, 100f);
        dataManager.SavePlayer();
    }

    public void Drink(float amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        dataManager.playerData.thirst = Mathf.Clamp(dataManager.playerData.thirst + amount, 0f, 100f);
        dataManager.SavePlayer();
    }

    public void Heal(float amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        dataManager.playerData.health = Mathf.Clamp(dataManager.playerData.health + amount, 0f, 100f);
        dataManager.SavePlayer();
    }

    public void TakeDamage(float amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        dataManager.playerData.health = Mathf.Clamp(dataManager.playerData.health - amount, 0f, 100f);

        if (dataManager.playerData.health <= 0f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        Debug.Log("Персонаж потерял сознание! Возрождение в больнице...");
        PlayerData data = dataManager.playerData;
        data.health = 50f;
        data.hunger = 50f;
        data.thirst = 50f;
        dataManager.SavePlayer();
    }
}
