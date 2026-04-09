using System;
using System.Collections.Generic;
using ExtensionSystems;

namespace Core.UI
{
    public class UIService : IUIService
    {
        private readonly Dictionary<Type, Screen> _screenCache = new();
        private readonly Dictionary<Type, Window> _windowCache = new();

        private Screen _currentScreen;
        
        public void ShowScreen<TScreen>() where TScreen : Screen
        {
            _currentScreen?.Hide();
            _currentScreen = GetScreen<TScreen>();
            _currentScreen.Show();
        }

        public void Register<TScreen>(TScreen screen) where TScreen : Screen
        {
            Type type = screen.GetType();

            if (screen is Window window)
                _windowCache[type] = window;
            else
                _screenCache[type] = screen;
        }

        public void ShowWindow<TWindow>() where TWindow : Window
        {
            Window window = _windowCache[typeof(TWindow)];
            window.Show();
        }

        public void ResetScreens() => _screenCache.ForEach(s => s.Value.Reset());

        public void Dispose()
        {
            _screenCache.Clear();
            _windowCache.Clear();
            _currentScreen = null;
        }

        public Screen GetScreen<TScreen>() where TScreen : Screen
        {
            if (_screenCache.TryGetValue(typeof(TScreen), out Screen screen))
                return screen;
            if (_windowCache.TryGetValue(typeof(TScreen), out Window window))
                return window;

            throw new Exception("Invalid screen type");
        }
    }
}