using System;
using Domain;
using UnityEngine;

namespace Infrastructure
{
    public class Axe : MonoBehaviour, IPoolable
    {
        private const float ANGULAR_SPEED_DEG = 90;

        private Action _onDespawned;
        private AxeStats _stats;
        private Vector3 _direction;

        public int PoolID { get; private set; }

        public void Init(AxeStats stats, Vector3 direction)
        {
            _stats = stats;
            _direction = direction;
        }

        private void Update()
        {
            transform.Rotate(0f, 0f, ANGULAR_SPEED_DEG * _stats.RotationSpeed * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * Time.deltaTime);

            _stats.LifeTime -= Time.deltaTime;
            if (_stats.LifeTime <= 0) _onDespawned.Invoke();
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(_stats.Damage);
                _onDespawned.Invoke();
            }
        }

        void IPoolable.OnSpawned(int poolID, Action onDespawned)
        {
            PoolID = poolID;
            _onDespawned = onDespawned;
            gameObject.SetActive(true);
        }
    }
}