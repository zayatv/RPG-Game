using System.Collections.Generic;
using UnityEngine;

public class CharacterDataSO : ScriptableObject
{
    [field: SerializeField] public List<PlayableCharacterSO> AllPlayableCharacters;
    [field: SerializeField] public List<PlayableCharacterSO> OwnedPlayableCharacters;
    [field: SerializeField] public List<PlayableCharacterSO> EquipedPlayableCharacters;
}
