using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseScreen : Screen
    {
        [SerializeField] private Button _homeButton;

        public void Construct() => _homeButton.onClick.AddListener(GameEvents.GameFlow.RaiseGameRestart);
    }
}