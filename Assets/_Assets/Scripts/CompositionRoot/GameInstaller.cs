using _Assets.Scripts.Misc;
using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Wallets;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private ContainerFactory containerFactory;
        [SerializeField] private AudioService audioService;
        [SerializeField] private PlayerFactory playerFactory;
        [SerializeField] private PlayerInput playerInput;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Wallet>(Lifetime.Singleton);
            
            builder.RegisterComponent(coroutineRunner);
            builder.RegisterComponent(audioService);

            builder.Register<GameModeService>(Lifetime.Singleton);
            
            builder.Register<TimeRushTimer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<CollisionService>(Lifetime.Singleton);


            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.Register<RandomNumberGenerator>(Lifetime.Singleton);

            builder.Register<ComboService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<ResetService>(Lifetime.Singleton);
            builder.Register<GameOverTimer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<ScoreService>(Lifetime.Singleton);

            builder.Register<MainMenuUIController>(Lifetime.Singleton);
            builder.Register<GameSelectionUIController>(Lifetime.Singleton);
            builder.Register<InGameUIController>(Lifetime.Singleton);
            builder.Register<PauseMenuUIController>(Lifetime.Singleton);
            builder.Register<SettingsUIController>(Lifetime.Singleton);
            builder.Register<GameOverEndlessUIController>(Lifetime.Singleton);
            builder.Register<GameOverTimeRushUIController>(Lifetime.Singleton);

            builder.Register<ContinueService>(Lifetime.Singleton);

            builder.RegisterComponent(playerInput);
            builder.RegisterComponent(containerFactory);
            builder.RegisterComponent(playerFactory);
            builder.Register<SuikasFactory>(Lifetime.Singleton);

            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.Register<GameOverService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}