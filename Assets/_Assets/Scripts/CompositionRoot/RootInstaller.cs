using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Yandex;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private ConfigProvider configProvider;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<YandexService>(Lifetime.Singleton);
            builder.RegisterComponent(configProvider);
        }
    }
}