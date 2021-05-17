using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

namespace Demo.UI
{
    public class Login : MonoBehaviour
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
        public Button SignInButton;
        public Button SignUpButton;

        #endregion

        #region Methods

        void Start()
        {
            SignUpButton.onClick.AddListener(() => SingUp());
            SignInButton.onClick.AddListener(() => _userManager.PlayFabManager.SingIn());
        }

        void Update()
        {

        }

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        void SingUp()
        {
            StartCoroutine(_userManager.PanelController.RegisterPanelAnimation());
        }

        #endregion
    }
}
