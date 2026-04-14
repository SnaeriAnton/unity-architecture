using UnityEngine;
using Contracts;
using Zenject;

namespace Game
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly TickableManager _tickableManager;
        private readonly IProjectileFactory _projectileFactory;
        private readonly KnightView.Pool _knightPool;
        private readonly SamuraiView.Pool _samuraiPool;
        private readonly VikingView.Pool _vikingPool;

        private readonly ITarget _player;

        private readonly IEnemyDeathProcessor _enemyDeathProcessor;

        public EnemyFactory(
            ITarget player, 
            IEnemyDeathProcessor enemyDeathProcessor, 
            TickableManager tickableManager, 
            IProjectileFactory projectileFactory,
            KnightView.Pool knightPool,
            SamuraiView.Pool samuraiPool,
            VikingView.Pool vikingPool
            )
        {
            _player = player;
            _enemyDeathProcessor = enemyDeathProcessor;
            _tickableManager = tickableManager;
            _projectileFactory = projectileFactory;
            _knightPool = knightPool;
            _samuraiPool = samuraiPool;
            _vikingPool = vikingPool;
        }

        public EnemyPresenter SpawnEnemy(EnemySpawnEntry enemyEntry, Vector3 pos)
        {
            EnemyModel model;
            EnemyPresenter presenter = null;
            EnemyView view = null;
            
            switch (enemyEntry.Type)
            {
                case EnemyNames.Samurai:
                    view = _samuraiPool.Spawn();
                    view.transform.position = pos;
                    model = new EnemyModel(enemyEntry.Stats.Stats);
                    presenter = new SamuraiPresenter(view, model, _enemyDeathProcessor, _player);
                    break;
                case EnemyNames.Viking:
                    view = _vikingPool.Spawn();
                    view.transform.position = pos;
                    model = new VikingModel(enemyEntry.Stats.Stats, (enemyEntry.Stats as VikingStats).AxeStats);
                    presenter = new VikingPresenter(view as VikingView, model, _enemyDeathProcessor, _projectileFactory, _player, _tickableManager);
                    break;
                case EnemyNames.Knight:
                    view = _knightPool.Spawn();
                    view.transform.position = pos;
                    model = new KnightModel(enemyEntry.Stats.Stats);
                    presenter = new KnightPresenter(view, model, _enemyDeathProcessor, _player);
                    break;
            }

            view.GetComponent<EnemyDamageReceiver>().Construct(presenter);
            return presenter;
        }

    
    }
}