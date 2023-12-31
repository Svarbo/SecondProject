using Players.StateMachine.PlayerStates;

namespace Players.StateMachine.Transitions
{
    public abstract class Transition
    {
        protected State NextState;
        protected PlayerInfo PlayerInfo;

        private bool _needTransit;

        public bool TryGetNextState(out State nextState)
        {
            _needTransit = CheckConditions();

            if (_needTransit)
                nextState = NextState;
            else
                nextState = null;

            return _needTransit;
        }

        protected abstract bool CheckConditions();
    }
}