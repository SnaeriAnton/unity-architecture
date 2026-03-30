using System;
using System.Collections.Generic;

namespace Presentation
{
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