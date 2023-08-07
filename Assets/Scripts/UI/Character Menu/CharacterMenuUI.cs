using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacterDataSO characterData;
    [SerializeField] private Transform container;
    [SerializeField] private KeyCode characterMenuKey;

    private bool isCharacterMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(characterMenuKey))
        {
            if (isCharacterMenuOpen)
            {
                
            }
            else
            {
               LoadCharacterList(); 
            }
        }
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
}
