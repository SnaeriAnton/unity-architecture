using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoseScreenView : Core.UI.Screen
    {
        [SerializeField] private Button _homeButton;

        private LoseScreenViewModel _viewModel;
        
        public void Bind(LoseScreenViewModel viewModel)
        {
            _viewModel = viewModel;
            _homeButton.onClick.AddListener(OnStartClicked);
        }

        public void Unbind()
        {
            _homeButton.onClick.RemoveListener(OnStartClicked);
            _viewModel = null;
        }

        private void OnStartClicked() => _viewModel?.StartGameCommand.Execute();
    }
}