namespace Game
{
    public class VikingModel : EnemyModel
    {
        public VikingModel(Stats stats, AxeStats axeStats) : base(stats)
        {
        AxeStats = axeStats;
            
        } 

        public AxeStats AxeStats { get; }
    }
}