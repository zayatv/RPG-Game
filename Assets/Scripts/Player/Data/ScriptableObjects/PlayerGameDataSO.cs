using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom/Player/PlayerData")]
public class PlayerGameDataSO : ScriptableObject
{
    [field: Header("General Account Data")]
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public LevelSystem PlayerLevelSystem { get; private set; }

    [field: Header("Playable Character Data")]
    [field: SerializeField] public List<PlayableCharacterSO> AllPlayableCharacters { get; private set; }
    [field: SerializeField] public List<PlayableCharacterSO> OwnedPlayableCharacters { get; private set; }
    [field: SerializeField] public List<PlayableCharacterSO> EquipedPlayableCharacters { get; private set; }

    [field: Header("Weapon Data")]
    [field: SerializeField] public List<WeaponSO> AllWeapons { get; private set; }
    [field: SerializeField] public List<WeaponSO> OwnedWeapons { get; private set; }

    [field: Header("Inventory Item Data")]
    [field: SerializeField] public List<string> OwnedItemsPlaceholder { get; private set; }
    
    [field: Header("Currency")]
    [field: SerializeField] public Currency FreeCurrency { get; private set; }
    [field: SerializeField] public Currency AnotherFreeCurrency { get; private set; }
    [field: SerializeField] public Currency PaidCurrency { get; private set; }
}
