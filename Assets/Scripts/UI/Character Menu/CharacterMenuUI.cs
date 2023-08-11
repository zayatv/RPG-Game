using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterMenuUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private AnimatorController controller;
    [SerializeField] private PlayerCharacterDataSO characterData;
    [SerializeField] private GameObject characterMenuUIParent;
    [SerializeField] private GameObject characterButton;
    [SerializeField] private Button selectCharacterButton;
    [SerializeField] private Transform container;
    [SerializeField] private Transform characterModelParent;
    [SerializeField] private KeyCode characterMenuKey;
    [SerializeField] private AnimatorController idleAnimatorController;

    [SerializeField] private float characterRotationModifier = 0.25f;

    private Player player;
    private PlayableCharacterSO selectedCharacter;

    private Vector2 prevMousePosition = Vector2.zero;
    private Vector2 mousePositionDelta = Vector2.zero;

    private Transform characterModelTransform;

    private bool isCharacterMenuOpen = false;
    private bool isDraggingCharacterModel = false;

    private void Start()
    {
        characterMenuUIParent.SetActive(false);
        selectedCharacter = characterData.OwnedPlayableCharacters[0];
        player = playerObject.GetComponent<Player>();

        LoadCharacterModel();

        selectCharacterButton.onClick.AddListener(OnSelectCharacterClick);
    }

    private void Update()
    {
        SetCharacterMenuUI();
    }

    private void SelectCharacter()
    {
        Destroy(playerObject.transform.GetChild(0).gameObject);

        GameObject character = Instantiate(selectedCharacter.CharacterModel, playerObject.transform);
        character.transform.SetAsFirstSibling();

        character.AddComponent<Animator>();
        character.AddComponent<PlayerAnimationEventTrigger>();

        Animator animator = character.GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        player.Animator = animator;
    }

    private void OnSelectCharacterClick()
    {
        SelectCharacter();
        CloseCharacterMenu();
    }

    private void OnMenuItemClick(GameObject button)
    {
        int siblingIndex = button.transform.GetSiblingIndex();
        selectedCharacter = characterData.OwnedPlayableCharacters[siblingIndex];
        LoadCharacterModel();
    }

    private void LoadCharacterList()
    {
        foreach (PlayableCharacterSO playableCharacter in characterData.OwnedPlayableCharacters)
        {
            var characterUIItem = Instantiate(characterButton);
            characterUIItem.GetComponent<Button>().onClick.AddListener(delegate { OnMenuItemClick(characterUIItem); });

            Image img = characterUIItem.transform.Find("Character Sprite").GetComponent<Image>();
            img.sprite = playableCharacter.UISprite;

            TextMeshProUGUI characterNameText = characterUIItem.transform.Find("Character Name").GetComponent<TextMeshProUGUI>();
            characterNameText.text = playableCharacter.name;

            characterUIItem.transform.SetParent(container);
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

        Animator animator = characterModel.AddComponent<Animator>();
        animator.runtimeAnimatorController = idleAnimatorController;

        characterModel.transform.localPosition = new Vector3(0f, -820f, 0f);
        characterModel.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        characterModel.transform.localScale = new Vector3(1f, 1f, 1f);
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform == characterModelParent) isDraggingCharacterModel = true;

        Debug.Log(eventData.pointerDrag.name);

        if (isDraggingCharacterModel)
            Cursor.lockState = CursorLockMode.Confined;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (isDraggingCharacterModel)
            Debug.Log(eventData.pointerDrag.name);

        mousePositionDelta = eventData.position - prevMousePosition;
        characterModelTransform.Rotate(transform.up, -Vector2.Dot(mousePositionDelta, uiCamera.transform.right) * characterRotationModifier);
        prevMousePosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform == characterModelParent) isDraggingCharacterModel = false;

        prevMousePosition = Vector2.zero;

        if (isDraggingCharacterModel)
            Cursor.lockState = CursorLockMode.None;
    }
}
