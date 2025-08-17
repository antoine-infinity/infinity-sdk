using UnityEngine;

namespace Infinity.Runtime.Core
{
    public class InfinitySDKProperties : ScriptableObject
    {
        [Header("Properties Settings")]
        public SettingsSource readSettingsFrom = SettingsSource.ScriptableObject;
        public bool logSettingAfterInit = false;
        
        [Space(10), Header("Connection Settings")]
        public InfinityEnums.Core.InstanceType instanceType;
        public string serverIp = "127.0.0.1";
        public ushort serverPort = 7777;
        
        [Space(10), Header("Base Settings")]
        public InfinityEnums.Core.PlayAreaSize playAreaSize;
        public string firstScene = "Lobby";
        public bool dualScreen = false;
        public bool autoStart = true;
        public bool autoStop = true;
        public int autoStopTimer = 15;
        
        [Space(10), Header("Game Settings")]
        public InfinityEnums.Game.Difficulty difficulty = InfinityEnums.Game.Difficulty.Normal;
        public bool skipEnabled = false;
        public bool hintEnabled = true;
        public float duration = 3600f;
        
        [Space(10), Header("Locale settings")]
        public string defaultLocale = "en-US";
        public string textLocale = "en-US";
        public string audioLocale = "en-US";
    }

    public enum SettingsSource
    {
        ScriptableObject,
        PlayerPrefs,
        AndroidGlobalSettings
    }
}