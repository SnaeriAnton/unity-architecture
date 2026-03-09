using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemyBase> Enemies;
        public float SpawnInterval;
    }
}