using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileTag>().Inc<Lifetime>().Exc<ProjectileDespawnRequestedTag>().End();
            
            EcsPool<Lifetime> lifetimePool = world.GetPool<Lifetime>();
            EcsPool<ProjectileDespawnRequestedTag> despawnRequestedPool = world.GetPool<ProjectileDespawnRequestedTag>();
            
            float deltaTime = Time.deltaTime;
            
            foreach (int projectile  in filter)
            {
                ref Lifetime lifetime = ref lifetimePool.Get(projectile);

                lifetime.TimeLeft -= deltaTime;

                if (lifetime.TimeLeft > 0f)
                    continue;

                if (!despawnRequestedPool.Has(projectile))
                    despawnRequestedPool.Add(projectile);
            }
            
        }
    }
}