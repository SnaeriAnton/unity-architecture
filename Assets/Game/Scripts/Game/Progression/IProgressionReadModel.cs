using System;

namespace Game
{
    public interface IProgressionReadModel
    {
        public int MaxUpgrade { get; }
        public int CurrentExperience { get; }
        
        public event Action OnLevelUp;
        public event Action OnProgressChanged;
    }
}