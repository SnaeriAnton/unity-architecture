using Leopotam.EcsLite;
using UnityEngine;

namespace Game
{
    public class EnemyFollowPlayerSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;

        public EnemyFollowPlayerSystem(PlayerApi playerApi) => _playerApi = playerApi;

        public void Run(IEcsSystems systems)
        {
            if (!_playerApi.HasPlayingTag()) return;

            if (!_playerApi.TryGetPlayerPosition(out Vector2 playerPosition)) return;

            EcsWorld world = systems.GetWorld();

            EcsFilter filter = world.Filter<EnemyTag>().Inc<Position>().Inc<MoveSpeed>().Exc<EnemyDeadTag>().End();

            EcsPool<Position> positionPool = world.GetPool<Position>();
            EcsPool<MoveSpeed> speedPool = world.GetPool<MoveSpeed>();

            float deltaTime = Time.deltaTime;

            foreach (int enemy in filter)
            {
                ref Position position = ref positionPool.Get(enemy);
                ref MoveSpeed speed = ref speedPool.Get(enemy);

                Vector2 direction = playerPosition - position.Value;

                if (direction.sqrMagnitude <= 0.001f)
                    continue;

                position.Value += direction.normalized * speed.Value * deltaTime;
            }
        }
    }
}