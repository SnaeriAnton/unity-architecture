using System;
using UniRx;

namespace Game
{
    public interface IUpgrade
    {
        public IObservable<Unit> OnUpgraded { get; }
        
        public bool TryUpgrade(Weapons name);
    }
}