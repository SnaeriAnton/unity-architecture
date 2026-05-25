namespace Game
{
    public class PlayerHealthView
    {
        private readonly EcsWorld _world;
        private readonly Entity _playerEntity;

        public PlayerHealthView(EcsWorld world, Entity playerEntity)
        {
            _world = world;
            _playerEntity = playerEntity;
        }

        public int CurrentHealth => _world.Get<Health>(_playerEntity).Current;
        public int MaxHealth => _world.Get<Health>(_playerEntity).Max;
    }
}