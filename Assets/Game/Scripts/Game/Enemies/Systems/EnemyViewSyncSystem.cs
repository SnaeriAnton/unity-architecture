using System.Linq;
using UnityEngine;

namespace Game
{
    public class EnemyViewSyncSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public EnemyViewSyncSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;
            EcsFilter test = _world.Filter().With<EnemyTag>().With<Position>().With<EnemyViewRef>();

            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<Position>().With<EnemyViewRef>())
            {
                Position position = _world.Get<Position>(enemy);
                EnemyViewRef viewRef = _world.Get<EnemyViewRef>(enemy);

                viewRef.Transform.position = new Vector3(
                    position.Value.x,
                    position.Value.y,
                    viewRef.Transform.position.z);
            }
        }
    }
}