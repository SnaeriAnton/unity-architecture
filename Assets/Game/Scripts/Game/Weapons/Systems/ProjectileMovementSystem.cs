using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class ProjectileMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<ProjectileTag>().Inc<Position>().Inc<Direction>().Inc<MoveSpeed>().Exc<ProjectileDespawnRequestedTag>().End();

            EcsPool<Position> positions = world.GetPool<Position>();
            EcsPool<Direction> directions = world.GetPool<Direction>();
            EcsPool<MoveSpeed> speeds = world.GetPool<MoveSpeed>();

            float deltaTime = Time.deltaTime;

            foreach (int entity in filter)
            {
                ref Position position = ref positions.Get(entity);
                ref Direction direction = ref directions.Get(entity);
                ref MoveSpeed speed = ref speeds.Get(entity);

                position.Value += direction.Value * speed.Value * deltaTime;
            }
        }
    }
}