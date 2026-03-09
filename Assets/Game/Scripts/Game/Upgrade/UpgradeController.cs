using System;
using System.Collections.Generic;
using ExtensionSystems;

namespace Game
{
    public class UpgradeController : IUpgrade
    {
        private readonly GameConfig _gameConfig;
        private readonly Factory _factory;
        private readonly PlayerModel _player;
        private readonly Wallet _wallet;
        private readonly UpgradeModel _model;
        
        public UpgradeController(IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData, PlayerLevelUpsData playerLevelUpsData, Factory factory, PlayerModel player, GameConfig gameConfig, Wallet wallet)
        {
            _factory = factory;
            _gameConfig = gameConfig;
            _player = player;
            _wallet = wallet;

            _model = new(weaponLevelUpsData, playerLevelUpsData);
        }
        
        public event Action OnUpgraded;
        
        public IUpgradeReadModel Model => _model;
        public bool IsMaxUpgrades => _model.IsMaxUpgrades;

        public void Init()
        {
            _model.Reset();
            Upgrade(Weapons.Player);
            _gameConfig.StartWeapons.ForEach(w => Upgrade(w));
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

        private void Upgrade(Weapons name)
        {
            _model.LevelUp(name);
            
            if (name == Weapons.Player)
                _player.SetPlayerStats(_model.PlayerLevelUpInfo.Stats);
            else
            {
                if (!_player.Weapons.ContainsKey(name))
                {
                    WeaponView weaponView = _factory.CreateWeapon(_model.GetWeaponTemplate(name));
                    
                    WeaponController controller = null;

                    switch (name)
                    {
                        case Weapons.Spear:
                            SpearsModel spearModel = new();
                            controller = new SpearsController((SpearsView)weaponView, spearModel);
                            weaponView.Init(spearModel);
                            break;

                        case Weapons.Sword:
                            SwordsModel swordModel = new();
                            controller = new SwordsController((SwordsView)weaponView, swordModel);
                            weaponView.Init(swordModel);
                            break;
                        
                        case Weapons.Shield:
                            ShieldModel shieldModel = new();
                            controller = new ShieldController((ShieldView)weaponView, shieldModel);
                            weaponView.Init(shieldModel);
                            break;
                        
                        case Weapons.Bow:
                            BowModel bowModel = new();
                            controller = new BowController((BowView)weaponView, bowModel);
                            weaponView.Init(bowModel);
                            break;
                    }
                    
                    _player.AddWeapon(name, controller);
                }

                _player.SetWeaponStats(name, _model.WeaponLevelUpsData[name].Stats);
            }

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