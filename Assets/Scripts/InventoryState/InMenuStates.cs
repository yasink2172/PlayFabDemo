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

    #region InventoryShowState 

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
            _userManager.PanelController.Shop.DestroyItems();
        }

        public override void Proceed()
        {
            base.Proceed();
            Exit();
        }
    }

    #endregion
}