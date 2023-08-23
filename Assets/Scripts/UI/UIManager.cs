using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetKeyDown(OpenCharacterMenuKeyCode))
        {
            OpenCharacterMenu();
        }
        else if (Input.GetKeyDown(OpenPauseMenuKeyCode))
        {
            OpenPauseMenu();
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

    private void OpenUiMenu()
    {
        StopTime();
        CloseAllMenus();

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
}
