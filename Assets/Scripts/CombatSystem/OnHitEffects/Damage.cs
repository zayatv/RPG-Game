namespace CombatSystem.OnHitEffects
{
    public class Damage : OnHitComponent
    {
        public float amount = 5;

        public override IOnHitBehavior GetBehavior()
        {
            return new DamageBehavior(this);
        }
    }

    public class DamageBehavior : OnHitBehavior<Damage>
    {
        public DamageBehavior(Damage data) : base(data)
        {
        }

        public override void OnHit(HitData hitData)
        {
            var health = hitData.Target.GetComponent<Health>();

            if (health != null)
                health.TakeDamage(data.amount);
        }
    }
}
