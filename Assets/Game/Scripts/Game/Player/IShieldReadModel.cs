using R3;

namespace Game
{
    public interface IShieldReadModel
    {
        public ReadOnlyReactiveProperty<bool> HasShield { get; }
        public ReadOnlyReactiveProperty<int> CurrentCoolDownCount { get; }
        public ReadOnlyReactiveProperty<int> CoolDown { get; }
    }
}