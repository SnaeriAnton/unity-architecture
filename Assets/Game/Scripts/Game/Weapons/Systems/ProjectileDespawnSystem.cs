using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileDespawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileTag>().Inc<ProjectileViewRef>().Inc<ProjectileDespawnRequestedTag>().End();

            EcsPool<ProjectileViewRef> viewRefPool = world.GetPool<ProjectileViewRef>();

            foreach (int projectile in filter)
            {
                ref ProjectileViewRef viewRef = ref viewRefPool.Get(projectile);

                if (viewRef.Link != null)
                    viewRef.Link.Clear();

                viewRef.Despawn?.Invoke();

                world.DelEntity(projectile);
            }
        }
    }
}