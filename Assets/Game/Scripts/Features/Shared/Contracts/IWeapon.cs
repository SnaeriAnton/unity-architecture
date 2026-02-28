namespace Shared
{
    public interface IWeapon
    {
        public IShield Shield { get; }

        public bool HasWeapon(Weapons name);
        public void Update();
        public void ApplyAll();
        public void Reset();
        public void SetStats(Weapons name, WeaponStats stats);
    }
}
