namespace Core.UI
{
    public interface IUIService
    {
        public void Register<TScreen>(TScreen screen) where TScreen : Screen;

        public void ShowScreen<TScreen>() where TScreen : Screen;
        public void ShowWindow<TWindow>() where TWindow : Window;
        public TPopup ShowPopup<TPopup>() where TPopup : Popup;

        public void ClosePopup();
        public void CloseWindow();

        public void ResetScreens();
        public void Dispose();

        public TScreen GetScreen<TScreen>() where TScreen : Screen;
    }
}