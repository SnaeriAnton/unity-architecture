using Core.MVVM;
using UnityEngine;

namespace Game
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly PlayerModel _model;
        private readonly ObservableProperty<Vector3> _position;
        private readonly ObservableProperty<bool> _isDead;
        private readonly ObservableProperty<bool> _isPlaying;
        private readonly ObservableProperty<int> _currentHealth;
        private readonly ObservableProperty<int> _maxHealth;
        
        public PlayerViewModel(PlayerModel model)
        {
            _model = model;

            _position = new(_model.Position);
            _isDead = new(_model.IsDead);
            _isPlaying = new(_model.IsPlaying);
            _currentHealth = new(_model.CurrentHealth);
            _maxHealth = new(_model.MaxHealth);

            _model.OnHealthChanged += OnHealthChanged;
            _model.OnDied += OnDied;
        }

        public override void Dispose()
        {
            _model.OnHealthChanged -= OnHealthChanged;
            _model.OnDied -= OnDied;
            base.Dispose();
        }

        public IReadOnlyObservableProperty<Vector3> Position => _position;
        public IReadOnlyObservableProperty<bool> IsDead => _isDead;
        public IReadOnlyObservableProperty<bool> IsPlaying => _isPlaying;
        public IReadOnlyObservableProperty<int> CurrentHealth => _currentHealth;
        public IReadOnlyObservableProperty<int> MaxHealth => _maxHealth;
        
        public void SetPosition(Vector3 position) => _position.Set(position);

        
        public void RefreshState()
        {
            _isDead.Set(_model.IsDead);
            _isPlaying.Set(_model.IsPlaying);
            _currentHealth.Set(_model.CurrentHealth);
            _maxHealth.Set(_model.MaxHealth);
        }
        
        private void OnHealthChanged() => _currentHealth.Set(_model.CurrentHealth);
        
        private void OnDied()
        {
            _isDead.Set(true);
            _isPlaying.Set(_model.IsPlaying);
        }
    }
}