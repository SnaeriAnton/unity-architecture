using Game;
using VContainer.Unity;

namespace Core.UI
{
    public class UIRegistry : IInitializable
    {
        private readonly UIService _uiService;
        private readonly HUDView _hudView;
        private readonly MenuScreenView _menuScreenView;
        private readonly LoseScreenView _loseScreenView;
        private readonly UpgradeWindowView _upgradeWindowView;

        private readonly HUDPresenter _hudPresenter;
        private readonly MenuScreenPresenter _menuScreenPresenter;
        private readonly LoseScreenPresenter _loseScreenPresenter;
        private readonly UpgradeWindowPresenter _upgradeWindowPresenter;

        public UIRegistry(
            UIService uiService,
            HUDView hudView,
            MenuScreenView menuScreenView,
            LoseScreenView loseScreenView,
            UpgradeWindowView upgradeWindowView,
            HUDPresenter hudPresenter,
            MenuScreenPresenter menuScreenPresenter,
            LoseScreenPresenter loseScreenPresenter,
            UpgradeWindowPresenter upgradeWindowPresenter)
        {
            _uiService = uiService;
            _hudView = hudView;
            _menuScreenView = menuScreenView;
            _loseScreenView = loseScreenView;
            _upgradeWindowView = upgradeWindowView;
            _hudPresenter = hudPresenter;
            _menuScreenPresenter = menuScreenPresenter;
            _loseScreenPresenter = loseScreenPresenter;
            _upgradeWindowPresenter = upgradeWindowPresenter;
        }

        public void Initialize()
        {
            _uiService.Register(_hudView, _hudPresenter);
            _uiService.Register(_menuScreenView, _menuScreenPresenter);
            _uiService.Register(_loseScreenView, _loseScreenPresenter);
            _uiService.Register(_upgradeWindowView, _upgradeWindowPresenter);
        }
    }
}