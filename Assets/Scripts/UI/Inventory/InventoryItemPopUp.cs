using UnityEngine;

public class InventoryItemPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;

    public GameObject popUp;

    public void OnItemEnter(InventoryItemUI item)
    {
        popUp = Instantiate(popUpPrefab, transform);
        popUp.transform.SetAsLastSibling();
        SetPosition(new Vector2(0, 0));
    }

    public void OnItemExit(InventoryItemUI item)
    {
        if (popUp != null) Destroy(popUp);
    }

    public void SetPosition(Vector2 position)
    {
        popUp.GetComponent<RectTransform>().anchoredPosition = position;
    }
}
