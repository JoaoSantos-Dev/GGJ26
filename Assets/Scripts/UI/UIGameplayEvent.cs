using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameplaySystem;
using UnityEngine;


namespace UI
{


    public class UIGameplayEvent : MonoBehaviour
    {
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private UIGameplayInicialization UiInitialization;
        [SerializeField] private UiGameOver UiGameOver;

        private void Start()
        {
            UiInitialization.SetState(true);
            UiGameOver?.SetState(false);
        }

        private void OnEnable()
        {
            gameplayController.GameStart += OnGameStart;
            gameplayController.GameOver += OnGameOver;
        }
        

        private void OnDisable()
        {
            gameplayController.GameStart -= OnGameStart;
            gameplayController.GameOver -= OnGameOver;
        }

        private async void OnGameStart()
        {
            await UniTask.Delay(500);
            UiInitialization.SetState(false);
            
        }
        
        private async void OnGameOver()
        {            
            await UniTask.Delay(500);
            UiGameOver.SetState(true);
        }
    }
}
