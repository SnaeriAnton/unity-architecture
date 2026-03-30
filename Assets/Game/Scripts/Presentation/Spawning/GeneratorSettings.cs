using System.Collections.Generic;

namespace Presentation
{
    public readonly struct GeneratorSettings
    {
        private readonly List<GeneratorInfoStage> _stages;

        public readonly Coin CoinTemplate;
        public readonly Crystal CrystalTemplate;
        public readonly float CoinsChanceOnSpawn;
        public readonly float CrystalsChanceOnSpawn;
        public readonly float RadiusPlayer;
        
        public GeneratorSettings(IReadOnlyList<GeneratorInfoStage> stages, Coin coinTemplate, Crystal crystalTemplate, float coinsChanceOnSpawn, float crystalsChanceOnSpawn, float radiusPlayer)
        {
            _stages = new(stages);
            CoinTemplate = coinTemplate;
            CrystalTemplate = crystalTemplate;
            CoinsChanceOnSpawn = coinsChanceOnSpawn;
            CrystalsChanceOnSpawn = crystalsChanceOnSpawn;
            RadiusPlayer = radiusPlayer;
        }
        
        public IReadOnlyList<GeneratorInfoStage> Stages => _stages;
    }
}