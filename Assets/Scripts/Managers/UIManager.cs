using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class UIManager : MonoBehaviour
    {
        public UserManager UserManager;

        public void Initialize()
        {
            UserManager.PanelController.Login.Initialize(UserManager,this);
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
            StartCoroutine(CanvasAlphaAnimation(mGameObject.GetComponent<CanvasGroup>()));
        }

        private IEnumerator CanvasAlphaAnimation(CanvasGroup canvasGroup)
        {
            float lerpTime = 0;

            float alphaFrom = canvasGroup.alpha;
            float alphaTo = 0;

            while (lerpTime <= 1)
            {
                canvasGroup.alpha = Mathf.Lerp(alphaFrom, alphaTo, lerpTime);
                lerpTime += (Time.deltaTime / 4); // 2 is speed
                yield return null;
            }
            canvasGroup.alpha = Mathf.Lerp(alphaFrom, alphaTo, 1);
        }
    }
}
