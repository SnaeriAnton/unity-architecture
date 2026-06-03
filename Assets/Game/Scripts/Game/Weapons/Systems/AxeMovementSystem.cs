using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class AxeMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world
                .Filter<AxeTag>()
                .Inc<Position>()
                .Inc<Direction>()
                .Inc<MoveSpeed>()
                .Exc<AxeDespawnRequestedTag>()
                .End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<Direction> directionPool = world.GetPool<Direction>();
            EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();

            float deltaTime = Time.deltaTime;

            foreach (int axe in filter)
            {
                ref Position position = ref positionPool.Get(axe);
                ref Direction direction = ref directionPool.Get(axe);
                ref MoveSpeed speed = ref speedPool.Get(axe);

                position.Value += direction.Value * speed.Value * deltaTime;
            }
        }
    }
}