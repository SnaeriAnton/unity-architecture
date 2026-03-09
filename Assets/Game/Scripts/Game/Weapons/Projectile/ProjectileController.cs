namespace Game
{
    public class ProjectileController<TModel, TView>
    {
        protected readonly TModel _model;
        protected readonly TView _view;

        public ProjectileController(TModel model, TView view)
        {
            _model = model;
            _view = view;
        }

        public virtual void Update() { }
    }
}