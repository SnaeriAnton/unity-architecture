namespace Game
{
    public class KnightModel : EnemyModel
    {
        public KnightModel(Stats stats) : base(stats) { }
        
        public bool PlayerInRange { get; private set; }
        public float AttackTimer { get; private set; }
        
        public void ChangePlayerInRange(bool value) => PlayerInRange = value; 
        
        public void SetAttackTimer(float time) => AttackTimer = time;
    }
}