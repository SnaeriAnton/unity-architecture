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
        [SerializeField] private TypeOfCurrency _typeOfCurrency;

        private HUDPresenter _hudPresenter;
        private MenuScreenPresenter _menuScreenPresenter;
        private LoseScreenPresenter _loseScreenPresenter;
        private UpgradeWindowPresenter _upgradeWindowPresenter; 
        
        public void Construct(
            IProgressionReadModel progressionModel,
            IWallet wallet, 
            GameManager gameManager, 
            IPlayerReadModel playerModel, 
            IShieldReadModel shieldModel,
            IUpgradeReadModel upgradeModel, 
            IUpgrade upgrade,
            IProgression progression,
            UIService uiService
            )
        {
            _hudPresenter = new(_hudView, progressionModel, wallet, playerModel, shieldModel, upgrade);
            _menuScreenPresenter = new(_menuScreenView, gameManager);
            _loseScreenPresenter = new(_loseScreenView, gameManager);
            _upgradeWindowPresenter = new(_upgradeWindowView, _typeOfCurrency, progression, upgrade, upgradeModel, wallet, progressionModel, uiService);
            uiService.Register(_hudView, _hudPresenter);
            uiService.Register(_menuScreenView, _menuScreenPresenter);
            uiService.Register(_loseScreenView, _loseScreenPresenter);
            uiService.Register(_upgradeWindowView, _upgradeWindowPresenter);
        }
        
        public void Initialize()
        {
            _hudPresenter.Initialize();
            _menuScreenPresenter.Initialize();
            _loseScreenPresenter.Initialize();
            _upgradeWindowPresenter.Initialize();
        }

        public void Dispose()
        {
            _hudPresenter.Dispose();
            _menuScreenPresenter.Dispose();
            _loseScreenPresenter.Dispose();
            _upgradeWindowPresenter.Dispose();
        }
    }
}