using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.OnHitEffects
{
    public class LogToConsole : OnHitComponent
    {
        public bool time;
        public bool weapon = true;
        public bool strength = true;

        public override IOnHitBehavior GetBehavior()
        {
            return new LogToConsoleBehavior(this);
        }
    }

    public class LogToConsoleBehavior : OnHitBehavior<LogToConsole>
    {
        public LogToConsoleBehavior(LogToConsole data) : base(data)
        {
        }

        public override void OnHit(HitData hitData)
        {
            var message = hitData.Attacker.name + " attacked " + hitData.Target.name;

            if (data.time)
                message += "\nTime: " + hitData.HitTime;

            if (data.weapon)
                message += "\nWeapon: " + hitData.Weapon.weaponName;

            if (data.strength)
                message += "\nStrength: " + hitData.HitStrength;

            Debug.Log(message);
        }
    }
}
