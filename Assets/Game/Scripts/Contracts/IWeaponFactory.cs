using Game;

namespace Contracts
{
    public interface IWeaponFactory
    {
        public Weapon CreateWeapon(Weapon template);
    }
}