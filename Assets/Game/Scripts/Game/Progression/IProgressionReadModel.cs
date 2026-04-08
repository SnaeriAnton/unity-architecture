using System;
using UniRx;

namespace Game
{
    public interface IProgressionReadModel
    {
        public IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        public IReadOnlyReactiveProperty<int> MaxUpgrade { get; }
        public IObservable<Unit> OnLevelUp { get; }
        
    }
}