using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo.UI
{
    public class SetItem : MonoBehaviour
    {
        #region Fields

        public PlayFabManager PlayFabManager;

        public TextMeshProUGUI ItemName;
        public TextMeshProUGUI ItemCountText;
        public TextMeshProUGUI BuyButtonText;
        public RawImage ItemImage;
        public Button BuyButton;

        public string ItemID;
        public int Price;
        public string Currency;
        public int ItemCount;
        public bool Isthatstorestuff = false;

        #endregion

        #region Methods

        private void Start()
        {
            ItemName.text = ItemID;

            if (Isthatstorestuff)
            {
                BuyButtonText.text = "BUY " + Price.ToString();
                BuyButton.onClick.AddListener(() => PlayFabManager.BuyItem(ItemID, Price));
            }
        }

        #endregion
    }
}
