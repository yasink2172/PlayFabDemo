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

        public void CreateItems(string itemID, int? itemCount, string itemImageURL)
        {
            GameObject _inventoryItem = Instantiate(InventoryItem);
            _inventoryItem.transform.parent = this.transform;
            _inventoryItem.GetComponent<SetItem>().ItemID = itemID;
            _inventoryItem.GetComponent<SetItem>().ItemCountText.text = itemCount.ToString();
            StartCoroutine(_uiManager.GetTexture(itemImageURL, _inventoryItem.GetComponent<SetItem>().ItemImage));
            InventoryItems.Add(_inventoryItem);
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
