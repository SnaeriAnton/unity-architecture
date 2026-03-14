using Core.UI;

namespace Game
{
    public class LoseScreenPresenter : ScreenPresenter
    {
        private readonly LoseScreenView _view;
        private readonly GameManager _gameManager;

        public LoseScreenPresenter(LoseScreenView view, GameManager gameManager)
        {
            _view = view;
            _gameManager = gameManager;
        }

        public void Initialize()
        {
            _view.Initialize();
            _view.OnClick += _gameManager.RestartRun;
        }

        public override void Dispose()
        {
            _view.Dispose();
            _view.OnClick -= _gameManager.RestartRun;
        }
        
        public override void Show()
        {
            base.Show();
            _view.Show();
        }

        public override void Hide()
        {
            base.Hide();
            _view.Hide();
        }
    }
}