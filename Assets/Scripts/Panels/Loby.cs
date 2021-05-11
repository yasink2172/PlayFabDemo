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

        private UserManager UserManager;
        private UIManager UIManager;

        public TextMeshProUGUI PlayerID;

        #endregion

        #region Methods

        void Start()
        {
            GetTitleData();
        }

        void Update()
        {

        }

        public void Initialize(UserManager userManager, UIManager uIManager)
        {
            UserManager = userManager;
            UIManager = uIManager;
        }

        void GetTitleData()
        {
            GetAccountInfoRequest request = new GetAccountInfoRequest();
            PlayFabClientAPI.GetAccountInfo(request, PlayerInfo, OnError);
        }

        void PlayerInfo(GetAccountInfoResult result)
        {
            PlayerID.text = "ID : " + result.AccountInfo.PlayFabId;
        }

        void OnError(PlayFabError error)
        {

        }

        #endregion
    }
}
