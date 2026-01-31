using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameplaySystem
{
    public class PlayersLifeCycle : MonoBehaviour
    {
        [SerializeField] private PlayerSessionSO playerSessionData;
        [SerializeField] PlayerInputManager playerInputManager;
        public List<PlayerController> Players { get; private set; } = new();

        public event Action<PlayerController> OnPlayerEnter;
        public event Action<PlayerController> OnPlayerExit;
        public int PlayerCount => Players.Count;
        
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
            playerController.Death += OnPlayerDeath;
            AddPlayer(playerController);
            if (playerSessionData.MaxPlayer == PlayerCount)
            {
                playerInputManager.DisableJoining();
            }
        }
        
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            var playerController = playerInput.GetComponent<PlayerController>();
            playerController.Death -= OnPlayerDeath;
            RemovePlayer(playerController);
        }
        
        private void AddPlayer(PlayerController player)
        {
            Players.Add(player);
            OnPlayerEnter?.Invoke(player);
        }

        private void RemovePlayer(PlayerController player)
        {
            Players.Remove(player);
            OnPlayerExit?.Invoke(player);
        }
        
        private void OnPlayerDeath(PlayerController player)
        {
            RemovePlayer(player);
        }


    }
}