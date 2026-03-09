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

        private IProgressionReadModel _progression;
        
        public void Construct(
            IProgressionReadModel progressionModel, 
            IShieldReadModel shield,
            IWallet wallet, 
            GameManager gameManager, 
            IPlayerReadModel playerModel, 
            IUpgradeReadModel upgradeModel, 
            IUpgrade upgrade,
            IProgression progression
            )
        {
            _progression = progressionModel;
            
            _loseScreen.Construct(gameManager);
            _menuScreen.Construct(gameManager);
            _upgradeWindow.Construct(progression, upgrade, upgradeModel, wallet);
            _hud.Construct(playerModel, shield, wallet, _progression, upgrade);
            UIManager.Register(_hud);
            UIManager.Register(_menuScreen);
            UIManager.Register(_loseScreen);
            UIManager.Register(_upgradeWindow);
            
            _progression.OnLevelUp += UIManager.ShowWindow<UpgradeWindow>;
        }

        public void Dispose()
        {
            _progression.OnLevelUp -= UIManager.ShowWindow<UpgradeWindow>;
        }
    }
}