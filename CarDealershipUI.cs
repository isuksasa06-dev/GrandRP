using UnityEngine;
using UnityEngine.UI;

public class CarDealershipUI : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public GameObject dealershipPanel;

    [Header("Машина для продажи")]
    public string carTitle = "Sedan Sport";
    public int carPrice = 3500;
    public GameObject playerCarObject;
    public Transform carSpawnPoint;

    [Header("UI Текст")]
    public Text carInfoText;
    public Text statusText;

    private void Start()
    {
        if (dealershipPanel != null)
            dealershipPanel.SetActive(false);

        // Если машина уже была куплена в прошлом сохранении — включаем её
        if (dataManager != null && dataManager.playerData != null && dataManager.playerData.hasCar)
        {
            if (playerCarObject != null)
                playerCarObject.SetActive(true);
        }
        else
        {
            if (playerCarObject != null)
                playerCarObject.SetActive(false);
        }
    }

    public void OpenDealership()
    {
        if (dealershipPanel == null) return;
        dealershipPanel.SetActive(true);

        if (carInfoText != null)
            carInfoText.text = $"{carTitle} — Цена: {carPrice}$";

        if (statusText != null)
            statusText.text = "";
    }

    public void CloseDealership()
    {
        if (dealershipPanel != null)
            dealershipPanel.SetActive(false);
    }

    public void BuyCar()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (dataManager.playerData.hasCar)
        {
            if (statusText != null) statusText.text = "У вас уже есть машина!";
            return;
        }

        if (dataManager.playerData.money >= carPrice)
        {
            dataManager.playerData.money -= carPrice;
            dataManager.playerData.hasCar = true;
            dataManager.playerData.ownedCarName = carTitle;
            dataManager.SavePlayer();

            if (playerCarObject != null)
            {
                if (carSpawnPoint != null)
                {
                    playerCarObject.transform.position = carSpawnPoint.position;
                    playerCarObject.transform.rotation = carSpawnPoint.rotation;
                }
                playerCarObject.SetActive(true);
            }

            if (statusText != null) statusText.text = "Поздравляем с покупкой! Авто на парковке.";
        }
        else
        {
            if (statusText != null) statusText.text = "Недостаточно наличных денег!";
        }
    }
}
