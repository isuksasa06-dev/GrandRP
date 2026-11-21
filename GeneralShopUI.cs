using UnityEngine;

public class GeneralShopUI : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public InventorySystem inventorySystem;
    public GameObject shopPanel;

    [Header("Цены в магазине")]
    public int burgerPrice = 30;
    public int waterPrice = 15;
    public int medkitPrice = 100;
    public int canisterPrice = 150;

    private void Start()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    public void BuyBurger() => TryBuy("burger", "Бургер", burgerPrice);
    public void BuyWater() => TryBuy("water", "Вода", waterPrice);
    public void BuyMedkit() => TryBuy("medkit", "Аптечка", medkitPrice);
    public void BuyCanister() => TryBuy("canister", "Канистра", canisterPrice);

    private void TryBuy(string id, string itemName, int price)
    {
        if (dataManager == null || dataManager.playerData == null || inventorySystem == null) return;

        if (dataManager.playerData.money >= price)
        {
            dataManager.playerData.money -= price;
            inventorySystem.AddItem(id, itemName, 1);
            Debug.Log($"Куплено: {itemName} за {price}$");
        }
        else
        {
            Debug.Log("Недостаточно наличных!");
        }
    }
}
