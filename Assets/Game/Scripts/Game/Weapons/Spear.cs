using System;
using Contracts;
using UnityEngine;
using Core.Pool;
using R3;

namespace Game
{
    public class Spear : MonoBehaviour, IPoolable, IDisposable
    {
        private CompositeDisposable _disposable = new();
        private Action _onDespawned;
        private WeaponStats _stats;
        private Vector3 _direction;
        private IUpdateStream _updateStream;
        private float _liveTime;
        private int _currentStrength;

        public int PoolID { get; private set; }

        public void Init(IUpdateStream updateStream, WeaponStats stats, Vector3 direction)
        {
            _updateStream = updateStream;
            _stats = stats;
            _direction = direction;
            _disposable = new();
            _currentStrength = _stats.Strength;
            _liveTime = _stats.LifeTime;

            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
        }

        void IPoolable.OnDespawned()
        {
            Dispose();
            gameObject.SetActive(false);
        }

        private void Tick(float dt)
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
            Dispose();
            _onDespawned?.Invoke();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}