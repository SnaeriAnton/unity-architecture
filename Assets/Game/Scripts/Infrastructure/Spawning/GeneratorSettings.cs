using System;
using System.Collections.Generic;
using Runtime;

namespace Infrastructure
{
    public readonly struct GeneratorSettings
    {
        private readonly List<GeneratorInfoStage> _stages;

        public readonly Coin CoinTemplate;
        public readonly Crystal CrystalTemplate;
        public readonly float CoinsChanceOnSpawn;
        public readonly float CrystalsChanceOnSpawn;
        public readonly float RadiusPlayer;

        public IReadOnlyList<GeneratorInfoStage> Stages => _stages;
        
        public GeneratorSettings(IReadOnlyList<GeneratorInfoStage> stages, Coin coinTemplate, Crystal crystalTemplate, float coinsChanceOnSpawn, float crystalsChanceOnSpawn, float radiusPlayer)
        {
            _stages = new(stages);
            CoinTemplate = coinTemplate;
            CrystalTemplate = crystalTemplate;
            CoinsChanceOnSpawn = coinsChanceOnSpawn;
            CrystalsChanceOnSpawn = crystalsChanceOnSpawn;
            RadiusPlayer = radiusPlayer;
        }
    }

    [Serializable]
    public readonly struct GeneratorInfoStage
    {
        public readonly List<EnemyBase> Enemies;
        public readonly float SpawnInterval;
        
        public GeneratorInfoStage(IReadOnlyList<EnemyBase> enemies, float spawnInterval)
        {
            Enemies = new(enemies);
            SpawnInterval = spawnInterval;
        }
    }
}