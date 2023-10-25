using CombatSystem.Utilities;

namespace CombatSystem
{
    public interface IOnHitBehavior : IActionListBehavior
    {
        void OnHit(HitData hitData);
    }
}
