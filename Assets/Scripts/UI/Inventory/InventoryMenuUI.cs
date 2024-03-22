using UnityEngine;

public class InventoryMenuUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;

    private void Update()
    {
        SetInventoryMenuUI();
    }

    private void OnDisable()
    {
        CloseInventoryMenu();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void SetInventoryMenuUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(uiManager.OpenInventoryMenuKeyCode))
        {
            CloseInventoryMenu();
        }
    }

    private void CloseInventoryMenu()
    {
        gameObject.SetActive(false);

        ContinueTime();

        player.Input.PlayerActions.Enable();

        UIManager.IsInMenu = false;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1f;
    }
}
