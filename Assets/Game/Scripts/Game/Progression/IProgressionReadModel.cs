using System;
using R3;

namespace Game
{
    public interface IProgressionReadModel
    {
        public ReadOnlyReactiveProperty<int> CurrentExperience { get; }
        public ReadOnlyReactiveProperty<int> MaxUpgrade { get; }
        public Observable<Unit> OnLevelUp { get; }
        
    }
}