namespace Contracts
{
    public interface ISpentable
    {
        public bool TrySpendCoin(int amount);
        public bool TrySpendCrystal(int amount);
    }
}