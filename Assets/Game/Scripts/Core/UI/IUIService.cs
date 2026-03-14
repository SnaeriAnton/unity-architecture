namespace Core.UI
{
    public interface IUIService
    {
        public void ShowScreen<TScreen>() where TScreen : Screen;
        public void ShowWindow<TWindow>() where TWindow : Window;
        public void ResetScreens();
    }
}