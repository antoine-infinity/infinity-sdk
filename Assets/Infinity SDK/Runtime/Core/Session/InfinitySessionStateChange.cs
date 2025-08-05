namespace Infinity.Runtime.Core.Session
{
    public class InfinitySessionStateChangeEvent
    {
        public readonly InfinityEnums.Core.SessionState OldState;
        public readonly InfinityEnums.Core.SessionState NewState;

        public InfinitySessionStateChangeEvent(
            InfinityEnums.Core.SessionState oldState,
            InfinityEnums.Core.SessionState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}