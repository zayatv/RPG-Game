using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItemSO Item;

    private InventoryItemPopUp inventoryItemPopUp;

    private void Start()
    {
        inventoryItemPopUp = transform.parent.parent.parent.parent.GetComponent<InventoryItemPopUp>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryItemPopUp.OnItemEnter(this);

        Vector2 popUpPos = GetComponent<RectTransform>().anchoredPosition + new Vector2(440, -280);
        if (popUpPos.x > 1500) popUpPos.x -= (175 + inventoryItemPopUp.popUp.GetComponent<RectTransform>().sizeDelta.x);
        inventoryItemPopUp.SetPosition(popUpPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryItemPopUp.OnItemExit(this);
    }
}
