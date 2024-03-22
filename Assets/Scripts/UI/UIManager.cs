using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] private GameObject menusGameObjectParent;
    [SerializeField] private Player player;
    public static bool IsInMenu = false;

    [Header("Character Menu")]
    [SerializeField] private GameObject characterMenuObject;
    [field: SerializeField] public KeyCode OpenCharacterMenuKeyCode { get; private set; }

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuObject;
    [field: SerializeField] public KeyCode OpenPauseMenuKeyCode { get; private set; }

    [Header("Inventory")]
    [SerializeField] private GameObject inventoryMenuObject;
    [field: SerializeField] public KeyCode OpenInventoryMenuKeyCode { get; private set; }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        characterMenuObject.SetActive(false);
    }

    void Update()
    {
        if (IsInMenu)
        {
            return;
        }

        SetCursorStatus();

        if (Input.GetKeyDown(OpenCharacterMenuKeyCode))
        {
            OpenCharacterMenu();
        }
        else if (Input.GetKeyDown(OpenPauseMenuKeyCode))
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(OpenInventoryMenuKeyCode))
        {
            OpenInventoryMenu();
        }
    }

    public void OpenPauseMenu()
    {
        OpenUiMenu();
        pauseMenuObject.SetActive(true);
    }

    public void OpenCharacterMenu()
    {
        OpenUiMenu();
        characterMenuObject.SetActive(true);
    }

    public void OpenInventoryMenu()
    {
        OpenUiMenu();
        inventoryMenuObject.SetActive(true);
    }

    private void OpenUiMenu()
    {
        StopTime();
        CloseAllMenus();

        SetCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        player.Input.PlayerActions.Disable();

        IsInMenu = true;
    }

    private void CloseAllMenus()
    {
        foreach (Transform child in menusGameObjectParent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void SetCursorStatus()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SetCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void SetCursorPosition(Vector2 newCusorPos)
    {
        Mouse.current.WarpCursorPosition(newCusorPos);
    }
}
