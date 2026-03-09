using System;
using System.Collections.Generic;

namespace Enemies
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemyBase> Enemies;
        public float SpawnInterval;
    }
}