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
            _stateManager.CheckTheProcess = true;
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
            _stateManager.ChangeState((int)StateType.SignInOrUp);
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
            _userManager.UIManager.ShowPanel(_userManager.PanelController.Login.gameObject);         
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _userManager.UIManager.HidePanel(_userManager.PanelController.Login.gameObject);
            _userManager.UIManager.HidePanel(_userManager.PanelController.Register.gameObject);
        }

        public override void Proceed()
        {
            base.Proceed();
            _stateManager.ChangeState((int)StateType.Loby);
        }
    }

    #endregion

    #region LobyState 

    public class UserLobyState : UserState
    {
        public UserLobyState(UserManager userManager) : base(StateType.Loby, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _userManager.UIManager.ShowPanel(_userManager.PanelController.Loby.gameObject);
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