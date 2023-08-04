using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Custom/Characters/Player")]
public class PlayableCharacterSO : ScriptableObject
{
    [field: Header("Character Details")]
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public GameObject CharacterModel { get; private set; }

    [field: Header("Character Stats")]
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
}
