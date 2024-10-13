using _Assets.Scripts.Configs;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService
    {
        private readonly ConfigProvider _configProvider;

        private SkinService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
    }

    public struct SuikaSkinData
    {
        public SuikaConfig.SuikaSkin SuikaSkin;
        public bool IsLocked;
    }
}