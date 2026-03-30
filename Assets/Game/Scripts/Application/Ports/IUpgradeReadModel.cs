using System;
using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IUpgradeReadModel
    {
        public event Action OnUpgrade;

        public IReadOnlyList<UpgradeButtonViewData> GetUpgradeItems();
    }
}
