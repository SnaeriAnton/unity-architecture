using Application;
using Runtime;
using UnityEngine;

namespace Infrastructure
{
    public class EnemyDeathHandler
    {
        private readonly Player _player;
        private readonly PoolManager _pool;
        private readonly GeneratorSettings _data;
        private readonly IHUDRefresher _refresher;
        
        public EnemyDeathHandler(Player player, PoolManager pool, GeneratorSettings data, IHUDRefresher refresher)
        {
            _player = player;
            _pool = pool;
            _data = data;
            _refresher = refresher;
        }
        
        public void Handle(EnemyBase enemy)
        {
            _player.KillEnemy();
            _refresher.Refresh();

            _pool.Despawn(enemy);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, enemy.transform.position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, enemy.transform.position, Quaternion.identity);
        }
    }
}