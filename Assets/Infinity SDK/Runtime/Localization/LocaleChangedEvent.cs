namespace Infinity.Runtime.Localization
{
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