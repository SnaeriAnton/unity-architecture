using System;
using System.Collections.Generic;
using UnityEngine;
using Contracts;
using Core.Pool;

namespace Game
{
    public class VikingView : EnemyView
    {
        private readonly List<AxeController> _controllers = new();
        
        private VikingModel _vikingModel;
        private float _currentAxeInterval;
        private AxeView _axeTemplate;
        private AxeStats _axeStats;

        private void Update()
        {
            if (_model.Target.IsDead) return;
            _currentAxeInterval -= Time.deltaTime;

            if (_currentAxeInterval <= 0)
            {
                _currentAxeInterval = _axeStats.AttacksPerSecond;
                ShootAxe();
            }
        }
        
        public override void Init(EnemyModel model)
        {
            base.Init(model);
            _vikingModel = model as VikingModel;
            _axeTemplate = (_model.Stats as VikingStats).AxeTemplate;
            _axeStats = (_model.Stats as VikingStats).AxeStats;
            _currentAxeInterval = _model.Stats.Stats.AttacksPerSecond;
        }
        
        private void ShootAxe()
        {
            Vector2 direction = _model.Target.Transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            AxeView axeView = _poolManager.Spawn(_axeTemplate, transform.position, Quaternion.Euler(0f, 0f, angle));
            AxeModel model = new(_axeStats, direction);
            axeView.Init(model);
            AxeController controller = new(model, axeView);
            _controllers.Add(controller);
        }
    }
}