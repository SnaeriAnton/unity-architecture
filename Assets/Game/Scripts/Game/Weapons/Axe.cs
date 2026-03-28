using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Axe : MonoBehaviour, IPoolable, ITickable
    {
        private const float ANGULAR_SPEED_DEG = 90;

        private Action _onDespawned;
        private AxeStats _stats;
        private Vector3 _direction;
        private IGameLoop _loop;

        public int PoolID { get; private set; }

        public void Init(IGameLoop loop, AxeStats stats, Vector3 direction)
        {
            _loop = loop;
            _stats = stats;
            _direction = direction;
        }

        public void Tick(float dt)
        {
            transform.Rotate(0f, 0f, ANGULAR_SPEED_DEG * _stats.RotationSpeed * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * dt);

            _stats.LifeTime -= dt;
            if (_stats.LifeTime <= 0) Destroy();
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITarget target))
            {
                target.TakeDamage(_stats.Damage);
                Destroy();
            }
        }

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
        
        private void Destroy()
        {
            _loop.Remove(this);
            _onDespawned?.Invoke();
        }
    }
}