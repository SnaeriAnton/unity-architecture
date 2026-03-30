using System;

namespace Application
{
    public interface IEnemyDeathHandler
    {
        public event Action OnEnemyDead; 
    }
}
