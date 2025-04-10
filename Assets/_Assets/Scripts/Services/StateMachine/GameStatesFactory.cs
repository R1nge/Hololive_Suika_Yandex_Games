﻿using System;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.Wallets;
using _Assets.Scripts.Services.Yandex;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly UIStateMachine _uiStateMachine;
        private readonly YandexService _yandexService;
        private readonly ContainerFactory _containerFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly SuikasFactory _suikasFactory;
        private readonly PlayerInput _playerInput;
        private readonly ResetService _resetService;
        private readonly AudioService _audioService;
        private readonly LocalizationService _localizationService;
        private readonly ScoreService _scoreService;
        private readonly ContinueService _continueService;
        private readonly TimeRushTimer _timeRushTimer;
        private readonly GameModeService _gameModeService;
        private readonly Wallet _wallet;

        private GameStatesFactory(UIStateMachine uiStateMachine, YandexService yandexService,
            ContainerFactory containerFactory, PlayerFactory playerFactory, SuikasFactory suikasFactory,
            PlayerInput playerInput, ResetService resetService, AudioService audioService, LocalizationService localizationService, ScoreService scoreService, ContinueService continueService, TimeRushTimer timeRushTimer, GameModeService gameModeService, Wallet wallet)
        {
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _suikasFactory = suikasFactory;
            _playerInput = playerInput;
            _resetService = resetService;
            _audioService = audioService;
            _localizationService = localizationService;
            _scoreService = scoreService;
            _continueService = continueService;
            _timeRushTimer = timeRushTimer;
            _gameModeService = gameModeService;
            _wallet = wallet;
        }

        public IAsyncState CreateAsyncState(GameStateType gameStateType, GameStateMachine gameStateMachine)
        {
            switch (gameStateType)
            {
                case GameStateType.Init:
                    return new InitState(gameStateMachine, _uiStateMachine, _yandexService, _playerInput, _audioService, _localizationService, _continueService, _wallet);
                case GameStateType.Endless:
                    return new EndlessGameState(gameStateMachine, _uiStateMachine, _containerFactory, _playerFactory, _playerInput, _gameModeService);
                case GameStateType.GameOverEndless:
                    return new GameOverEndlessGameState(_yandexService, _uiStateMachine, _scoreService, _resetService, _continueService);
                case GameStateType.ContinueEndless:
                    return new ContinueEndless(_uiStateMachine, _containerFactory, _playerFactory, _playerInput, _continueService);
                case GameStateType.TimeRush:
                    return new TimeRushGameState(gameStateMachine, _uiStateMachine, _containerFactory, _playerFactory, _playerInput, _timeRushTimer, _gameModeService);
                case GameStateType.GameOverTimeRush:
                    return new GameOverTimeRushGameState(_yandexService, _uiStateMachine, _scoreService, _resetService, _continueService);
                case GameStateType.ContinueTimeRush:
                    return new ContinueTimeRush(_uiStateMachine, _containerFactory, _playerFactory, _playerInput, _continueService, _timeRushTimer);
                case GameStateType.Continue:
                    return new Continue(_gameModeService, gameStateMachine);
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateType), gameStateType, null);
            }
        }
    }
}