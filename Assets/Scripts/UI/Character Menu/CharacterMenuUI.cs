using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacterDataSO characterData;
    [SerializeField] private GameObject characterMenuUIParent;
    [SerializeField] private Transform container;
    [SerializeField] private KeyCode characterMenuKey;

    private bool isCharacterMenuOpen = false;

    private void Start()
    {
        characterMenuUIParent.SetActive(false);
    }

    private void Update()
    {
        SetCharacterMenuUI();
    }

    private void LoadCharacterList()
    {
        foreach (PlayableCharacterSO playableCharacter in characterData.OwnedPlayableCharacters)
        {
            GameObject go = new GameObject("UIElement");
            Image img = go.AddComponent<Image>();
            img.sprite = playableCharacter.UISprite;
            img.transform.SetParent(container);
        }
    }

    private void UnloadCharacterList()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }

    public void SetCharacterMenuUI()
    {
        if (Input.GetKeyDown(characterMenuKey))
        {
            if (isCharacterMenuOpen)
            {
                CloseCharacterMenu();
            }
            else
            {
                OpenCharacterMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isCharacterMenuOpen)
        {
            CloseCharacterMenu();
        }
    }

    public void OpenCharacterMenu()
    {
        characterMenuUIParent.SetActive(true);
        isCharacterMenuOpen = true;

        StopTime();
        LoadCharacterList();
    }

    public void CloseCharacterMenu()
    {
        characterMenuUIParent.SetActive(false);
        isCharacterMenuOpen = false;

        ContinueTime();
        UnloadCharacterList();
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1f;
    }
}
