using UnityEngine;
using UniRx;

namespace Game
{
    public class PlayerViewModel
    {
        private readonly IPlayerReadModel _model;

        public PlayerViewModel(IPlayerReadModel model) => _model = model;

        public IReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public IReadOnlyReactiveProperty<bool> IsDead => _model.IsDeadReactive;
        public IReadOnlyReactiveProperty<bool> IsPlaying => _model.IsPlaying;
        public IReadOnlyReactiveProperty<int> CurrentHealth => _model.CurrentHealth;
        public IReadOnlyReactiveProperty<int> MaxHealth => _model.MaxHealth;
    }
}