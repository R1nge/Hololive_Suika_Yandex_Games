using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.UIs.StateMachine;
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

        public GameObject CreateUI(UIStateType uiStateType)
        {
            switch (uiStateType)
            {
                case UIStateType.None:
                    break;
                case UIStateType.Loading:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.LoadingUI);
                case UIStateType.MainMenu:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.MainMenuUI);
                case UIStateType.Settings:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.SettingsUI);
                case UIStateType.GameModeSelection:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.GameModeSelectionUI);
                case UIStateType.Game:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.GameUI);
                case UIStateType.GameOver:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.GameOverUI);
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }

            return null;
        }
    }
}