using System;
using _Assets.Scripts.Misc;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Song Config", menuName = "Configs/Song Config", order = 0)]
    public class SoundsConfig : ScriptableObject
    {
        [SerializeField] private Song[] songs;
        [SerializeField] private Sound[] sounds;

        public int SongsLength => songs.Length;
        public int SoundsLength => sounds.Length;

        public Song GetSong(int index)
        {
            index = Mathf.Clamp(index, 0, songs.Length - 1);
            return songs[index];
        }
        
        public Sound GetSound(int index)
        {
            index = Mathf.Clamp(index, 0, sounds.Length - 1);
            return sounds[index];
        }
    }

    [Serializable]
    public struct Song
    {
        public float volume;
        public string title;
        public AssetReferenceAudioClip audioClip;
    }

    [Serializable]
    public struct Sound
    {
        public float volume;
        public string title;
        public AssetReferenceAudioClip audioClip;
    }
}