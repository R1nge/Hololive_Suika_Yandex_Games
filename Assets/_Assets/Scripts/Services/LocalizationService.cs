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
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                _currentLanguage = Language.English;
            }
            else if (str == "ru")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                _currentLanguage = Language.Russian;
            }
            else if (str == "tr")
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
                _currentLanguage = Language.Turkish;
            }
            else
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                _currentLanguage = Language.English;
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