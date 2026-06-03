using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class ProjectileViewSyncSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            EcsFilter filter = world.Filter<ProjectileTag>().Inc<Position>().Inc<Rotation>().Inc<ProjectileViewRef>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<Rotation> rotationPool = world.GetPool<Rotation>();
            EcsPool<ProjectileViewRef> viewRefPool = world.GetPool<ProjectileViewRef>();
            
            foreach (int projectile in filter)
            {
                ref Position position = ref positionPool.Get(projectile);
                ref Rotation rotation = ref rotationPool.Get(projectile);
                ref ProjectileViewRef viewRef = ref viewRefPool.Get(projectile);

                Vector3 currentPosition = viewRef.Transform.position;

                viewRef.Transform.SetPositionAndRotation(new Vector3(position.Value.x, position.Value.y, currentPosition.z), rotation.Value);
            }
        }
    }
}