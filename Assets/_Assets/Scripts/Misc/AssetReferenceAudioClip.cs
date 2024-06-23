using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Assets.Scripts.Misc
{
    [Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid)
        {
        }
    }
}