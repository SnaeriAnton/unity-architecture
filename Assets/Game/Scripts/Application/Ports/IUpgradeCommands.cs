using Domain;

namespace Application
{
    public interface IUpgradeCommands
    {
        public bool TryUpgrade(Weapons name);
        public void Init();
    }
}
