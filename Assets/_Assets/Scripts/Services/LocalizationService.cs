using _Assets.Scripts.Services.Yandex;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

namespace _Assets.Scripts.Services
{
    public class LocalizationService
    {
        private readonly YandexService _yandexService;

        private LocalizationService(YandexService yandexService)
        {
            _yandexService = yandexService;
        }

        public async UniTask SetLanguage(Language language)
        {
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
        }

        public async UniTask InitYandex(string str)
        {
            await LocalizationSettings.InitializationOperation.Task;

            if (str == "en")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            }
            else if (str == "ru")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            }
            else if (str == "tr")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
            }
            else
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            }

            await LocalizationSettings.SelectedLocaleAsync.Task;
        }

        public enum Language
        {
            English = 0,
            Russian = 1,
            Turkish = 2
        }
    }
}