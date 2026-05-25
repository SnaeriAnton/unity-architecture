using UnityEngine;
using Core.UI;

namespace Game
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private MenuScreen _menuScreen;
        
        public void Construct(ProgressionSystem progression, UpgradeSystem upgradeStates, Wallet wallet, GameManager gameManager, PlayerHealthView playerHealthView, PlayerShieldView playerShieldView)
        {
            _loseScreen.Construct(gameManager);
            _menuScreen.Construct(gameManager);
            _upgradeWindow.Construct(progression, upgradeStates, wallet);
            _hud.Construct(playerHealthView, playerShieldView, wallet, progression);
            UIManager.Register(_hud);
            UIManager.Register(_menuScreen);
            UIManager.Register(_loseScreen);
            UIManager.Register(_upgradeWindow);
        }
    }
}