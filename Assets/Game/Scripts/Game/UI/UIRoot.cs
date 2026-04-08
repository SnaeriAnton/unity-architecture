using Core.UI;
using UnityEngine;

namespace Game
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private HUDView _hudView;
        [SerializeField] private LoseScreenView _loseScreenView;
        [SerializeField] private UpgradeWindowView _upgradeWindowView;
        [SerializeField] private MenuScreenView _menuScreenView;

        public void Construct(
            LoseScreenViewModel loseScreenViewModel,
            MenuScreenViewModel menuScreenViewModel,
            UpgradeWindowViewModel upgradeWindowViewModel,
            HUDViewModel hudViewModel,
            UIService uiService
        )
        {
            _menuScreenView.Bind(menuScreenViewModel);
            _loseScreenView.Bind(loseScreenViewModel);
            _hudView.Bind(hudViewModel);
            _upgradeWindowView.Bind(upgradeWindowViewModel);


            uiService.Register(_hudView);
            uiService.Register(_menuScreenView);
            uiService.Register(_loseScreenView);
            uiService.Register(_upgradeWindowView);
        }
        
        public void Dispose()
        {
            _menuScreenView.Unbind();
            _loseScreenView.Unbind();
            _hudView.Unbind();
            _upgradeWindowView.Unbind();
        }
    }
}