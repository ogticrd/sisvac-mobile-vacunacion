using System;
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
            get
            {
                var loggedInDate = Preferences.Get(IsLoggedInDateKey, IsLoggedInDateDefault);
                var expirationDate = loggedInDate.AddHours(12);

                if (expirationDate > DateTime.Now)
                {
                    return Preferences.Get(IsLoggedInKey, IsLoggedInDefault);
                }

                RemoveAllSettings();
                return false;
            }
            set 
            { 
                Preferences.Set(IsLoggedInKey, value);
                Preferences.Set(IsLoggedInDateKey, DateTime.Now);
            }
        }

        private const string IsLoggedInKey = "isLoggedIn_key";
        private static readonly bool IsLoggedInDefault = false;

        private const string IsLoggedInDateKey = "isLoggedInDate_key";
        private static readonly DateTime IsLoggedInDateDefault = new DateTime();

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
