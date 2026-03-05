namespace Domain
{
    public class Wallet
    {
        public int Coins { get; private set; }
        public int Crystals { get; private set; }

        public Wallet(int coins, int crystals)
        {
            Coins = coins;
            Crystals = crystals;
            
            Coins = 1000;
            Crystals = 1000;
        }

        public void AddCoin() => Coins++;
        public void AddCrystal() => Crystals++;

        public bool TrySpendCoin(int amount)
        {
            if (Coins < amount) return false;

            Coins -= amount;
            return true;
        }
        
        public bool TrySpendCrystal(int amount)
        {
            if (Crystals < amount) return false;

            Crystals -= amount;
            return true;
        }

        public void Reset()
        {
            Coins = 0;
            Crystals = 0;
            
            Coins = 1000;
            Crystals = 1000;
        }
    }
}