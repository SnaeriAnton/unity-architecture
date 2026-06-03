using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class EnemyDamageSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyDamageRequest>().End();

            EcsPool<EnemyDamageRequest> requestPool = world.GetPool<EnemyDamageRequest>();
            EcsPool<EnemyTag> enemyPool = world.GetPool<EnemyTag>();
            EcsPool<EnemyHealth> healthPool = world.GetPool<EnemyHealth>();
            EcsPool<EnemyDeadTag> deadPool = world.GetPool<EnemyDeadTag>();

            foreach (int requestEntity in filter)
            {
                ref EnemyDamageRequest request = ref requestPool.Get(requestEntity);

                int enemy = request.Target;

                if (!enemyPool.Has(enemy))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                if (!healthPool.Has(enemy))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                if (deadPool.Has(enemy))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                ref EnemyHealth health = ref healthPool.Get(enemy);

                health.Current -= request.Amount;
                health.Current = Mathf.Clamp(health.Current, 0, health.Max);

                if (health.Current <= 0f && !deadPool.Has(enemy))
                    deadPool.Add(enemy);

                world.DelEntity(requestEntity);
            }
        }
    }
}