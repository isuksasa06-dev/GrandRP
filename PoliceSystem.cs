using UnityEngine;
using UnityEngine.UI;

public class PoliceSystem : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public Transform playerTransform;

    [Header("Точки телепорта")]
    public Transform jailCellPoint;      // Камера в тюрьме
    public Transform policeExitPoint;    // Выход из участка после отсидки

    [Header("UI Розыска и Тюрьмы")]
    public Text wantedStarsText;
    public Text jailTimerText;

    private void Start()
    {
        UpdateUI();
        // Если игрок перезашел в игру, но срок еще не вышел — возвращаем в камеру
        if (dataManager != null && dataManager.playerData != null && dataManager.playerData.jailTimeLeft > 0f)
        {
            TeleportTo(jailCellPoint);
        }
    }

    private void Update()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;

        if (data.jailTimeLeft > 0f)
        {
            data.jailTimeLeft -= Time.deltaTime;

            if (jailTimerText != null)
                jailTimerText.text = $"Тюрьма: {Mathf.CeilToInt(data.jailTimeLeft)} сек.";

            if (data.jailTimeLeft <= 0f)
            {
                ReleaseFromJail();
            }
        }
    }

    public void AddWantedStars(int stars)
    {
        if (dataManager == null || dataManager.playerData == null) return;

        dataManager.playerData.wantedLevel = Mathf.Clamp(dataManager.playerData.wantedLevel + stars, 0, 5);
        dataManager.SavePlayer();
        UpdateUI();
    }

    public void ArrestPlayer(float secondsPerStar = 30f)
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;
        if (data.wantedLevel <= 0) return;

        data.jailTimeLeft = data.wantedLevel * secondsPerStar;
        data.wantedLevel = 0;
        dataManager.SavePlayer();

        TeleportTo(jailCellPoint);
        UpdateUI();
    }

    public void ReleaseFromJail()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        dataManager.playerData.jailTimeLeft = 0f;
        dataManager.SavePlayer();

        if (jailTimerText != null)
            jailTimerText.text = "";

        TeleportTo(policeExitPoint);
        UpdateUI();
    }

    public void PayBountyAtStation(int costPerStar = 200)
    {
        if (dataManager == null || dataManager.playerData == null) return;

        PlayerData data = dataManager.playerData;
        if (data.wantedLevel <= 0) return;

        int totalFine = data.wantedLevel * costPerStar;
        if (data.money >= totalFine)
        {
            data.money -= totalFine;
            data.wantedLevel = 0;
            dataManager.SavePlayer();
            UpdateUI();
        }
    }

    private void TeleportTo(Transform target)
    {
        if (playerTransform == null || target == null) return;

        CharacterController cc = playerTransform.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        playerTransform.position = target.position;
        if (cc != null) cc.enabled = true;
    }

    private void UpdateUI()
    {
        if (wantedStarsText == null || dataManager == null || dataManager.playerData == null) return;

        int stars = dataManager.playerData.wantedLevel;
        wantedStarsText.text = stars > 0 ? $"Розыск: {stars} ★" : "";
    }
}
