using Players.StateMachine.PlayerStates;

namespace Players.StateMachine.Transitions
{
    public class SlideTransition : Transition
    {
        public SlideTransition(State nextState, PlayerInfo playerInfo)
        {
            NextState = nextState;
            PlayerInfo = playerInfo;
        }

        protected override bool CheckConditions()
        {
            return !PlayerInfo.IsGrounded
                && PlayerInfo.IsWallHooked
                && !PlayerInfo.IsJumpButtonPressed;
        }
    }
}