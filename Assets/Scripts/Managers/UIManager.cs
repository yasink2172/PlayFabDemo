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

        private UserManager _userManager;

        private bool _waitHideAnimationEnd;
        private bool _waitShowAnimationEnd;

        #endregion

        #region Methods

        public void Initialize(UserManager UserManager)
        {
            _userManager = UserManager;
            UserManager.PanelController.Initialize(UserManager, this);
            UserManager.PanelController.Login.Initialize(UserManager, this);
            UserManager.PanelController.Register.Initialize(UserManager, this);
            UserManager.PanelController.Loby.Initialize(UserManager, this);
            UserManager.PanelController.Inventory.Initialize(UserManager, this);
            UserManager.PanelController.Shop.Initialize(UserManager, this);
            UserManager.PanelController.GameCam.Initialize(UserManager, this);
            UserManager.PanelController.Leaderboard.Initialize(UserManager, this);
            UserManager.PlayFabManager.Initialize(UserManager, this);
            UserManager.GameManager.Initialize(UserManager, this);
        }

        public void Init()
        {
            _waitHideAnimationEnd = false;
            _waitShowAnimationEnd = false;
        }

        //Opens closed panels.
        public void ShowPanel(GameObject panel)
        {
            if (!panel.activeInHierarchy)
            {
                panel.SetActive(true);
            }
        }

        //Closes open panels.
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

        //Prints the required message.
        public void MessageArea(GameObject MessageObject, TextMeshProUGUI MessageText, Color color, string Message)
        {
            MessageObject.GetComponent<Image>().color = color;
            MessageObject.SetActive(true);
            MessageText.text = Message;
            HideAnimation(MessageObject);
        }

        //Hide animation.
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

        //Show animation.
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

        //Load items image.
        public IEnumerator GetTexture(string url, RawImage image)
        {
            WWW wwwLoader = new WWW(url);
            yield return wwwLoader;
            image.texture = wwwLoader.texture;
        }

        #endregion
    }
}
