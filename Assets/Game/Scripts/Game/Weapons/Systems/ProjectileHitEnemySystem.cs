using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileHitEnemySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileTag>().Inc<Damage>().Inc<ProjectileHitEnemyTarget>().Exc<ProjectileDespawnRequestedTag>().End();

            EcsPool<Damage> damagePool = world.GetPool<Damage>();
            EcsPool<ProjectileHitEnemyTarget> targetPool = world.GetPool<ProjectileHitEnemyTarget>();
            EcsPool<ProjectileDespawnRequestedTag> despawnPool = world.GetPool<ProjectileDespawnRequestedTag>();
            EcsPool<EnemyDamageRequest> enemyDamagePool = world.GetPool<EnemyDamageRequest>();

            foreach (int projectile in filter)
            {
                ref Damage damage = ref damagePool.Get(projectile);
                ref ProjectileHitEnemyTarget target = ref targetPool.Get(projectile);

                int damageRequest = world.NewEntity();

                ref EnemyDamageRequest request = ref enemyDamagePool.Add(damageRequest);
                request.Target = target.Enemy;
                request.Amount = damage.Value;

                if (!despawnPool.Has(projectile))
                    despawnPool.Add(projectile);
            }
        }
    }
}