using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class PanelController : MonoBehaviour
    {
        #region Fields

        private UserManager UserManager;
        private UIManager UIManager;

        public Login Login;
        public Register Register;
        public Loby Loby;

        static int[] SingInPanelRectTransformPos = new int[] { 0, -500 };//MID , LEFT

        #endregion

        public void Initialize(UserManager userManager, UIManager uIManager)
        {
            UserManager = userManager;
            UIManager = uIManager;
        }

        #region Methods

        //Animations               
        public IEnumerator RegisterPanelAnimation()
        {
            if (Login.gameObject.GetComponent<RectTransform>().anchoredPosition.x != SingInPanelRectTransformPos[1])
            {
                float time = 0;
                while (time < 1)
                {
                    time += Time.deltaTime * 2;
                    yield return new WaitForEndOfFrame();
                    Vector2 PosFirst = new Vector2(SingInPanelRectTransformPos[0], Login.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                    Vector2 PosSecond = new Vector2(SingInPanelRectTransformPos[1], Login.gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                    Login.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(PosFirst, PosSecond, time);
                }
                UIManager.ShowAnimation(Register.gameObject);
            }
        }

        #endregion
    }
}
