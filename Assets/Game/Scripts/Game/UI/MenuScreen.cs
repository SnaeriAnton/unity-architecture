using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreen : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;
        
        public void Construct(GameManager gameManager) => _startGameButton.onClick.AddListener(gameManager.StartGame);
    }
}
