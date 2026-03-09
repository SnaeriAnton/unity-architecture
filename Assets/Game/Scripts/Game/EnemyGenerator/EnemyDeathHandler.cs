using UnityEngine;
using Core.Pool;

namespace Game
{
    public class EnemyDeathHandler
    {
        private readonly PlayerModel _player;
        private readonly PoolManager _pool;
        private readonly GeneratorData _data;
        
        public EnemyDeathHandler(PlayerModel player, PoolManager pool, GeneratorData data)
        {
            _player = player;
            _pool = pool;
            _data = data;
        }
        
        public void Handle(EnemyView enemy)
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