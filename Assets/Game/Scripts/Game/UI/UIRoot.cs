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
        
        public void Construct(ProgressionSystem progression, UpgradeSystem upgradeStates, Wallet wallet, GameRoot gameRoot, Player player)
        {
            _loseScreen.Construct(gameRoot);
            _menuScreen.Construct(gameRoot);
            _upgradeWindow.Construct(progression, upgradeStates, wallet);
            _hud.Construct(player, wallet, progression);
            UIManager.Register(_hud);
            UIManager.Register(_menuScreen);
            UIManager.Register(_loseScreen);
            UIManager.Register(_upgradeWindow);
        }
    }
}