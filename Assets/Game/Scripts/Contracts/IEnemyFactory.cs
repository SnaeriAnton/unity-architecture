using System;
using UnityEngine;
using Game;

namespace Contracts
{
    public interface IEnemyFactory
    {
        public EnemyPresenter SpawnEnemy(EnemySpawnEntry enemyEntry, Vector3 pos);
    }
}