namespace Application
{
    public readonly struct ProgressSettings
    {
        public readonly float ExperienceMultiplier;
        public readonly int LevelUpStageStep;
        public readonly int ExperienceBeforeLevelUp;

        public ProgressSettings(float experienceMultiplier, int levelUpStageStep, int experienceBeforeLevelUp)
        {
            ExperienceMultiplier = experienceMultiplier;
            LevelUpStageStep = levelUpStageStep;
            ExperienceBeforeLevelUp = experienceBeforeLevelUp;
        }
    }
}
