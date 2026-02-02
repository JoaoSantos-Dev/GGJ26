using System;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using GameplaySystem;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private float delayStartup = 3;
        [SerializeField] private GameSessionSO gameSessionData;
        [SerializeField] private PlayerInputManager  playerInputManager;
        [SerializeField] private PlayersLifeCycle playersLifeCycle;
        [SerializeField] private UIGameplayEvent uiGameplayEvent;
        public VictoryCondition victoryCondition {get; private set;}

        public bool IsGameplayActive {get; private set;}= false;
        public event Action GameStart;
        public event Action GameOver;
        public event Action AllPlayerJoined;
        private List<PlayerController> playerJoined;


        private void Awake()
        {
            victoryCondition = new (playersLifeCycle);
        }

        private void OnEnable()
        {
            playersLifeCycle.OnPlayerEnter += OnPlayerEnter;
            victoryCondition.PlayerWin += OnPlayerWin;
        }

        private void OnPlayerEnter(PlayerController player)
        {
             player.PlayerInput.DeactivateInput();
            if (playersLifeCycle.PlayerCount == gameSessionData.MaxPlayer)
            {
                AllPlayerJoined?.Invoke();
                StartGame();
            }
            
            
        }

        private void OnDisable()
        {
            victoryCondition.PlayerWin -= OnPlayerWin;
        }

        private void OnPlayerWin(PlayerController obj)
        {
            GameOver?.Invoke();
            IsGameplayActive = false;

        }

        private async void StartGame()
        {
            uiGameplayEvent.Start321GoEvent();
            await UniTask.Delay((int)(delayStartup*1000));
            GameStart?.Invoke();
            IsGameplayActive = true;
            playersLifeCycle.SetPlayerInputState(true);


        }
    }
}

