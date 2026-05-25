using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;

        public PlayerMovementSystem(EcsWorld world) => _world = world;

        public void Run(float deltaTime)
        {
            IEnumerable<Entity> entities = _world.Filter().With<PlayerTag>().With<Position>().With<MoveInput>().With<MoveSpeed>().With<MovementBounds>();

            foreach (Entity entity in entities)
            {
                if (_world.Has<DeadTag>(entity)) continue;

                Position position = _world.Get<Position>(entity);
                MoveInput input = _world.Get<MoveInput>(entity);
                MoveSpeed speed = _world.Get<MoveSpeed>(entity);
                MovementBounds bounds = _world.Get<MovementBounds>(entity);

                Vector2 newPosition = position.Value + input.Value * speed.Value * deltaTime;

                float minX = -bounds.Size.x / 2f + bounds.PlayerScale.x / 2f;
                float maxX = bounds.Size.x / 2f - bounds.PlayerScale.x / 2f;

                float minY = -bounds.Size.y / 2f + bounds.PlayerScale.y / 2f;
                float maxY = bounds.Size.y / 2f - bounds.PlayerScale.y / 2f;

                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                position.Value = newPosition;
                _world.Set(entity, position);
            }
        }
    }
}