using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<PlayerTag>().Inc<Position>().Inc<MoveInput>().Inc<MoveSpeed>().Inc<MovementBounds>().Inc<PlayingTag>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<MoveInput> inputPool = world.GetPool<MoveInput>();
            EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();
            EcsPool<MovementBounds> boundsPool = world.GetPool<MovementBounds>();

            float deltaTime = Time.deltaTime;

            foreach (int player in filter)
            {
                ref Position position = ref positionPool.Get(player);
                ref MoveInput input = ref inputPool.Get(player);
                ref MoveSpeed speed = ref speedPool.Get(player);
                ref MovementBounds bounds = ref boundsPool.Get(player);

                Vector2 nextPosition = position.Value + input.Value * speed.Value * deltaTime;

                float halfPlayerX = bounds.PlayerScale.x * 0.5f;
                float halfPlayerY = bounds.PlayerScale.y * 0.5f;

                nextPosition.x = Mathf.Clamp(
                    nextPosition.x,
                    -bounds.Size.x * 0.5f + halfPlayerX,
                    bounds.Size.x * 0.5f - halfPlayerX);

                nextPosition.y = Mathf.Clamp(
                    nextPosition.y,
                    -bounds.Size.y * 0.5f + halfPlayerY,
                    bounds.Size.y * 0.5f - halfPlayerY);

                position.Value = nextPosition;
            }
        }
    }
}