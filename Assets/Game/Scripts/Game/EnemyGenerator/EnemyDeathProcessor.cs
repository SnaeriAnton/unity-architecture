using UnityEngine;
using Contracts;
using Core.Pool;

namespace Game
{
    public class EnemyDeathProcessor : IEnemyDeathProcessor
    {
        private readonly IPoolService _pool;
        private readonly IWeaponSystem _weapon;
        private readonly IGameLoop _gameLoop;
        private readonly EnemyDropConfig _data;
        
        public EnemyDeathProcessor(IPoolService pool, EnemyDropConfig data, IWeaponSystem weapon, IGameLoop gameLoop)
        {
            _pool = pool;
            _data = data;
            _weapon = weapon;
            _gameLoop = gameLoop;
        }
        
        public void Handle(EnemyView view, ITickable tickable)
        {
            _weapon.RefreshWeapons();
           _gameLoop.Remove(tickable);
            _pool.Despawn(view);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, view.transform.position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, view.transform.position, Quaternion.identity);
        }
    }
}