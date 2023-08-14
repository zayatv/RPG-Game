using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
            characterMenuObject.SetActive(true);
            OpenUiMenu();
        }
    }

    private void OpenUiMenu()
    {
        StopTime();
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }
}
