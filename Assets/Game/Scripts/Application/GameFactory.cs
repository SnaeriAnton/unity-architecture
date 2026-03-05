using System.Collections.Generic;
using UnityEngine;
using Infrastructure;

namespace Application
{
    public class GameFactory
    {
        private readonly Dictionary<int, Sprite> _upgradeIconDictionary = new();

        public IReadOnlyDictionary<int, Sprite> UpgradeIconDictionary => _upgradeIconDictionary;
        public WalletService WalletService { get; }
        public EnemyDeathHandler EnemyHandler { get; }
        public EnemySpawnerController EnemySpawnerController { get; }
        public ProgressionService Progression { get; }
        public UpgradeSystem UpgradeSystem { get; }
        public GameSessionService Game { get; }

        public GameFactory(
            IReadOnlyList<WeaponLevelUpsData> weaponLevelUpsData,
            PlayerLevelUpsData playerLevelUpsData,
            Factory factory,
            ProgressSettings progressSettings,
            Player player,
            GameConfig gameConfig,
            GameTime gameTime,
            Border border,
            PoolManager poolManager,
            GeneratorData generatorData,
            IInput input,
            int coins,
            int crystals
        )
        {
            (int, Sprite) description = playerLevelUpsData.GetDescription();
            _upgradeIconDictionary[description.Item1] = description.Item2;

            foreach (WeaponLevelUpsData levelUps in weaponLevelUpsData)
            {
                description = levelUps.GetDescription();
                _upgradeIconDictionary[description.Item1] = description.Item2;
            }

            WalletService = new(new(coins, crystals));
            EnemyHandler = new(player, poolManager, generatorData);
            EnemySpawnerController = new(player, generatorData, EnemyHandler, factory, border);
            UpgradeSystem = new(weaponLevelUpsData, playerLevelUpsData, gameConfig, WalletService, factory, player);
            Progression = new(WalletService, progressSettings, gameTime, EnemySpawnerController, UpgradeSystem);
            Game = new(WalletService, input, player, EnemySpawnerController, Progression, poolManager, UpgradeSystem);
        }


        public void Init() => UpgradeSystem.Init();
    }
}