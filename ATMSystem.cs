using UnityEngine;

public class ATMSystem : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    public void Deposit(int amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        if (amount <= 0) return;

        if (dataManager.playerData.money >= amount)
        {
            dataManager.playerData.money -= amount;
            dataManager.playerData.bankMoney += amount;
            dataManager.SavePlayer();
            Debug.Log($"Внесено в банк: {amount}$");
        }
        else
        {
            Debug.Log("Недостаточно наличных!");
        }
    }

    public void Withdraw(int amount)
    {
        if (dataManager == null || dataManager.playerData == null) return;
        if (amount <= 0) return;

        if (dataManager.playerData.bankMoney >= amount)
        {
            dataManager.playerData.bankMoney -= amount;
            dataManager.playerData.money += amount;
            dataManager.SavePlayer();
            Debug.Log($"Снято со счета: {amount}$");
        }
        else
        {
            Debug.Log("Недостаточно средств на счету!");
        }
    }
}
