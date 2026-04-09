using UnityEngine;
using R3;

namespace Game
{
    public class EnemyViewModel
    {
        protected readonly EnemyModel _model;

        public EnemyViewModel(EnemyModel model) => _model = model;

        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<bool> IsDead => _model.IsDead;
    }
}