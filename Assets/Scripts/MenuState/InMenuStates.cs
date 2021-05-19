using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    #region InventoryShowState 

    public class InventoryShowStates : InMenuState
    {
        public InventoryShowStates(UserManager userManager) : base(StateType.Inventory, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _userManager.UIManager.ShowPanel(_userManager.PanelController.Inventory.gameObject);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _userManager.UIManager.HidePanel(_userManager.PanelController.Inventory.gameObject);
            _userManager.PanelController.Inventory.DestroyItems();
        }

        public override void Proceed()
        {
            base.Proceed();
            Exit();
        }
    }

    #endregion

    #region ShopShowState 

    public class ShopShowStates : InMenuState
    {
        public ShopShowStates(UserManager userManager) : base(StateType.Shop, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _userManager.UIManager.ShowPanel(_userManager.PanelController.Shop.gameObject);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _userManager.UIManager.HidePanel(_userManager.PanelController.Shop.gameObject);
        }

        public override void Proceed()
        {
            base.Proceed();
            Exit();
        }
    }

    #endregion

    #region GameCamShowState 

    public class GameCamShowState : InMenuState
    {
        public GameCamShowState(UserManager userManager) : base(StateType.GameCam, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _userManager.UIManager.ShowPanel(_userManager.PanelController.GameCam.gameObject);
            _userManager.GameManager.ResetPanel();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _userManager.UIManager.HidePanel(_userManager.PanelController.GameCam.gameObject);
            _userManager.GameManager.ResetPanel();
        }

        public override void Proceed()
        {
            base.Proceed();
            Exit();
        }
    }

    #endregion

    #region LeaderboardsShowState 

    public class LeaderboardsShowState : InMenuState
    {
        public LeaderboardsShowState(UserManager userManager) : base(StateType.Leaderboards, userManager)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _userManager.UIManager.ShowPanel(_userManager.PanelController.Leaderboard.gameObject);
            _userManager.PanelController.Leaderboard.DestroyBar();
            _userManager.PlayFabManager.GetLeaderboarder();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            _userManager.UIManager.HidePanel(_userManager.PanelController.Leaderboard.gameObject);
            _userManager.PanelController.Leaderboard.DestroyBar();
        }

        public override void Proceed()
        {
            base.Proceed();
            Exit();
        }
    }

    #endregion
}