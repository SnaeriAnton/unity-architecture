using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Spear : MonoBehaviour, ITickable
    {
        private WeaponStats _stats;
        private Vector3 _direction;
        private TickableManager _tickableManager;
        private Spear.Pool _pool;
        private float _liveTime;
        private int _currentStrength;

        [Inject]
        public void Construct(TickableManager tickableManager, Spear.Pool pool)
        {
            _tickableManager = tickableManager;
            _pool = pool;
        }
        
        public void Init(WeaponStats stats, Vector3 direction)
        {
            _stats = stats;
            _direction = direction;
            _currentStrength = _stats.Strength;
            _liveTime = _stats.LifeTime;
        }

        public void Tick()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * Time.deltaTime);

            _liveTime -= Time.deltaTime;

            if (_liveTime <= 0) Destroy();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemyTarget enemy))
            {
                enemy.TakeDamage(_stats.Damage);
                _currentStrength--;

                if (_currentStrength == 0) Destroy();
            }
        }

        private void Destroy()
        {
            _tickableManager.Remove(this);
            _pool.Despawn(this);
        }
        
        public class Pool : MonoMemoryPool<Spear>
        {
        }
    }
}