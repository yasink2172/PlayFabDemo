using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo.UI
{
    public class Register : MonoBehaviour
    {
        #region Fields

        private UserManager _userManager;
        private UIManager _uiManager;

        public GameObject Message;
        public TextMeshProUGUI MessageText;

        public Color Red;
        public Color Green;

        public TMP_InputField MailInput;
        public TMP_InputField Password;
        public TMP_InputField TryPassword;
        public Button RegisterButton;

        #endregion

        #region Methods

        void Start()
        {
            RegisterButton.onClick.AddListener(() => _userManager.PlayFabManager.SingUp());
        }

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        #endregion
    }
}
