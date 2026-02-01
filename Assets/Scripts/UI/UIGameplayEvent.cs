using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameplaySystem;
using TMPro;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;


namespace UI
{


    public class UIGameplayEvent : MonoBehaviour
    {
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private UIGameplayInicialization UiInitialization;
        [SerializeField] private UiGameOver UiGameOver;
        [SerializeField] private TMP_Text text_321_go;
        private void Start()
        {
            UiInitialization.SetState(true);
            UiGameOver?.SetState(false);
        }

        private void OnEnable()
        {
            gameplayController.AllPlayerJoined += OnAllPlayerJoined;
            gameplayController.GameOver += OnGameOver;
        }
        

        private void OnDisable()
        {
            gameplayController.AllPlayerJoined -= OnAllPlayerJoined;
            gameplayController.GameOver -= OnGameOver;
        }

        private async void OnAllPlayerJoined()
        {
            await UniTask.Delay(100);
            UiInitialization.SetState(false);
            
        }
        
        private async void OnGameOver()
        {            
            await UniTask.Delay(500);
            UiGameOver.SetState(true);
        }

        public async Task Start321GoEvent()
        {
            text_321_go.enabled = true;
            for (int i = 1; i <= 3; i++)
            {
                text_321_go.SetText(i.ToString());
                text_321_go.transform.DOScale(Vector2.one * 2,1).SetEase(Ease.OutCubic);
                await UniTask.Delay(1000);
                text_321_go.transform.localScale = Vector2.one;

            }
            text_321_go.SetText("Go");
            text_321_go.transform.DOScale(Vector2.one * 2,1).SetEase(Ease.OutCubic);
            await UniTask.Delay(1000);
            text_321_go.gameObject.SetActive(false);
            
            
        }
        
        
    }
    
}
