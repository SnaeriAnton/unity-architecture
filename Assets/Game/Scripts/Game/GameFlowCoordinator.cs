using Core.UI;

namespace Game
{
    public class GameFlowCoordinator
    {
        private readonly IUIService _ui;

        public GameFlowCoordinator(IUIService ui) => _ui = ui;

        public void ShowHud() => _ui.ShowScreen<HUDView>();
        public void ShowLose() => _ui.ShowScreen<LoseScreenView>();
        public void ResetUI() => _ui.ResetScreens();

        public void ShowMenu() => _ui.ShowScreen<MenuScreenView>();
    }
}