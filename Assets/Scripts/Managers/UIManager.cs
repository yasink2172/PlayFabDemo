using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Fields

        public UserManager UserManager;

        private bool _waitHideAnimationEnd;
        private bool _waitShowAnimationEnd;

        #endregion

        #region Methods

        public void Initialize()
        {
            UserManager.PanelController.Initialize(UserManager, this);
            UserManager.PanelController.Login.Initialize(UserManager, this);
            UserManager.PanelController.Register.Initialize(UserManager, this);
            UserManager.PanelController.Loby.Initialize(UserManager, this);

            _waitHideAnimationEnd = false;
            _waitShowAnimationEnd = false;
        }

        public void ShowPanel(GameObject panel)
        {
            if (!panel.activeInHierarchy)
            {
                panel.SetActive(true);
            }
        }

        public void HidePanel(GameObject panel)
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
            }
        }

        public void HideAnimation(GameObject mGameObject)
        {
            if (!_waitHideAnimationEnd)
            {
                _waitHideAnimationEnd = true;
                StartCoroutine(CanvasAlphaHideAnimation(mGameObject.GetComponent<CanvasGroup>()));
            }
        }

        public void ShowAnimation(GameObject mGameObject)
        {
            if (!_waitShowAnimationEnd)
            {
                _waitShowAnimationEnd = true;
                StartCoroutine(CanvasAlphaShowAnimation(mGameObject.GetComponent<CanvasGroup>()));
            }
        }

        public void MessageArea(GameObject MessageObject, TextMeshProUGUI MessageText, Color color, string Message)
        {
            MessageObject.GetComponent<Image>().color = color;
            MessageObject.SetActive(true);
            MessageText.text = Message;
            HideAnimation(MessageObject);
        }

        private IEnumerator CanvasAlphaHideAnimation(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);

            float lerpTime = 0;
            float alphaFrom = 1;
            float alphaTo = 0;

            while (lerpTime <= 1)
            {
                canvasGroup.alpha = Mathf.Lerp(alphaFrom, alphaTo, lerpTime);
                lerpTime += (Time.deltaTime / 4); // 2 is speed
                yield return null;
            }

            canvasGroup.gameObject.SetActive(false);
            _waitHideAnimationEnd = false;
        }

        private IEnumerator CanvasAlphaShowAnimation(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);

            float lerpTime = 0;
            canvasGroup.alpha = 0;
            float alphaFrom = canvasGroup.alpha;
            float alphaTo = 1;

            while (lerpTime <= 1)
            {
                canvasGroup.alpha = Mathf.Lerp(alphaFrom, alphaTo, lerpTime);
                lerpTime += (Time.deltaTime / 2); // 2 is speed
                yield return null;
            }
            _waitShowAnimationEnd = false;
        }

        #endregion
    }
}
