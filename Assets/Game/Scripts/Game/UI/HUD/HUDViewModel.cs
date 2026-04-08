using System;
using UniRx;

namespace Game
{
    public class HUDViewModel
    {
        private readonly IProgressionReadModel _progressionModel;
        private readonly IWallet _wallet;
        private readonly IPlayerReadModel _player;
        private readonly IShieldReadModel _shield;

        public IReadOnlyReactiveProperty<bool> HasShield => _shield.HasShield;
        public IReadOnlyReactiveProperty<int> Coins => _wallet.Coins;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _progressionModel.CurrentExperience;
        public IReadOnlyReactiveProperty<int> MaxUpgrade => _progressionModel.MaxUpgrade;
        public IReadOnlyReactiveProperty<int> MaxHealth => _player.MaxHealth;
        public IReadOnlyReactiveProperty<int> CurrentHealth => _player.CurrentHealth;
        public IReadOnlyReactiveProperty<int> CurrentCoolDownCount => _shield.CurrentCoolDownCount;
        public IReadOnlyReactiveProperty<int> CoolDown => _shield.CoolDown;

        public HUDViewModel(IProgressionReadModel progressionModel, IWallet wallet, IPlayerReadModel player, IShieldReadModel shield)
        {
            _progressionModel = progressionModel;
            _wallet = wallet;
            _player = player;
            _shield = shield;
        }
    }
}