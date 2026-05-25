namespace Game
{
    public class PlayerShieldView
    {
        private readonly EcsWorld _world;
        private readonly Entity _playerEntity;

        public PlayerShieldView(EcsWorld world, Entity playerEntity)
        {
            _world = world;
            _playerEntity = playerEntity;
        }

        public bool IsActive => _world.Get<PlayerShield>(_playerEntity).IsActive;
        public int Current => _world.Get<PlayerShield>(_playerEntity).Current;
        public int Cooldown => _world.Get<PlayerShield>(_playerEntity).Cooldown;

        public bool IsReady
        {
            get
            {
                PlayerShield shield = _world.Get<PlayerShield>(_playerEntity);
                return shield.IsActive && shield.Cooldown > 0 && shield.Current >= shield.Cooldown;
            }
        }
    }
}