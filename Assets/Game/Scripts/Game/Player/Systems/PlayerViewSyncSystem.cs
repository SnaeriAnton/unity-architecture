using UnityEngine;

namespace Game
{
    public class PlayerViewSyncSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public PlayerViewSyncSystem(EcsWorld world) => _world = world;
        
        public void Run(float deltaTime)
        {
            foreach (Entity entity in _world.Filter().With<PlayerTag>().With<Position>().With<PlayerViewRef>())
            {
                Position position = _world.Get<Position>(entity);
                PlayerViewRef viewRef = _world.Get<PlayerViewRef>(entity);

                viewRef.Transform.position = new Vector3(
                    position.Value.x,
                    position.Value.y,
                    viewRef.Transform.position.z);
            }
        }
    }
}