using System;
using UniRx;

namespace Game
{
    public class LoseScreenViewModel
    {
        private readonly CompositeDisposable _disposable = new();
        public ReactiveCommand RestartGameCommand { get; } = new();

        public LoseScreenViewModel(Action restartGameAction) => RestartGameCommand.Subscribe(_ => restartGameAction()).AddTo(_disposable);

        public void Dispose() => _disposable.Dispose();
    }
}