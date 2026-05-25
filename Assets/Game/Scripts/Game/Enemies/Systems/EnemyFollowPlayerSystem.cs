using System.Linq;
using UnityEngine;

namespace Game
{
    public class EnemyFollowPlayerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public EnemyFollowPlayerSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run(float deltaTime)
        {
            if (!_world.Filter().With<PlayingTag>().Any()) return;
            
            Vector2 playerPosition = default;
            bool hasPlayer = false;

            foreach (Entity player in _world.Filter().With<PlayerTag>().With<Position>())
            {
                playerPosition = _world.Get<Position>(player).Value;
                hasPlayer = true;
                break;
            }

            if (!hasPlayer) return;
            foreach (Entity enemy in _world.Filter().With<EnemyTag>().With<Position>().With<MoveSpeed>())
            {
                Position position = _world.Get<Position>(enemy);
                MoveSpeed speed = _world.Get<MoveSpeed>(enemy);

                Vector2 direction = playerPosition - position.Value;

                if (direction.sqrMagnitude > 0.001f)
                    direction.Normalize();

                position.Value += direction * speed.Value * deltaTime;

                _world.Set(enemy, position);
            }
        }
    }
}