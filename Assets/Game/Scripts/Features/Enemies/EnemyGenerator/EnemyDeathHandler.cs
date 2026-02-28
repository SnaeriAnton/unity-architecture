using UnityEngine;
using Shared;

namespace Enemies
{
    public class EnemyDeathHandler
    {
        private readonly IKillTracker _tracker;
        private readonly IObjectPool _pool;
        private readonly ILookFactory _lookFactory;
        private readonly GeneratorData _data;
        
        public EnemyDeathHandler(IKillTracker tracker, IObjectPool pool, ILookFactory lookFactory, GeneratorData data)
        {
            _tracker = tracker;
            _pool = pool;
            _lookFactory = lookFactory;
            _data = data;
        }
        
        public void Handle(EnemyBase enemy)
        {
            _tracker.KillEnemy();

            _pool.Despawn(enemy);
            float chance = Random.Range(0f, 1f);

            if (chance <= _data.CoinsChanceOnSpawn)
                _lookFactory.CreateLoot(CurrencyType.Coin, enemy.transform.position);
            else if (_data.CoinsChanceOnSpawn + _data.CrystalsChanceOnSpawn >= chance)
                _lookFactory.CreateLoot(CurrencyType.Crystal, enemy.transform.position);
        }
    }
}