using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Leaderboard : MonoBehaviour
    {
        #region Fields

        private UserManager _userManager;
        private UIManager _uiManager;

        public GameObject LeaderboardScroll;
        public GameObject LeaderboardBar;

        public List<GameObject> LeaderboardBars;

        #endregion

        #region Methods

        public void Initialize(UserManager UserManager, UIManager UIManager)
        {
            _userManager = UserManager;
            _uiManager = UIManager;
        }

        public void CreateLeaderboardBar(int Number, string ID, int XP)
        {
            GameObject bar = Instantiate(LeaderboardBar);
            bar.transform.parent = LeaderboardScroll.transform;
            bar.GetComponent<LeaderboardBar>().NumberText.text = Number.ToString() + " -";
            bar.GetComponent<LeaderboardBar>().IDText.text = ID;
            bar.GetComponent<LeaderboardBar>().XPText.text = XP.ToString();
            LeaderboardBars.Add(bar);
        }

        public void DestroyBar()
        {
            foreach (var bar in LeaderboardBars)
            {
                Destroy(bar);
            }
            LeaderboardBars.Clear();
        }

        #endregion
    }
}
