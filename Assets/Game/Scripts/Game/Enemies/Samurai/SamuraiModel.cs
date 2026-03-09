using Contracts;

namespace Game
{
    public class SamuraiModel : EnemyModel
    {
        public SamuraiModel(EnemyStats stats, ITarget target) : base(stats, target)
        {
        }
        
        public override void OnHit()
        {
            Target.TakeDamage(Stats.Stats.Damage);
            Die();
        }
    }
}