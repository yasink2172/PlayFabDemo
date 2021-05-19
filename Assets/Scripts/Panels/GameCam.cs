using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo.UI
{
    public class GameCam : MonoBehaviour
    {
        #region Fields

        public GameObject StartScreen;
        public GameObject EndScreen;

        public TextMeshProUGUI StartCounter;
        public TextMeshProUGUI KillCounter;
        public TextMeshProUGUI EndKillCount;
        public TextMeshProUGUI TimeCounter;

        public Button StartButton;
        public Button SendButton;

        private UserManager _userManager;
        private UIManager _uiManager;

        public int GameTime;
        public int Kill;
        public int StartTime;

        #endregion

        #region Methods

        void Start()
        {
            StartButton.onClick.AddListener(() => StartCoroutine(StartTimeCounting()));
            SendButton.onClick.AddListener(() => SendKillCount());
        }

        private void Update()
        {
            if (_userManager.GameManager.GameOver)
            {
                EndKillCount.text = Kill.ToString() + " Kill";
            }
            else if (_userManager.GameManager.GameStarted)
            {
                KillCounter.text = Kill.ToString();
            }
        }

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        void SendKillCount()
        {
            _userManager.PlayFabManager.CalculateXp(Kill);
            _userManager.GameManager.ResetPanel();
        }

        IEnumerator StartTimeCounting()
        {
            StartButton.gameObject.SetActive(false);

            while (StartTime != -1)
            {
                if (StartTime == 0)
                {
                    StartCounter.text = "START";
                }
                else
                {
                    StartCounter.text = StartTime.ToString();
                }

                yield return new WaitForSeconds(1);
                StartTime--;
            }

            StartScreen.SetActive(false);
            _userManager.GameManager.GameStarted = true;
            StartCoroutine(GameTimeCounting());
        }

        IEnumerator GameTimeCounting()
        {
            while (GameTime != 0)
            {
                if (_userManager.GameManager.GameOver)
                {
                    break;
                }

                yield return new WaitForSeconds(1);
                TimeCounter.text = "Time : " + GameTime.ToString();
                GameTime--;
            }
            if (!_userManager.GameManager.GameOver)
            {
                _userManager.GameManager.FinishGame();
            }
        }

        #endregion
    }
}
