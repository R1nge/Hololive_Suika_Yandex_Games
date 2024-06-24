using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs
{
    public class UIFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;

        public UIFactory(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public async UniTask<GameObject> CreateUI(UIStateType uiStateType)
        {
            switch (uiStateType)
            {
                case UIStateType.None:
                    break;
                case UIStateType.Loading:
                    var loadingUI = await _configProvider.UIConfig.LoadingUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(loadingUI);
                    return loadingUI;
                case UIStateType.MainMenu:
                    var mainMenuUI = await _configProvider.UIConfig.MainMenuUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(mainMenuUI);
                    return mainMenuUI;
                case UIStateType.Settings:
                    var settingsUI = await _configProvider.UIConfig.SettingsUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(settingsUI);
                    return settingsUI;
                case UIStateType.GameModeSelection:
                    var gameModeSelectionUI = await _configProvider.UIConfig.GameModeSelectionUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(gameModeSelectionUI);
                    return gameModeSelectionUI;
                case UIStateType.Pause:
                    var pauseUI = await _configProvider.UIConfig.PauseUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(pauseUI);
                    return pauseUI;
                case UIStateType.Endless:
                    var endlessUI = await _configProvider.UIConfig.EndlessUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(endlessUI);
                    return endlessUI;
                case UIStateType.GameOverEndless:
                    var gameOverUI = await _configProvider.UIConfig.GameOverEndlessUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(gameOverUI);
                    return gameOverUI;
                case UIStateType.TimeRush:
                    var timeRushUI = await _configProvider.UIConfig.TimeRushUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(timeRushUI);
                    return timeRushUI;
                case UIStateType.GameOverTimeRush:
                    var gameOverTimeRushUI = await _configProvider.UIConfig.GameOverTimeRushUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(gameOverTimeRushUI);
                    return gameOverTimeRushUI;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }

            return null;
        }
    }
}