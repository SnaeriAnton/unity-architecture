using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuScreen : Screen
    {
        [SerializeField] private Button _startGameButton;
        
        public void Construct() => _startGameButton.onClick.AddListener(GameEvents.GameFlow.RaiseGameStarted);
    }
}
