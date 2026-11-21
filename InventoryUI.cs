using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Ссылки")]
    public InventorySystem inventorySystem;
    public GameObject inventoryPanel;

    [Header("Текст количества предметов")]
    public Text burgerCountText;
    public Text waterCountText;
    public Text medkitCountText;
    public Text canisterCountText;

    private void Start()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
            RefreshUI();
    }

    public void RefreshUI()
    {
        if (inventorySystem == null) return;

        if (burgerCountText != null)
            burgerCountText.text = "Бургер: " + inventorySystem.GetItemCount("burger") + " шт.";

        if (waterCountText != null)
            waterCountText.text = "Вода: " + inventorySystem.GetItemCount("water") + " шт.";

        if (medkitCountText != null)
            medkitCountText.text = "Аптечка: " + inventorySystem.GetItemCount("medkit") + " шт.";

        if (canisterCountText != null)
            canisterCountText.text = "Канистра: " + inventorySystem.GetItemCount("canister") + " шт.";
    }

    // Методы для кнопок "Использовать" в UI рюкзака
    public void UseBurger()
    {
        if (inventorySystem == null) return;
        inventorySystem.UseItem("burger");
        RefreshUI();
    }

    public void UseWater()
    {
        if (inventorySystem == null) return;
        inventorySystem.UseItem("water");
        RefreshUI();
    }

    public void UseMedkit()
    {
        if (inventorySystem == null) return;
        inventorySystem.UseItem("medkit");
        RefreshUI();
    }

    public void UseCanister()
    {
        if (inventorySystem == null) return;
        inventorySystem.UseItem("canister");
        RefreshUI();
    }
}
