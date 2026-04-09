using R3;

namespace Game
{
    public class HUDViewModel
    {
        private readonly IProgressionReadModel _progressionModel;
        private readonly IWallet _wallet;
        private readonly IPlayerReadModel _player;
        private readonly IShieldReadModel _shield;

        public ReadOnlyReactiveProperty<bool> HasShield => _shield.HasShield;
        public ReadOnlyReactiveProperty<int> Coins => _wallet.Coins;
        public ReadOnlyReactiveProperty<int> CurrentExperience => _progressionModel.CurrentExperience;
        public ReadOnlyReactiveProperty<int> MaxUpgrade => _progressionModel.MaxUpgrade;
        public ReadOnlyReactiveProperty<int> MaxHealth => _player.MaxHealth;
        public ReadOnlyReactiveProperty<int> CurrentHealth => _player.CurrentHealth;
        public ReadOnlyReactiveProperty<int> CurrentCoolDownCount => _shield.CurrentCoolDownCount;
        public ReadOnlyReactiveProperty<int> CoolDown => _shield.CoolDown;

        public HUDViewModel(IProgressionReadModel progressionModel, IWallet wallet, IPlayerReadModel player, IShieldReadModel shield)
        {
            _progressionModel = progressionModel;
            _wallet = wallet;
            _player = player;
            _shield = shield;
        }
    }
}