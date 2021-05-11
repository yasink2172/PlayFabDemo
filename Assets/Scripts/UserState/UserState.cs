using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class UserState : IState
    {
        #region Fields

        public enum StateType
        {
            Start = 0,
            SignInOrUp,
            Loby,
        }

        protected StateType mStateType;
        protected StateManager StateManager;
        protected UserManager UserManager;

        #endregion

        #region Constructors

        public UserState(StateType stateType, UserManager userManager)
        {
            mStateType = stateType;
            UserManager = userManager;
            StateManager = UserManager.StateManager;
        }

        #endregion

        #region Interface

        public int ID { get { return (int)mStateType; } }

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {
            if (StateManager.CheckTheProcess)
            {
                Proceed();
                StateManager.CheckTheProcess = false;
            }
        }

        public virtual void Exit()
        {

        }

        public virtual void Proceed()
        {

        }

        #endregion
    }
}
