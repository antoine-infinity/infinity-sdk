namespace Infinity.Runtime.Core
{
    public class InfinityEnums
    {
        public class Core
        {
            public enum InstanceType
            {
                Client,
                Server,
                Host
            }

            public enum PlayAreaSize
            {
                Area4X4,
                Area5X5,
                Area6X6,
                Area6X8,
                Area8X8,
                Area8X10,
                Area10X10
            }

            public enum SessionState
            {
                Launching,
                Initialized,
                NetworkStarted,
                Lobby,
                InGame,
                Ending,
                Ended,
                Exiting
            }
    }
        
        public class Game
        {
            public enum Difficulty
            {
                Easy,
                Normal,
                Hard
            }
        }
    }
}