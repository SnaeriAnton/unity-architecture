using Core.UI;

namespace Game
{
    public class GameUiBridge
    {
        public void RefreshHud() => UIManager.GetScreen<HUD>().Refresh();
        public void ShowUpgradeWindow() => UIManager.ShowWindow<UpgradeWindow>();
        public void ShowMenu() => UIManager.ShowScreen<MenuScreen>();
        public void ShowHUD() => UIManager.ShowScreen<HUD>();
        public void ShowLoseScreen() => UIManager.ShowScreen<LoseScreen>();
        public void ResetScreens() => UIManager.ResetScreens();
    }
}