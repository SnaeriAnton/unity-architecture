using Contracts;
using UnityEngine;
using Core.Pool;

namespace Game
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly PoolManager _poolManager;
        private readonly ITarget _player;
        private readonly IRuntimeTickRegistry _runtimeTickRegistry;
        private readonly IProjectileFactory _projectileFactory;

        private readonly IEnemyDeathProcessor _enemyDeathProcessor;

        public EnemyFactory(PoolManager poolManager, ITarget player, IEnemyDeathProcessor enemyDeathProcessor, IRuntimeTickRegistry runtimeTickRegistry, IProjectileFactory projectileFactory)
        {
            _poolManager = poolManager;
            _player = player;
            _enemyDeathProcessor = enemyDeathProcessor;
            _runtimeTickRegistry = runtimeTickRegistry;
            _projectileFactory = projectileFactory;
        }
        public EnemyPresenter SpawnEnemy(EnemySpawnEntry enemyEntry, Vector3 pos)
        {
            EnemyModel model;
            EnemyPresenter presenter = null;

            EnemyView view = _poolManager.Spawn(enemyEntry.ViewPrefab, pos, Quaternion.identity);
            
            switch (enemyEntry.Type)
            {
                case EnemyNames.Samurai:
                    model = new SamuraiModel(enemyEntry.Stats.Stats);
                    presenter = new SamuraiPresenter(view, model, _enemyDeathProcessor, _player);
                    break;
                case EnemyNames.Viking:
                    model = new VikingModel(enemyEntry.Stats.Stats, (enemyEntry.Stats as VikingStats).AxeStats);
                    presenter = new VikingPresenter(view as VikingView, model, (enemyEntry.Stats as VikingStats).AxeTemplate, _enemyDeathProcessor, _projectileFactory, _player, _runtimeTickRegistry);
                    break;
                case EnemyNames.Knight:
                    model = new KnightModel(enemyEntry.Stats.Stats);
                    presenter = new KnightPresenter(view, model, _enemyDeathProcessor, _player);
                    break;
            }

            view.GetComponent<EnemyDamageReceiver>().Bind(presenter);
            return presenter;
        }
    }
}