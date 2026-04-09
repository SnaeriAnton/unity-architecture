using System;
using UnityEngine;
using R3;

namespace Game
{
    public class PlayerModel : IPlayerReadModel, IDisposable
    {
        private readonly ReactiveProperty<Vector3> _position = new();
        private readonly ReactiveProperty<bool> _isPlaying = new();
        private readonly ReactiveProperty<int> _currentHealth = new();
        private readonly ReactiveProperty<int> _maxHealth = new();
        private readonly Subject<Unit> _onDied = new();
        private readonly CompositeDisposable _disposable = new();

        private SpartanStats _spartanStats;

        public PlayerModel()
        {
            IsDead = _currentHealth
                .Select(x => x <= 0)
                .DistinctUntilChanged()
                .ToReadOnlyReactiveProperty()
                .AddTo(_disposable);
        }

        public ReadOnlyReactiveProperty<bool> IsDead { get; }
        public ReadOnlyReactiveProperty<Vector3> Position => _position;
        public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;
        public ReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;
        public ReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public Observable<Unit> OnDied => _onDied;

        public void StartPlay() => _isPlaying.Value = true;
        public void Restart() => _spartanStats = default;
        public void SetPosition(Vector3 pos) => _position.Value = pos;

        public void SetPlayerStats(SpartanStats stats)
        {
            _spartanStats = stats;
            _maxHealth.OnNext(_spartanStats.Health);
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