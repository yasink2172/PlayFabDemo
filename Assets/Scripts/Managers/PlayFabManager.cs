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

        #region PlayFab

        //Logs in.
        public void SingIn()
        {
            if (_login.Password.text != null || _login.MailInput.text != null)
            {
                var request = new LoginWithEmailAddressRequest
                {
                    Email = _login.MailInput.text,
                    Password = _login.Password.text,
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
            _uiManager.MessageArea(_login.Message, _login.MessageText, _login.Green, "Login successful.");
            _userManager.Invoke("LoginAccount", 2f);
        }

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
        }

        //Player info.
        public void GetTitleData()
        {
            GetAccountInfoRequest request = new GetAccountInfoRequest();
            PlayFabClientAPI.GetAccountInfo(request, PlayerInfo, OnError);
        }

        void PlayerInfo(GetAccountInfoResult result)
        {
            _loby.PlayerID.text = result.AccountInfo.PlayFabId;
        }

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
            for (int i = 0; i < _shop.ShopItems.Count; i++)
            {
                _shop.ShopItems[i].GetComponent<SetItem>().ItemID = _items[i].ItemId;
                _shop.ShopItems[i].GetComponent<SetItem>().Price = (int)_items[i].VirtualCurrencyPrices["GD"];
                _shop.ShopItems[i].GetComponent<SetItem>().PlayFabManager = this;
                _shop.ShopItems[i].GetComponent<SetItem>().Isthatstorestuff = true;
                StartCoroutine(GetTexture(_items[i].ItemImageUrl, _shop.ShopItems[i].GetComponent<SetItem>().ItemImage));
            }
        }

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
        }

        //Inventory items.
        public void UpdateInventory()
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory(request,TakeInventory,OnError);
        }

        void TakeInventory(GetUserInventoryResult result)
        {
            List<ItemInstance> _items = result.Inventory;
            _inventory.CreateItems(_items.Count);
            for (int i = 0; i < _inventory.InventoryItems.Count; i++)
            {
                _inventory.InventoryItems[i].GetComponent<SetItem>().ItemID = _items[i].ItemId;
                _inventory.InventoryItems[i].GetComponent<SetItem>().PlayFabManager = this;
                //StartCoroutine(GetTexture(_items[i]., _inventory.ShopItems[i].GetComponent<SetItem>().ItemImage));
            }
        }

        //Load items image.
        IEnumerator GetTexture(string url, RawImage image)
        {
            WWW wwwLoader = new WWW(url);
            yield return wwwLoader;
            image.texture = wwwLoader.texture;
        }

        //Errors
        void OnError(PlayFabError error)
        {
            if (_login.gameObject.activeInHierarchy)
            {
                _uiManager.MessageArea(_login.Message, _login.MessageText, _login.Red, error.ErrorMessage);
            }
            else if (_register.gameObject.activeInHierarchy)
            {
                _uiManager.MessageArea(_register.Message, _register.MessageText, _register.Red, error.ErrorMessage);
            }
            else
            {
                print(error);
            }
        }

        #endregion
    }
}
