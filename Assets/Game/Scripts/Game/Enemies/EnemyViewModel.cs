using UnityEngine;
using UniRx;

namespace Game
{
    public class EnemyViewModel
    {
        protected readonly EnemyModel _model;

        public EnemyViewModel(EnemyModel model) => _model = model;

        public IReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public IReadOnlyReactiveProperty<bool> IsDead => _model.IsDead;
    }
}