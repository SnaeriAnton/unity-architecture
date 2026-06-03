using UnityEngine;
using Leopotam.EcsLite;

namespace Game
{
    public class PlayerApi
    {
        private readonly EcsWorld _world;
        private PlayerInputSystem _inputSystem;
        private int _playerEntity = -1;

        public int PlayerEntity => _playerEntity;
        public bool HasPlayer => _playerEntity >= 0;

        public PlayerApi(EcsWorld world) => _world = world;

        public void SetInputSystem(PlayerInputSystem inputSystem) => _inputSystem = inputSystem;
        
        public void RegisterPlayer(Transform playerTransform, Border border, float speed, float iFramesDuration)
        {
            if (_playerEntity >= 0)
                return;

            _playerEntity = _world.NewEntity();

            _world.GetPool<PlayerTag>().Add(_playerEntity);

            ref Position position = ref _world.GetPool<Position>().Add(_playerEntity);
            position.Value = playerTransform.position;

            ref MoveInput moveInput = ref _world.GetPool<MoveInput>().Add(_playerEntity);
            moveInput.Value = Vector2.zero;

            ref PlayerShield shield = ref _world.GetPool<PlayerShield>().Add(_playerEntity);
            shield.IsActive = false;
            shield.Current = 0;
            shield.Cooldown = 0;

            ref MoveSpeed moveSpeed = ref _world.GetPool<MoveSpeed>().Add(_playerEntity);
            moveSpeed.Value = speed;

            ref MovementBounds bounds = ref _world.GetPool<MovementBounds>().Add(_playerEntity);
            bounds.Size = border.Size;
            bounds.PlayerScale = playerTransform.localScale;

            ref PlayerViewRef viewRef = ref _world.GetPool<PlayerViewRef>().Add(_playerEntity);
            viewRef.Transform = playerTransform;

            ref Health health = ref _world.GetPool<Health>().Add(_playerEntity);
            health.Current = 0;
            health.Max = 0;

            ref Invulnerability invulnerability = ref _world.GetPool<Invulnerability>().Add(_playerEntity);
            invulnerability.TimeLeft = 0f;
            invulnerability.Duration = iFramesDuration;
        }

        public bool HasPlayingTag()
        {
            if (_playerEntity < 0)
                return false;

            return _world.GetPool<PlayingTag>().Has(_playerEntity);
        }

        public bool TryGetPlayerPosition(out Vector2 position)
        {
            position = default;

            if (_playerEntity < 0)
                return false;

            EcsPool<Position> positionPool = _world.GetPool<Position>();

            if (!positionPool.Has(_playerEntity))
                return false;

            position = positionPool.Get(_playerEntity).Value;
            return true;
        }
        
        public void RequestPlayerDamage(float damage)
        {
            if (_playerEntity < 0)
                return;

            int request = _world.NewEntity();

            ref DamageRequest damageRequest = ref _world
                .GetPool<DamageRequest>()
                .Add(request);

            damageRequest.Target = _playerEntity;
            damageRequest.Amount = damage;
        }
        
        public void RequestSetShield(bool isActive, int cooldown)
        {
            int request = _world.NewEntity();

            ref PlayerShieldSetRequest data = ref _world
                .GetPool<PlayerShieldSetRequest>()
                .Add(request);

            data.IsActive = isActive;
            data.Cooldown = cooldown;
        }
        
        public void Start()
        {
            if (_playerEntity < 0) return;

            _inputSystem?.Enable();
            EcsPool<PlayingTag> playingPool = _world.GetPool<PlayingTag>();

            if (!playingPool.Has(_playerEntity))
                playingPool.Add(_playerEntity);
        }

        public void Stop()
        {
            if (_playerEntity < 0) return;
            
            _inputSystem?.Disable();
            EcsPool<PlayingTag> playingPool = _world.GetPool<PlayingTag>();

            if (playingPool.Has(_playerEntity))
                playingPool.Del(_playerEntity);
        }

        public void SetHealth(SpartanStats stats)
        {
            if (_playerEntity < 0)
                return;

            EcsPool<Health> healthPool = _world.GetPool<Health>();

            if (!healthPool.Has(_playerEntity))
                return;

            ref Health health = ref healthPool.Get(_playerEntity);
            health.Current = stats.Health;
            health.Max = stats.Health;
        }

        public void SetPosition(Vector2 position)
        {
            if (_playerEntity < 0)
                return;

            EcsPool<Position> positionPool = _world.GetPool<Position>();

            if (!positionPool.Has(_playerEntity))
                return;

            ref Position playerPosition = ref positionPool.Get(_playerEntity);
            playerPosition.Value = position;
        }

        public void ResetInvulnerability()
        {
            if (_playerEntity < 0)
                return;

            EcsPool<Invulnerability> pool = _world.GetPool<Invulnerability>();

            if (!pool.Has(_playerEntity))
                return;

            ref Invulnerability invulnerability = ref pool.Get(_playerEntity);
            invulnerability.TimeLeft = 0f;
        }

        public void Revive()
        {
            if (_playerEntity < 0)
                return;

            EcsPool<DeadTag> deadPool = _world.GetPool<DeadTag>();
            EcsPool<DeathHandledTag> handledPool = _world.GetPool<DeathHandledTag>();

            if (deadPool.Has(_playerEntity))
                deadPool.Del(_playerEntity);

            if (handledPool.Has(_playerEntity))
                handledPool.Del(_playerEntity);
        }
    }
}