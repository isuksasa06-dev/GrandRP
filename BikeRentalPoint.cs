using UnityEngine;
using UnityEngine.UI;

public class BikeRentalPoint : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public GameObject rentalBikeObject;
    public Transform spawnPoint;
    public CarInteraction bikeInteraction;

    [Header("Настройки аренды")]
    public int rentalPrice = 100;
    public float rentalDuration = 300f; // 5 минут (300 секунд)

    [Header("UI Таймера аренды (необязательно)")]
    public Text rentalTimerText;

    private float timeLeft = 0f;
    private bool isRented = false;

    private void Start()
    {
        if (rentalBikeObject != null)
            rentalBikeObject.SetActive(false);

        if (rentalTimerText != null)
            rentalTimerText.text = "";
    }

    private void Update()
    {
        if (!isRented) return;

        timeLeft -= Time.deltaTime;

        if (rentalTimerText != null)
        {
            int mins = Mathf.FloorToInt(timeLeft / 60f);
            int secs = Mathf.FloorToInt(timeLeft % 60f);
            rentalTimerText.text = $"Аренда скутера: {mins:00}:{secs:00}";
        }

        if (timeLeft <= 0f)
        {
            EndRental();
        }
    }

    // Вызывается через Interactable -> onInteract
    public void RentBike()
    {
        if (dataManager == null || dataManager.playerData == null || rentalBikeObject == null) return;

        if (isRented)
        {
            NotificationUI.Show("Вы уже арендовали скутер!");
            return;
        }

        if (dataManager.playerData.money >= rentalPrice)
        {
            dataManager.playerData.money -= rentalPrice;
            dataManager.SavePlayer();

            isRented = true;
            timeLeft = rentalDuration;

            if (spawnPoint != null)
            {
                rentalBikeObject.transform.position = spawnPoint.position;
                rentalBikeObject.transform.rotation = spawnPoint.rotation;
            }

            rentalBikeObject.SetActive(true);
            AudioManager.PlayMoney();
            NotificationUI.ShowSuccess($"Скутер арендован на 5 мин. за {rentalPrice}$!");
        }
        else
        {
            AudioManager.PlayError();
            NotificationUI.ShowError($"Нужно {rentalPrice}$ для аренды скутера!");
        }
    }

    private void EndRental()
    {
        isRented = false;
        timeLeft = 0f;

        if (rentalTimerText != null)
            rentalTimerText.text = "";

        // Если игрок в этот момент за рулем — аккуратно высаживаем его перед удалением скутера
        if (bikeInteraction != null)
            bikeInteraction.ExitCar();

        if (rentalBikeObject != null)
            rentalBikeObject.SetActive(false);

        NotificationUI.Show("Время аренды скутера истекло.");
    }
}
