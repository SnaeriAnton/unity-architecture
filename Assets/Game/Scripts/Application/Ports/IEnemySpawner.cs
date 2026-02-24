namespace Application
{
    public interface IEnemySpawner
    {
        public void Start();
        public void Stop();
        public void Reset();
        public void LevelUp();
    }
}