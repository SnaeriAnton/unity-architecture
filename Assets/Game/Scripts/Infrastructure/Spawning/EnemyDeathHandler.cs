using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure
{
    public class EnemyDeathHandler
    {
        private readonly Player _player;
        private readonly PoolManager _pool;
        private readonly GeneratorData _generatorData;
        
        public EnemyDeathHandler(Player player, PoolManager pool, GeneratorData generatorData)
        {
            _player = player;
            _pool = pool;
            _generatorData = generatorData;
        }
        
        public event Action OnEnemyDied;
        
        public void Handle(EnemyBase enemy)
        {
            _player.KillEnemy();
            OnEnemyDied?.Invoke();

            _pool.Despawn(enemy);
            float chance = Random.Range(0f, 1f);

            if (chance <= _generatorData.CoinsChanceOnSpawn)
                _pool.Spawn(_generatorData.CoinTemplate, enemy.transform.position, Quaternion.identity);
            else if (_generatorData.CoinsChanceOnSpawn + _generatorData.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_generatorData.CrystalTemplate, enemy.transform.position, Quaternion.identity);
        }
    }
}