using UnityEngine;

namespace Game
{
    public class SwordsController : WeaponController
    {
        public SwordsController(SwordsView view, SwordsModel model) : base(view, model) { }

        public override void Apply() => (Model as SwordsModel).SetCurrentAngle(-90f * Model.Stats.RoundSpeed * Time.deltaTime);
    }
}