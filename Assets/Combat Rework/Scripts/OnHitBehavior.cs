namespace CombatSystem
{
    public abstract class OnHitBehavior<T> : IOnHitBehavior where T : OnHitComponent
    {
        protected T data;

        protected OnHitBehavior(T data)
        {
            this.data = data;
        }

        public abstract void OnHit(HitData hitData);
    }
}
