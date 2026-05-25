using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly Entity _playerEntity;
        private readonly IInput _input;

        private Vector2 _axis;
        private bool _enabled;
        
        public PlayerInputSystem(EcsWorld world, Entity playerEntity, IInput input)
        {
            _world = world;
            _playerEntity = playerEntity;
            _input = input;
        }

        public void Enable()
        {
            if (_enabled) return;

            _enabled = true;
            _input.OnAxis += OnAxis;
        }

        public void Disable()
        {
            if (!_enabled) return;

            _enabled = false;
            _input.OnAxis -= OnAxis;

            _axis = Vector2.zero;
            SetMoveInput(Vector2.zero);
        }

        public void Run(float deltaTime)
        {
            if (!_enabled)
            {
                SetMoveInput(Vector2.zero);
                return;
            }
            
            if (!_world.Has<PlayingTag>(_playerEntity))
            {
                SetMoveInput(Vector2.zero);
                return;
            }

            if (_world.Has<DeadTag>(_playerEntity))
            {
                SetMoveInput(Vector2.zero);
                return;
            }

            SetMoveInput(_axis);
        }

        private void OnAxis(Vector2 axis)
        {
            _axis = axis;
        }

        private void SetMoveInput(Vector2 axis)
        {
            MoveInput moveInput = _world.Get<MoveInput>(_playerEntity);
            moveInput.Value = axis;
            _world.Set(_playerEntity, moveInput);
        }
    }
}