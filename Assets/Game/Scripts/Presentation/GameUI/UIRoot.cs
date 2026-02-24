using System.Collections.Generic;
using Application;
using UnityEngine;
using Domain;
using Runtime;

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

        public void Construct(IReadOnlyDictionary<Weapons, Sprite> upgradeIconDictionary, ProgressionService progression, UpgradeSystem upgradeStates, Wallet wallet, GameSessionService game, Player player)
        {
            _loseScreen.Construct(game);
            _menuScreen.Construct(game);
            _upgradeWindow.Construct(upgradeIconDictionary, progression, upgradeStates, wallet);
            _hud.Construct(player, wallet, progression);
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