using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class ChargeAttack : MovesetComponent
    {
        //We set the charge animations on the Animator Override for the weapon directly but they
        //absolutely can be changed here through code (see SingleAttack for reference)
        //Just showing all the possible ways Animator Override Controllers can be used.

        public float maxHoldTime = 2f;
        [Tooltip("The Animator must be in one of the states having these tags to allow attacking.")]
        public List<string> validStates = new List<string> { "Grounded" };
        public OnHitEffect onHitEffect;
        public override IMovesetBehavior GetBehavior()
        {
            return new ChargeAttackBehavior(this);
        }
    }

    public class ChargeAttackBehavior : MovesetBehavior<ChargeAttack>
    {
        private Animator animator;
        private Armory armory;
        private bool charging;
        private float chargeStartTime;
        private float chargeEndTime;

        public ChargeAttackBehavior(ChargeAttack data) : base(data)
        {
            
        }

        public override void Equip(GameObject user)
        {
            armory = user.GetComponent<Armory>();
            animator = user.GetComponent<Animator>();
           
                base.Equip(user);

               
                armory = user.GetComponent<Armory>();
                armory.CurrentWeapon = armory.bow;
                armory.CurrentWeaponBehavior.OnHit += OnTriggerEnter;
            
        }

        public override void Unequip()
        {
            base.Unequip();

            armory.CurrentWeaponBehavior.OnHit -= OnTriggerEnter;
        }

        protected override void OnInputPerformed(InputAction.CallbackContext obj)
        {
            //armory.EquipWeapon(armory.CurrentWeapon);
                     base.ChargeOnInputPerformed(obj);
                     //Debug.Log("CHARGING!!");
               
                    charging = true;
                    animator.SetBool("aim",true);
                    animator.CrossFade("aim", 0, 0);
                    chargeStartTime = Time.time;
                
            
        }
        

        protected override void OnInputCanceled(InputAction.CallbackContext obj)
        {
            
            
            if(armory.CurrentWeapon== armory.bow)
            {
                armory.leftHand.gameObject.transform.GetChild(0).GetComponent<WeaponBehavior>().Fire();
            }
               
                
                animator.SetBool("aim", false);
                charging = false;
             
                chargeEndTime = Time.time;
           
            base.ChargeInputCanceled(obj);


        }

        private void OnTriggerEnter(Collider other)
        {

        


        }
        
    


        //Maybe a bit too simple but we see what state the animator is currently in
        private bool CanAttack()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);

            var match = false;

            foreach (var stateName in data.validStates)
            {
                if (state.IsName(stateName))
                {
                    match = true;
                    break;
                }
            }

            return match;
        }

        private bool IsAttacking()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Charge_Attack");
        }
    }
}
