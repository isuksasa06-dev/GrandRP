using UnityEngine;

public class MobileOptimizer : MonoBehaviour
{
    [Header("Настройки производительности")]
    public int targetFPS = 60;
    public bool preventScreenSleep = true;

    private void Awake()
    {
        // Снимаем ограничение в 30 FPS и ставим плавные 60 кадров
        Application.targetFrameRate = targetFPS;

        // Запрещаем экрану телефона гаснуть во время игры
        if (preventScreenSleep)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }

    // Методы для кнопок выбора качества графики в меню паузы (PlayerMenuUI)
    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0, true);
        Debug.Log("Установлена низкая графика");
    }

    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(2, true);
        Debug.Log("Установлена средняя графика");
    }

    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(4, true);
        Debug.Log("Установлена высокая графика");
    }
}
