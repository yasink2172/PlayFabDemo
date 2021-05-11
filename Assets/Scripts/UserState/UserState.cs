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
        }

        protected StateType myStateType;

        #endregion

        #region Constructors

        public UserState(StateType stateType)
        {
            myStateType = stateType;
        }

        #endregion

        #region Interface

        public int ID { get { return (int)myStateType; } }

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

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
