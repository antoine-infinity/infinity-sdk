using System;
using System.Collections.Generic;

namespace Infinity.Runtime.Localization
{
    [Serializable]
    public class LocaleEntry
    {
        public string key;
        public string value;
    }

    [Serializable]
    public class LocaleFile
    {
        public string locale;
        public List<LocaleEntry> entries;
    }
}