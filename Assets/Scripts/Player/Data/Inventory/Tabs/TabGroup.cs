using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> TabButtons;

    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;

    private TabButton selectedTab;

    [SerializeField] private Transform scrollContainer;

    [SerializeField] private Player player;

    public void Subscribe(TabButton button)
    {
        if (TabButtons == null)
        {
            TabButtons = new List<TabButton>();
        }

        TabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;

        int index = button.transform.GetSiblingIndex();
        LoadInventoryTab(index);
    }

    public void ResetTabs()
    {
        foreach (TabButton button in TabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }

    private void LoadInventoryTab(int index)
    {
        if (index < 2)
        {
            List<InventoryItemSO> inventoryPage = GetInventoryList(index);

            foreach (InventoryItemSO item in inventoryPage)
            {
                //Instantiate Inventory Item Image
            }
        }
        else
        {
            SerializedDictionary<InventoryItemSO, int> inventoryPage = GetInventoryDic(index);

            foreach (KeyValuePair<InventoryItemSO, int> item in inventoryPage)
            {
                //Instantiate Inventory Item Image
            }
        }
    }

    private List<InventoryItemSO> GetInventoryList(int index)
    {
        List<InventoryItemSO> inventoryPage;

        switch (index)
        {
            case 0:
                inventoryPage = player.InventoryData.Weapons;
                break;
            case 1:
                inventoryPage = player.InventoryData.Armor;
                break;
            default:
                inventoryPage = new List<InventoryItemSO>();
                break;
        }

        return inventoryPage;
    }

    private SerializedDictionary<InventoryItemSO, int> GetInventoryDic(int index)
    {
        SerializedDictionary<InventoryItemSO, int> inventoryPage;

        switch (index)
        {
            case 2:
                inventoryPage = player.InventoryData.Runestones;
                break;
            case 3:
                inventoryPage = player.InventoryData.Potions;
                break;
            case 4:
                inventoryPage = player.InventoryData.Other;
                break;
            case 5:
                inventoryPage = player.InventoryData.KeyItems;
                break;
            default:
                inventoryPage = new SerializedDictionary<InventoryItemSO, int>();
                break;
        }

        return inventoryPage;
    }
}
