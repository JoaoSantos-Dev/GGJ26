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
            playerController.Death += OnPlayerDeath;
            AddPlayer(playerController);
        }
        
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            var playerController = playerInput.GetComponent<PlayerController>();
            playerController.Death -= OnPlayerDeath;
            RemovePlayer(playerController);
        }
        
        private void AddPlayer(PlayerController player)
        {
            players.Add(player);
            OnPlayerEnter?.Invoke(player);
            PlayerCountUpdate?.Invoke(players.Count);
        }

        private void RemovePlayer(PlayerController player)
        {
            players.Remove(player);
            OnPlayerExit?.Invoke(player);
            PlayerCountUpdate?.Invoke(players.Count);
        }
        private void OnPlayerDeath(PlayerController player)
        {
            RemovePlayer(player);
        }


    }
}