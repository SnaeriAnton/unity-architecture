using System;
using R3;

namespace Game
{
    public class MenuScreenViewModel
    {
        private readonly CompositeDisposable _disposable = new();

        public ReactiveCommand StartGameCommand { get; } = new();

        public MenuScreenViewModel(Action startGameAction) => StartGameCommand.Subscribe(_ => startGameAction()).AddTo(_disposable);
        
        public void Dispose() => _disposable.Dispose();
    }
}