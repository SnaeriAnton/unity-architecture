using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerInputController
    {
        private readonly PlayerMovement _movement;
        private readonly PlayerModel _model;
        private readonly IInput _input;

        public PlayerInputController(PlayerMovement movement, PlayerModel model, IInput input)
        {
            _movement = movement;
            _model = model;
            _input = input;
            
            _input.OnAxis += HandleAxisInput;
        }
        
        public void Dispose() => _input.OnAxis -= HandleAxisInput;

        private void HandleAxisInput(Vector2 axis)
        {
            if (_model.IsDead) return;
            _movement.OnAxis(axis);
        }
    }
}