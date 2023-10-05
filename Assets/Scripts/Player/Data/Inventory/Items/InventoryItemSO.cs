using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Custom/Inventory/InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public int Id;
    public ItemCategory Category;
}
