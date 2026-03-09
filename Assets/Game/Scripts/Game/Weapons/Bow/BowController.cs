using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BowController : WeaponController
    {
        private readonly List<ArrowController> _controllers = new();
        
        private float _lastTimeSpawn;
        
        public BowController(BowView view, BowModel model) : base(view, model) { }
        
        public override void Apply()
        {
            if (Model.Stats.Equals(default)) return;

            float interval = Time.time - _lastTimeSpawn;

            if (interval >= Model.Stats.AttacksPerSecond)
            {
                _lastTimeSpawn = Time.time;

                for (int i = 0; i < (Model as BowModel).CountSpears; i++)
                {
                    (Vector2, float) coordinates = (Model as BowModel).GetCoordinates();
                    ArrowModel model = new(Model.Stats, coordinates.Item1);
                    ArrowView view = (_view as BowView).Shoot(coordinates.Item2);
                    view.Init(model);
                    ArrowController spearController = new(model, view);
                    spearController.OnDisable += OnDisable;
                    _controllers.Add(spearController);
                }
            }

            for (int i = _controllers.Count - 1; i >= 0; i--)
                _controllers[i].Update();
        }
        
        private void OnDisable(ArrowController controller)
        {
            controller.OnDisable -= OnDisable;
            _controllers.Remove(controller);
        }
    }
}