using RPG.Gameplay;
using UnityEngine;

namespace RPG.Combat
{
    public class Armory : MonoBehaviour
    {
        [SerializeField] private Weapon startingWeapon;

        private Actor actor;

        public Weapon CurrentWeapon { get; private set; }
        public WeaponBehavior CurrentWeaponBehavior { get; private set; }

        private void Start()
        {
            actor = GetComponent<Gameplay.Player>();

            if (startingWeapon != null)
                EquipWeapon(startingWeapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
                UnequipCurrentWeapon();

            CurrentWeapon = weapon;

            if (CurrentWeapon.weaponType == WeaponType.Bow)
                CurrentWeaponBehavior = Instantiate(weapon.prefab, actor.CharacterModel.leftHand);
            else
                CurrentWeaponBehavior = Instantiate(weapon.prefab, actor.CharacterModel.rightHand);

            if (CurrentWeapon.changeAnimator)
            {
                if (CurrentWeapon.animatorController != actor.CurrentController)
                {
                    actor.UpdateAnimatorController(CurrentWeapon.animatorController);
                }
            }
        }

        public void UnequipCurrentWeapon()
        {
            if (CurrentWeapon == null) 
                return;

            if (CurrentWeapon.changeAnimator)
                actor.UpdateAnimatorController(null);

            CurrentWeapon = null;
            Destroy(CurrentWeaponBehavior.gameObject);
        }
    }
}