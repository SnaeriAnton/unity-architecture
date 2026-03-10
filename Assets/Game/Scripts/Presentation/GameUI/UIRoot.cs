using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application;
using Infrastructure;

namespace Presentation
{
    public class UIRoot : MonoBehaviour
    {
        private readonly Dictionary<Type, Screen> _screens = new();
        
        [SerializeField] private HUD _hud;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private MenuScreen _menuScreen;

        private Screen _currentScreen;
        private ProgressionService _progression;
        private GameSessionService _game;

        public HUD HUD => _hud;

        public void Construct(
            IReadOnlyDictionary<int, Sprite> currencyOfTypes,
            ProgressionService progression,
            UpgradeSystem upgradeStates,
            WalletService wallet,
            GameSessionService game,
            Player player,
            EnemyDeathHandler handler
        )
        {
            _game = game;
            _progression = progression;
            _loseScreen.Construct(game);
            _menuScreen.Construct(game);
            _upgradeWindow.Construct(currencyOfTypes, progression, upgradeStates, wallet);
            _hud.Construct(player, wallet, progression, handler, upgradeStates);
            
            _screens[typeof(HUD)] = _hud;
            _screens[typeof(MenuScreen)] = _menuScreen;
            _screens[typeof(LoseScreen)] = _loseScreen;

            _game.OnLost += ShowLose;
            _game.OnStartedGame += ShowHud;
            _game.OnRestartGame += ShowMenu;
            _game.OnResetValues += ResetScreens;
            _progression.OnReachedGoal += ShowUpgrade;
        }

        public void Dispose()
        {
            _game.OnLost -= ShowLose;
            _game.OnStartedGame -= ShowHud;
            _game.OnRestartGame -= ShowMenu;
            _game.OnResetValues -= ResetScreens;
            _progression.OnReachedGoal -= ShowUpgrade;

            List<Button> buttons = new();
            _screens.ForEach(s => s.Value.Dispose());
            _screens.ForEach(s => buttons.AddRange(s.Value.transform.GetComponentsInChildren<Button>()));
            buttons.ForEach(b => b.onClick.RemoveAllListeners());
            _screens.Clear();
            _currentScreen = null;
        }
        
        public void ShowUpgrade() => _upgradeWindow.Show();
        public void ShowMenu() => ShowScreen<MenuScreen>();
        public void ShowHud() => ShowScreen<HUD>();
        public void ShowLose() => ShowScreen<LoseScreen>();
        
        public void ResetScreens()
        {
            _hud.Reset();
            _loseScreen.Reset();
            _upgradeWindow.Reset();
            _menuScreen.Reset();
        }
        
        private void ShowScreen<T>() where T : Screen
        {
            _currentScreen?.Hide();
            _currentScreen = _screens[typeof(T)];
            _currentScreen.Show();
        }

    }
}