using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventoryData
{
    [field: Header("Non-Stackable Items")]
    [field: SerializeField] public List<InventoryItemSO> Weapons { get; private set; }
    [field: SerializeField] public List<InventoryItemSO> Armor { get; private set; }

    [field: Header("Stackable Items")]
    [field: SerializeField, SerializedDictionary("Item", "Amount")] public SerializedDictionary<InventoryItemSO, int> Runestones { get; private set; }
    [field: SerializeField, SerializedDictionary("Item", "Amount")] public SerializedDictionary<InventoryItemSO, int> Potions { get; private set; }
    [field: SerializeField, SerializedDictionary("Item", "Amount")] public SerializedDictionary<InventoryItemSO, int> Other { get; private set; }
    [field: SerializeField, SerializedDictionary("Item", "Amount")] public SerializedDictionary<InventoryItemSO, int> KeyItems { get; private set; }

    [field: Header("Currencies")]
    [field: SerializeField] public int Gold { get; set; }
    [field: SerializeField] public int FruitsOfLife { get; set; }
    [field: SerializeField] public int AscensionScore { get; set; }

    public void Initialize()
    {
        Weapons = new List<InventoryItemSO>();
        Armor = new List<InventoryItemSO>();

        Runestones = new SerializedDictionary<InventoryItemSO, int>();
        Potions = new SerializedDictionary<InventoryItemSO, int>();
        Other = new SerializedDictionary<InventoryItemSO, int>();
        KeyItems = new SerializedDictionary<InventoryItemSO, int>();

        Gold = 0;
        FruitsOfLife = 0;
        AscensionScore = 0;

        LoadInventoryData();
    }

    private void LoadInventoryData()
    {
        //Load Saved Inventory Data
    }

    private void UnloadInventoryData()
    {
        SaveInventoryData();

        Weapons.Clear();
        Armor.Clear();

        Runestones.Clear();
        Potions.Clear();
        Other.Clear();
        KeyItems.Clear();
    }

    private void SaveInventoryData()
    {

    }
}
