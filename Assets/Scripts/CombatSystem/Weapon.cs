using Animancer;
using System.Collections.Generic;
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
        public List<AttackData> attackCombo;
        public WeaponType weaponType;
    }

    [System.Serializable]
    public class AttackData
    {
        public ClipTransition animation;
        public OnHitEffect onHitEffect;
    }
}