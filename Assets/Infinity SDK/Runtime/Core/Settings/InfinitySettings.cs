using System;
using Infinity.Runtime.Core.Logging;
using Infinity.Runtime.Utils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Infinity.Runtime.Core.Settings
{
    public static class InfinitySettings
    {
        public static BaseSettings Base;
        public static GameSettings Game;
        public static LocaleSettings Locale;

        public static void Initialize()
        {
            Base = new BaseSettings();
            Game = new GameSettings();
            Locale = new LocaleSettings();

            switch (InfinitySDK.Properties.readSettingsFrom)
            {
                case SettingsSource.ScriptableObject:
                    InfinityLog.Info(typeof(InfinitySettings), $"Initializing settings from properties scriptable object.");
                    InitializeFromScriptable();
                    break;
                case SettingsSource.PlayerPrefs:
                    InfinityLog.Info(typeof(InfinitySettings), $"Initializing settings from player prefs.");
                    InitializeFromPlayerPrefs();
                    break;
                case SettingsSource.AndroidGlobalSettings:
                    InfinityLog.Info(typeof(InfinitySettings), $"Initializing settings from android global settings.");
                    InitializeFromAndroidGlobalSettings();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void InitializeFromScriptable()
        {
            Base.InstanceType.Value = InfinitySDK.Properties.instanceType;
            Base.PlayAreaSize.Value = InfinitySDK.Properties.playAreaSize;
            Base.ServerIp.Value = InfinitySDK.Properties.serverIp;
            Base.ServerPort.Value = InfinitySDK.Properties.serverPort;
            Base.FirstScene.Value = InfinitySDK.Properties.firstScene;
            Base.DualScreen.Value = InfinitySDK.Properties.dualScreen;
            Base.AutoStart.Value = InfinitySDK.Properties.autoStart;
            Base.AutoStop.Value = InfinitySDK.Properties.autoStop;
            Base.AutoStopTimer.Value = InfinitySDK.Properties.autoStopTimer;

            Game.Difficulty.Value = InfinitySDK.Properties.difficulty;
            Game.SkipEnabled.Value = InfinitySDK.Properties.skipEnabled;
            Game.HintEnabled.Value = InfinitySDK.Properties.hintEnabled;
            Game.Duration.Value = InfinitySDK.Properties.duration;

            Locale.DefaultLocale.Value = InfinitySDK.Properties.defaultLocale;
            Locale.TextLocale.Value = InfinitySDK.Properties.textLocale;
            Locale.AudioLocale.Value = InfinitySDK.Properties.audioLocale;
        }

        private static void InitializeFromPlayerPrefs()
        {
            Base.InstanceType.Value =
                (InfinityEnums.Core.InstanceType)PlayerPrefs.GetInt(Base.InstanceType.SettingKey, 1);
            Base.PlayAreaSize.Value =
                (InfinityEnums.Core.PlayAreaSize)PlayerPrefs.GetInt(Base.PlayAreaSize.SettingKey, 1);
            Base.ServerIp.Value = PlayerPrefs.GetString(Base.ServerIp.SettingKey, "127.0.0.1");
            Base.ServerPort.Value = (ushort)PlayerPrefs.GetInt(Base.ServerPort.SettingKey, 7777);
            Base.FirstScene.Value = PlayerPrefs.GetString(Base.FirstScene.SettingKey, "Lobby");
            Base.DualScreen.Value = PlayerPrefs.GetInt(Base.DualScreen.SettingKey, 0) != 0;
            Base.AutoStart.Value = PlayerPrefs.GetInt(Base.AutoStart.SettingKey, 0) != 0;
            Base.AutoStop.Value = PlayerPrefs.GetInt(Base.AutoStop.SettingKey, 0) != 0;
            Base.AutoStopTimer.Value = PlayerPrefs.GetInt(Base.AutoStopTimer.SettingKey, 15);

            Game.Difficulty.Value = (InfinityEnums.Game.Difficulty)PlayerPrefs.GetInt(Game.Difficulty.SettingKey, 1);
            Game.SkipEnabled.Value = PlayerPrefs.GetInt(Game.SkipEnabled.SettingKey, 0) != 0;
            Game.HintEnabled.Value = PlayerPrefs.GetInt(Game.HintEnabled.SettingKey, 0) != 0;
            Game.Duration.Value = PlayerPrefs.GetFloat(Game.Duration.SettingKey, 3600);
            
            Locale.DefaultLocale.Value = PlayerPrefs.GetString(Locale.DefaultLocale.SettingKey, "en-US");
            Locale.TextLocale.Value = PlayerPrefs.GetString(Locale.TextLocale.SettingKey, "en-US");
            Locale.AudioLocale.Value = PlayerPrefs.GetString(Locale.AudioLocale.SettingKey, "en-US");
        }

        private static void InitializeFromAndroidGlobalSettings()
        {
#if UNITY_ANDROID
            Base.InstanceType.Value =
                AndroidSettingFetcher.FetchEnum(Base.InstanceType.SettingKey, InfinityEnums.Core.InstanceType.Client);
            Base.PlayAreaSize.Value =
                AndroidSettingFetcher.FetchEnum(Base.PlayAreaSize.SettingKey, InfinityEnums.Core.PlayAreaSize.Area5X5);

            Base.ServerIp.Value = AndroidSettingFetcher.Fetch(Base.ServerIp.SettingKey, "127.0.0.1");
            Base.ServerPort.Value = (ushort)AndroidSettingFetcher.FetchInt(Base.ServerPort.SettingKey, 7777);
            Base.FirstScene.Value = AndroidSettingFetcher.Fetch(Base.FirstScene.SettingKey, "Lobby");
            Base.DualScreen.Value = AndroidSettingFetcher.FetchBool(Base.DualScreen.SettingKey, false);
            Base.AutoStart.Value = AndroidSettingFetcher.FetchBool(Base.AutoStart.SettingKey, true);
            Base.AutoStop.Value = AndroidSettingFetcher.FetchBool(Base.AutoStop.SettingKey, true);
            Base.AutoStopTimer.Value = AndroidSettingFetcher.FetchInt(Base.AutoStopTimer.SettingKey, 15);

            Game.Difficulty.Value =
                AndroidSettingFetcher.FetchEnum(Game.Difficulty.SettingKey, InfinityEnums.Game.Difficulty.Normal);
            Game.SkipEnabled.Value = AndroidSettingFetcher.FetchBool(Game.SkipEnabled.SettingKey, false);
            Game.HintEnabled.Value = AndroidSettingFetcher.FetchBool(Game.HintEnabled.SettingKey, true);
            Game.Duration.Value = AndroidSettingFetcher.FetchFloat(Game.Duration.SettingKey, 3600);
            
            Locale.DefaultLocale.Value = AndroidSettingFetcher.Fetch(Locale.DefaultLocale.SettingKey, "en-US");
            Locale.TextLocale.Value = AndroidSettingFetcher.Fetch(Locale.TextLocale.SettingKey, "en-US");
            Locale.AudioLocale.Value = AndroidSettingFetcher.Fetch(Locale.AudioLocale.SettingKey, "en-US");
#else
            InfinityLog.Warning(typeof(InfinitySettings),
                $"Trying to initialize settings from Android Global Settings on a non Android platform. Using default values....");
#endif
        }

        public static void DisplaySettings()
        {
            InfinityLog.Info(typeof(InfinitySettings), $"[Infinity Settings]\n\n{Base}\n\n{Game}");
        }
    }

    public class BaseSettings
    {
        public InfinitySettingEntry<InfinityEnums.Core.InstanceType> InstanceType =
            new("instance_type", InfinityEnums.Core.InstanceType.Server);

        public InfinitySettingEntry<InfinityEnums.Core.PlayAreaSize> PlayAreaSize = new("play_area",
            InfinityEnums.Core.PlayAreaSize.Area5X5);

        public InfinitySettingEntry<string> ServerIp = new("server_ip", "127.0.0.1");
        public InfinitySettingEntry<ushort> ServerPort = new("server_port", 7777);
        public InfinitySettingEntry<string> FirstScene = new("first_scene", "Lobby");
        public InfinitySettingEntry<bool> DualScreen = new("dual_screen", false);
        public InfinitySettingEntry<bool> AutoStart = new("auto_start", true);
        public InfinitySettingEntry<bool> AutoStop = new("auto_stop", true);
        public InfinitySettingEntry<int> AutoStopTimer = new("auto_stop_timer", 15);

        public override string ToString()
        {
            return
                $"{PlayAreaSize}\n{ServerIp}\n{ServerPort}" +
                $"\n{FirstScene}\n{DualScreen}\n{AutoStart}" +
                $"\n{AutoStop}\n{AutoStopTimer}";
        }
    }

    public class GameSettings
    {
        public InfinitySettingEntry<InfinityEnums.Game.Difficulty> Difficulty = new("difficulty",
            InfinityEnums.Game.Difficulty.Normal);

        public InfinitySettingEntry<bool> SkipEnabled = new("skip_enabled", false);
        public InfinitySettingEntry<bool> HintEnabled = new("hint_enabled", true);
        public InfinitySettingEntry<float> Duration = new("duration", 3600f);

        public override string ToString()
        {
            return $"{Difficulty}\n{SkipEnabled}\n{HintEnabled}\n{Duration}";
        }
    }

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