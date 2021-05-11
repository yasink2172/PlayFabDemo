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
            StateManager.ChangeState((int)StateType.SignInOrUp);
        }
    }

    #endregion

    #region SignInOrUpState 

    public class UserSignInOrUpState : UserState
    {
        public UserSignInOrUpState(UserManager userManager) : base(StateType.SignInOrUp, userManager)
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
            UserManager.UIManager.HidePanel(UserManager.PanelController.Login.gameObject);
            UserManager.UIManager.HidePanel(UserManager.PanelController.Register.gameObject);
        }

        public override void Proceed()
        {
            base.Proceed();
            StateManager.ChangeState((int)StateType.Loby);
        }
    }

    #endregion

    #region SignInOrUpState 

    public class UserLobyState : UserState
    {
        public UserLobyState(UserManager userManager) : base(StateType.Loby, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            UserManager.UIManager.ShowPanel(UserManager.PanelController.Loby.gameObject);
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