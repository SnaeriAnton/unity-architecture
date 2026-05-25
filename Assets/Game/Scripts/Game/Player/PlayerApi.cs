using UnityEngine;

namespace Game
{
    public class PlayerApi
    {
        private readonly EcsWorld _world;
        private readonly Entity _playerEntity;
        private readonly PlayerInputSystem _inputSystem;
        private readonly CommandBuffer _commandBuffer;

        public PlayerApi(EcsWorld world, Entity playerEntity, PlayerInputSystem inputSystem, CommandBuffer commandBuffer)
        {
            _world = world;
            _playerEntity = playerEntity;
            _inputSystem = inputSystem;
            _commandBuffer = commandBuffer;
        }

        public void Start()
        {
            if (!_world.Has<PlayingTag>(_playerEntity))
                _commandBuffer.Add(_playerEntity, new PlayingTag());

            _inputSystem.Enable();
        }

        public void Stop()
        {
            if (_world.Has<PlayingTag>(_playerEntity))
                _commandBuffer.Remove<PlayingTag>(_playerEntity);

            _inputSystem.Disable();
        }

        public void SetHealth(SpartanStats stats)
        {
            Health health = _world.Get<Health>(_playerEntity);

            health.Current = Mathf.Clamp(stats.Health, 0, stats.Health);
            health.Max = stats.Health;

            _world.Set(_playerEntity, health);
        }

        public void SetPosition(Vector2 position)
        {
            Position playerPosition = _world.Get<Position>(_playerEntity);
            playerPosition.Value = position;

            _world.Set(_playerEntity, playerPosition);
        }

        public void ResetInvulnerability()
        {
            Invulnerability invulnerability = _world.Get<Invulnerability>(_playerEntity);
            invulnerability.TimeLeft = 0f;

            _world.Set(_playerEntity, invulnerability);
        }

        public void Revive()
        {
            if (_world.Has<DeadTag>(_playerEntity))
                _commandBuffer.Remove<DeadTag>(_playerEntity);

            if (_world.Has<DeathHandledTag>(_playerEntity))
                _commandBuffer.Remove<DeathHandledTag>(_playerEntity);
        }
    }
}