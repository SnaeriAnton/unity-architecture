using Shared;

namespace Player
{
    public class PlayerInputController
    {
        private readonly IInput _input;
        private readonly PlayerMovement _movement;

        public PlayerInputController(PlayerMovement movement, IInput input)
        {
            _input = input;
            _movement = movement;
        }

        public void Enable()  => _input.OnAxis += _movement.OnAxis;
        public void Disable() => _input.OnAxis -= _movement.OnAxis;
    }
}
