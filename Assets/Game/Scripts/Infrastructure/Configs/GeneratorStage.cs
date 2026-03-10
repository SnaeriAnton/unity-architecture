using System;
using System.Collections.Generic;

namespace Infrastructure
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemyBase> Enemies;
        public float SpawnInterval;
    }
}