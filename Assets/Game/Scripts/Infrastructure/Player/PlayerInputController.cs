namespace Infrastructure
{
    public class PlayerInputController
    {
        private readonly PlayerMovement _movement;
        private readonly IInput _input;

        public PlayerInputController(PlayerMovement movement, IInput input)
        {
            _input = input;
            _movement = movement;
        }

        public void Enable()  => _input.OnAxis += _movement.OnAxis;
        public void Disable() => _input.OnAxis -= _movement.OnAxis;
    }
}
