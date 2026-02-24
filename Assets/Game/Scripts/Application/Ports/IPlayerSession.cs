using Domain;

namespace Application
{
    public interface IPlayerSession
    {
        public bool HasWeapon(Weapons weapon);
        public void StartPlay();
        public void Restart();
        public void SetPlayerStats(SpartanStats stats);
        public void SetWeaponStats(Weapons name, WeaponStats stats);
    }
}
