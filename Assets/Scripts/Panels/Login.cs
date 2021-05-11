using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class Login : MonoBehaviour
    {
        public UserManager UserManager;
        public UIManager UIManager;

        public GameObject ErrorMessage;

        public Text UserName;
        public Text Password;

        void Start()
        {
            //UIManager.HideAnimation(ErrorMessage);
        }

        void Update()
        {

        }

        public void Initialize(UserManager userManager, UIManager uIManager)
        {
            UserManager = userManager;
            UIManager = uIManager;
        }
    }
}
