using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(menuName = "Combat Rework/Weapon")]
    public class Weapon : ScriptableObject
    {
        public string weaponName;
        [TextArea]
        public string description;
        public WeaponBehavior prefab;
        public WeaponType weaponType;
        public bool changeAnimator = true;
        [ShowIf(nameof(changeAnimator))]
        public RuntimeAnimatorController animatorController;
    }
}