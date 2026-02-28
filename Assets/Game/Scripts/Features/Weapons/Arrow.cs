using System;
using UnityEngine;
using Shared;

namespace Weapons
{
    public class Arrow : MonoBehaviour, IPoolable
    {
        private Action _onDespawned;
        private WeaponStats _stats;
        private Vector3 _direction;
        private float _liveTime;

        public int PoolID { get; private set; }

        public void Init(WeaponStats stats, Vector3 direction)
        {
            _stats = stats;
            _direction = direction;
            _liveTime = _stats.LifeTime;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * Time.deltaTime);

            _liveTime -= Time.deltaTime;
            if (_liveTime <= 0) _onDespawned.Invoke();
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_stats.Damage);
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