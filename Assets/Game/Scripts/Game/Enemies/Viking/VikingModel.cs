using Contracts;

namespace Game
{
    public class VikingModel : EnemyModel
    {
        public VikingModel(EnemyStats stats, ITarget target) : base(stats, target)
        {
        }
    }
}