using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class UserManager : MonoBehaviour
    {
        #region Fields

        public UIManager UIManager;
        public PanelController PanelController;
        public StateManager StateManager { get; private set; }

        #endregion

        #region Methods

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
            StateManager.AddState(new UserSignInOrUpState(this));
            StateManager.AddState(new UserLobyState(this));
        }

        private void Initialize()
        {
            UIManager.Initialize();
        }

        public void LoginAccount()
        {
            StateManager.CheckTheProcess = true;
        }

        #endregion
    }
}
