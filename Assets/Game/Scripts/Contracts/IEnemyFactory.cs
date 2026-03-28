using System;
using UnityEngine;
using Game;

namespace Contracts
{
    public interface IEnemyFactory
    {
        public EnemyController SpawnEnemy(EnemySpawnEntry enemyEntry, Vector3 pos);
    }
}