namespace Shared
{
    public interface IEnemySpawner
    {
        public void Start();
        public void Stop();
        
        public void LevelUp();
        public void Reset();
    }
}
