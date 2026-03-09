using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SpearsController : WeaponController
    {
        private readonly List<SpearController> _controllers = new();

        private float _lastTimeSpawn;

        public SpearsController(SpearsView view, SpearsModel model) : base(view, model) { }

        public override void Apply()
        {
            if (Model.Stats.Equals(default)) return;

            float interval = Time.time - _lastTimeSpawn;

            if (interval >= Model.Stats.AttacksPerSecond)
            {
                _lastTimeSpawn = Time.time;

                for (int i = 0; i < (Model as SpearsModel).CountSpears; i++)
                {
                    (Vector2, float) coordinates = (Model as SpearsModel).GetCoordinates();
                    SpearModel model = new(Model.Stats, coordinates.Item1);
                    SpearView view = (_view as SpearsView).Shoot(coordinates.Item2);
                    view.Init(model);
                    SpearController spearController = new(model, view);
                    spearController.OnDisable += OnDisable;
                    _controllers.Add(spearController);
                }
            }

            for (int i = _controllers.Count - 1; i >= 0; i--)
                _controllers[i].Update();
        }

        private void OnDisable(SpearController controller)
        {
            controller.OnDisable -= OnDisable;
            _controllers.Remove(controller);
        }
    }
}