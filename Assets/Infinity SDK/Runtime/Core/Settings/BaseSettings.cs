namespace Infinity.Runtime.Core.Settings
{
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
}