using UnityEngine;
using UnityEngine.UI;

public class CarDamageAndRepair : MonoBehaviour
{
    [Header("Ссылки")]
    public CarController carController;
    public PlayerDataManager dataManager;

    [Header("Здоровье машины")]
    public float maxCarHealth = 1000f;
    public float currentCarHealth = 1000f;
    public float minImpactSpeed = 6f;      // Минимальная скорость удара для урона
    public float damageMultiplier = 15f;   // Множитель урона от столкновения

    [Header("Эффекты и UI (необязательно)")]
    public GameObject smokeEffect;         // Дым из капота при HP < 400
    public Slider carHealthSlider;
    public int repairCost = 200;

    private void Start()
    {
        if (smokeEffect != null)
            smokeEffect.SetActive(false);

        UpdateHealthUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce >= minImpactSpeed)
        {
            float damage = (impactForce - minImpactSpeed) * damageMultiplier;
            TakeCarDamage(damage);
        }
    }

    public void TakeCarDamage(float amount)
    {
        currentCarHealth = Mathf.Clamp(currentCarHealth - amount, 0f, maxCarHealth);
        UpdateHealthUI();

        // Включаем дым из капота, если состояние ниже 40%
        if (smokeEffect != null)
            smokeEffect.SetActive(currentCarHealth < maxCarHealth * 0.4f && currentCarHealth > 0f);

        // Если прочность упала до 0 — машина сломана и не едет
        if (currentCarHealth <= 0f && carController != null)
        {
            carController.isDriving = false;
            NotificationUI.ShowError("Машина разбита! Вызовите механика или воспользуйтесь СТО.");
        }
    }

    // Метод для зоны СТО (вызывается через Interactable или триггер автосервиса)
    public void RepairCarAtService()
    {
        if (dataManager == null || dataManager.playerData == null) return;

        if (currentCarHealth >= maxCarHealth - 5f)
        {
            NotificationUI.Show("Машина в отличном состоянии, ремонт не требуется!");
            return;
        }

        if (dataManager.playerData.money >= repairCost)
        {
            dataManager.playerData.money -= repairCost;
            dataManager.SavePlayer();

            currentCarHealth = maxCarHealth;
            if (smokeEffect != null) smokeEffect.SetActive(false);

            if (carController != null)
                carController.isDriving = true;

            UpdateHealthUI();
            AudioManager.PlayMoney();
            NotificationUI.ShowSuccess($"Машина отремонтирована на СТО за {repairCost}$!");
        }
        else
        {
            AudioManager.PlayError();
            NotificationUI.ShowError($"Недостаточно денег для ремонта! Нужно {repairCost}$");
        }
    }

    private void UpdateHealthUI()
    {
        if (carHealthSlider != null)
        {
            carHealthSlider.maxValue = maxCarHealth;
            carHealthSlider.value = currentCarHealth;
        }
    }
}
