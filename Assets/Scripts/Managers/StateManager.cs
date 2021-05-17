using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class StateManager
    {
        #region Fields

        private Dictionary<int, IState> _states;
        private IState _currentState;

        #endregion

        #region Properties

        public int CurrentState { get { return _currentState == null ? 0 : _currentState.ID; } }
        public bool CheckTheProcess { get; set; }

        #endregion

        #region Constructors

        public StateManager()
        {
            _currentState = null;
            _states = new Dictionary<int, IState>();
        }

        #endregion

        #region Methods

        //To start the situation.
        public void StartState(int stateID)
        {
            _currentState = _states[stateID];
            _currentState.Enter();
        }

        //To add a status.
        public void AddState(IState state)
        {
            _states.Add(state.ID, state);
        }

        //To clear the status list.
        public void ClearStates()
        {
            _states.Clear();
        }

        //To change the situation.
        public void ChangeState(int stateID)
        {
            if (_states.ContainsKey(stateID))
            {
                IState newState = _states[stateID];

                if (_currentState != null)
                    _currentState.Exit();

                _currentState = newState;
                _currentState.Enter();
            }
        }

        //Status update.
        public void Update()
        {
            if (_currentState != null)
                _currentState.Update();
        }

        //To change the state of the moment.
        public void SetStates(int currentState)
        {
            _currentState = _states[currentState];
        }

        #endregion
    }
}
