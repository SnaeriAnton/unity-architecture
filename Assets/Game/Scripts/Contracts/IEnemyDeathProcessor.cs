using Game;

namespace Contracts
{
    public interface IEnemyDeathProcessor
    {
        public void Handle(EnemyView view, ITickable tickable);
    }
}