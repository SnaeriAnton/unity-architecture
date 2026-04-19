using Core.BootstrapperSystem;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AppStartup>();
        }
    }
}