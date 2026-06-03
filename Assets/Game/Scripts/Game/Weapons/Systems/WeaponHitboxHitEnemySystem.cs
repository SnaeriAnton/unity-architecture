using Leopotam.EcsLite;

namespace Game
{
    public class WeaponHitboxHitEnemySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<WeaponHitboxHitEnemyRequest>().End();

            EcsPool<WeaponHitboxHitEnemyRequest> requestPool = world.GetPool<WeaponHitboxHitEnemyRequest>();
            EcsPool<WeaponHitboxTag> hitboxPool = world.GetPool<WeaponHitboxTag>();
            EcsPool<Damage> damagePool = world.GetPool<Damage>();
            EcsPool<EnemyDamageRequest> enemyDamagePool = world.GetPool<EnemyDamageRequest>();

            foreach (int requestEntity in filter)
            {
                ref WeaponHitboxHitEnemyRequest request = ref requestPool.Get(requestEntity);

                if (hitboxPool.Has(request.WeaponHitbox) && damagePool.Has(request.WeaponHitbox))
                {
                    ref Damage damage = ref damagePool.Get(request.WeaponHitbox);

                    int damageRequest = world.NewEntity();

                    ref EnemyDamageRequest damageData = ref enemyDamagePool.Add(damageRequest);

                    damageData.Target = request.Enemy;
                    damageData.Amount = damage.Value;
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}