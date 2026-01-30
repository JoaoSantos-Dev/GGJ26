using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PlayerHudManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;

        [SerializeField] private PlayerHUD[] playerHUDs;
        private readonly Dictionary<PlayerController, PlayerHUD> playerHUDMap = new();
        private int playerCount = -1;

        private void Awake()
        {
            DisableAllHuds();
        }

        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
            playerInputManager.onPlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
            playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            playerCount++;
            var playerController = playerInput.GetComponent<PlayerController>();
            var currentHud = playerHUDs[playerCount];
            currentHud.gameObject.SetActive(true);
            currentHud.Initialize(playerController);
            playerHUDMap.Add(playerController, playerHUDs[playerCount]);
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            playerCount--;
            var playerController = playerInput.GetComponent<PlayerController>();
            playerHUDMap[playerController].gameObject.SetActive(false);
            playerHUDMap.Remove(playerController);
        }


        private void DisableAllHuds()
        {
            for (var i = 0; i < playerHUDs.Length; i++) playerHUDs[i].gameObject.SetActive(false);
        }
    }
}