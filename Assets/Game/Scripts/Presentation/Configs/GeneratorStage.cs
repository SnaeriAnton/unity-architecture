using System;
using System.Collections.Generic;

namespace Presentation
{
    [Serializable]
    public struct GeneratorStage
    {
        public List<EnemyBase> Enemies;
        public float SpawnInterval;
    }
}