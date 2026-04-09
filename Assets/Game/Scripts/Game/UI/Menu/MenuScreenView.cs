using UnityEngine;
using UnityEngine.UI;
using R3;

namespace Game
{
    public class MenuScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;

        private CompositeDisposable _disposable = new();
        private MenuScreenViewModel _viewModel;
        
        public void Bind(MenuScreenViewModel viewModel)
        {
            _viewModel = viewModel;
            _disposable = new();
            _startGameButton.OnClickAsObservable().Subscribe(_ => _viewModel?.StartGameCommand.Execute(Unit.Default)).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable.Dispose();
            _viewModel = null;
        }
    }
}
