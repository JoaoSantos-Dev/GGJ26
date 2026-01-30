using System.Collections.Generic;
using GameplaySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PlayerHudManager : MonoBehaviour
    {
        [SerializeField] PlayersLifeCycle playersLifeCycle;
        [SerializeField] private PlayerHUD[] playerHUDs;
        private readonly Dictionary<PlayerController, PlayerHUD> playerHUDMap = new();
        private int playerCount = -1;

        private void Awake()
        {
            DisableAllHuds();
        }

        private void OnEnable()
        {
            playersLifeCycle.OnPlayerEnter += OnPlayerJoined;
            playersLifeCycle.OnPlayerExit += OnPlayerLeft;
        }

        private void OnDisable()
        {            
            playersLifeCycle.OnPlayerEnter -= OnPlayerJoined;
            playersLifeCycle.OnPlayerExit -= OnPlayerLeft;
           
        }

        private void OnPlayerJoined(PlayerController playerController)
        {
            playerCount++;
            var currentHud = playerHUDs[playerCount];
            currentHud.gameObject.SetActive(true);
            currentHud.Initialize(playerController);
            playerHUDMap.Add(playerController, playerHUDs[playerCount]);
        }

        private void OnPlayerLeft(PlayerController playerController)
        {
            playerCount--;
            playerHUDMap[playerController].gameObject.SetActive(false);
            playerHUDMap.Remove(playerController);
        }


        private void DisableAllHuds()
        {
            for (var i = 0; i < playerHUDs.Length; i++) playerHUDs[i].gameObject.SetActive(false);
        }
    }
}