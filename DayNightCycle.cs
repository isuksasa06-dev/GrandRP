using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [Header("Солнце (Directional Light)")]
    public Light sunLight;

    [Header("Настройки времени")]
    [Range(0f, 24f)]
    public float currentTimeOfDay = 12f; // Старт в 12:00 дня
    public float fullDayDurationSeconds = 1200f; // 1 игровые сутки = 20 реальных минут

    [Header("Ночное освещение (фары машин, уличные фонари)")]
    public GameObject[] nightLights;

    [Header("UI Часы на экране (необязательно)")]
    public Text timeClockText;

    private bool isNight = false;

    private void Start()
    {
        // Сразу выставляем правильное состояние фонарей при запуске сцены
        isNight = (currentTimeOfDay >= 20f || currentTimeOfDay < 6f);
        ToggleNightLights(isNight);
        
        UpdateLightingAndLights();
        UpdateClockUI();
    }

    private void Update()
    {
        // Считаем ход игрового времени
        float timeSpeed = 24f / fullDayDurationSeconds;
        currentTimeOfDay += timeSpeed * Time.deltaTime;

        if (currentTimeOfDay >= 24f)
            currentTimeOfDay = 0f;

        UpdateLightingAndLights();
        UpdateClockUI();
    }

    private void UpdateLightingAndLights()
    {
        // Вращаем солнце по кругу (в 6:00 восход, в 12:00 зенит, в 18:00 закат)
        if (sunLight != null)
        {
            float sunAngle = (currentTimeOfDay / 24f) * 360f - 90f;
            sunLight.transform.localRotation = Quaternion.Euler(sunAngle, 170f, 0f);

            // Плавно меняем яркость солнца
            if (currentTimeOfDay >= 6f && currentTimeOfDay <= 18f)
                sunLight.intensity = Mathf.Lerp(sunLight.intensity, 1.1f, Time.deltaTime * 2f);
            else
                sunLight.intensity = Mathf.Lerp(sunLight.intensity, 0.15f, Time.deltaTime * 2f);
        }

        // Проверяем, наступила ли ночь (с 20:00 до 06:00)
        bool shouldBeNight = (currentTimeOfDay >= 20f || currentTimeOfDay < 6f);

        if (shouldBeNight != isNight)
        {
            isNight = shouldBeNight;
            ToggleNightLights(isNight);
        }
    }

    private void ToggleNightLights(bool state)
    {
        if (nightLights == null) return;

        foreach (GameObject lightObj in nightLights)
        {
            if (lightObj != null)
                lightObj.SetActive(state);
        }
    }

    private void UpdateClockUI()
    {
        if (timeClockText == null) return;

        int hours = Mathf.FloorToInt(currentTimeOfDay);
        int minutes = Mathf.FloorToInt((currentTimeOfDay - hours) * 60f);
        timeClockText.text = $"{hours:00}:{minutes:00}";
    }
}

