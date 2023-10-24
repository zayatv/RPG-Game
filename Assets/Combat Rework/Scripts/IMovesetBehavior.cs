using CombatSystem.Utilities;
using UnityEngine;

namespace CombatSystem
{
    public interface IMovesetBehavior : IActionListBehavior
    {
        void Equip(GameObject user);
        void Unequip();
        void Tick(float deltaTime);
    }
}