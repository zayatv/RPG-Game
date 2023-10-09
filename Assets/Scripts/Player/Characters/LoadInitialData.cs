using UnityEngine;
using UnityEngine.TextCore.Text;

public class LoadInitialData : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private Player player;

    private void Awake()
    {
        Destroy(playerObject.GetChild(0).gameObject);

        var playableCharacter = Instantiate(player.CharacterData.CurrentPlayableCharacter.CharacterModel, playerObject);

        playableCharacter.transform.SetParent(playerObject);
        playableCharacter.transform.SetAsFirstSibling();

        player.Animator = playableCharacter.GetComponent<Animator>();

        player.WeaponParentTransform = FindChild.RecursiveFindChild(playableCharacter.transform, "Weapon");
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
}
