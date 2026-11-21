using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour
{
    public static NotificationUI Instance;

    [Header("UI Элементы уведомления")]
    public GameObject notificationPanel;
    public Text notificationText;
    public Image backgroundImage;

    [Header("Настройки")]
    public float displayDuration = 3f;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (notificationPanel != null)
            notificationPanel.SetActive(false);
    }

    // Обычное уведомление (синее/темное)
    public static void Show(string message)
    {
        if (Instance != null)
            Instance.Display(message, new Color(0.15f, 0.15f, 0.2f, 0.9f));
    }

    // Успех / Деньги / PayDay (зеленое)
    public static void ShowSuccess(string message)
    {
        if (Instance != null)
            Instance.Display(message, new Color(0.1f, 0.55f, 0.25f, 0.9f));
    }

    // Ошибка / Розыск / Недостаточно денег (красное)
    public static void ShowError(string message)
    {
        if (Instance != null)
            Instance.Display(message, new Color(0.7f, 0.15f, 0.15f, 0.9f));
    }

    private void Display(string message, Color bgColor)
    {
        if (notificationPanel == null || notificationText == null) return;

        notificationText.text = message;

        if (backgroundImage != null)
            backgroundImage.color = bgColor;

        notificationPanel.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (notificationPanel != null)
            notificationPanel.SetActive(false);
    }
}
