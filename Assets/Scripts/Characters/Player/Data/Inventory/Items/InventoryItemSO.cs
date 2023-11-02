using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Custom/Inventory/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public int Id;
    public Rarity Rarity;
    public ItemCategory Category;
}
