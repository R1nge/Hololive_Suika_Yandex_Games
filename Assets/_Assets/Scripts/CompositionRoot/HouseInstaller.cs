using _Assets.Scripts.Houses;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class HouseInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GridCellFactory>(Lifetime.Singleton);
        }
    }
}