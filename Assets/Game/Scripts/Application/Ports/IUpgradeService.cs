namespace Application
{
    public interface IUpgradeService
    {
        public bool IsMaxUpgrades { get; }

        public void Init();
    }
}