using Core.GSystem;
using Core.UI;
using UnityEngine;

namespace Game
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private MenuScreen _menuScreen;

        private void Awake()
        {
            IUIService ui = G.Main.Resolve<IUIService>();
            ui.Register(_hud);
            ui.Register(_menuScreen);
            ui.Register(_loseScreen);
            ui.Register(_upgradeWindow);
        }
    }
}