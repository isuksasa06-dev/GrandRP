using UnityEngine;

public class GasStationPoint : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public CarFuel targetCarFuel;
    public CarInteraction carInteraction;

    [Header("Настройки цены")]
    public int pricePerLiter = 5;

    public void RefuelFullTank()
    {
        if (dataManager == null || dataManager.playerData == null || targetCarFuel == null)
        {
            Debug.LogWarning("Не назначены ссылки в GasStationPoint!");
            return;
        }

        float neededFuel = targetCarFuel.maxFuel - targetCarFuel.currentFuel;
        if (neededFuel <= 1f)
        {
            Debug.Log("Бак уже полный!");
            return;
        }

        int totalCost = Mathf.CeilToInt(neededFuel) * pricePerLiter;

        if (dataManager.playerData.money >= totalCost)
        {
            dataManager.playerData.money -= totalCost;
            targetCarFuel.Refuel(neededFuel);
            dataManager.SavePlayer();

            // Если машина заглохла до этого — снова разрешаем ехать
            if (targetCarFuel.carController != null)
                targetCarFuel.carController.isDriving = true;

            Debug.Log($"Машина заправлена на {Mathf.CeilToInt(neededFuel)} л. за {totalCost}$!");
        }
        else
        {
            Debug.Log($"Недостаточно денег! Нужно {totalCost}$");
        }
    }
}
