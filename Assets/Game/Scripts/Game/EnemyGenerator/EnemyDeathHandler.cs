using UnityEngine;
using Core.Pool;

namespace Game
{
    public class EnemyDeathHandler
    {
        private readonly PoolManager _pool;
        private readonly GeneratorData _data;
        
        public EnemyDeathHandler(PoolManager pool, GeneratorData data)
        {
            _pool = pool;
            _data = data;
        }
        
        public void Handle(EnemyBase enemy) => _pool.Despawn(enemy);
        
        public void DropLoot(Vector2 position)
        {
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, position, Quaternion.identity);
        }
    }
}