using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueUI : MonoBehaviour
{
    [Header("UI Элементы")]
    public GameObject dialoguePanel;
    public Text npcNameText;
    public Text dialogueText;

    [Header("Кнопки ответов")]
    public Button option1Button;
    public Text option1Text;

    public Button option2Button;
    public Text option2Text;

    private UnityEvent currentOption1Event;
    private UnityEvent currentOption2Event;

    private void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (option1Button != null)
            option1Button.onClick.AddListener(OnOption1Clicked);

        if (option2Button != null)
            option2Button.onClick.AddListener(OnOption2Clicked);
    }

    public void ShowDialogue(string npcName, string message, string opt1Label, UnityEvent opt1Event, string opt2Label = "Уйти", UnityEvent opt2Event = null)
    {
        if (dialoguePanel == null) return;

        dialoguePanel.SetActive(true);

        if (npcNameText != null) npcNameText.text = npcName;
        if (dialogueText != null) dialogueText.text = message;

        currentOption1Event = opt1Event;
        currentOption2Event = opt2Event;

        if (option1Button != null)
        {
            option1Button.gameObject.SetActive(!string.IsNullOrEmpty(opt1Label));
            if (option1Text != null) option1Text.text = opt1Label;
        }

        if (option2Button != null)
        {
            option2Button.gameObject.SetActive(!string.IsNullOrEmpty(opt2Label));
            if (option2Text != null) option2Text.text = opt2Label;
        }
    }

    public void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    private void OnOption1Clicked()
    {
        CloseDialogue();
        currentOption1Event?.Invoke();
    }

    private void OnOption2Clicked()
    {
        CloseDialogue();
        currentOption2Event?.Invoke();
    }
}
