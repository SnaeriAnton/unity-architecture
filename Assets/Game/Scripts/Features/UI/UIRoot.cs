using System.Collections.Generic;
using UnityEngine;
using Shared;
using ExtensionSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        private readonly Dictionary<GameScreen, Screen> _screenCache = new();
        
        [SerializeField] private HUD _hud;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private MenuScreen _menuScreen;

        private Screen _currentScreen;
        
        public void Construct(IUpgrade upgradeStates)
        {
            _loseScreen.Construct();
            _menuScreen.Construct();
            _upgradeWindow.Construct(upgradeStates);
            _hud.Construct();
            _screenCache[GameScreen.Hud] = _hud;
            _screenCache[GameScreen.Menu] = _menuScreen;
            _screenCache[GameScreen.Lose] = _loseScreen;

            GameEvents.Progression.PauseRequested += SetPause;
            GameEvents.Progression.UpgradeMenuRequested += _upgradeWindow.Show;
            GameEvents.GameFlow.ScreenRequested += ShowScreen;
            GameEvents.GameFlow.GameRestarted += Restarted;
        }

        public void Dispose()
        {
            List<Button> buttons = new();
            _screenCache.ForEach(s => s.Value.Dispose());
            _screenCache.ForEach(s => buttons.AddRange(s.Value.transform.GetComponentsInChildren<Button>()));
            buttons.ForEach(b => b.onClick.RemoveAllListeners());
            _screenCache.Clear();
            _currentScreen = null;
            
            GameEvents.Progression.PauseRequested -= SetPause;
            GameEvents.Progression.UpgradeMenuRequested -= _upgradeWindow.Show;
        }

        private void ShowScreen(GameScreen screen)
        {
            _currentScreen?.Hide();
            _currentScreen = _screenCache[screen];
            _currentScreen.Show();
        }

        private void Restarted() => _screenCache.ForEach(s => s.Value.Reset());

        private void SetPause(bool pause) => Time.timeScale = pause ? 0 : 1;
    }
}