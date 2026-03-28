using System;
using Core.MVVM;

namespace Game
{
    public class UpgradeButtonViewModel : ViewModelBase
    {
        private readonly ObservableProperty<UpgradeButtonViewData> _data;

        public IReadOnlyObservableProperty<UpgradeButtonViewData> Data => _data;
        public Command UpgradeCommand { get; }
        public Weapons Name { get; }

        public UpgradeButtonViewModel(UpgradeButtonViewData data, Action upgradeAction)
        {
            Name = data.Name;
            _data = new(data);
            UpgradeCommand = new(upgradeAction);
        }

        public void Update(UpgradeButtonViewData data) => _data.Set(data);
    }
}