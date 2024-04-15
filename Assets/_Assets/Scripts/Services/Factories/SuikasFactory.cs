using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Audio;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Factories
{
    public class SuikasFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;
        private readonly RandomNumberGenerator _randomNumberGenerator;
        private readonly ScoreService _scoreService;
        private readonly ResetService _resetService;
        private readonly AudioService _audioService;
        private readonly ComboService _comboService;

        private SuikasFactory(IObjectResolver objectResolver, ConfigProvider configProvider,
            RandomNumberGenerator randomNumberGenerator, ScoreService scoreService, ResetService resetService,
            AudioService audioService,
            ComboService comboService)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _randomNumberGenerator = randomNumberGenerator;
            _scoreService = scoreService;
            _resetService = resetService;
            _audioService = audioService;
            _comboService = comboService;
        }

        public Rigidbody2D CreateKinematic(Vector3 position, Transform parent)
        {
            var index = _randomNumberGenerator.PickRandomSuika();

            var suikaPrefab = _configProvider.SuikaConfig.GetPrefab(index);
            var suikaInstance = _objectResolver
                .Instantiate(suikaPrefab.gameObject, position, Quaternion.identity, parent).GetComponent<Suika>();
            suikaInstance.SetIndex(index);

            var rigidbody = suikaInstance.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;

            AddToResetService(suikaInstance);
            
            return rigidbody;
        }

        public void Create(int index, Vector3 position)
        {
            index++;

            if (!_configProvider.SuikaConfig.HasPrefab(index))
            {
                AddScore(index--);
                Debug.LogError($"SuikasFactory: Index is out of range {index}");
                return;
            }

            var suikaPrefab = _configProvider.SuikaConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity)
                .GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            suikaInstance.Drop();
            suikaInstance.Scale(1f);

            AddScore(index);
            AddToResetService(suikaInstance);
            _comboService.AddCombo();
            _audioService.AddToMergeSoundsQueue(index);
        }

        public void CreateContinue(int index, Vector3 position)
        {
            var suikaPrefab = _configProvider.SuikaConfig.GetPrefab(index);
            var suikaInstance = _objectResolver.Instantiate(suikaPrefab.gameObject, position, Quaternion.identity)
                .GetComponent<Suika>();
            suikaInstance.SetIndex(index);
            suikaInstance.Drop();
            suikaInstance.Land();

            AddToResetService(suikaInstance);
        }

        public Rigidbody2D CreatePlayerContinue(Vector3 position, Transform parent)
        {
            var index = _randomNumberGenerator.Current;
            var suikaPrefab = _configProvider.SuikaConfig.GetPrefab(index);
            var suikaInstance = _objectResolver
                .Instantiate(suikaPrefab.gameObject, position, Quaternion.identity, parent).GetComponent<Suika>();
            suikaInstance.SetIndex(index);

            AddToResetService(suikaInstance);
            var rigidbody2D = suikaInstance.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;

            return rigidbody2D;
        }

        private void AddScore(int index) => _scoreService.AddScore(index);

        private void AddToResetService(Suika suika) => _resetService.AddSuika(suika);
    }
}