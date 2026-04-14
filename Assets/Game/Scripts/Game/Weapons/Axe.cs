using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Axe : MonoBehaviour, ITickable
    {
        private const float ANGULAR_SPEED_DEG = 90;

        private AxeStats _stats;
        private Vector3 _direction;
        private TickableManager _tickableManager;
        private Axe.Pool _pool;
        
        [Inject]
        public void Construct(TickableManager tickableManager, Axe.Pool pool)
        {
            _tickableManager = tickableManager;
            _pool = pool;
        }
        
        public void Init(TickableManager tickableManager, AxeStats stats, Vector3 direction)
        {
            _tickableManager = tickableManager;
            _stats = stats;
            _direction = direction;
        }

        public void Tick()
        {
            transform.Rotate(0f, 0f, ANGULAR_SPEED_DEG * _stats.RotationSpeed * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * Time.deltaTime);

            _stats.LifeTime -= Time.deltaTime;
            if (_stats.LifeTime <= 0) Destroy();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITarget target))
            {
                target.TakeDamage(_stats.Damage);
                Destroy();
            }
        }
        
        private void Destroy()
        {
            _tickableManager.Remove(this);
            _pool.Despawn(this);
        }
        
        public class Pool : MonoMemoryPool<Axe>
        {
        }
    }
}