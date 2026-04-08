using UniRx;

namespace Game
{
    public interface IShieldReadModel
    {
        public IReadOnlyReactiveProperty<bool> HasShield { get; }
        public IReadOnlyReactiveProperty<int> CurrentCoolDownCount { get; }
        public IReadOnlyReactiveProperty<int> CoolDown { get; }
    }
}