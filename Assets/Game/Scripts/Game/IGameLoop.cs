namespace Contracts
{
    public interface IGameLoop
    {
        public void Add(ITickable tickable);
        public void Remove(ITickable tickable);
    }
}