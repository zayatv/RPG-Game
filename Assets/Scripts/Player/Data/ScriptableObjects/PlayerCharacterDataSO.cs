using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterData", menuName = "Custom/Player/Data/PlayableCharacterData")]
public class PlayerCharacterDataSO : ScriptableObject
{
    [field: SerializeField] public List<PlayableCharacterSO> AllPlayableCharacters { get; private set; }
    [field: SerializeField] public List<PlayableCharacterSO> OwnedPlayableCharacters { get; private set; }
    [field: SerializeField] public PlayableCharacterSO CurrentPlayableCharacter { get; set; }
}
