namespace Game
{
    public class ShieldController : WeaponController
    {
        public ShieldController(ShieldView view, ShieldModel model) : base(view, model) { }
        
        public override void UpdateValues() => (Model as ShieldModel).UpdateValues();
    }
}