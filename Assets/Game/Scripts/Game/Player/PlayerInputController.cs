
using Contracts;
using Core.GSystem;

namespace Game
{
    public class PlayerInputController
    {
        private readonly PlayerMovement _movement;
        
        private IInput _input;

        public PlayerInputController(PlayerMovement movement) => _movement = movement;
        
        private IInput Input => _input ??= G.Main.Resolve<IInput>();
        
        public void Enable()  => Input.OnAxis += _movement.OnAxis;
        public void Disable() => Input.OnAxis -= _movement.OnAxis;
    }
}
