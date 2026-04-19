using System;
using Contracts;
using UnityEngine;

namespace Game
{
    public class VikingPresenter : EnemyPresenter
    {
        private readonly IProjectileFactory _factory;
        private readonly IRuntimeTickRegistry _runtimeTickRegistry;
        private readonly Axe _axeTemplate;
        
        private float _currentAxeInterval;

        public VikingPresenter(EnemyView view, EnemyModel model, Axe axeTemplate, IEnemyDeathProcessor enemyDeathProcessor, IProjectileFactory factory, ITarget target, IRuntimeTickRegistry runtimeTickRegistry) : base(view, model, enemyDeathProcessor, target)
        {
            _factory = factory;
            _axeTemplate = axeTemplate;
            _runtimeTickRegistry = runtimeTickRegistry;
            _currentAxeInterval = _model.Stats.AttacksPerSecond;
        }

        public override void Tick(float dt)
        {
            if (_target.IsDead) return;
            
            _currentAxeInterval -= dt;

            if (_currentAxeInterval <= 0)
            {
                _currentAxeInterval = (_model as VikingModel).AxeStats.AttacksPerSecond;
                ShootAxe();
            }
        }
        
        private void ShootAxe()
        {
            Vector2 direction = _target.Position - _view.Position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Axe axe = _factory.SpawnAxe(_axeTemplate, _view.Position, Quaternion.Euler(0f, 0f, angle));
            axe.Init(_runtimeTickRegistry, (_model as VikingModel).AxeStats, direction.normalized);
        }
    }
}