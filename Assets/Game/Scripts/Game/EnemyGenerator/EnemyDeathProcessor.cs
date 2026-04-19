using UnityEngine;
using Contracts;
using Core.Pool;

namespace Game
{
    public class EnemyDeathProcessor : IEnemyDeathProcessor
    {
        private readonly IPoolService _pool;
        private readonly IWeaponSystem _weapon;
        private readonly IRuntimeTickRegistry _runtimeTickRegistry;
        private readonly EnemyDropConfig _data;
        
        public EnemyDeathProcessor(IPoolService pool, EnemyDropConfig data, IWeaponSystem weapon, IRuntimeTickRegistry runtimeTickRegistry)
        {
            _pool = pool;
            _data = data;
            _weapon = weapon;
            _runtimeTickRegistry = runtimeTickRegistry;
        }
        
        public void Handle(EnemyView view, IRuntimeTickable runtimeTickable)
        {
            _weapon.RefreshWeapons();
           _runtimeTickRegistry.Remove(runtimeTickable);
            _pool.Despawn(view);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _pool.Spawn(_data.CoinTemplate, view.transform.position, Quaternion.identity);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _pool.Spawn(_data.CrystalTemplate, view.transform.position, Quaternion.identity);
        }
    }
}