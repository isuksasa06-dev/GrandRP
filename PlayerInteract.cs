using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Настройки поиска")]
    public float interactRange = 2.5f;
    public LayerMask interactLayer;

    [Header("UI Кнопка на экране")]
    public Button interactButton;
    public Text interactButtonText;

    private Interactable currentInteractable;

    private void Start()
    {
        if (interactButton != null)
        {
            interactButton.gameObject.SetActive(false);
            interactButton.onClick.AddListener(OnInteractButtonClick);
        }
    }

    private void Update()
    {
        FindNearestInteractable();
    }

    private void FindNearestInteractable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactLayer);

        Interactable closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            Interactable interactable = hit.GetComponent<Interactable>();
            if (interactable != null)
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = interactable;
                }
            }
        }

        if (closest != currentInteractable)
        {
            currentInteractable = closest;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (interactButton == null) return;

        if (currentInteractable != null)
        {
            interactButton.gameObject.SetActive(true);
            if (interactButtonText != null)
                interactButtonText.text = currentInteractable.actionText;
        }
        else
        {
            interactButton.gameObject.SetActive(false);
        }
    }

    private void OnInteractButtonClick()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
