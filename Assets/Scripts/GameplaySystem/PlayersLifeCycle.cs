using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplaySystem
{
    public class PlayersLifeCycle : MonoBehaviour 
    {
        [SerializeField] PlayerInputManager playerInputManager;
        private List<PlayerController> players = new();

        public event Action<PlayerController> OnPlayerEnter;
        public event Action<PlayerController> OnPlayerExit;
        public event Action<int> PlayerCountUpdate;
        
        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
            playerInputManager.onPlayerLeft += OnPlayerLeft;
        }

        public void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
            playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            var playerController = playerInput.GetComponent<PlayerController>();
            players.Add(playerController);
            OnPlayerEnter?.Invoke(playerController);
            PlayerCountUpdate?.Invoke(players.Count);
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            var playerController = playerInput.GetComponent<PlayerController>();
            players.Remove(playerController);
            OnPlayerExit?.Invoke(playerController);
            PlayerCountUpdate?.Invoke(players.Count);

        }


    }
}