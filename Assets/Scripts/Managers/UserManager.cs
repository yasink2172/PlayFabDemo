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
        public PlayFabManager PlayFabManager;
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
            Init();
            StateManager.StartState(0);
        }

        void Update()
        {
            StateManager.Update();
        }

        void Initialize()
        {
            UIManager.Initialize(this);
        }

        void Init()
        {
            InitUserState();
            UIManager.Init();
        }
        void InitUserState()
        {
            StateManager.ClearStates();
            StateManager.AddState(new UserStartState(this));
            StateManager.AddState(new UserSignInOrUpState(this));
            StateManager.AddState(new UserLobyState(this));
        }

        public void InitLobyState()
        {
            StateManager.ClearStates();
            StateManager.AddState(new InventoryShowStates(this));
            StateManager.AddState(new ShopShowStates(this));
        }

        //The account is logged in.
        public void LoginAccount()
        {
            StateManager.CheckTheProcess = true;
            Invoke("InitLobyState", 0.1f);
        }

        #endregion
    }
}
