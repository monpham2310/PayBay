using System;
using Windows.Storage;

namespace SplitViewSampleWithMVVM.Utilities.Helpers
{
    public class SettingsHelper
    {
        /// <summary>
        /// Set a setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">setting key</param>
        /// <param name="value">setting value</param>
        public static void SetSetting<T>(string key, T value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        /// <summary>
        /// Get a value of a setting
        /// </summary>
        /// <typeparam name="T">setting value data type</typeparam>
        /// <param name="key">setting key</param>
        /// <returns></returns>
        public static T GetSetting<T>(string key)
        {
            try
            {
                return (T) ApplicationData.Current.LocalSettings.Values[key];
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }

    public sealed class SettingKey
    {
        //Setup setting key. So you don't have to use plaintext as settingkey again
        public static readonly SettingKey SaveMode = new SettingKey("SaveMode");
        public static readonly SettingKey SavePath = new SettingKey("SavePath");
        public static readonly SettingKey SaveToken = new SettingKey("SaveToken");
        public static readonly SettingKey DatabaseVersion = new SettingKey("DatabaseVersion");

        private SettingKey(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    /// <summary>
    /// Sample class to get and set settings
    /// </summary>
    public class SettingManager
    {
        public static int GetSaveMode()
        {
            return SettingsHelper.GetSetting<int>(SettingKey.SaveMode.Value);
        }

        /// <summary>
        /// Set save mode
        /// 1: Save in the same folder
        /// 2: Choose where to save
        /// 3: Save in specific folder
        /// </summary>
        /// <param name="saveMode">save mode</param>
        /// <param name="path">path to specific folder. Apply for 3</param>
        /// <param name="token">token to access later. Apply for 3</param>
        public static void SetSaveMode(int saveMode, string path = null, string token = null)
        {
            SettingsHelper.SetSetting(SettingKey.SaveMode.Value, saveMode);
            if (path != null)
            {
                SettingsHelper.SetSetting(SettingKey.SavePath.Value, path);
                SettingsHelper.SetSetting(SettingKey.SaveToken.Value, token);
            }
        }

        public static string GetSavePath()
        {
            //Use SettingKey class to avoid using plaintext as setting key
            return SettingsHelper.GetSetting<string>(SettingKey.SavePath.Value);
        }

        public static string GetSaveToken()
        {
            return SettingsHelper.GetSetting<string>(SettingKey.SaveToken.Value);
        }
    }
}
