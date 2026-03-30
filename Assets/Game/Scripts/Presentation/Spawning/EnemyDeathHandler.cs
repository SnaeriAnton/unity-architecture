using System;
using UnityEngine;
using Application;
using Random = UnityEngine.Random;

namespace Presentation
{
    public class EnemyDeathHandler : IEnemyDeathHandler
    {
        private readonly PoolManager _pool;
        private readonly GeneratorSettings _data;
        private readonly IPlayer _player;
        
        public EnemyDeathHandler(IPlayer player, PoolManager pool, GeneratorSettings data)
        {
            _player = player;
            _pool = pool;
            _data = data;
        }

        public event Action OnEnemyDead; 
        
        public void Handle(EnemyBase enemy)
        {
            _player.KillEnemy();
            OnEnemyDead?.Invoke();

            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, enemy.transform.position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, enemy.transform.position, Quaternion.identity);
        }
    }
}