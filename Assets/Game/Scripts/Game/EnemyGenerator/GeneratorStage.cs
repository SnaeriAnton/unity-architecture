using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemySpawnEntry> Enemies;
        public float SpawnInterval;
    }
}