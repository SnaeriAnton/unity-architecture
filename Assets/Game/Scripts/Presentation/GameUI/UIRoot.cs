using System.Collections.Generic;
using UnityEngine;
using Application;
using Domain;

namespace Presentation
{
    public class UIRoot : MonoBehaviour, IUIRouter
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private MenuScreen _menuScreen;

        public Screen _currentScreen;

        public HUD HUD => _hud;

        public void Construct(
            IReadOnlyDictionary<Weapons, Sprite> upgradeIconDictionary, 
            IProgressionReadModel progression, 
            IProgressionCommands progressionCommands, 
            IUpgradeReadModel upgradeModel,
            IUpgradeCommands upgradeCommands,
            IWalletReadModel wallet, 
            IGameSessionCommands game, 
            IShieldReadModel shield,
            IPlayerReadModel player,
            IEnemyDeathHandler enemyDeathHandler
            )
        {
            _loseScreen.Construct(game);
            _menuScreen.Construct(game);
            _upgradeWindow.Construct(upgradeIconDictionary, progressionCommands, upgradeModel, upgradeCommands, wallet);
            _hud.Construct(player, wallet, progression, shield, enemyDeathHandler, upgradeModel);
        }

        public void ShowMenu()
        {
            _currentScreen?.Hide();
            _currentScreen = _menuScreen;
            _currentScreen.Show();
        }

        public void ShowHud()
        {
            _currentScreen?.Hide();
            _currentScreen = _hud;
            _currentScreen.Show();
        }

        public void ShowLose()
        {
            _currentScreen?.Hide();
            _currentScreen = _loseScreen;
            _currentScreen.Show();
        }

        public void ShowUpgrade() => _upgradeWindow.Show();

        public void ResetScreens()
        {
            _hud.Reset();
            _loseScreen.Reset();
            _upgradeWindow.Reset();
            _menuScreen.Reset();
        }
    }
}