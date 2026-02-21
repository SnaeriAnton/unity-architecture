using Core.GSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreen : Core.UI.Screen
    {
        [SerializeField] private Button _startGameButton;

        public override void Show()
        {
            _startGameButton.onClick.AddListener(G.Main.Resolve<IGameManager>().StartGame);
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _startGameButton.onClick.RemoveAllListeners();
        }
    }
}
