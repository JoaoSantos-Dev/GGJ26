using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace GameplaySystem
{
    public class PlayersLifeCycle : MonoBehaviour
    {
        [FormerlySerializedAs("playerSessionData")] [SerializeField] private GameSessionSO gameSessionData;
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
            SetPlayerVisualConfigs(playerController);
            if (gameSessionData.MaxPlayer == PlayerCount)
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


        private void SetPlayerVisualConfigs(PlayerController playerController)
        {
            var color = gameSessionData.playerDatas[PlayerCount - 1].Color;
            var headSprite = gameSessionData.characterSprites.GetRandomHeadSprite();
            playerController.SetVisual(headSprite,color);
        }

    }
}