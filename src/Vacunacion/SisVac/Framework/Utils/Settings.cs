using Xamarin.Essentials;

namespace SisVac.Framework.Utils
{
    public static class Settings
    {
        /// <summary>
        /// Get/Set if the user is logged in
        /// </summary>
        public static bool IsLoggedIn
        {
            get => Preferences.Get(IsLoggedInKey, IsLoggedInDefault);
            set => Preferences.Set(IsLoggedInKey, value);
        }

        private const string IsLoggedInKey = "isLoggedIn_key";
        private static readonly bool IsLoggedInDefault = false;

        /// <summary>
        /// Get/Set if the it is the first time in the app
        /// </summary>
        public static bool IsFirstTime
        {
            get => Preferences.Get(IsFirstTimeKey, IsFirstTimeDefault);
            set => Preferences.Set(IsFirstTimeKey, value);
        }

        private const string IsFirstTimeKey = "isFirstTime_key";
        private static readonly bool IsFirstTimeDefault = true;

        /// <summary>
        /// To remove the key from settings
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveSetting(string key) => Preferences.Remove(key);

        /// <summary>
        /// To remove all settings
        /// </summary>
        public static void RemoveAllSettings() => Preferences.Clear();
    }
}
