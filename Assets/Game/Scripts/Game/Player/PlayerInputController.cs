using R3;
using Contracts;

namespace Game
{
    public class PlayerInputController
    {
        private readonly PlayerMovement _movement;
        private readonly IInput _input;

        private CompositeDisposable _disposable = new();

        public PlayerInputController(PlayerMovement movement, IInput input)
        {
            _input = input;
            _movement = movement;
        }

        public void Enable()
        {
            _disposable = new();
            _input.OnAxis.Subscribe(_movement.OnAxis).AddTo(_disposable);
        }

        public void Disable() => _disposable.Dispose();
    }
}