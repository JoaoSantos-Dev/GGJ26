using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ModularWindow : MonoBehaviour, ISwitchable
    {
        [SerializeField] private Button QuitButton;
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup.alpha = 0;
            SetState(false);
        }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            QuitButton.onClick.AddListener(ButtonClicked);
        }

        public void ActiveWindow()
        {
            SetState(true);
        }
        

        void ButtonClicked()
        {
            SetState(false);
        }

        public void SetState(bool state)
        {
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
            canvasGroup.DOFade(state ? 1f : 0f,0.2f);
        }
        
    }
}