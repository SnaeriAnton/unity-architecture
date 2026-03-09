namespace Game
{
    public abstract class EnemyController
    {
        protected readonly EnemyModel _model;
        protected readonly EnemyView _view;
        
        public EnemyController(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }
    }
}