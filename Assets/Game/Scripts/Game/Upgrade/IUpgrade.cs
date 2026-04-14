using System;

namespace Game
{
    public interface IUpgrade
    {
        public event Action OnUpgraded;
        
        public bool TryUpgrade(Weapons name);
    }
}