using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Inventory : MonoBehaviour
    {
        #region Fields

        public GameObject InventoryItem;
        public List<GameObject> InventoryItems;

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
                GameObject shopitem = Instantiate(InventoryItem);
                shopitem.transform.parent = this.transform;
                InventoryItems.Add(shopitem);
            }
        }

        public void DestroyItems()
        {
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                Destroy(InventoryItems[i]);
            }
            InventoryItems.Clear();
        }

        #endregion
    }
}
