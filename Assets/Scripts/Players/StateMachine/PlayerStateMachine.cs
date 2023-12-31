using System.Collections.Generic;
using Players.StateMachine.PlayerStates;
using Players.StateMachine.Transitions;
using UnityEngine;

namespace Players.StateMachine
{
    [RequireComponent(typeof(Player))]
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private AppearingState _appearingState;
        [SerializeField] private DesappearingState _desappearingState;
        [SerializeField] private FinishState _finishState;
        [SerializeField] private ExtraJumpState _extraJumpState;
        [SerializeField] private WallJumpState _wallJumpState;
        [SerializeField] private SlideState _slideState;
        [SerializeField] private DeceleratedWalkState _deceleratedWalkState;
        [SerializeField] private DeceleratedIdleState _deceleratedIdleState;
        [SerializeField] private IdleState _idleState;
        [SerializeField] private WalkState _walkState;
        [SerializeField] private JumpState _jumpState;
        [SerializeField] private HitState _hitState;
        [SerializeField] private FallState _fallState;
        [SerializeField] private HorizontalMover _horizontalMover;
        [SerializeField] private Fliper _playerFliper;

        private List<Transition> _transitions = new List<Transition>();
        private State _currentState;

        private void Awake()
        {
            _currentState = _appearingState;
            _currentState.Enter();

            InitializeTransitions();
        }

        private void OnEnable() =>
            _playerInfo.SomeParameterChanged += TryChangeState;

        private void OnDisable() =>
            _playerInfo.SomeParameterChanged -= TryChangeState;

        private void TryChangeState()
        {
            if (_currentState.IsCompleted())
            {
                State nextState;

                foreach (Transition transition in _transitions)
                {
                    if (transition.TryGetNextState(out nextState))
                    {
                        ChangeState(nextState);
                        return;
                    }
                }
            }
        }

        private void ChangeState(State nextState)
        {
            _currentState.Exit();

            _currentState = nextState;
            _currentState.Enter();

            _horizontalMover.enabled = nextState.IsMovable;
            _playerFliper.enabled = nextState.IsFlippable;
        }

        private void InitializeTransitions()
        {
            DesappearingTransition desappearingTransition = new DesappearingTransition(_desappearingState, _playerInfo);
            FinishTransition finishTransition = new FinishTransition(_finishState, _playerInfo);
            HitTransition hitTransition = new HitTransition(_hitState, _playerInfo);
            DeceleratedWalkTransition deceleratedWalkTransition = new DeceleratedWalkTransition(_deceleratedWalkState, _playerInfo);
            DeceleratedIdleTransition deceleratedIdleTransition = new DeceleratedIdleTransition(_deceleratedIdleState, _playerInfo);
            JumpTransition jumpTransition = new JumpTransition(_jumpState, _playerInfo);
            WallJumpTransition wallJumpTransition = new WallJumpTransition(_wallJumpState, _playerInfo);
            SlideTransition slideTransition = new SlideTransition(_slideState, _playerInfo);
            IdleTransition idleTransition = new IdleTransition(_idleState, _playerInfo);
            WalkTransition walkTransition = new WalkTransition(_walkState, _playerInfo);
            ExtraJumpTransition extraJumpTransition = new ExtraJumpTransition(_extraJumpState, _playerInfo);
            FallTransition fallTransition = new FallTransition(_fallState, _playerInfo);

            _transitions.Add(desappearingTransition);
            _transitions.Add(finishTransition);
            _transitions.Add(hitTransition);
            _transitions.Add(deceleratedWalkTransition);
            _transitions.Add(deceleratedIdleTransition);
            _transitions.Add(jumpTransition);
            _transitions.Add(extraJumpTransition);
            _transitions.Add(wallJumpTransition);
            _transitions.Add(slideTransition);
            _transitions.Add(idleTransition);
            _transitions.Add(walkTransition);
            _transitions.Add(fallTransition);
        }
    }
}