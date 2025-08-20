namespace Infinity.Runtime.Core.Settings
{
    public class LocaleSettings
    {
        public InfinitySettingEntry<string> DefaultLocale = new("default_locale", "en-US");
        public InfinitySettingEntry<string> TextLocale = new("text_locale", "en-US");
        public InfinitySettingEntry<string> AudioLocale = new("audio_locale", "en-US");

        public override string ToString()
        {
            return $"{DefaultLocale}\n{TextLocale}\n{AudioLocale}";
        }
    }
}