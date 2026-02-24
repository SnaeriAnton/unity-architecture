using Domain;

namespace Application
{
    public class WalletService
    {
        private readonly Wallet _wallet;
        private readonly IHUDRefresher _refresher;
        
        public WalletService(Wallet wallet, IHUDRefresher hudRefresher)
        {
            _wallet = wallet;
            _refresher = hudRefresher;
        }

        public void AddCoin()
        {
            _wallet.AddCoin();
            _refresher.Refresh();
        }
    }
}
