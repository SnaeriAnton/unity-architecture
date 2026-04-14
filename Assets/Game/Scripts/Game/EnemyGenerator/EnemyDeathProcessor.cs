using UnityEngine;
using Contracts;
using Zenject;

namespace Game
{
    public class EnemyDeathProcessor : IEnemyDeathProcessor
    {
        private readonly IWeaponSystem _weapon;
        private readonly TickableManager _tickableManager;
        private readonly EnemyDropConfig _data;
        private readonly Crystal.Pool _crystalPool;
        private readonly Coin.Pool _coinPool;

        public EnemyDeathProcessor(EnemyDropConfig data, IWeaponSystem weapon, TickableManager tickableManager, Crystal.Pool crystalPool, Coin.Pool coinPool)
        {
            _data = data;
            _weapon = weapon;
            _tickableManager = tickableManager;
            _crystalPool = crystalPool;
            _coinPool = coinPool;
        }

        public void Handle(EnemyView view, ITickable tickable)
        {
            _weapon.RefreshWeapons();
            _tickableManager.Remove(tickable);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _coinPool.Spawn().transform.position = view.transform.position;
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _crystalPool.Spawn().transform.position = view.transform.position;
        }
    }
}