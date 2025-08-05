using UnityEngine;

namespace Infinity.Runtime.Core
{
    public class InfinitySDKProperties : ScriptableObject
    {
        public SettingsSource readSettingsFrom = SettingsSource.ScriptableObject;
        public bool logSettingAfterInit = false;
        [Space(10)]
        
        public InfinityEnums.Core.InstanceType instanceType;
        public InfinityEnums.Core.PlayAreaSize playAreaSize;
        public string serverIp = "127.0.0.1";
        public ushort serverPort = 7777;
        public string firstScene = "Lobby";
        public bool dualScreen = false;
        public bool autoStart = true;
        public bool autoStop = true;
        public int autoStopTimer = 15;
        public InfinityEnums.Game.Difficulty difficulty = InfinityEnums.Game.Difficulty.Normal;
        public bool skipEnabled = false;
        public bool hintEnabled = true;
        public float duration = 3600f;
    }

    public enum SettingsSource
    {
        ScriptableObject,
        PlayerPrefs,
        AndroidGlobalSettings
    }
}