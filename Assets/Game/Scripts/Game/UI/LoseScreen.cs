using Core.GSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoseScreen : Core.UI.Screen
    {
        [SerializeField] private Button _homeButton;
        
        public override void Show()
        {
            _homeButton.onClick.AddListener(G.Main.Resolve<IGameManager>().RestartRun);
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _homeButton.onClick.RemoveAllListeners();
        }
    }
}