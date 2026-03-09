using Contracts;
using UnityEngine;

namespace Game
{
    public class PlayerController
    {
        private readonly PlayerInputController _inputController;
        private readonly PlayerMovement _movement;
        private readonly PlayerModel _model;
        
        public PlayerController(Transform playerTransform, PlayerModel model, Border border, IInput input, float speed)
        {
            _model = model;
            _movement = new(playerTransform, border, speed);
            _inputController = new(_movement, _model, input);
        }
        
        public void Dispose() => _inputController.Dispose();
    }
}