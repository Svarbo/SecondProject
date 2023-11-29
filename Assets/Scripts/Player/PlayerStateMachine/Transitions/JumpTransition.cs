namespace Player
{
    public class JumpTransition : Transition
    {
        public JumpTransition(State nextState, Info playerInfo)
        {
            NextState = nextState;
            PlayerInfo = playerInfo;
        }

        protected override bool CheckConditions() =>
            PlayerInfo.IsGrounded && PlayerInfo.IsJumpButtonPressed;
    }
}