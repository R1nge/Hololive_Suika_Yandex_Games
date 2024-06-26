using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using YG;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource mergeSource;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private YandexService _yandexService;
        private int _lastSongIndex;
        private readonly List<int> _mergeSoundsQueue = new(10);
        private bool _queueIsPlaying;
        private CancellationTokenSource _cancellationSource = new();
        private bool _paused;
        private bool _init;


        private float _vfxVolume = .1f;
        public float VfxVolume => _vfxVolume;
        private float _musicVolume = .1f;
        public float MusicVolume => _musicVolume;
        public event Action OnSongChanged;

        public string GetSongName() => _configProvider.SoundsConfig.GetSong(_lastSongIndex).title;

        public bool IsMusicPlaying => musicSource.isPlaying && !_paused;

        public int LastSongIndex => _lastSongIndex;

        public void Init()
        {
            _init = true;
            _yandexService.OnFullScreenAdShown += PauseAudioAd;
            _yandexService.OnFullScreenAdClosed += UnPauseAudioAd;
            
            _musicVolume = YandexGame.savesData.musicVolume;
            _vfxVolume = YandexGame.savesData.vfxVolume;
            musicSource.volume = _musicVolume;
            mergeSource.volume = _vfxVolume;
            
            PlayRandomSong().Forget();
        }

        private void UnPauseAudioAd()
        {
            _paused = false;
            musicSource.UnPause();
        }

        private void PauseAudioAd()
        {
            _paused = true;
            musicSource.Pause();
        }

        public void ChangeMusicVolume(float volume)
        {
            _musicVolume = volume;
            musicSource.volume = volume;
            
            YandexGame.savesData.musicVolume = volume;
            YandexGame.SaveProgress();
        }

        public void ChangeSoundVolume(float volume)
        {
            _vfxVolume = volume;
            mergeSource.volume = volume;
            
            YandexGame.savesData.vfxVolume = volume;
            YandexGame.SaveProgress();
        }

        public void PauseMusic()
        {
            _paused = true;
            musicSource.Pause();
        }

        public async UniTask PlaySong(int index)
        {
            if (_musicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            if (index == _lastSongIndex && musicSource.clip != null && !Ended() && _paused)
            {
                musicSource.UnPause();
                _paused = false;
                Debug.LogWarning("Continue last played song (Unpause)");
                return;
            }

            _paused = false;
            _lastSongIndex = index;
            musicSource.clip = null;

            var audioData = _configProvider.SoundsConfig.GetSong(index);

            _cancellationSource?.Cancel();
            _cancellationSource = new CancellationTokenSource();

            var reference = audioData.audioClip;
            var song = await reference.LoadAssetAsync<AudioClip>();

            musicSource.clip = song;
            musicSource.volume = _musicVolume;
            musicSource.Play();
            OnSongChanged?.Invoke();
        }

        public async UniTask PlayRandomSong()
        {
            if (_musicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            var index = Random.Range(0, _configProvider.SoundsConfig.SongsLength);
            await PlaySong(index);
        }

        private bool Ended()
        {
            return musicSource.clip == null || musicSource.time >= musicSource.clip.length;
        }

        public void PlaySelectedSong()
        {
            if (_musicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            PlaySong(_lastSongIndex);
        }

        public void PlayNextSong()
        {
            _lastSongIndex = (LastSongIndex + 1) % _configProvider.SoundsConfig.SongsLength;
            PlaySong(_lastSongIndex);
        }

        public void PlayPreviousSong()
        {
            _lastSongIndex = (LastSongIndex - 1 + _configProvider.SoundsConfig.SongsLength) %
                             _configProvider.SoundsConfig.SongsLength;
            PlaySong(_lastSongIndex);
        }

        public void AddToMergeSoundsQueue(int index)
        {
            if (_vfxVolume <= 0)
            {
                Debug.LogWarning("Sounds are disabled");
                return;
            }

            _mergeSoundsQueue.Add(index);

            PlayMergeFromQueue().Forget();
        }

        private async UniTask PlayMergeFromQueue()
        {
            if (_queueIsPlaying)
            {
                Debug.LogWarning("The queue is already playing, nothing to do");
                return;
            }

            _queueIsPlaying = true;

            if (_vfxVolume <= 0)
            {
                Debug.LogWarning("Sounds are disabled");
                return;
            }

            if (_mergeSoundsQueue.Count == 0)
            {
                Debug.LogWarning("The queue is empty, nothing to do");
                return;
            }

            await UniTask.WaitForSeconds(0.2f);

            var index = _mergeSoundsQueue.Max();

            var clip = await _configProvider.SuikaConfig.GetSound(index);

            mergeSource.volume = _vfxVolume;
            mergeSource.clip = clip;
            mergeSource.Play();
            await UniTask.Delay((int)(clip.length * 1000), cancellationToken: _cancellationSource.Token);
            _queueIsPlaying = false;
            _mergeSoundsQueue.Clear();
        }


        private void OnDestroy()
        {
            _yandexService.OnFullScreenAdShown -= PauseAudioAd;
            _yandexService.OnFullScreenAdClosed -= UnPauseAudioAd;
        }
    }
}