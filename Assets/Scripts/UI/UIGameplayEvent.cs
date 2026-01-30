using System;
using GameplaySystem;
using UnityEngine;


namespace UI
{


    public class UIGameplayEvent : MonoBehaviour
    {
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private GameObject UiInitialization;
        [SerializeField] private GameObject UiGameOver;

        private void Start()
        {
            UiInitialization?.SetActive(true);
            UiGameOver?.SetActive(false);
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

        private void OnGameStart()
        {
            UiInitialization?.SetActive(false);
            
        }
        
        private void OnGameOver()
        {
            UiGameOver?.SetActive(true);
        }
    }
}
