using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class AxeLifetimeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<AxeTag>().Inc<Lifetime>().Exc<AxeDespawnRequestedTag>().End();

            EcsPool<Lifetime> lifetimePool = world.GetPool<Lifetime>();
            EcsPool<AxeDespawnRequestedTag> despawnRequestedPool = world.GetPool<AxeDespawnRequestedTag>();

            float deltaTime = Time.deltaTime;

            foreach (int axe in filter)
            {
                ref Lifetime lifetime = ref lifetimePool.Get(axe);

                lifetime.TimeLeft -= deltaTime;

                if (lifetime.TimeLeft > 0f)
                    continue;

                if (!despawnRequestedPool.Has(axe))
                    despawnRequestedPool.Add(axe);
            }
        }
    }
}