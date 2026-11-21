using UnityEngine;
using UnityEngine.UI;

public class ATMUI : MonoBehaviour
{
    [Header("Ссылки")]
    public ATMSystem atmSystem;
    public PlayerDataManager dataManager;

    [Header("UI Элементы")]
    public GameObject atmPanel;
    public InputField amountInput;
    public Text cashBalanceText;
    public Text bankBalanceText;

    private void Start()
    {
        if (atmPanel != null)
            atmPanel.SetActive(false);
    }

    public void OpenATM()
    {
        if (atmPanel != null)
        {
            atmPanel.SetActive(true);
            UpdateBalanceUI();
        }
    }

    public void CloseATM()
    {
        if (atmPanel != null)
            atmPanel.SetActive(false);
    }

    public void OnDepositButton()
    {
        if (atmSystem == null || amountInput == null) return;

        if (int.TryParse(amountInput.text, out int amount) && amount > 0)
        {
            atmSystem.Deposit(amount);
            amountInput.text = "";
            UpdateBalanceUI();
        }
    }

    public void OnWithdrawButton()
    {
        if (atmSystem == null || amountInput == null) return;

        if (int.TryParse(amountInput.text, out int amount) && amount > 0)
        {
            atmSystem.Withdraw(amount);
            amountInput.text = "";
            UpdateBalanceUI();
        }
    }

    public void UpdateBalanceUI()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (cashBalanceText != null)
            cashBalanceText.text = "Наличные: " + dataManager.playerData.money + "$";

        if (bankBalanceText != null)
            bankBalanceText.text = "На счету: " + dataManager.playerData.bankMoney + "$";
    }
}
