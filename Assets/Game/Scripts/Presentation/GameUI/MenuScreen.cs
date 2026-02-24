using UnityEngine;
using UnityEngine.UI;
using Application;

namespace Presentation
{
    public class MenuScreen : Screen
    {
        [SerializeField] private Button _startGameButton;
        
        public void Construct(GameSessionService game) => _startGameButton.onClick.AddListener(game.StartGame);
    }
}
