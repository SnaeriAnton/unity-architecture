using UnityEngine;
using Contracts;
using Zenject;

namespace Game
{
    public class Arrow : MonoBehaviour, ITickable
    {
        private WeaponStats _stats;
        private Arrow.Pool _pool;
        private Vector3 _direction;
        private TickableManager _tickableManager;
        private float _liveTime;

        [Inject]
        public void Construct(TickableManager tickableManager, Arrow.Pool pool)
        {
            _tickableManager = tickableManager;
            _pool = pool;
        }

        public void Init(WeaponStats stats, Vector3 direction)
        {
            _stats = stats;
            _direction = direction;
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
                Destroy();
            }
        }

        private void Destroy()
        {
            _pool.Despawn(this);
            _tickableManager.Remove(this);
        }

        public class Pool : MonoMemoryPool<Arrow>
        {
        }
    }
}