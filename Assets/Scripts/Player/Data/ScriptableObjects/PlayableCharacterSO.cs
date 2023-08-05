using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Custom/Characters/PlayableCharacter")]
public class PlayableCharacterSO : ScriptableObject
{
    [field: Header("Character Details")]
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public GameObject CharacterModel { get; private set; }
    [field: SerializeField] public Sprite UISPrite { get; private set; }
    [field: SerializeField] public bool IsInParty { get; set; }
    [field: SerializeField] public bool IsOwned { get; set; }

    [field: Header("LevelSystem")]
    [field:SerializeField] public LevelSystem LevelSystem { get; set; }

    [field: Header("Character Stats")]
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
}
