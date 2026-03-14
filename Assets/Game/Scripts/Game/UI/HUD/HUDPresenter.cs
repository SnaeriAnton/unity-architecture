using Core.UI;
using UnityEngine;

namespace Game
{
    public class HUDPresenter : ScreenPresenter
    {
        private readonly HUDView _view;
        private readonly IProgressionReadModel _progressionModel;
        private readonly IWallet _wallet;
        private readonly IPlayerReadModel _player;
        private readonly IUpgrade _upgrade;
        private readonly IShieldReadModel _shield;

        public HUDPresenter(HUDView view, IProgressionReadModel progressionModel, IWallet wallet, IPlayerReadModel player, IShieldReadModel shield, IUpgrade upgrade)
        {
            _view = view;
            _progressionModel = progressionModel;
            _wallet = wallet;
            _player = player;
            _shield = shield;
            _upgrade = upgrade;
        }

        public void Initialize()
        {
            _wallet.OnChanged += OnWalletChanged;
            _player.OnHealthChanged += OnHealthChanged;
            _progressionModel.OnProgressChanged += OnProgressChanged;
            _upgrade.OnUpgraded += OnUpgrade;
            _upgrade.OnUpgraded += RenderShieldState;
            _shield.OnShieldStateChanged += RenderShieldState;

            Reset();
        }

        public override void Show()
        {
            base.Show();
            _view.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _view.Hide();
        }

        public override void Dispose()
        {
            _wallet.OnChanged -= OnWalletChanged;
            _player.OnHealthChanged -= OnHealthChanged;
            _progressionModel.OnProgressChanged -= OnProgressChanged;
            _upgrade.OnUpgraded -= OnUpgrade;
            _upgrade.OnUpgraded -= RenderShieldState;
        }

        private void OnWalletChanged() => _view.RenderCoin(_wallet.Coins);
        private void OnHealthChanged() => _view.RenderHealthValues(_player.CurrentHealth);
        private void OnProgressChanged() => _view.RenderProgress(_progressionModel.CurrentExperience, _progressionModel.MaxUpgrade);
        
        public override void Reset()
        {
            _view.Reset();
            _view.RenderHealth(_player.MaxHealth);
            _view.RenderHealthValues(_player.CurrentHealth);
            _view.RenderCoin(_wallet.Coins);
            _view.RenderProgress(_progressionModel.CurrentExperience, _progressionModel.MaxUpgrade);
            RenderShieldState();
        }

        private void OnUpgrade()
        {
            _view.RenderCoin(_wallet.Coins);
            _view.RenderHealth(_player.MaxHealth);
            _view.RenderHealthValues(_player.CurrentHealth);
            RenderShieldState();
        }

        private void RenderShieldState()
        {
            if (_shield.HasShield)
                _view.RenderShieldValues(_shield.CurrentCoolDownCount, _shield.CoolDown);
        }
    }
}