using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{
    public class Armory : MonoBehaviour
    {
        public Weapon startingWeapon;
        public Transform rightHand;

        public Weapon CurrentWeapon { get; private set; }
        public WeaponBehavior CurrentWeaponBehavior { get; private set; }
        public List<IMovesetBehavior> MovesetBehaviors { get; private set; }

        private void Start()
        {
            if (startingWeapon != null)
                EquipWeapon(startingWeapon);
        }

        private void Update()
        {
            if (MovesetBehaviors != null && MovesetBehaviors.Count > 0)
            {
                MovesetBehaviors.ForEach(a => a.Tick(Time.deltaTime));
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
                UnequipCurrentWeapon();

            CurrentWeapon = weapon;
            MovesetBehaviors = CurrentWeapon.moveset.components.Select(a => a.GetBehavior()).ToList();
            CurrentWeaponBehavior = Instantiate(weapon.prefab, rightHand);

            MovesetBehaviors.ForEach(a => a.Equip(gameObject));
        }

        public void UnequipCurrentWeapon()
        {
            if (CurrentWeapon == null) 
                return;

            MovesetBehaviors.ForEach(a => a.Unequip());

            MovesetBehaviors.Clear();
            CurrentWeapon = null;
            Destroy(CurrentWeaponBehavior.gameObject);
        }
    }
}