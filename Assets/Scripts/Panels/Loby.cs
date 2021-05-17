using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Newtonsoft.Json;

namespace Demo.UI
{
    public class Loby : MonoBehaviour
    {
        #region Fields

        private UserManager _userManager;
        private UIManager _uiManager;

        public SetItem[] items;

        public TextMeshProUGUI PlayerID;
        public TextMeshProUGUI PlayerXP;

        public Button InventoryButton;
        public Button ShopButton;

        #endregion

        #region Methods

        void Start()
        {
            _userManager.PlayFabManager.GetTitleData();
            InventoryButton.onClick.AddListener(() => Inventory());
            ShopButton.onClick.AddListener(() => Shop());
        }

        void Update()
        {

        }

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        void Inventory()
        {
            if (!_userManager.PanelController.Inventory.gameObject.activeInHierarchy)
            {
                _userManager.StateManager.ChangeState(0);
                _userManager.PlayFabManager.UpdateInventory();
            }
            InventoryButton.enabled = false;
            ShopButton.enabled = true;
        }

        void Shop()
        {
            if (!_userManager.PanelController.Shop.gameObject.activeInHierarchy)
            {
                _userManager.StateManager.ChangeState(1);
                _userManager.PlayFabManager.GetItemPrices();
            }
            InventoryButton.enabled = true;
            ShopButton.enabled = false;
        }

        #endregion
    }
}
