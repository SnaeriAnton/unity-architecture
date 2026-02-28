namespace Shared
{
    public interface IWallet
    {
        public void AddCoin();
        public void AddCrystal();
        public bool TrySpendCoin(int amount);
        public bool TrySpendCrystal(int amount);
        public void Reset();
    }
}
