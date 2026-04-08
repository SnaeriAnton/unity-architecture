using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Game
{
    public class LoseScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _homeButton;

        private CompositeDisposable _disposable = new();
        private LoseScreenViewModel _viewModel;
        
        public void Bind(LoseScreenViewModel viewModel)
        {
            _viewModel = viewModel;
            _disposable = new();
            _homeButton.OnClickAsObservable().Subscribe(_ => _viewModel?.RestartGameCommand.Execute()).AddTo(_disposable);
        }

        public void Unbind()
        {
            _disposable.Dispose();
            _viewModel = null;
        }
    }
}