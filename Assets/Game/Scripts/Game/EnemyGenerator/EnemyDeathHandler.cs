using UnityEngine;
using Core.Pool;

namespace Game
{
    public class EnemyDeathHandler
    {
        private readonly Player _player;
        private readonly PoolManager _pool;
        private readonly GeneratorData _data;
        
        public EnemyDeathHandler(Player player, PoolManager pool, GeneratorData data)
        {
            _player = player;
            _pool = pool;
            _data = data;
        }
        
        public void Handle(EnemyBase enemy)
        {
            _player.KillEnemy();

            _pool.Despawn(enemy);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, enemy.transform.position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, enemy.transform.position, Quaternion.identity);
        }
    }
}