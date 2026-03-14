using System;
using System.Collections.Generic;
using ExtensionSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIService : IUIService
    {
        private readonly Dictionary<Type, ScreenPresenter> _screenCache = new();
        private readonly Dictionary<Type, ScreenPresenter> _windowCache = new();

        private ScreenPresenter _currentScreen;
        
        public void ShowScreen<TScreen>() where TScreen : Screen
        {
            _currentScreen?.Hide();
            _currentScreen = GetScreen<TScreen>();
            _currentScreen.Show();
        }

        public void Register<TScreen>(TScreen screen, ScreenPresenter presenter) where TScreen : Screen
        {
            Type type = screen.GetType();

            if (screen is Window)
                _windowCache[type] = presenter;
            else
                _screenCache[type] = presenter;
        }

        public void ShowWindow<TWindow>() where TWindow : Window
        {
            ScreenPresenter window = _windowCache[typeof(TWindow)];
            window.Show();
        }

        public void ResetScreens() => _screenCache.ForEach(s => s.Value.Reset());

        public void Dispose()
        {
            _screenCache.Clear();
            _windowCache.Clear();
            _currentScreen = null;
        }

        public ScreenPresenter GetScreen<TScreen>() where TScreen : Screen
        {
            if (_screenCache.TryGetValue(typeof(TScreen), out ScreenPresenter screen))
                return screen;
            if (_windowCache.TryGetValue(typeof(TScreen), out ScreenPresenter window))
                return window;

            throw new Exception("Invalid screen type");
        }
    }
}