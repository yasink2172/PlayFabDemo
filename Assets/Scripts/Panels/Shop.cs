using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Shop : MonoBehaviour
    {
        #region Fields

        public GameObject ShopItem;
        public List<GameObject> ShopItems;

        private UserManager _userManager;
        private UIManager _uiManager;

        #endregion

        #region Methods

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        public void CreateItems(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                GameObject _shopitem = Instantiate(ShopItem);
                _shopitem.transform.parent = this.transform;
                ShopItems.Add(_shopitem);
            }
        }

        public void DestroyItems()
        {
            for (int i = 0; i < ShopItems.Count; i++)
            {
                Destroy(ShopItems[i]);
            }
            ShopItems.Clear();
        }

        #endregion
    }
}
