using RPG.Combat.Utilities;

namespace RPG.Combat
{
    public interface IOnHitBehavior : IActionListBehavior
    {
        void OnHit(HitData hitData);
    }
}
