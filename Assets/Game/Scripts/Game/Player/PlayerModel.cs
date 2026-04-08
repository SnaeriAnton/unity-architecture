using System;
using UnityEngine;
using UniRx;

namespace Game
{
    public class PlayerModel : IPlayerReadModel, IDisposable
    {
        private readonly Vector3ReactiveProperty _position = new();
        private readonly BoolReactiveProperty _isPlaying = new();
        private readonly IntReactiveProperty _currentHealth = new();
        private readonly IntReactiveProperty _maxHealth = new();
        private readonly ReadOnlyReactiveProperty<bool> _isDead;
        private readonly Subject<Unit> _onDied = new();
        private readonly CompositeDisposable _disposable = new();

        private SpartanStats _spartanStats;

        public PlayerModel()
        {
            _isDead = _currentHealth
                .Select(x => x <= 0)
                .DistinctUntilChanged()
                .ToReadOnlyReactiveProperty()
                .AddTo(_disposable);
        }

        public bool IsDead => _isDead.Value;
        public IReadOnlyReactiveProperty<bool> IsDeadReactive => _isDead;
        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public IReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;
        public IReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;
        public IReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public IObservable<Unit> OnDied => _onDied;

        public void StartPlay() => _isPlaying.Value = true;
        public void Restart() => _spartanStats = default;
        public void SetPosition(Vector3 pos) => _position.Value = pos;

        public void SetPlayerStats(SpartanStats stats)
        {
            _spartanStats = stats;
            _maxHealth.SetValueAndForceNotify(_spartanStats.Health);
            _currentHealth.Value = _spartanStats.Health;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth.Value -= damage;
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, _spartanStats.Health);

            if (_currentHealth.Value == 0) Die();
        }

        private void Die()
        {
            _isPlaying.Value = false;
            _onDied.OnNext(Unit.Default);
        }

        public void Dispose() => _disposable.Dispose();
    }
}