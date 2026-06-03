using UnityEngine;
using Leopotam.EcsLite;
using Contracts;

namespace Game
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly PlayerApi _playerApi;
        private readonly IInput _input;

        private Vector2 _axis;
        private bool _enabled;

        public PlayerInputSystem(PlayerApi playerApi, IInput input)
        {
            _playerApi = playerApi;
            _input = input;
        }
        
        private void OnAxis(Vector2 axis) => _axis = axis;

        public void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;
            _input.OnAxis += OnAxis;
        }

        public void Disable()
        {
            if (!_enabled)
                return;

            _enabled = false;
            _input.OnAxis -= OnAxis;

            _axis = Vector2.zero;
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            if (!_playerApi.HasPlayer)
                return;

            int player = _playerApi.PlayerEntity;

            EcsPool<MoveInput> moveInputPool = world.GetPool<MoveInput>();
            EcsPool<PlayingTag> playingPool = world.GetPool<PlayingTag>();
            EcsPool<DeadTag> deadPool = world.GetPool<DeadTag>();

            if (!moveInputPool.Has(player))
                return;

            if (!_enabled)
            {
                SetMoveInput(moveInputPool, player, Vector2.zero);
                return;
            }

            if (!playingPool.Has(player))
            {
                SetMoveInput(moveInputPool, player, Vector2.zero);
                return;
            }

            if (deadPool.Has(player))
            {
                SetMoveInput(moveInputPool, player, Vector2.zero);
                return;
            }

            SetMoveInput(moveInputPool, player, _axis);
        }

        private void SetMoveInput(EcsPool<MoveInput> moveInputPool, int player, Vector2 axis)
        {
            ref MoveInput moveInput = ref moveInputPool.Get(player);
            moveInput.Value = axis;
        }
    }
}