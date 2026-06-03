using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class AxeViewSyncSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<AxeTag>().Inc<Position>().Inc<Rotation>().Inc<AxeViewRef>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<Rotation> rotationPool = world.GetPool<Rotation>();
            EcsPool<AxeViewRef> viewRefPool = world.GetPool<AxeViewRef>();

            foreach (int axe in filter)
            {
                ref Position position = ref positionPool.Get(axe);
                ref Rotation rotation = ref rotationPool.Get(axe);
                ref AxeViewRef viewRef = ref viewRefPool.Get(axe);

                Vector3 currentPosition = viewRef.Transform.position;

                viewRef.Transform.SetPositionAndRotation(
                    new Vector3(position.Value.x, position.Value.y, currentPosition.z),
                    rotation.Value);
            }
        }
    }
}