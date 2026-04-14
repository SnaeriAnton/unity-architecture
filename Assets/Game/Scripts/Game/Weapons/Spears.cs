using UnityEngine;
using Zenject;

namespace Game
{
    public class Spears : Weapon
    {
        private Spear.Pool _pool;
        private float _attackTimer;
        
        [Inject]
        public void Construct(Spear.Pool pool) => _pool = pool;
        
        public override void Tick(float dt)
        {
            if (_stats.Equals(default)) return;
            
            _attackTimer += dt;

            if (_attackTimer >= _stats.AttacksPerSecond)
            {
                _attackTimer = 0;
                
                for (int i = 0; i < _stats.Count; i++)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Spear spear = _pool.Spawn();
                    spear.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, angle));
                    spear.Init(_stats, direction);
                    _tickableManager.Add(spear);
                }
            }
        }
    }
}