using Core.UI;

namespace Game
{
    public class MenuScreenPresenter : ScreenPresenter
    {
        private readonly MenuScreenView _view;
        private readonly GameManager _gameManager;

        public MenuScreenPresenter(MenuScreenView view, GameManager gameManager)
        {
            _view = view;
            _gameManager = gameManager;
        }

        public void Initialize()
        {
            _view.Initialize();
            _view.OnClick += _gameManager.StartGame;
        }

        public override void Dispose()
        {
            _view.Dispose();
            _view.OnClick -= _gameManager.StartGame;
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