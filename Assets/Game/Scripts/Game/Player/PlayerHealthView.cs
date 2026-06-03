
using Leopotam.EcsLite;

namespace Game
{
    public class PlayerHealthView
    {
        private readonly PlayerApi _playerApi;
        private readonly EcsWorld _world;

        public PlayerHealthView(PlayerApi playerApi, EcsWorld world)
        {
            _playerApi = playerApi;
            _world = world;
        }

        public int CurrentHealth
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return 0;

                EcsPool<Health> healthPool = _world.GetPool<Health>();

                if (!healthPool.Has(_playerApi.PlayerEntity))
                    return 0;

                return healthPool.Get(_playerApi.PlayerEntity).Current;
            }
        }

        public int MaxHealth
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return 0;

                EcsPool<Health> healthPool = _world.GetPool<Health>();

                if (!healthPool.Has(_playerApi.PlayerEntity))
                    return 0;

                return healthPool.Get(_playerApi.PlayerEntity).Max;
            }
        }

        public bool IsDead
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return false;

                return _world.GetPool<DeadTag>().Has(_playerApi.PlayerEntity);
            }
        }
    }
}