using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Genral Information")]
    [SerializeField] private GameObject menusGameObjectParent;

    [Header("Character Menu")]
    [SerializeField] private GameObject characterMenuObject;
    [field: SerializeField] public KeyCode OpenCharacterMenuKeyCode { get; private set; }
    
    void Start()
    {
        characterMenuObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(OpenCharacterMenuKeyCode))
        {
            OpenCharacterMenu();
        }
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
