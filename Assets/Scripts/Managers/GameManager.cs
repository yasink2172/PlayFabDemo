using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demo.UI
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        public bool GameStarted;
        public bool GameOver;

        private GameObject Player;
        public UserManager _userManager;
        private UIManager _uiManager;
        public List<GameObject> AllInGameObjects;
        private float _spawnTime;

        #endregion

        #region Methods

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        public void ResetPanel()
        {
            GameStarted = false;
            GameOver = false;
            _userManager.PanelController.GameCam.StartScreen.SetActive(true);
            _userManager.PanelController.GameCam.StartButton.gameObject.SetActive(true);
            _userManager.PanelController.GameCam.EndScreen.SetActive(false);
            _userManager.PanelController.GameCam.GameTime = 30;
            _userManager.PanelController.GameCam.StartTime = 5;
            _userManager.PanelController.GameCam.Kill = 0;
            _userManager.PanelController.GameCam.StartCounter.text = "";
            _userManager.PanelController.GameCam.KillCounter.text = "Kill : 0";
            _userManager.PanelController.GameCam.TimeCounter.text = "Time : 0";
            _userManager.PanelController.GameCam.EndKillCount.text = "0 Kill";

            foreach (var inGameObject in AllInGameObjects)
            {
                inGameObject.SetActive(false);
            }
        }


        public void FinishGame()
        {
            _userManager.PanelController.GameCam.EndScreen.SetActive(true);
            _userManager.PanelController.GameCam.EndKillCount.text = _userManager.PanelController.GameCam.KillCounter + " Kill";
            GameStarted = false;
            GameOver = true;
            foreach (var inGameObject in AllInGameObjects)
            {
                inGameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (GameStarted)
            {
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            if (_spawnTime >= 0)
            {
                _spawnTime -= Time.deltaTime;
            }
            else
            {
                Enemy _enemy = EnemyPooller.Instance.Get();
                _enemy.gameObject.SetActive(true);
                _enemy.transform.position = RandomCircle(Player.transform.position, 6f);
                _spawnTime = 0.8f;
            }
        }

        Vector3 RandomCircle(Vector3 center, float radius)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z;
            return pos;
        }

        #endregion
    }
}
