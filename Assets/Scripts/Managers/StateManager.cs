using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class StateManager
    {
        #region Fields

        private Dictionary<int, IState> mStates;
        private IState mCurrentState;

        #endregion

        #region Properties

        public int CurrentState { get { return mCurrentState == null ? 0 : mCurrentState.ID; } }
        public bool CheckTheProcess { get; set; }

        #endregion

        #region Constructors

        public StateManager()
        {
            mCurrentState = null;
            mStates = new Dictionary<int, IState>();
        }

        #endregion

        #region Methods

        public void StartState(int stateID)
        {
            mCurrentState = mStates[stateID];
            mCurrentState.Enter();
        }

        public void AddState(IState state)
        {
            mStates.Add(state.ID, state);
        }

        public void ChangeState(int stateID)
        {
            if (mStates.ContainsKey(stateID))
            {
                IState newState = mStates[stateID];

                if (mCurrentState != null)
                    mCurrentState.Exit();

                mCurrentState = newState;
                mCurrentState.Enter();
            }
        }

        public void Update()
        {
            if (mCurrentState != null)
                mCurrentState.Update();
        }

        public void SetStates(int currentState)
        {
            mCurrentState = mStates[currentState];
        }

        #endregion
    }
}
