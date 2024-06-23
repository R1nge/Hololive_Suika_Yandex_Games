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
                case UIStateType.Endless:
                    var endlessUI = await _configProvider.UIConfig.GameUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(endlessUI);
                    return endlessUI;
                case UIStateType.GameOver:
                    var gameOverUI = await _configProvider.UIConfig.GameOverUI.InstantiateAsync();
                    _objectResolver.InjectGameObject(gameOverUI);
                    return gameOverUI;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }

            return null;
        }
    }
}