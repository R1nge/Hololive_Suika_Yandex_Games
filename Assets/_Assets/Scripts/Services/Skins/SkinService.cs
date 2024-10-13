using _Assets.Scripts.Configs;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService
    {
        private readonly ConfigProvider _configProvider;
        private SuikaSkinData[] _suikaSkinData;
        private SuikaSkinData[] _selected;

        private SkinService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Init()
        {
            _selected = new SuikaSkinData[12];
            _suikaSkinData = new SuikaSkinData[_configProvider.SuikaConfig.SuikaSkins.Count];

            var i = 0;
            foreach (var skin in _configProvider.SuikaConfig.SuikaSkins)
            {
                _suikaSkinData[i].SuikaSkin = skin;
                _suikaSkinData[i].IsLocked = Random.Range(0, 2) == 0;
                i++;
            }
        }

        public void Swap(int index, int targetIndex)
        {
            (_selected[index], _selected[targetIndex]) = (_selected[targetIndex], _selected[index]);
        }

        public void Set(int skinIndex, int positionIndex)
        {
            _selected[positionIndex] = _suikaSkinData[skinIndex];
        }

        public void Reset()
        {
            for (int i = 0; i < _selected.Length; i++)
            {
                _selected[i] = _suikaSkinData[i];
            }
        }
    }

    public struct SuikaSkinData
    {
        public SuikaConfig.SuikaSkin SuikaSkin;
        public bool IsLocked;
    }
}