using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    #region StartState 

    public class UserStartState : UserState
    {
        public UserStartState(UserManager userManager) : base(StateType.Start, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StateManager.CheckTheProcess = true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Proceed()
        {
            base.Proceed();
            StateManager.ChangeState((int)StateType.SignIn);
        }
    }

    #endregion

    #region SignInState 

    public class UserSignInState : UserState
    {
        public UserSignInState(UserManager userManager) : base(StateType.SignIn, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            UserManager.UIManager.ShowPanel(UserManager.PanelController.Login.gameObject);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Proceed()
        {
            base.Proceed();
        }
    }

    #endregion

    #region SignUpState 

    public class UserSignUpState : UserState
    {
        public UserSignUpState(UserManager userManager) : base(StateType.SignUp, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Proceed()
        {
            base.Proceed();
        }
    }

    #endregion

}