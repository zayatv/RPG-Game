using UnityEngine;

namespace CombatSystem.Movesets
{
    public class ChargeAttack : MovesetComponent
    {
        public override IMovesetBehavior GetBehavior()
        {
            return new ChargeAttackBehavior(this);
        }
    }

    public class ChargeAttackBehavior : MovesetBehavior<ChargeAttack>
    {
        public ChargeAttackBehavior(ChargeAttack data) : base(data)
        {
        }
    }
}
