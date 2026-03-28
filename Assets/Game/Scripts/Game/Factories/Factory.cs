using System;
using Contracts;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Pool;

namespace Game
{
    public class Factory : IEnemyFactory, IWeaponFactory, IProjectileFactory
    {
        private readonly PoolManager _poolManager;
        private readonly Transform _playerParent;
        private readonly ITarget _player;
        private readonly IGameLoop _gameLoop;

        private readonly IEnemyDeathProcessor _enemyDeathProcessor;

        public Factory(PoolManager poolManager, Transform playerParent, ITarget player, IEnemyDeathProcessor enemyDeathProcessor, IGameLoop gameLoop)
        {
            _poolManager = poolManager;
            _playerParent = playerParent;
            _player = player;
            _enemyDeathProcessor = enemyDeathProcessor;
            _gameLoop = gameLoop;
        }

        public Weapon CreateWeapon(Weapon template)
        {
            Weapon weapon = Object.Instantiate(template, _playerParent);
            weapon.Construct(_gameLoop, _poolManager);
            return weapon;
        }

        public EnemyController SpawnEnemy(EnemySpawnEntry enemyEntry, Vector3 pos)
        {
            EnemyModel model;
            EnemyController controller = null;

            EnemyView view = _poolManager.Spawn(enemyEntry.ViewPrefab, pos, Quaternion.identity);
            EnemyViewModel viewModel = null;

            switch (enemyEntry.Type)
            {
                case EnemyNames.Samurai:
                    model = new EnemyModel(enemyEntry.Stats.Stats);
                    model.SetPosition(pos);
                    viewModel = new(model);
                    view.Bind(viewModel);
                    controller = new SamuraiController(viewModel, view, model, _enemyDeathProcessor, _player);
                    break;
                case EnemyNames.Viking:
                    model = new VikingModel(enemyEntry.Stats.Stats, (enemyEntry.Stats as VikingStats).AxeStats);
                    model.SetPosition(pos);
                    viewModel = new(model);
                    view.Bind(viewModel);
                    controller = new VikingController(viewModel, view as VikingView, model, (enemyEntry.Stats as VikingStats).AxeTemplate, _enemyDeathProcessor, this, _player, _gameLoop);
                    break;
                case EnemyNames.Knight:
                    model = new KnightModel(enemyEntry.Stats.Stats);
                    model.SetPosition(pos);
                    viewModel = new(model);
                    view.Bind(viewModel);
                    controller = new KnightController(viewModel, view, model, _enemyDeathProcessor, _player);
                    break;
            }

            view.GetComponent<EnemyDamageReceiver>().Construct(controller);
            return controller;
        }

        public Axe SpawnAxe(Axe template, Vector3 position, Quaternion rotate) => _poolManager.Spawn(template, position, rotate);
    }
}