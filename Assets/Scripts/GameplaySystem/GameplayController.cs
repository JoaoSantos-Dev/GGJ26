using System;
using System.Collections.Generic;
using GameplaySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplaySystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager  playerInputManager;
        [SerializeField] private PlayersLifeCycle playersLifeCycle;
        private VictoryCondition victoryCondition;

        private bool gameStarted = false;
        public event Action GameStart;
        public event Action GameOver;
        

        private void Awake()
        {
            victoryCondition = new (playersLifeCycle);
        }

        private void OnEnable()
        {
            playersLifeCycle.OnPlayerEnter += OnPlayerEnter;
            victoryCondition.PlayerWin += OnPlayerWin;
        }

        private void OnPlayerEnter(PlayerController obj)
        {
            if (playersLifeCycle.PlayerCount == 2)
            {
                gameStarted = true;
                GameStart?.Invoke();
            }
        }

        private void OnDisable()
        {
            victoryCondition.PlayerWin -= OnPlayerWin;
        }

        private void OnPlayerWin(PlayerController obj)
        {
            GameOver?.Invoke();
        }
    }
}

