using System.Collections.Generic;

namespace Shared
{
    public interface IUpgrade
    {
        public bool IsMaxUpgrades { get; }

        public void Init();
        public bool TryUpgrade(Weapons name);
        public IReadOnlyList<UpgradeInfo> GetUpgradeInfo();
    }
}
