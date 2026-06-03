using Leopotam.EcsLite;

namespace Game
{
    public class PlayerShieldView
    {
        private readonly PlayerApi _playerApi;
        private readonly EcsWorld _world;

        public PlayerShieldView(PlayerApi playerApi, EcsWorld world)
        {
            _playerApi = playerApi;
            _world = world;
        }

        public bool IsActive
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return false;

                EcsPool<PlayerShield> shieldPool = _world.GetPool<PlayerShield>();

                if (!shieldPool.Has(_playerApi.PlayerEntity))
                    return false;

                return shieldPool.Get(_playerApi.PlayerEntity).IsActive;
            }
        }

        public int Current
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return 0;

                EcsPool<PlayerShield> shieldPool = _world.GetPool<PlayerShield>();

                if (!shieldPool.Has(_playerApi.PlayerEntity))
                    return 0;

                return shieldPool.Get(_playerApi.PlayerEntity).Current;
            }
        }

        public int Cooldown
        {
            get
            {
                if (!_playerApi.HasPlayer)
                    return 0;

                EcsPool<PlayerShield> shieldPool = _world.GetPool<PlayerShield>();

                if (!shieldPool.Has(_playerApi.PlayerEntity))
                    return 0;

                return shieldPool.Get(_playerApi.PlayerEntity).Cooldown;
            }
        }

        public bool IsReady => IsActive && Cooldown > 0 && Current >= Cooldown;
    }
}