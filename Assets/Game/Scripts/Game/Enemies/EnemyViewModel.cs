using UnityEngine;
using Core.MVVM;

namespace Game
{
    public class EnemyViewModel : ViewModelBase
    {    
        protected readonly EnemyModel _model;
        
        private readonly ObservableProperty<Vector3> _position;
        private readonly ObservableProperty<bool> _isDead;

        public EnemyViewModel(EnemyModel model)
        {
            _model = model;

            _position = new(_model.Position);
            _isDead = new(false);
            
            _model.OnDie += Die;
        }
        
        public IReadOnlyObservableProperty<Vector3> Position => _position;
        public IReadOnlyObservableProperty<bool> IsDead => _isDead;

        public override void Dispose()
        {
            base.Dispose();
            _model.OnDie -= Die;
        }

        public void SetPosition(Vector3 position) => _position.Set(position);
        
        private void Die()
        {
            _isDead.Set(true);
        }
    }
}