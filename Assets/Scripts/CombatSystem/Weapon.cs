using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(menuName = "Combat Rework/Weapon")]
    public class Weapon : ScriptableObject
    {
        public string weaponName;
        [TextArea]
        public string description;
        public WeaponBehavior prefab;
        public Moveset moveset;
    }
}