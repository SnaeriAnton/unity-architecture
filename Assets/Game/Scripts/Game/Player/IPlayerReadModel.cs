using System;
using UniRx;
using UnityEngine;

namespace Game
{
    public interface IPlayerReadModel
    {
        public IReadOnlyReactiveProperty<Vector3> Position  { get; }
        public IReadOnlyReactiveProperty<bool> IsDeadReactive { get; }
        public IReadOnlyReactiveProperty<bool> IsPlaying { get; }
        public IReadOnlyReactiveProperty<int> CurrentHealth { get; }
        public IReadOnlyReactiveProperty<int> MaxHealth { get; }
        public bool IsDead { get; }

        public IObservable<Unit> OnDied { get; }
    }
}