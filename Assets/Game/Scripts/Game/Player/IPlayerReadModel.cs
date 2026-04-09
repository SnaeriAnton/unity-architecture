using System;
using UnityEngine;
using R3;

namespace Game
{
    public interface IPlayerReadModel
    {
        public ReadOnlyReactiveProperty<Vector3> Position  { get; }
        public ReadOnlyReactiveProperty<bool> IsDead { get; }
        public ReadOnlyReactiveProperty<bool> IsPlaying { get; }
        public ReadOnlyReactiveProperty<int> CurrentHealth { get; }
        public ReadOnlyReactiveProperty<int> MaxHealth { get; }

        public Observable<Unit> OnDied { get; }
    }
}