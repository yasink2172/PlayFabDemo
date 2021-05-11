using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class UserManager : MonoBehaviour
    {
        public UIManager UIManager;
        public PanelController PanelController;
        public StateManager StateManager { get; private set; }

        private void Awake()
        {
            StateManager = new StateManager();
            Initialize();
        }
        void Start()
        {
            InitState();
            StateManager.StartState(0);
        }

        void Update()
        {
            StateManager.Update();
        }

        void InitState()
        {
            StateManager.AddState(new UserStartState(this));
            StateManager.AddState(new UserSignInState(this));
            StateManager.AddState(new UserSignUpState(this));
        }

        private void Initialize()
        {
            UIManager.Initialize();
        }
    }
}
