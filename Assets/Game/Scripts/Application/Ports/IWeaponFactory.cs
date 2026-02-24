using Domain;

namespace Application
{
    public interface IWeaponFactory
    {
        public void CreateWeapon(Weapons type);
    }
}
