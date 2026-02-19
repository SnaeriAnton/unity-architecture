using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoseScreen : Core.UI.Screen
    {
        [SerializeField] private Button _homeButton;

        public void Construct(GameManager gameManager) => _homeButton.onClick.AddListener(gameManager.RestartRun);
    }
}