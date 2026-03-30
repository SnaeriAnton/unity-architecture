using System.Collections.Generic;
using Domain;
using Presentation;

namespace Main
{
    public class GeneratorSettingsBuilder
    {
        private readonly GeneratorData _generatorData;

        public GeneratorSettingsBuilder(GeneratorData generatorData)
        {
            _generatorData = generatorData;
            
            List<GeneratorInfoStage> stages = new();
            _generatorData.Stages.ForEach(s => stages.Add(new(s.Enemies, s.SpawnInterval)));
            Settings = new(stages, _generatorData.CoinTemplate, _generatorData.CrystalTemplate, _generatorData.CoinsChanceOnSpawn, _generatorData.CrystalsChanceOnSpawn, _generatorData.RadiusPlayer);
        }

        public GeneratorSettings Settings { get; private set; }
    }
}
