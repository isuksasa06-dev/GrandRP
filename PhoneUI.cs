using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public Transform playerTransform;
    public GameObject playerCarObject;

    [Header("UI Телефона")]
    public GameObject phonePanel;
    public Text phoneInfoText;

    [Header("GPS Навигатор")]
    public RectTransform gpsArrowUI; // UI-стрелка на экране
    public Transform jobLocation;
    public Transform bankLocation;
    public Transform shopLocation;
    public Transform dealershipLocation;

    private Transform currentGpsTarget;

    private void Start()
    {
        if (phonePanel != null)
            phonePanel.SetActive(false);

        if (gpsArrowUI != null)
            gpsArrowUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateGPSArrow();
    }

    public void TogglePhone()
    {
        if (phonePanel == null) return;

        bool isActive = !phonePanel.activeSelf;
        phonePanel.SetActive(isActive);

        if (isActive)
            RefreshPhoneInfo();
    }

    public void RefreshPhoneInfo()
    {
        if (dataManager == null || dataManager.playerData == null || phoneInfoText == null) return;

        PlayerData d = dataManager.playerData;
        phoneInfoText.text = $"Игрок: {d.playerName}\n" +
                             $"Наличные: {d.money}$\n" +
                             $"Банк: {d.bankMoney}$\n" +
                             $"Авто: {d.ownedCarName}";
    }

    // Вызов личного авто к игроку (платно)
    public void CallPersonalCar()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (!dataManager.playerData.hasCar || playerCarObject == null)
        {
            if (phoneInfoText != null) phoneInfoText.text = "У вас нет личного авто!";
            return;
        }

        int deliveryFee = 100;
        if (dataManager.playerData.money >= deliveryFee)
        {
            dataManager.playerData.money -= deliveryFee;
            dataManager.SavePlayer();

            Vector3 spawnPos = playerTransform.position + playerTransform.forward * 4f + Vector3.up * 0.5f;
            playerCarObject.transform.position = spawnPos;
            playerCarObject.transform.rotation = playerTransform.rotation;
            playerCarObject.SetActive(true);

            RefreshPhoneInfo();
        }
        else
        {
            if (phoneInfoText != null) phoneInfoText.text = "Нужно 100$ наличными для доставки авто!";
        }
    }

    // Кнопки выбора точки в GPS
    public void SetGPSToJob() => SetGPSTarget(jobLocation);
    public void SetGPSToBank() => SetGPSTarget(bankLocation);
    public void SetGPSToShop() => SetGPSTarget(shopLocation);
    public void SetGPSToDealership() => SetGPSTarget(dealershipLocation);

    public void ClearGPS()
    {
        currentGpsTarget = null;
        if (gpsArrowUI != null)
            gpsArrowUI.gameObject.SetActive(false);
    }

    private void SetGPSTarget(Transform target)
    {
        if (target == null) return;
        currentGpsTarget = target;
        if (gpsArrowUI != null)
            gpsArrowUI.gameObject.SetActive(true);
        TogglePhone(); // Закрываем телефон после выбора маршрута
    }

    private void UpdateGPSArrow()
    {
        if (currentGpsTarget == null || playerTransform == null || gpsArrowUI == null) return;

        Vector3 dirToTarget = currentGpsTarget.position - playerTransform.position;
        dirToTarget.y = 0f;

        // Если игрок доехал (ближе 5 метров) — выключаем GPS
        if (dirToTarget.magnitude < 5f)
        {
            ClearGPS();
            return;
        }

        float angle = Vector3.SignedAngle(playerTransform.forward, dirToTarget, Vector3.up);
        gpsArrowUI.localEulerAngles = new Vector3(0f, 0f, -angle);
    }
}
