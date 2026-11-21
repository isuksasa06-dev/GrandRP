using UnityEngine;
using UnityEngine.UI;

public class FactionAndPayDaySystem : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    [Header("Модели одежды персонажа (необязательно)")]
    public GameObject civilianSkin;
    public GameObject policeSkin;
    public GameObject medicSkin;

    [Header("Настройки PayDay (зарплата)")]
    public float payDayInterval = 600f; // Раз в 10 минут (600 сек)
    public int baseCitizenPay = 200;    // Пособие гражданским
    public int factionSalary = 1200;    // Зарплата во фракции
    public int payDayExp = 50;

    [Header("UI Фракции и таймера (необязательно)")]
    public Text factionStatusText;
    public Text payDayTimerText;

    private float timer;

    private void Start()
    {
        timer = payDayInterval;
        ApplyCurrentFactionSkin();
        UpdateFactionUI();
    }

    private void Update()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        timer -= Time.deltaTime;

        if (payDayTimerText != null)
        {
            int mins = Mathf.FloorToInt(timer / 60f);
            int secs = Mathf.FloorToInt(timer % 60f);
            payDayTimerText.text = $"PayDay: {mins:00}:{secs:00}";
        }

        if (timer <= 0f)
        {
            TriggerPayDay();
            timer = payDayInterval;
        }
    }

    // Вступить в Полицию (можно вызвать через NPCDialogue)
    public void JoinPolice()
    {
        SetFaction("Полиция", 1);
    }

    // Вступить в Медики
    public void JoinMedic()
    {
        SetFaction("Медики", 1);
    }

    // Уволиться и стать гражданским
    public void LeaveFaction()
    {
        SetFaction("Гражданский", 0);
    }

    private void SetFaction(string newFaction, int rank)
    {
        if (dataManager == null || dataManager.playerData == null) return;

        // В гос. фракции нельзя с розыском
        if (newFaction != "Гражданский" && dataManager.playerData.wantedLevel > 0)
        {
            Debug.Log("Нельзя устроиться во фракцию с розыском!");
            return;
        }

        dataManager.playerData.factionName = newFaction;
        dataManager.playerData.factionRank = rank;
        dataManager.SavePlayer();

        ApplyCurrentFactionSkin();
        UpdateFactionUI();
    }

    public void TriggerPayDay()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;
        int salary = (data.factionName == "Гражданский") ? baseCitizenPay : factionSalary * data.factionRank;

        data.bankMoney += salary;
        dataManager.AddExperience(payDayExp);
        dataManager.SavePlayer();

        Debug.Log($"[PayDay] На банковский счет зачислено {salary}$ и {payDayExp} опыта!");
    }

    private void ApplyCurrentFactionSkin()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        string f = dataManager.playerData.factionName;

        if (civilianSkin != null) civilianSkin.SetActive(f == "Гражданский");
        if (policeSkin != null) policeSkin.SetActive(f == "Полиция");
        if (medicSkin != null) medicSkin.SetActive(f == "Медики");
    }

    private void UpdateFactionUI()
    {
        if (factionStatusText == null || dataManager == null || dataManager.playerData == null) return;
        factionStatusText.text = $"Фракция: {dataManager.playerData.factionName} (Ранг: {dataManager.playerData.factionRank})";
    }
}
