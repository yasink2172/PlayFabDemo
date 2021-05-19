using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

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
        public Button PlayButton;
        public Button LeaderboardButton;

        #endregion

        #region Methods

        void Start()
        {
            _userManager.PlayFabManager.GetTitleData();
            InventoryButton.onClick.AddListener(() => Inventory());
            ShopButton.onClick.AddListener(() => Shop());
            PlayButton.onClick.AddListener(() => PlayGame());
            LeaderboardButton.onClick.AddListener(() => Leaderboard());
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
            PlayButton.enabled = true;
            LeaderboardButton.enabled = true;
        }

        void Shop()
        {
            if (!_userManager.PanelController.Shop.gameObject.activeInHierarchy)
            {
                _userManager.StateManager.ChangeState(1);
            }
            InventoryButton.enabled = true;
            ShopButton.enabled = false;
            PlayButton.enabled = true;
            LeaderboardButton.enabled = true;
        }

        void PlayGame()
        {
            if (!_userManager.PanelController.GameCam.gameObject.activeInHierarchy)
            {
                _userManager.StateManager.ChangeState(2);
            }
            InventoryButton.enabled = true;
            ShopButton.enabled = true;
            PlayButton.enabled = false;
            LeaderboardButton.enabled = true;
        }

        void Leaderboard()
        {
            if (!_userManager.PanelController.Leaderboard.gameObject.activeInHierarchy)
            {
                _userManager.StateManager.ChangeState(3);
            }
            InventoryButton.enabled = true;
            ShopButton.enabled = true;
            PlayButton.enabled = true;
            LeaderboardButton.enabled = false;
        }

        #endregion
    }
}
