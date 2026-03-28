using System;
using Core.MVVM;

namespace Game
{
    public class MenuScreenViewModel : ViewModelBase
    {
        public Command StartGameCommand { get; }

        public MenuScreenViewModel(Action startGameAction) => StartGameCommand = new(startGameAction);
    }
}