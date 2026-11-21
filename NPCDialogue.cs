using UnityEngine;
using UnityEngine.Events;

public class NPCDialogue : MonoBehaviour
{
    [Header("Ссылки")]
    public DialogueUI dialogueUI;

    [Header("Настройки персонажа")]
    public string npcName = "Прораб Иван";
    [TextArea(2, 5)]
    public string greetingMessage = "Привет! Нужна подработка? Можешь потаскать ящики и заработать немного наличных.";

    [Header("Вариант ответа 1 (Действие)")]
    public string option1Label = "Начать работу";
    public UnityEvent onOption1Selected;

    [Header("Вариант ответа 2 (Отказ / Выход)")]
    public string option2Label = "В другой раз";
    public UnityEvent onOption2Selected;

    // Этот метод вызывается через Interactable -> onInteract
    public void StartConversation()
    {
        if (dialogueUI == null)
        {
            Debug.LogWarning("Не назначен DialogueUI в NPCDialogue!");
            return;
        }

        dialogueUI.ShowDialogue(
            npcName,
            greetingMessage,
            option1Label,
            onOption1Selected,
            option2Label,
            onOption2Selected
        );
    }
}
