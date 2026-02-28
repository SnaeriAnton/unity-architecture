namespace Upgrades
{
    public class LevelUpInfo<TLevelUp, TStats> where TLevelUp : LevelUpsData<TStats> where TStats : struct
    {
        public LevelUpInfo(TLevelUp levelUpData) => LevelUpData = levelUpData;
        
        public TStats Stats => LevelUpData.LevelUps[CurrentLevelUp].Stats;
        public TLevelUp LevelUpData { get; }
        public int CurrentLevelUp { get; private set; } = -1;
        public int CountLevelUps => LevelUpData.Count;

        public LevelUpDescription<TStats> GetNextStats()
        {
            int index = CurrentLevelUp + 1;
            if (index >= CountLevelUps) index = 0;
            return LevelUpData.LevelUps[index];
        }

        public void LevelUp() => CurrentLevelUp++;
        public void Reset() => CurrentLevelUp = -1;
    }
}