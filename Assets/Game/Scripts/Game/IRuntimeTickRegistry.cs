namespace Contracts
{
    public interface IRuntimeTickRegistry
    {
        public void Add(IRuntimeTickable runtimeTickable);
        public void Remove(IRuntimeTickable runtimeTickable);
    }
}