using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UiSwitch : MonoBehaviour, ISwitchable
    {
        protected CanvasGroup canvasGroup;
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetState(bool state)
        {
            if(state) 
                canvasGroup.DOFade(1,0.5f);

            else
            {
                canvasGroup.DOFade(0,0.5f);
            }
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
        
    }
}