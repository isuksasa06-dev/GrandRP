using UnityEngine;
using UnityEngine.UI;

public class CarFuel : MonoBehaviour
{
    [Header("Ссылки")]
    public CarController carController;
    public Slider fuelSlider;

    [Header("Настройки бака")]
    public float maxFuel = 100f;
    public float currentFuel = 100f;
    public float fuelConsumption = 1.5f; // Расход в секунду при езде

    private void Start()
    {
        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
            fuelSlider.value = currentFuel;
        }
    }

    private void Update()
    {
        if (carController == null) return;

        // Тратим бензин только когда игрок за рулем
        if (carController.isDriving && currentFuel > 0f)
        {
            currentFuel -= fuelConsumption * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

            if (fuelSlider != null)
                fuelSlider.value = currentFuel;

            // Если топливо кончилось — глушим мотор
            if (currentFuel <= 0f)
            {
                carController.isDriving = false;
                Debug.Log("Бензин закончился!");
            }
        }
    }

    public void Refuel(float amount)
    {
        currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
        if (fuelSlider != null)
            fuelSlider.value = currentFuel;
    }

    public bool HasFuel()
    {
        return currentFuel > 0f;
    }
}
