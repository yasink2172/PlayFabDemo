﻿using System.Collections;
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

        private UserManager UserManager;
        private UIManager UIManager;

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
            SignInButton.onClick.AddListener(() => SingIn());
        }

        void Update()
        {

        }

        public void Initialize(UserManager userManager, UIManager uIManager)
        {
            UserManager = userManager;
            UIManager = uIManager;
        }

        void SingUp()
        {
            StartCoroutine(UserManager.PanelController.RegisterPanelAnimation());
        }

        void SingIn()
        {
            if (Password.text != null || MailInput.text != null)
            {
                var request = new LoginWithEmailAddressRequest
                {
                    Email = MailInput.text,
                    Password = Password.text,
                };
                PlayFabClientAPI.LoginWithEmailAddress(request, OnRegisterSuccess, OnError);

                UserManager.Invoke("LoginAccount", 2f);
            }
            else
            {
                UIManager.MessageArea(Message, MessageText, Red, "Email or password is incorrect, please try again.");
            }
        }

        void OnRegisterSuccess(LoginResult result)
        {
            UIManager.MessageArea(Message, MessageText, Green, "Login successful.");
        }

        void OnError(PlayFabError error)
        {
            UIManager.MessageArea(Message, MessageText, Red, error.ErrorMessage);
        }

        #endregion
    }
}
