using System;
using System.Collections.Generic;
using Infrastructure.Payloads;
using Infrastructure.States;

namespace Infrastructure.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private IState _currentState;
        private readonly Dictionary<Type, IState> _states;

        public StateMachine(Dictionary<Type, IState> states) =>
            _states = states ?? throw new InvalidOperationException();

        public void Enter(Type state)
        {
            if (_states.ContainsKey(state) == false)
                throw new KeyNotFoundException();

            _currentState?.Exit();
            _currentState = _states[state];
            _currentState?.Enter();
        }
        
        public void Enter<T>(T payload) where T : IPayloadForState
        {
            foreach (KeyValuePair<Type, IState> state in _states)
            {
                if (state.Value is IPayloadState<T> newState)
                {
                    _currentState?.Exit();
                    _currentState = newState;
                    newState.Enter(payload);
                    return;
                }
            }
            
            throw new InvalidOperationException("Dictinary value not found " + payload);
        }
        
        public void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
        }
        public void FixedUpdate(float deltaTime)
        {
            _currentState.FixedUpdate(deltaTime);
        }
        public void LateUpdate(float deltaTime)
        {
            _currentState.LateUpdate(deltaTime);
        }
    }
}