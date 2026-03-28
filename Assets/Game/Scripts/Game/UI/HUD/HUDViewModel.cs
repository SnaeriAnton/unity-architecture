using Core.MVVM;
using UnityEngine;

namespace Game
{
    public class HUDViewModel : ViewModelBase
    {
        private readonly IProgressionReadModel _progressionModel;
        private readonly IWallet _wallet;
        private readonly IPlayerReadModel _player;
        private readonly IUpgrade _upgrade;
        private readonly IShieldReadModel _shield;

        private readonly ObservableProperty<bool> _hasShield;
        private readonly ObservableProperty<int> _coins;
        private readonly ObservableProperty<int> _currentExperience;
        private readonly ObservableProperty<int> _maxUpgrade;
        private readonly ObservableProperty<int> _maxHealth;
        private readonly ObservableProperty<int> _currentHealth;
        private readonly ObservableProperty<float> _currentCoolDownCount;
        private readonly ObservableProperty<float> _coolDown;

        public IReadOnlyObservableProperty<bool> HasShield => _hasShield;
        public IReadOnlyObservableProperty<int> Coins => _coins;
        public IReadOnlyObservableProperty<int> CurrentExperience => _currentExperience;
        public IReadOnlyObservableProperty<int> MaxUpgrade => _maxUpgrade;
        public IReadOnlyObservableProperty<int> MaxHealth => _maxHealth;
        public IReadOnlyObservableProperty<int> CurrentHealth => _currentHealth;
        public IReadOnlyObservableProperty<float> CurrentCoolDownCount => _currentCoolDownCount;
        public IReadOnlyObservableProperty<float> CoolDown => _coolDown;

        public HUDViewModel(IProgressionReadModel progressionModel, IWallet wallet, IPlayerReadModel player, IUpgrade upgrade, IShieldReadModel shield)
        {
            _progressionModel = progressionModel;
            _wallet = wallet;
            _player = player;
            _upgrade = upgrade;
            _shield = shield;

            _hasShield = new ObservableProperty<bool>(_shield.HasShield);
            _coins = new ObservableProperty<int>(_wallet.Coins);
            _currentExperience = new ObservableProperty<int>(_progressionModel.CurrentExperience);
            _maxUpgrade = new ObservableProperty<int>(_progressionModel.MaxUpgrade);
            _maxHealth = new ObservableProperty<int>(_player.MaxHealth);
            _currentHealth = new ObservableProperty<int>(_player.CurrentHealth);
            _currentCoolDownCount = new ObservableProperty<float>(_shield.CurrentCoolDownCount);
            _coolDown = new ObservableProperty<float>(_shield.CoolDown);

            Initialize();
            RefreshAll();
        }

        private void Initialize()
        {
            _wallet.OnChanged += OnWalletChanged;
            _player.OnHealthChanged += OnHealthChanged;
            _progressionModel.OnProgressChanged += OnProgressChanged;
            _upgrade.OnUpgraded += OnUpgrade;
            _upgrade.OnUpgraded += OnShieldStateChanged;
            _shield.OnShieldStateChanged += OnShieldStateChanged;
        }

        public override void Dispose()
        {
            _wallet.OnChanged -= OnWalletChanged;
            _player.OnHealthChanged -= OnHealthChanged;
            _progressionModel.OnProgressChanged -= OnProgressChanged;
            _upgrade.OnUpgraded -= OnUpgrade;
            _upgrade.OnUpgraded -= OnShieldStateChanged;
            _shield.OnShieldStateChanged -= OnShieldStateChanged;

            base.Dispose();
        }

        private void RefreshAll()
        {
            _coins.Set(_wallet.Coins);
            _maxHealth.Set(_player.MaxHealth, true);
            _currentHealth.Set(_player.CurrentHealth);
            _currentExperience.Set(_progressionModel.CurrentExperience);
            _maxUpgrade.Set(_progressionModel.MaxUpgrade);
            RefreshShieldState();
        }

        private void OnWalletChanged() => _coins.Set(_wallet.Coins);
        private void OnShieldStateChanged() => RefreshShieldState();
        private void OnHealthChanged() => _currentHealth.Set(_player.CurrentHealth);

        private void OnProgressChanged()
        {
            _currentExperience.Set(_progressionModel.CurrentExperience);
            _maxUpgrade.Set(_progressionModel.MaxUpgrade);
        }

        private void OnUpgrade()
        {
            _coins.Set(_wallet.Coins);
            _maxHealth.Set(_player.MaxHealth, true);
            _currentHealth.Set(_player.CurrentHealth);
            RefreshShieldState();
        }

        private void RefreshShieldState()
        {
            _hasShield.Set(_shield.HasShield, true);
            _currentCoolDownCount.Set(_shield.CurrentCoolDownCount);
            _coolDown.Set(_shield.CoolDown);
        }
    }
}