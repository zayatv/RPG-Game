using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "Custom/Player/PlayableCharacter")]
public class PlayableCharacterSO : ScriptableObject
{
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public Rarity CharacterRarity { get; private set; }
    [field: SerializeField] public GameObject CharacterModel { get; private set; }
    [field: SerializeField] public Sprite UISprite { get; private set; }
}
