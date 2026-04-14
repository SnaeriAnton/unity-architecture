using Game;
using UnityEngine;
using Zenject;

namespace Core.Installer
{
    public class GamePrefabPoolInstaller : MonoInstaller
    {
        [SerializeField] private Crystal _crystalTemplate;
        [SerializeField] private Coin _coinTemplate;
        [SerializeField] private Sword _swordTemplate;
        [SerializeField] private Arrow _arrowTemplate;
        [SerializeField] private Spear _spearTemplate;
        [SerializeField] private Axe _axeTemplate;
        [SerializeField] private KnightView _knightViewTemplate;
        [SerializeField] private SamuraiView _samuraiViewTemplate;
        [SerializeField] private VikingView _vikingViewTemplate;
        [SerializeField] private UpgradeButtonView _upgradeButtonViewTemplate;
        [SerializeField] private HealthView _healthViewTemplate;
        
        public override void InstallBindings()
        {
            Container.BindMemoryPool<UpgradeButtonView, UpgradeButtonView.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_upgradeButtonViewTemplate).UnderTransformGroup("UpgradeButtonsPool");
            Container.BindMemoryPool<HealthView, HealthView.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_healthViewTemplate).UnderTransformGroup("HealthViewPool");
            Container.BindMemoryPool<Crystal, Crystal.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_crystalTemplate).UnderTransformGroup("CrystalPool");
            Container.BindMemoryPool<Coin, Coin.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_coinTemplate).UnderTransformGroup("CoinPool");
            Container.BindMemoryPool<Sword, Sword.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_swordTemplate).UnderTransformGroup("SwordPool");
            Container.BindMemoryPool<Arrow, Arrow.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_arrowTemplate).UnderTransformGroup("ArrowPool");
            Container.BindMemoryPool<Spear, Spear.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_spearTemplate).UnderTransformGroup("SpearPool");
            Container.BindMemoryPool<Axe, Axe.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_axeTemplate).UnderTransformGroup("AxePool");
            Container.BindMemoryPool<KnightView, KnightView.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_knightViewTemplate).UnderTransformGroup("KnightViewPool");
            Container.BindMemoryPool<SamuraiView, SamuraiView.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_samuraiViewTemplate).UnderTransformGroup("SamuraiViewPool");
            Container.BindMemoryPool<VikingView, VikingView.Pool>().WithInitialSize(8).FromComponentInNewPrefab(_vikingViewTemplate).UnderTransformGroup("VikingViewPool");
        }
    }
}