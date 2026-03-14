using System;
using Contracts;
using ExtensionSystems;

namespace Game
{
    public class UpgradePresenter : IUpgrade
    {
        private readonly GameConfig _gameConfig;
        private readonly IWeaponFactory _factory;
        private readonly IPlayer _player;
        private readonly IWeaponSystem _weapons;
        private readonly ISpentable _wallet;
        private readonly UpgradeModel _model;

        public UpgradePresenter(UpgradeModel model, IWeaponFactory factory, IPlayer player, GameConfig gameConfig, ISpentable wallet, IWeaponSystem weapons)
        {
            _model = model;
            _factory = factory;
            _gameConfig = gameConfig;
            _player = player;
            _wallet = wallet;
            _weapons = weapons;
        }

        public event Action OnUpgraded;

        public void Initialize()
        {
            _model.Reset();
            ApplyStartLoadout();
        }

        public void Reset()
        {
            _model.Reset();
            ApplyStartLoadout();
        }

        private void ApplyStartLoadout()
        {
            UpgradeSilently(Weapons.Player);
            _gameConfig.StartWeapons.ForEach(UpgradeSilently);
        }

        public bool TryUpgrade(Weapons name)
        {
            (CurrencyType, int) info = _model.GetPaymentInfo(name);

            if (TrySpend(info.Item1, info.Item2))
            {
                Upgrade(name);
                return true;
            }

            return false;
        }

        private void UpgradeSilently(Weapons name) => ApplyUpgrade(name);

        private void ApplyUpgrade(Weapons name)
        {
            _model.LevelUp(name);

            if (name == Weapons.Player)
                _player.SetPlayerStats(_model.PlayerLevelUpInfo.Stats);
            else
            {
                if (!_weapons.HasWeapon(name))
                {
                    Weapon weapon = _factory.CreateWeapon(_model.GetWeaponTemplate(name));
                    _weapons.AddWeapon(name, weapon);
                }

                _weapons.SetStats(name, _model.WeaponLevelUpsData[name].Stats);
            }
        }

        private void Upgrade(Weapons name)
        {
            ApplyUpgrade(name);
            OnUpgraded?.Invoke();
        }

        private bool TrySpend(CurrencyType type, int price)
        {
            if (type == CurrencyType.Coin)
                return _wallet.TrySpendCoin(price);

            if (type == CurrencyType.Crystal)
                return _wallet.TrySpendCrystal(price);

            return false;
        }
    }
}