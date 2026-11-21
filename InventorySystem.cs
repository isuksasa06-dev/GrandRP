using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public PlayerNeeds playerNeeds;
    public CarFuel carFuel;

    // Добавить предмет в рюкзак
    public void AddItem(string id, string displayName, int count = 1)
    {
        if (dataManager == null || dataManager.playerData == null || count <= 0) return;

        var items = dataManager.playerData.inventory;
        InventoryItem existing = items.Find(i => i.itemId == id);

        if (existing != null)
        {
            existing.amount += count;
        }
        else
        {
            items.Add(new InventoryItem
            {
                itemId = id,
                itemName = displayName,
                amount = count
            });
        }

        dataManager.SavePlayer();
        Debug.Log($"В рюкзак добавлено: {displayName} x{count}");
    }

    // Использовать предмет по его ID
    public void UseItem(string id)
    {
        if (dataManager == null || dataManager.playerData == null) return;

        var items = dataManager.playerData.inventory;
        InventoryItem item = items.Find(i => i.itemId == id);

        if (item == null || item.amount <= 0)
        {
            Debug.Log("Такого предмета нет в рюкзаке!");
            return;
        }

        bool used = ApplyItemEffect(id);

        if (used)
        {
            item.amount--;
            if (item.amount <= 0)
            {
                items.Remove(item);
            }
            dataManager.SavePlayer();
        }
    }

    // Эффекты от предметов
    private bool ApplyItemEffect(string id)
    {
        switch (id)
        {
            case "burger":
                if (playerNeeds != null)
                {
                    playerNeeds.Eat(40f);
                    Debug.Log("Вы съели бургер (+40 к сытости)");
                    return true;
                }
                break;

            case "water":
                if (playerNeeds != null)
                {
                    playerNeeds.Drink(35f);
                    Debug.Log("Вы выпили воды (+35 к жажде)");
                    return true;
                }
                break;

            case "medkit":
                if (playerNeeds != null)
                {
                    playerNeeds.Heal(50f);
                    Debug.Log("Вы использовали аптечку (+50 HP)");
                    return true;
                }
                break;

            case "canister":
                if (carFuel != null)
                {
                    carFuel.Refuel(30f);
                    if (carFuel.carController != null)
                        carFuel.carController.isDriving = true;
                    Debug.Log("Вы заправили машину из канистры (+30 л.)");
                    return true;
                }
                break;

            default:
                Debug.Log("Этот предмет нельзя использовать напрямую.");
                break;
        }
        return false;
    }

    // Узнать количество конкретного предмета
    public int GetItemCount(string id)
    {
        if (dataManager == null || dataManager.playerData == null) return 0;
        InventoryItem item = dataManager.playerData.inventory.Find(i => i.itemId == id);
        return item != null ? item.amount : 0;
    }
}
