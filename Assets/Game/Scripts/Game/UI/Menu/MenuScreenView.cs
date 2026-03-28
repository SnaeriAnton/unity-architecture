using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;

        private MenuScreenViewModel _viewModel;
        
        public void Bind(MenuScreenViewModel viewModel)
        {
            _viewModel = viewModel;
            _startGameButton.onClick.AddListener(OnStartClicked);
        }

        public void Unbind()
        {
            _startGameButton.onClick.RemoveListener(OnStartClicked);
            _viewModel = null;
        }

        private void OnStartClicked() => _viewModel?.StartGameCommand.Execute();
    }
}
