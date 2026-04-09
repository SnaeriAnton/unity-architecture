using System;
using R3;

namespace Game
{
    public interface IUpgrade
    {
        public Observable<Unit> OnUpgraded { get; }
        
        public bool TryUpgrade(Weapons name);
    }
}