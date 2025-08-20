namespace Infinity.Runtime.Core.Settings
{
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
}