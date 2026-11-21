using UnityEngine;
using UnityEngine.UI;

public class CarSpeedometerUI : MonoBehaviour
{
    [Header("Ссылки")]
    public Rigidbody carRigidbody;
    public CarController carController;

    [Header("UI Элементы")]
    public Text speedText;
    public Text mileageText;

    [Header("Настройки")]
    public float speedMultiplier = 3.6f; // Перевод из м/с в км/ч

    private float totalMileageKm = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        if (carRigidbody != null)
            lastPosition = carRigidbody.position;
    }

    private void Update()
    {
        if (carRigidbody == null) return;

        // Считаем скорость в км/ч
        float speedKmh = carRigidbody.velocity.magnitude * speedMultiplier;

        if (speedText != null)
        {
            speedText.text = Mathf.RoundToInt(speedKmh) + " км/ч";
        }

        // Считаем пробег, если игрок за рулем
        if (carController != null && carController.isDriving)
        {
            float distanceMeters = Vector3.Distance(carRigidbody.position, lastPosition);
            totalMileageKm += distanceMeters / 1000f;

            if (mileageText != null)
            {
                mileageText.text = $"Пробег: {totalMileageKm:F1} км";
            }
        }

        lastPosition = carRigidbody.position;
    }
}
