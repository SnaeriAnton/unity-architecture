using System;
using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class Spear : MonoBehaviour, IPoolable, ITickable
    {
        private Action _onDespawned;
        private WeaponStats _stats;
        private Vector3 _direction;
        private IGameLoop _loop;
        private float _liveTime;
        private int _currentStrength;

        public int PoolID { get; private set; }

        public void Init(IGameLoop loop, WeaponStats stats, Vector3 direction)
        {
            _loop = loop;
            _stats = stats;
            _direction = direction;
            _currentStrength = _stats.Strength;
            _liveTime = _stats.LifeTime;
        }

        void IPoolable.OnDespawned() => gameObject.SetActive(false);

        public void Tick(float dt)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _stats.FlightSpeed * dt);

            _liveTime -= dt;

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