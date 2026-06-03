using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class PlayerDamageSystem : IEcsRunSystem
    {
         public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<DamageRequest>().End();

            EcsPool<DamageRequest> requestPool = world.GetPool<DamageRequest>();
            EcsPool<PlayerTag> playerPool = world.GetPool<PlayerTag>();
            EcsPool<DeadTag> deadPool = world.GetPool<DeadTag>();
            EcsPool<Health> healthPool = world.GetPool<Health>();
            EcsPool<Invulnerability> invulnerabilityPool = world.GetPool<Invulnerability>();
            EcsPool<HudRefreshRequest> hudPool = world.GetPool<HudRefreshRequest>();

            foreach (int requestEntity in filter)
            {
                ref DamageRequest request = ref requestPool.Get(requestEntity);
                int target = request.Target;

                if (!playerPool.Has(target))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                if (deadPool.Has(target))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                if (!healthPool.Has(target) || !invulnerabilityPool.Has(target))
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                ref Invulnerability invulnerability = ref invulnerabilityPool.Get(target);

                if (invulnerability.TimeLeft > 0f)
                {
                    world.DelEntity(requestEntity);
                    continue;
                }

                ref Health health = ref healthPool.Get(target);

                health.Current -= (int)request.Amount;
                health.Current = Mathf.Clamp(health.Current, 0, health.Max);

                invulnerability.TimeLeft = invulnerability.Duration;

                int hudRequest = world.NewEntity();
                hudPool.Add(hudRequest);

                if (health.Current <= 0 && !deadPool.Has(target))
                    deadPool.Add(target);

                world.DelEntity(requestEntity);
            }
        }
    }
}