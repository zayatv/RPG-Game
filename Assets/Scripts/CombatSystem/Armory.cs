using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{
    public class Armory : MonoBehaviour
    {
        public Weapon startingWeapon;
        public Transform rightHand, leftHand;
        public bool equippedBow = false;

        public Weapon CurrentWeapon;
        public Weapon bow;
        public WeaponBehavior CurrentWeaponBehavior { get; set; }
        public List<IMovesetBehavior> MovesetBehaviors { get; set; }
        public archerInputScript bowCheck;
        private void Start()
        {
            if (startingWeapon != null)
                EquipWeapon(startingWeapon);
            EquipRangedWeapon(bow);
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
        public void EquipRangedWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
               UnequipCurrentWeapon();
               
            

            
            CurrentWeapon = weapon;
            MovesetBehaviors = CurrentWeapon.moveset.components.Select(a => a.GetBehavior()).ToList();
            CurrentWeaponBehavior = Instantiate(weapon.prefab, leftHand);
            
            MovesetBehaviors.ForEach(a => a.Equip(gameObject));

        }

        public void UnequipCurrentWeapon()
        {
            if (CurrentWeapon == null) 
                return;

            MovesetBehaviors.ForEach(a => a.Unequip());

            MovesetBehaviors.Clear();
            CurrentWeapon = null;
            if (CurrentWeaponBehavior.isRangedWeapon == true)
            {
                Destroy(CurrentWeaponBehavior.gameObject);
            }
        }
    }
}