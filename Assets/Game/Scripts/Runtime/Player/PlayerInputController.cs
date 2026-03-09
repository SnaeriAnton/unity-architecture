namespace Runtime
{
    public class PlayerInputController
    {
        private readonly PlayerMovement _movement;
        private readonly IRuntimeInput _input;

        public PlayerInputController(PlayerMovement movement, IRuntimeInput input)
        {
            _input = input;
            _movement = movement;
        }

        public void Enable()  => _input.Move += _movement.OnAxis;
        public void Disable() => _input.Move -= _movement.OnAxis;
    }
}
