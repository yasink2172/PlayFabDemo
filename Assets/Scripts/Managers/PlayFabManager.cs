using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace Demo.UI
{
    public class PlayFabManager : MonoBehaviour
    {
        #region Fields

        private UserManager _userManager;
        private UIManager _uiManager;
        private Login _login;
        private Register _register;
        private Loby _loby;
        private Inventory _inventory;
        private Shop _shop;
        private string _mail;
        private string _password;

        public Dictionary<string, string> ImageUrlList;
        public GetPlayerCombinedInfoRequestParams info;

        #endregion

        #region Methods

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
            _login = UserManager.PanelController.Login;
            _register = UserManager.PanelController.Register;
            _loby = UserManager.PanelController.Loby;
            _inventory = UserManager.PanelController.Inventory;
            _shop = UserManager.PanelController.Shop;
        }

        #endregion

        #region PlayFabSingIn

        //Logs in.
        public void SingIn()
        {
            if (_login.Password.text != null || _login.MailInput.text != null)
            {
                var request = new LoginWithEmailAddressRequest
                {
                    Email = _login.MailInput.text,
                    Password = _login.Password.text,
                    InfoRequestParameters = info
                };
                PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
            }
            else
            {
                _uiManager.MessageArea(_login.Message, _login.MessageText, _login.Red, "Email or password is incorrect, please try again.");
            }
        }

        void OnLoginSuccess(LoginResult result)
        {
            _mail = _login.MailInput.text;
            _password = _login.Password.text;
            _uiManager.MessageArea(_login.Message, _login.MessageText, _login.Green, "Login successful.");
            _userManager.Invoke("LoginAccount", 2f);
            _loby.PlayerGL.text = result.InfoResultPayload.UserVirtualCurrency["GD"].ToString();
        }

        #endregion

        #region PlayFabSingUp
        //Registers.
        public void SingUp()
        {
            if (_register.Password.text == _register.TryPassword.text || _register.MailInput.text != null)
            {
                var request = new RegisterPlayFabUserRequest
                {
                    Email = _register.MailInput.text,
                    Password = _register.Password.text,
                    RequireBothUsernameAndEmail = false
                };
                PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
            }
            else
            {
                _uiManager.MessageArea(_register.Message, _register.MessageText, _register.Red, "Email or password is incorrect, please try again.");
            }
        }

        void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            _uiManager.MessageArea(_register.Message, _register.MessageText, _register.Green, "Your registration was successful.");

            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>{
                new StatisticUpdate
                {
                    StatisticName = "XP",
                    Value = 0
                }
            }
            }, SetStatsResult, OnError);
        }

        void SetStatsResult(UpdatePlayerStatisticsResult result)
        {
            print("Added XP stats");
        }

        #endregion

        #region PlayFabGetStatistics

        public void GetStatistics()
        {
            PlayFabClientAPI.GetPlayerStatistics(
                new GetPlayerStatisticsRequest(),
                OnGetStatistics,
                error => Debug.LogError(error.GenerateErrorReport())
            );
        }

        void OnGetStatistics(GetPlayerStatisticsResult result)
        {
            foreach (var eachStat in result.Statistics)
            {
                _userManager.PanelController.Loby.PlayerXP.text = eachStat.Value.ToString();
            }
        }

        #endregion

        #region PlayFabGeTitleData

        //Player info.
        public void GetTitleData()
        {
            GetAccountInfoRequest request = new GetAccountInfoRequest();
            PlayFabClientAPI.GetAccountInfo(request, PlayerInfo, OnError);
        }

        void PlayerInfo(GetAccountInfoResult result)
        {
            _loby.PlayerID.text = result.AccountInfo.PlayFabId;
            GetStatistics();
        }

        #endregion

        #region PlayFabGetItemPrices

        //Shop items.
        public void GetItemPrices()
        {
            GetCatalogItemsRequest request = new GetCatalogItemsRequest();
            request.CatalogVersion = "Items";
            PlayFabClientAPI.GetCatalogItems(request, ItemInfo, OnError);
        }

        void ItemInfo(GetCatalogItemsResult result)
        {
            List<CatalogItem> _items = result.Catalog;
            _shop.CreateItems(_items.Count);
            ImageUrlList = new Dictionary<string, string>();

            for (int i = 0; i < _shop.ShopItems.Count; i++)
            {
                ImageUrlList.Add(_items[i].ItemId, _items[i].ItemImageUrl);
                _shop.ShopItems[i].GetComponent<SetItem>().ItemID = _items[i].ItemId;
                _shop.ShopItems[i].GetComponent<SetItem>().Price = (int)_items[i].VirtualCurrencyPrices["GD"];
                _shop.ShopItems[i].GetComponent<SetItem>().PlayFabManager = this;
                _shop.ShopItems[i].GetComponent<SetItem>().Isthatstorestuff = true;
                StartCoroutine(_uiManager.GetTexture(ImageUrlList[_items[i].ItemId], _shop.ShopItems[i].GetComponent<SetItem>().ItemImage));
            }
        }

        #endregion

        #region PlayFabBuyItem

        public void BuyItem(string name, int price)
        {
            PurchaseItemRequest request = new PurchaseItemRequest();
            request.CatalogVersion = "Items";
            request.ItemId = name;
            request.VirtualCurrency = "GD";
            request.Price = price;
            PlayFabClientAPI.PurchaseItem(request, ItemPurchased, OnError);
        }

        void ItemPurchased(PurchaseItemResult result)
        {
            print("The item's been taken.");
            var request = new LoginWithEmailAddressRequest
            {
                Email = _mail,
                Password = _password,
                InfoRequestParameters = info
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLogin, OnError);
        }

        void OnLogin(LoginResult result)
        {
            _loby.PlayerGL.text = result.InfoResultPayload.UserVirtualCurrency["GD"].ToString();
        }

        #endregion

        #region PlayFabUpdateInventory

        //Inventory items.
        public void UpdateInventory()
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(request, TakeInventory, OnError);
        }

        void TakeInventory(GetUserInventoryResult result)
        {
            List<ItemInstance> _items = result.Inventory;

            for (int i = 0; i < _items.Count; i++)
            {
                _inventory.CreateItems(_items[i].ItemId, _items[i].RemainingUses, ImageUrlList[_items[i].ItemId]);
            }
        }

        #endregion

        #region PlayFabCalculateXp

        public void CalculateXp(int a)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "CalculateXp",
                FunctionParameter = new
                {
                    kill = a
                }
            };
            PlayFabClientAPI.ExecuteCloudScript(request,
                result =>
                {
                    GetStatistics();
                },
                OnError);
        }

        #endregion

        #region PlayFabLeaderboards

        public void GetLeaderboarder()
        {
            var request = new GetLeaderboardRequest
            {
                StartPosition = 0,
                StatisticName = "XP",
                MaxResultsCount = 30
            };
            PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
        }

        void OnGetLeaderboard(GetLeaderboardResult result)
        {
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                _userManager.PanelController.Leaderboard.CreateLeaderboardBar(i + 1,
                    result.Leaderboard[i].PlayFabId, result.Leaderboard[i].StatValue);
            }
        }

        #endregion

        #region PlayFabOnError

        //Errors
        void OnError(PlayFabError error)
        {
            if (_login.gameObject.activeInHierarchy)
            {
                _uiManager.MessageArea(_login.Message, _login.MessageText, _login.Red, error.ErrorMessage);
                print(error);
            }
            else if (_register.gameObject.activeInHierarchy)
            {
                _uiManager.MessageArea(_register.Message, _register.MessageText, _register.Red, error.ErrorMessage);
                print(error);
            }
            else
            {
                print(error);
            }
        }

        #endregion
    }
}
