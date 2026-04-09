using System;
using Random = UnityEngine.Random;
using Contracts;
using R3;

namespace Game
{
    public class EnemySpawnerController : IEnemyStageProgression, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly GeneratorData _data;
        private readonly ISpawnPointProvider _border;
        private readonly IEnemyFactory _factory;
        private readonly ITarget _player;
        private readonly IUpdateStream _updateStream;

        private GeneratorStage _currentStage;
        private bool _isSpawning;
        private int _currentStageIndex;
        private float _spawnTimer;

        public EnemySpawnerController(GeneratorData data, IEnemyFactory factory, ISpawnPointProvider border, ITarget player, IUpdateStream updateStream)
        {
            _data = data;
            _factory = factory;
            _border = border;
            _player = player;
            _updateStream = updateStream;
            _spawnTimer = 0f;

            _updateStream.OnUpdate.Subscribe(Tick).AddTo(_disposable);
        }

        public void Start()
        {
            _currentStage = _data.Stages[_currentStageIndex];
            _isSpawning = true;
        }

        public void Stop() => _isSpawning = false;
        public void Dispose() => _disposable?.Dispose();

        public void Tick(float dt)
        {
            if (!_isSpawning) return;
            _spawnTimer += dt;

            if (_spawnTimer < _currentStage.SpawnInterval) return;

            _spawnTimer = 0f;
            EnemyController enemy = _factory.SpawnEnemy(_currentStage.Enemies[Random.Range(0, _currentStage.Enemies.Count)], _border.PickPoint(_player.Position, _data.RadiusPlayer));
        }

        public void Reset()
        {
            _currentStageIndex = 0;
            _spawnTimer = 0f;
            _currentStage = _data.Stages[_currentStageIndex];
        }

        public void LevelUp()
        {
            if (_currentStageIndex < _data.Stages.Count - 1)
            {
                _currentStageIndex++;
                _currentStage = _data.Stages[_currentStageIndex];
            }
        }

    }
}