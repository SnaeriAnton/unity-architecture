using Random = UnityEngine.Random;
using Contracts;

namespace Game
{
    public class EnemySpawnerController : ITickable, IEnemyStageProgression
    {
        private readonly GeneratorData _data;
        private readonly ISpawnPointProvider _border;
        private readonly IEnemyFactory _factory;
        private readonly IGameLoop _gameLoop;
        private readonly ITarget _player;

        private GeneratorStage _currentStage;
        private bool _isSpawning;
        private int _currentStageIndex;
        private float _spawnTimer;

        public EnemySpawnerController(GeneratorData data, IEnemyFactory factory, ISpawnPointProvider border, ITarget player, IGameLoop gameLoop)
        {
            _data = data;
            _factory = factory;
            _border = border;
            _player = player;
            _gameLoop = gameLoop;
            _spawnTimer = 0f;
        }

        public void Start()
        {
            _currentStage = _data.Stages[_currentStageIndex];
            _isSpawning = true;
        }

        public void Stop() => _isSpawning = false;

        public void Tick(float dt)
        {
            if (!_isSpawning) return;
            _spawnTimer += dt;

            if (_spawnTimer < _currentStage.SpawnInterval) return;

            _spawnTimer = 0f;
            EnemyController enemy = _factory.SpawnEnemy(_currentStage.Enemies[Random.Range(0, _currentStage.Enemies.Count)], _border.PickPoint(_player.Position, _data.RadiusPlayer));
            _gameLoop.Add(enemy);
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