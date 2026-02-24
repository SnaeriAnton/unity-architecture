using UnityEngine;
using UnityEngine.UI;
using Application;

namespace Presentation
{
    public class LoseScreen : Screen
    {
        [SerializeField] private Button _homeButton;

        public void Construct(GameSessionService game) => _homeButton.onClick.AddListener(game.RestartRun);
    }
}