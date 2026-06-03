using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class PlayerViewSyncSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<PlayerTag>().Inc<Position>().Inc<PlayerViewRef>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<PlayerViewRef> viewPool = world.GetPool<PlayerViewRef>();

            foreach (int player in filter)
            {
                ref Position position = ref positionPool.Get(player);
                ref PlayerViewRef viewRef = ref viewPool.Get(player);

                Vector3 current = viewRef.Transform.position;

                viewRef.Transform.position = new Vector3(
                    position.Value.x,
                    position.Value.y,
                    current.z);
            }
        }
    }
}