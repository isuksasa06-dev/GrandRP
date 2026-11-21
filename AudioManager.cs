using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Источники звука")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Базовые звуки RP")]
    public AudioClip buttonClickClip;
    public AudioClip moneyCashClip;
    public AudioClip errorClip;
    public AudioClip levelUpClip;

    private bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем звук при переходе между сценами
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlayClick()
    {
        if (Instance != null)
            Instance.PlaySFX(Instance.buttonClickClip);
    }

    public static void PlayMoney()
    {
        if (Instance != null)
            Instance.PlaySFX(Instance.moneyCashClip);
    }

    public static void PlayError()
    {
        if (Instance != null)
            Instance.PlaySFX(Instance.errorClip);
    }

    public static void PlayLevelUp()
    {
        if (Instance != null)
            Instance.PlaySFX(Instance.levelUpClip);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isMuted || clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // Для кнопки "Звук Вкл/Выкл" в меню паузы (PlayerMenuUI)
    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (sfxSource != null) sfxSource.mute = isMuted;
        if (musicSource != null) musicSource.mute = isMuted;

        NotificationUI.Show(isMuted ? "Звук выключен" : "Звук включен");
    }
}

