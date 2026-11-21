using UnityEngine;
using UnityEngine.UI;

public class HouseSystem : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public Transform playerTransform;

    [Header("Настройки дома")]
    public string houseTitle = "Квартира в центре";
    public int housePrice = 10000;

    [Header("Точки телепорта (Вход / Выход)")]
    public Transform insideSpawnPoint;   // Точка внутри интерьера дома
    public Transform outsideExitPoint;   // Точка на улице перед дверью

    [Header("UI Окно покупки дома")]
    public GameObject houseBuyPanel;
    public Text houseInfoText;
    public Text statusText;

    private void Start()
    {
        if (houseBuyPanel != null)
            houseBuyPanel.SetActive(false);

        // Если дом уже куплен и игрок не в тюрьме — спавним его дома при запуске игры
        if (dataManager != null && dataManager.playerData != null)
        {
            PlayerData data = dataManager.playerData;
            if (data.hasHouse && data.jailTimeLeft <= 0f)
            {
                TeleportPlayer(insideSpawnPoint);
            }
        }
    }

    // Вызывается при нажатии на входную дверь (через Interactable)
    public void InteractWithDoor()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (dataManager.playerData.hasHouse)
        {
            // Если дом уже куплен — просто заходим внутрь
            TeleportPlayer(insideSpawnPoint);
        }
        else
        {
            // Если не куплен — открываем меню покупки
            OpenBuyMenu();
        }
    }

    // Вызывается при нажатии на дверь изнутри дома
    public void ExitHouse()
    {
        TeleportPlayer(outsideExitPoint);
    }

    public void OpenBuyMenu()
    {
        if (houseBuyPanel == null) return;
        houseBuyPanel.SetActive(true);

        if (houseInfoText != null)
            houseInfoText.text = $"{houseTitle}\nЦена: {housePrice}$";

        if (statusText != null)
            statusText.text = "";
    }

    public void CloseBuyMenu()
    {
        if (houseBuyPanel != null)
            houseBuyPanel.SetActive(false);
    }

    public void BuyHouse()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (dataManager.playerData.hasHouse)
        {
            if (statusText != null) statusText.text = "Этот дом уже ваш!";
            return;
        }

        if (dataManager.playerData.money >= housePrice)
        {
            dataManager.playerData.money -= housePrice;
            dataManager.playerData.hasHouse = true;
            dataManager.playerData.ownedHouseName = houseTitle;
            dataManager.SavePlayer();

            CloseBuyMenu();
            TeleportPlayer(insideSpawnPoint);
        }
        else
        {
            if (statusText != null) statusText.text = "Недостаточно наличных денег!";
        }
    }

    private void TeleportPlayer(Transform target)
    {
        if (playerTransform == null || target == null) return;

        CharacterController cc = playerTransform.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        playerTransform.position = target.position;
        playerTransform.rotation = target.rotation;
        if (cc != null) cc.enabled = true;
    }
}
