using UnityEngine;

public class FoodShopPoint : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerDataManager dataManager;
    public PlayerNeeds playerNeeds;

    [Header("Товар и цена")]
    public string itemName = "Бургер и вода";
    public int price = 50;

    [Header("Сколько восстанавливает")]
    public float restoreHunger = 35f;
    public float restoreThirst = 25f;
    public float restoreHealth = 10f;

    public void BuyItem()
    {
        if (dataManager == null || dataManager.playerData == null || playerNeeds == null)
        {
            Debug.LogWarning("Не назначены ссылки в FoodShopPoint!");
            return;
        }

        if (dataManager.playerData.money >= price)
        {
            dataManager.playerData.money -= price;

            if (restoreHunger > 0) playerNeeds.Eat(restoreHunger);
            if (restoreThirst > 0) playerNeeds.Drink(restoreThirst);
            if (restoreHealth > 0) playerNeeds.Heal(restoreHealth);

            dataManager.SavePlayer();
            Debug.Log($"Вы купили: {itemName} за {price}$!");
        }
        else
        {
            Debug.Log("Недостаточно наличных денег!");
        }
    }
}
