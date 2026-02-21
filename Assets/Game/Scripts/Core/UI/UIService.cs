using System;
using System.Collections.Generic;
using ExtensionSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIService : IUIService
    {
        private readonly Dictionary<Type, Screen> _screenCache = new();
        private readonly Dictionary<Type, Window> _windowCache = new();
        private readonly Dictionary<Type, Popup> _popupCache = new();
        private readonly Stack<Popup> _popupStack = new();
        private readonly Stack<Window> _windowStack = new();

        private Screen _currentScreen;

        public void Register<TScreen>(TScreen screen) where TScreen : Screen
        {
            Type type = screen.GetType();

            if (screen is Window window)
                _windowCache[type] = window;
            else if (screen is Popup popup)
                _popupCache[type] = popup;
            else
                _screenCache[type] = screen;
        }

        public void ShowScreen<TScreen>() where TScreen : Screen
        {
            _currentScreen?.Hide();
            _currentScreen = GetScreen<TScreen>();
            _currentScreen.Show();
        }

        public void ShowWindow<TWindow>() where TWindow : Window
        {
            Window window = _windowCache[typeof(TWindow)];
            window.Show();
            _windowStack.Push(window);
        }

        public TPopup ShowPopup<TPopup>() where TPopup : Popup
        {
            TPopup popup = GetScreen<TPopup>();
            popup.Show();
            _popupStack.Push(popup);
            return popup;
        }

        public void ClosePopup()
        {
            if (_popupStack.TryPop(out Popup popup))
                popup.Hide();
        }

        public void CloseWindow()
        {
            if (_windowStack.TryPop(out Window window))
                window.Hide();
        }

        public void ResetScreens() => _screenCache.ForEach(s => s.Value.Reset());

        public void Dispose()
        {
            List<Button> buttons = new();
            _screenCache.ForEach(s => s.Value.Dispose());
            _windowCache.ForEach(s => s.Value.Dispose());
            _popupCache.ForEach(s => s.Value.Dispose());
            _screenCache.ForEach(s => buttons.AddRange(s.Value.transform.GetComponentsInChildren<Button>()));
            _windowCache.ForEach(s => buttons.AddRange(s.Value.transform.GetComponentsInChildren<Button>()));
            _popupCache.ForEach(s => buttons.AddRange(s.Value.transform.GetComponentsInChildren<Button>()));
            buttons.ForEach(b => b.onClick.RemoveAllListeners());
            _screenCache.Clear();
            _windowCache.Clear();
            _popupCache.Clear();
            _popupStack.Clear();
            _windowStack.Clear();
            _currentScreen = null;
        }

        public TScreen GetScreen<TScreen>() where TScreen : Screen
        {
            if (_screenCache.TryGetValue(typeof(TScreen), out Screen screen))
                return screen as TScreen;
            if (_windowCache.TryGetValue(typeof(TScreen), out Window window))
                return window as TScreen;
            if (_popupCache.TryGetValue(typeof(TScreen), out Popup popup))
                return popup as TScreen;

            throw new Exception("Invalid screen type");
        }
    }
}