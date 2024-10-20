using _Assets.Scripts.Configs;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Quests;
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
        [SerializeField] private QuestsService questsService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<YandexService>(Lifetime.Singleton);
            builder.Register<LocalizationService>(Lifetime.Singleton);
            builder.RegisterComponent(configProvider);
            builder.RegisterComponent(questsService);
        }
    }
}