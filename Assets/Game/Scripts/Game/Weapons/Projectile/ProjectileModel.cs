namespace Game
{
    public class ProjectileModel
    {
        public WeaponStats Stats { get; }
        
        public ProjectileModel(WeaponStats stats) => Stats = stats;
    }
}