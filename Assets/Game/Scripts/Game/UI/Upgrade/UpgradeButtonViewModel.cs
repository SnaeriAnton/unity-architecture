using System;
using UniRx;

namespace Game
{
    public class UpgradeButtonViewModel : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly ReactiveProperty<UpgradeButtonViewData> _data;

        public IReadOnlyReactiveProperty<UpgradeButtonViewData> Data => _data;
        public ReactiveCommand UpgradeCommand { get; } = new();
        public Weapons Name { get; }

        public UpgradeButtonViewModel(UpgradeButtonViewData data, Action upgradeAction)
        {
            Name = data.Name;
            _data = new(data);
            UpgradeCommand.Subscribe(_ => upgradeAction()).AddTo(_disposable);
        }

        public void Update(UpgradeButtonViewData data) => _data.Value = data;

        public void Dispose()
        {
            _data.Dispose();
            _disposable.Dispose();
        }
    }
}