
namespace Application
{
    public class LevelUpInfo<TLevelUp, TStats> where TLevelUp : UpgradeDefinition<TStats> where TStats : struct
    {
        public LevelUpInfo(TLevelUp levelUpData) => LevelUpData = levelUpData;

        public TStats Stats => LevelUpData.Upgrades[CurrentLevelUp].Stats;
        public TLevelUp LevelUpData { get; }
        public int CurrentLevelUp { get; private set; } = -1;
        public int CountLevelUps => LevelUpData.Count;

        public UpgradeDescription<TStats> GetNextStats()
        {
            int index = CurrentLevelUp + 1;
            if (index >= CountLevelUps) index = 0;
            return LevelUpData.Upgrades[index];
        }

        public void LevelUp() => CurrentLevelUp++;
        public void Reset() => CurrentLevelUp = -1;
    }
}