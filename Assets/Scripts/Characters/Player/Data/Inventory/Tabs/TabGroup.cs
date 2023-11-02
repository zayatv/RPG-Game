using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> TabButtons;

    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;

    private TabButton selectedTab;

    [SerializeField] private Transform scrollContainer;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject inventoryItemStackablePrefab;

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
        Debug.Log(index);
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
        Debug.Log("LoadInventoryTab Index: " + index);
        if (index < 2)
        {
            Debug.Log("Setting Inventory List");
            List<InventoryItemSO> inventoryPage = GetInventoryList(index);

            foreach (InventoryItemSO item in inventoryPage)
            {
                //Instantiate Inventory Item Image
                Debug.Log("Instantiating Inventory Item");

                var inventoryItem = Instantiate(inventoryItemPrefab, scrollContainer);
                inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;

                inventoryItem.GetComponent<InventoryItemUI>().Item = item;
            }
        }
        else
        {
            SerializedDictionary<InventoryItemSO, int> inventoryPage = GetInventoryDic(index);

            foreach (KeyValuePair<InventoryItemSO, int> item in inventoryPage)
            {
                //Instantiate Inventory Item Image

                var inventoryItem = Instantiate(inventoryItemStackablePrefab, scrollContainer);
                inventoryItem.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.Icon;

                Transform amountText = inventoryItem.transform.GetChild(inventoryItem.transform.childCount - 1);
                amountText.GetComponent<TextMeshProUGUI>().text = item.Value.ToString();

                inventoryItem.GetComponent<InventoryItemUI>().Item = item.Key;
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
                Debug.Log("Weapon List Selected");
                break;
            case 1:
                inventoryPage = player.InventoryData.Armor;
                break;
            default:
                inventoryPage = new List<InventoryItemSO>();
                break;
        }

        Debug.Log("List Selection done!");

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
