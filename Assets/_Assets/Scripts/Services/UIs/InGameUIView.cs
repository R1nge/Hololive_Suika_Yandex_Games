using System;
using _Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class InGameUIView : MonoBehaviour
    {
        [SerializeField] private Image nextSuikaImage;
        [SerializeField] private Button pauseButton;
        [Inject] private InGameUIController _inGameUIController;
        [Inject] private RandomNumberGenerator _randomNumberGenerator;
        [Inject] private ConfigProvider _configProvider;

        private void Awake()
        {
            pauseButton.onClick.AddListener(Pause);
        }

        private void Start()
        {
            _randomNumberGenerator.OnSuikaPicked += SuikaPicked;
            SuikaPicked(0, 0, _randomNumberGenerator.Next);
        }

        private void SuikaPicked(int previous, int current, int next)
        {
            nextSuikaImage.sprite = _configProvider.SuikaConfig.GetSprite(_randomNumberGenerator.Next);
        }

        private void Pause() => _inGameUIController.Pause();

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(Pause);
            _randomNumberGenerator.OnSuikaPicked -= SuikaPicked;
        }
    }
}