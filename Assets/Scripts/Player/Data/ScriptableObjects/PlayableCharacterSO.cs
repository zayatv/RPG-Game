using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Custom/Characters/PlayableCharacter")]
public class PlayableCharacterSO : ScriptableObject
{
    [field: Header("Character Details")]
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public Rarity CharacterRarity { get; private set; }
    [field: SerializeField] public GameObject CharacterModel { get; private set; }
    [field: SerializeField] public Sprite UISprite { get; private set; }

    [field: Header("LevelSystem")]
    [field:SerializeField] public LevelSystem LevelSystem { get; set; }

    [field: Header("Character Stats")]
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Defense { get; private set; }
    [field: SerializeField] public Stat Attack { get; private set; }
    [field: SerializeField] public Stat Strength { get; private set; }
    [field: SerializeField] public Stat AttackSpeed { get; private set; }
    [field: SerializeField] public Stat CriticalHitChance { get; private set; }
    [field: SerializeField] public Stat CriticalHitDamage { get; private set; }
}
