using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemyData> Enemies;
        public float SpawnInterval;
    }
}