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

        private UserManager UserManager;
        private UIManager UIManager;

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
            RegisterButton.onClick.AddListener(() => SingUp());
        }

        public void Initialize(UserManager userManager, UIManager uIManager)
        {
            UserManager = userManager;
            UIManager = uIManager;
        }

        void SingUp()
        {
            if (Password.text == TryPassword.text || MailInput.text != null)
            {
                var request = new RegisterPlayFabUserRequest
                {
                    Email = MailInput.text,
                    Password = Password.text,
                    RequireBothUsernameAndEmail = false
                };
                PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
            }
            else
            {
                UIManager.MessageArea(Message, MessageText, Red, "Email or password is incorrect, please try again.");
            }
        }

        void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            UIManager.MessageArea(Message, MessageText, Green, "Your registration was successful.");
        }

        void OnError(PlayFabError error)
        {
            UIManager.MessageArea(Message, MessageText, Red, error.ErrorMessage);
        }

        #endregion
    }
}
