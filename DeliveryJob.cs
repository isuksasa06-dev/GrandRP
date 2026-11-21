using UnityEngine;
using UnityEngine.UI;

public class DeliveryJob : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;

    [Header("Точки маршрута (Маркеры)")]
    public GameObject pickupPointMarker;   // Точка А (где брать груз)
    public GameObject dropoffPointMarker;  // Точка Б (куда нести груз)

    [Header("Визуал груза (необязательно)")]
    public GameObject cargoInHandsModel;   // Коробка в руках персонажа

    [Header("UI Статус работы (необязательно)")]
    public Text jobStatusText;

    [Header("Оплата за 1 рейс")]
    public int payPerDelivery = 250;
    public int expPerDelivery = 30;

    private bool isWorking = false;
    private bool hasCargo = false;

    private void Start()
    {
        ResetJobObjects();
    }

    // Вызывается из NPCDialogue -> Option 1 ("Начать работу")
    public void StartJob()
    {
        if (isWorking)
        {
            Debug.Log("Вы уже на смене!");
            return;
        }

        isWorking = true;
        hasCargo = false;

        if (pickupPointMarker != null) pickupPointMarker.SetActive(true);
        if (dropoffPointMarker != null) dropoffPointMarker.SetActive(false);
        if (cargoInHandsModel != null) cargoInHandsModel.SetActive(false);

        UpdateStatusText("Идите на склад (Точка А) за грузом");
        Debug.Log("Смена началась! Идите на точку погрузки.");
    }

    // Вызывается из NPCDialogue -> ("Закончить работу")
    public void StopJob()
    {
        isWorking = false;
        hasCargo = false;
        ResetJobObjects();
        UpdateStatusText("");
        Debug.Log("Вы закончили смену.");
    }

    // Игрок зашел в Точку А (взял груз)
    public void OnPickupReached()
    {
        if (!isWorking || hasCargo) return;

        hasCargo = true;

        if (pickupPointMarker != null) pickupPointMarker.SetActive(false);
        if (dropoffPointMarker != null) dropoffPointMarker.SetActive(true);
        if (cargoInHandsModel != null) cargoInHandsModel.SetActive(true);

        UpdateStatusText("Отнесите груз на разгрузку (Точка Б)");
        Debug.Log("Груз взят! Несите его на разгрузку.");
    }

    // Игрок зашел в Точку Б (сдал груз)
    public void OnDropoffReached()
    {
        if (!isWorking || !hasCargo) return;

        hasCargo = false;

        if (dropoffPointMarker != null) dropoffPointMarker.SetActive(false);
        if (pickupPointMarker != null) pickupPointMarker.SetActive(true);
        if (cargoInHandsModel != null) cargoInHandsModel.SetActive(false);

        if (dataManager != null)
        {
            dataManager.AddMoney(payPerDelivery);
            dataManager.AddExperience(expPerDelivery);
        }

        UpdateStatusText("Груз сдан! Идите за следующим (Точка А)");
        Debug.Log($"Рейс выполнен! Получено {payPerDelivery}$ и {expPerDelivery} опыта.");
    }

    private void ResetJobObjects()
    {
        if (pickupPointMarker != null) pickupPointMarker.SetActive(false);
        if (dropoffPointMarker != null) dropoffPointMarker.SetActive(false);
        if (cargoInHandsModel != null) cargoInHandsModel.SetActive(false);
    }

    private void UpdateStatusText(string msg)
    {
        if (jobStatusText != null)
            jobStatusText.text = msg;
    }
}
