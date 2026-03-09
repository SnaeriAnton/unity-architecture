using Contracts;
using Core.Pool;

namespace Game
{
    public abstract class WeaponController
    {
        protected readonly WeaponView _view;

        public WeaponController(WeaponView view, WeaponModel model)
        {
            _view = view;
            Model = model;
        }
        
        public WeaponModel Model { get; private set; }

        public virtual void Dispose() => _view.Dispose();
        
        public virtual void Apply() { }
        public virtual void UpdateValues() { }
        
        protected virtual void OnHit(IEnemyTarget target, ProjectileModel projectile) { }
    }
}