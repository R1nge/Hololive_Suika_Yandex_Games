using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

namespace _Assets.Scripts.Services
{
    public class LocalizationService
    {
        private Language _currentLanguage;

        public Language CurrentLanguage => _currentLanguage;

        public async UniTask SetLanguage(Language language)
        {
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
            _currentLanguage = language;
        }

        public async UniTask InitYandex(string str)
        {
            await LocalizationSettings.InitializationOperation.Task;

            if (str == "en")
            {
                await SetLanguage(Language.English);
            }
            else if (str == "ru")
            {
                await SetLanguage(Language.Russian);
            }
            else if (str == "tr")
            {
                await SetLanguage(Language.Turkish);
            }
            else
            {
                await SetLanguage(Language.English);
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