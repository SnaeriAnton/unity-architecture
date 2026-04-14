using Contracts;
using UnityEngine;
using Zenject;

namespace Game
{
    public class VikingPresenter : EnemyPresenter
    {
        private readonly IProjectileFactory _factory;
        private readonly TickableManager _tickableManager;
        
        private float _currentAxeInterval;

        public VikingPresenter(EnemyView view, EnemyModel model, IEnemyDeathProcessor enemyDeathProcessor, IProjectileFactory factory, ITarget target, TickableManager tickableManager) : base(view, model, enemyDeathProcessor, target)
        {
            _factory = factory;
            _tickableManager = tickableManager;
            _currentAxeInterval = _model.Stats.AttacksPerSecond;
        }

        public override void Tick()
        {
            if (_target.IsDead) return;
            
            _currentAxeInterval -= Time.deltaTime;

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
            Axe axe = _factory.SpawnAxe(_view.Position, Quaternion.Euler(0f, 0f, angle));
            axe.Init(_tickableManager, (_model as VikingModel).AxeStats, direction.normalized);
        }
    }
}