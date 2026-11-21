using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerDataManager dataManager;

    [Header("Текст")]
    public Text levelText;
    public Text experienceText;
    public Text moneyText;
    public Text bankMoneyText;

    [Header("Полоски (Sliders)")]
    public Slider experienceSlider;
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;

    private void Update()
    {
        if (dataManager == null || dataManager.playerData == null)
            return;

        PlayerData data = dataManager.playerData;

        if (levelText != null)
            levelText.text = "Уровень: " + data.level;

        if (experienceText != null)
            experienceText.text = "Опыт: " + data.experience;

        if (moneyText != null)
            moneyText.text = "Деньги: " + data.money;

        if (bankMoneyText != null)
            bankMoneyText.text = "Банк: " + data.bankMoney;

        if (experienceSlider != null)
        {
            int requiredXP = 100 + (data.level - 1) * 50;
            experienceSlider.maxValue = requiredXP;
            experienceSlider.value = data.experience;
        }

        if (healthSlider != null)
            healthSlider.value = data.health;

        if (hungerSlider != null)
            hungerSlider.value = data.hunger;

        if (thirstSlider != null)
            thirstSlider.value = data.thirst;
    }
}
