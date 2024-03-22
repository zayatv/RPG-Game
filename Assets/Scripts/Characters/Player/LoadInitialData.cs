using CombatSystem;
using UnityEngine;

public class LoadInitialData : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private Player player;

    private void Awake()
    {
        Destroy(playerObject.GetComponentInChildren<PlayerAnimationEventTrigger>().gameObject);

        var playableCharacter = Instantiate(player.CharacterData.CurrentPlayableCharacter.CharacterModel, playerObject);

        playableCharacter.transform.SetParent(playerObject);
        playableCharacter.transform.SetAsFirstSibling();

        player.Animator = playableCharacter.GetComponent<Animator>();

        player.Armory = GetComponentInChildren<Armory>();
        player.WeaponParentTransform = player.Armory.rightHand;
        //ApplyWeaponToCharacter();
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
}
