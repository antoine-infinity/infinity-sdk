using System;
using System.Linq;
using Infinity.Runtime.Core.Logging;
using UnityEngine;

namespace Infinity.Runtime.Utils
{
    public static class AndroidSettingFetcher
    {
        public static string Fetch(string key, string defaultValue = default)
        {
            using var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            var context = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            var systemGlobal = new AndroidJavaClass("android.provider.Settings$Global");

            var value = systemGlobal.CallStatic<string>("getString",
                context.Call<AndroidJavaObject>("getContentResolver"), key);

            InfinityLog.Info(typeof(AndroidSettingFetcher), $"Got [{key}: {value}] from android global settings");
            return value ?? defaultValue;
        }

        public static T FetchEnum<T>(string key, T defaultValue = default) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var value = Fetch(key);

            var enumValue = Enum.TryParse(typeof(T), value, out var result) ? result : defaultValue;
            return (T)enumValue;
        }

        public static int FetchInt(string key, int defaultValue = default)
        {
            var stringValue = Fetch(key);
            return int.TryParse(stringValue, out var result) ? result : defaultValue;
        }

        public static float FetchFloat(string key, float defaultValue = default)
        {
            var stringValue = Fetch(key);
            return float.TryParse(stringValue, out var result) ? result : defaultValue;
        }

        public static bool FetchBool(string key, bool defaultValue = default)
        {
            var stringValue = Fetch(key);
            return bool.TryParse(stringValue, out var result) ? result : defaultValue;
        }
    }
}