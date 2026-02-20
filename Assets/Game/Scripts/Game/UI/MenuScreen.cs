using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreen : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;
        
        public void Construct(GameRoot gameRoot) => _startGameButton.onClick.AddListener(gameRoot.StartGame);
    }
}
