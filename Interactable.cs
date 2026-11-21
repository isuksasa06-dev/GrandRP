using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Настройки взаимодействия")]
    public string actionText = "Взаимодействовать";

    [Header("Что произойдет при нажатии")]
    public UnityEvent onInteract;

    public void Interact()
    {
        onInteract?.Invoke();
    }
}
