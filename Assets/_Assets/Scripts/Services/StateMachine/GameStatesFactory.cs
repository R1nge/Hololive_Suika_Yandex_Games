using System;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;
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

        private GameStatesFactory(UIStateMachine uiStateMachine, YandexService yandexService, ContainerFactory containerFactory, PlayerFactory playerFactory, SuikasFactory suikasFactory, PlayerInput playerInput)
        {
            _uiStateMachine = uiStateMachine;
            _yandexService = yandexService;
            _containerFactory = containerFactory;
            _playerFactory = playerFactory;
            _suikasFactory = suikasFactory;
            _playerInput = playerInput;
        }

        public IAsyncState CreateAsyncState(GameStateType gameStateType, GameStateMachine gameStateMachine)
        {
            switch (gameStateType)
            {
                case GameStateType.Init:
                    return new InitState(gameStateMachine, _uiStateMachine, _yandexService, _playerInput);
                case GameStateType.Endless:
                    return new EndlessGameState(gameStateMachine, _uiStateMachine, _containerFactory, _playerFactory, _suikasFactory, _playerInput);
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateType), gameStateType, null);
            }
        }
    }
}