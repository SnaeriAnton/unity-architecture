using UnityEngine;
using R3;

namespace Game
{
    public class PlayerViewModel
    {
        private readonly IPlayerReadModel _model;

        public PlayerViewModel(IPlayerReadModel model) => _model = model;

        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<bool> IsDead => _model.IsDead;
        public ReadOnlyReactiveProperty<bool> IsPlaying => _model.IsPlaying;
        public ReadOnlyReactiveProperty<int> CurrentHealth => _model.CurrentHealth;
        public ReadOnlyReactiveProperty<int> MaxHealth => _model.MaxHealth;
    }
}