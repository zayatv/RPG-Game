using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterMenuUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject playerObject;

    [SerializeField] private Player player;
    [SerializeField] private GameObject characterButton;
    [SerializeField] private Button selectCharacterButton;
    [SerializeField] private Transform scrollViewContainer;
    [SerializeField] private Transform characterModelParent;
    [SerializeField] private RuntimeAnimatorController idleAnimatorController;

    [SerializeField] private float characterRotationModifier = 0.1f;

    private PlayableCharacterSO selectedCharacter;

    private Vector2 mousePositionWhenEnteredDragging;
    private Vector2 mousePositionDelta = Vector2.zero;

    private Transform characterModelTransform;

    private void Start()
    {
        selectedCharacter = player.CharacterData.CurrentPlayableCharacter;

        LoadCharacterModel();

        selectCharacterButton.onClick.AddListener(OnSelectCharacterClick);
    }

    private void OnEnable()
    {
        selectedCharacter = player.CharacterData.CurrentPlayableCharacter;

        OpenCharacterMenu();
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        SetCharacterMenuUI();
    }

    private void SelectCharacter()
    {
        Destroy(playerObject.transform.GetChild(0).gameObject);

        player.CharacterData.CurrentPlayableCharacter = selectedCharacter;

        GameObject character = Instantiate(selectedCharacter.CharacterModel, playerObject.transform);
        character.transform.SetAsFirstSibling();

        Animator animator = character.GetComponent<Animator>();
        player.Animator = animator;

        player.WeaponParentTransform = FindChild.RecursiveFindChild(character.transform, "Weapon");
        ApplyWeaponToCharacter();
    }

    private void ApplyWeaponToCharacter()
    {
        foreach (Transform child in player.WeaponParentTransform)
        {
            Destroy(child.gameObject);
        }

        var currentWeapon = Instantiate(player.CurrentEquippedWeapon.WeaponPrefab, player.WeaponParentTransform);
        currentWeapon.transform.SetParent(player.WeaponParentTransform);
    }

    private void OnSelectCharacterClick()
    {
        SelectCharacter();
        CloseCharacterMenu();
    }

    private void OnMenuItemClick(GameObject button)
    {
        int siblingIndex = button.transform.GetSiblingIndex();
        selectedCharacter = player.CharacterData.OwnedPlayableCharacters[siblingIndex];
        LoadCharacterModel();
    }

    private void LoadCharacterList()
    {
        UnloadCharacterList();
        foreach (PlayableCharacterSO playableCharacter in player.CharacterData.OwnedPlayableCharacters)
        {
            var characterUIItem = Instantiate(characterButton);
            characterUIItem.GetComponent<Button>().onClick.AddListener(delegate { OnMenuItemClick(characterUIItem); });

            Image img = characterUIItem.transform.Find("Character Sprite").GetComponent<Image>();
            img.sprite = playableCharacter.UISprite;

            TextMeshProUGUI characterNameText = characterUIItem.transform.Find("Character Name").GetComponent<TextMeshProUGUI>();
            characterNameText.text = playableCharacter.name;

            characterUIItem.transform.SetParent(scrollViewContainer);
            characterUIItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
            characterUIItem.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void LoadCharacterModel()
    {
        Destroy(characterModelParent.GetChild(0).gameObject);
        GameObject characterModel = Instantiate(selectedCharacter.CharacterModel, characterModelParent);

        characterModelTransform = characterModel.transform;

        characterModelTransform.SetParent(characterModelParent);
        characterModelTransform.SetAsFirstSibling();
        characterModel.layer = LayerMask.NameToLayer("UICharacterModel");

        foreach (Transform child in characterModel.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UICharacterModel");
        }

        Animator animator = characterModel.GetComponent<Animator>();
        animator.runtimeAnimatorController = idleAnimatorController;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;

        characterModel.transform.localPosition = new Vector3(0f, -820f, 0f);
        characterModel.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        characterModel.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void UnloadCharacterList()
    {
        for (int i = 0; i < scrollViewContainer.childCount; i++)
        {
            Destroy(scrollViewContainer.GetChild(i).gameObject);
        }
    }

    public void SetCharacterMenuUI()
    {
        if (Input.GetKeyDown(uiManager.OpenCharacterMenuKeyCode) || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCharacterMenu();
        }
    }

    public void OpenCharacterMenu()
    {
        LoadCharacterList();
    }

    public void CloseCharacterMenu()
    {
        gameObject.SetActive(false);

        ContinueTime();
        UnloadCharacterList();

        player.Input.PlayerActions.Enable();

        UIManager.IsInMenu = false;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cursor.visible = false;

        mousePositionWhenEnteredDragging = Mouse.current.position.value;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        mousePositionDelta = Mouse.current.delta.ReadValue();
        characterModelTransform.Rotate(transform.up, -Vector2.Dot(mousePositionDelta, uiCamera.transform.right) * characterRotationModifier);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Mouse.current.WarpCursorPosition(mousePositionWhenEnteredDragging);

        Cursor.visible = true;
    }
}
