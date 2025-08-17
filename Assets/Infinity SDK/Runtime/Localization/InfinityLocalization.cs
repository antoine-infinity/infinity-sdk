using System.Collections.Generic;
using Infinity.Runtime.Core.Events;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.Core.Settings;
using UnityEngine;

namespace Infinity.Runtime.Localization
{
    public static class InfinityLocalization
    {
        public static string DefaultLocale => InfinitySettings.Locale.DefaultLocale.Value;
        public static string TextLocale => InfinitySettings.Locale.TextLocale.Value;
        public static string AudioLocale => InfinitySettings.Locale.AudioLocale.Value;
        private static Dictionary<string, string> _loadedLocale = new();
        
        public static void Initialize()
        {
            InfinityLog.Info(typeof(InfinityLocalization), $"Initializing Localization");

            InfinitySettings.Locale.DefaultLocale.OnValueChanged += DefaultLocaleChanged;
            InfinitySettings.Locale.TextLocale.OnValueChanged += TextLocaleChanged;
            InfinitySettings.Locale.AudioLocale.OnValueChanged += AudioLocaleChanged;
            
            LoadLocalization();
        }

        private static void LoadLocalization()
        {
            var file = Resources.Load<TextAsset>($"Localization/locale-{TextLocale}");
            InfinityLog.Info(typeof(InfinityLocalization), $"Loaded locale: {TextLocale} - {file.bytes.Length} bytes");
            _loadedLocale = JsonUtility.FromJson<Dictionary<string, string>>(file.text);
            
            InfinityLog.Info(typeof(InfinityLocalization), Localize("session.start"));
        }
        
        private static void DefaultLocaleChanged(string oldValue, string newValue)
        {
            InfinityEventBus.Publish(new LocaleChangedEvent(LocaleChangedEvent.LocaleChangedEventType.Default, 
                oldValue, newValue));
        }
        private static void TextLocaleChanged(string oldValue, string newValue)
        {
            InfinityEventBus.Publish(new LocaleChangedEvent(LocaleChangedEvent.LocaleChangedEventType.Text, 
                oldValue, newValue));
        }
        private static void AudioLocaleChanged(string oldValue, string newValue)
        {
            InfinityEventBus.Publish(new LocaleChangedEvent(LocaleChangedEvent.LocaleChangedEventType.Audio, 
                oldValue, newValue));
        }

        public static string Localize(string key)
        {
            return _loadedLocale.GetValueOrDefault(key, key);
        }
    }

    public class LocaleChangedEvent
    {
        public LocaleChangedEventType Type;
        public string OldLocale;
        public string NewLocale;

        public LocaleChangedEvent(LocaleChangedEventType type, string oldLocale, string newLocale)
        {
            Type = type;
            OldLocale = oldLocale;
            NewLocale = newLocale;
        }

        public enum LocaleChangedEventType
        {
            Default,
            Text,
            Audio
        }
    }
}