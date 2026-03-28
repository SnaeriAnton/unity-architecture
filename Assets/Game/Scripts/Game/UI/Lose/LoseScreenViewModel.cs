using System;
using Core.MVVM;

namespace Game
{
    public class LoseScreenViewModel : ViewModelBase
    {
        public Command StartGameCommand { get; }

        public LoseScreenViewModel(Action startGameAction) => StartGameCommand = new(startGameAction);
    }
}