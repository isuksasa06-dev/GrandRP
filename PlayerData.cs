using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemId;
    public string itemName;
    public int amount;
}

[System.Serializable]
public class PlayerData
{
    public string playerName = "NewPlayer";

    public int level = 1;
    public int experience = 0;

    public int money = 5000;
    public int bankMoney = 0;

    public float health = 100f;
    public float hunger = 100f;
    public float thirst = 100f;

    public bool hasCar = false;
    public string ownedCarName = "Нет";

    public bool hasHouse = false;
    public string ownedHouseName = "Нет";

    public string factionName = "Гражданский";
    public int factionRank = 0;

    public int wantedLevel = 0;
    public float jailTimeLeft = 0f;

    public List<InventoryItem> inventory = new List<InventoryItem>();
}


