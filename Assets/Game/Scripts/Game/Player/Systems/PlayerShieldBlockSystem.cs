using Leopotam.EcsLite;

namespace Game
{
    public class PlayerShieldBlockSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<DamageRequest>().End();

            EcsPool<DamageRequest> damageRequestPool = world.GetPool<DamageRequest>();
            EcsPool<PlayerTag> playerPool = world.GetPool<PlayerTag>();
            EcsPool<DeadTag> deadPool = world.GetPool<DeadTag>();
            EcsPool<PlayerShield> shieldPool = world.GetPool<PlayerShield>();
            EcsPool<HudRefreshRequest> hudPool = world.GetPool<HudRefreshRequest>();

            foreach (int requestEntity in filter)
            {
                ref DamageRequest request = ref damageRequestPool.Get(requestEntity);
                int target = request.Target;

                if (!playerPool.Has(target))
                    continue;

                if (deadPool.Has(target))
                    continue;

                if (!shieldPool.Has(target))
                    continue;

                ref PlayerShield shield = ref shieldPool.Get(target);

                if (!shield.IsActive)
                    continue;

                if (shield.Current < shield.Cooldown)
                    continue;

                shield.Current = 0;

                int hudRequest = world.NewEntity();
                hudPool.Add(hudRequest);

                world.DelEntity(requestEntity);
            }
        }
    }
}