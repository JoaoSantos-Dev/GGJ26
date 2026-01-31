using System;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace UI
{

    public class UIGameplayInicialization : UiSwitch
    {
        [FormerlySerializedAs("playerSession")] [SerializeField] private GameSessionSO gameSession;
        [SerializeField] private PlayerInputManager playerInputManager;
        private UiInputConfirmation[] uiInputConfirmations;
        private int playerCount = -1;

        protected override void Awake()
        {
            base.Awake();
            uiInputConfirmations = GetComponentsInChildren<UiInputConfirmation>();
            foreach (var uiInputconfirmation in uiInputConfirmations)
            {
                uiInputconfirmation.gameObject.SetActive(false);
            }

            for (int i = 0; i < gameSession.MaxPlayer; i++)
            {
                uiInputConfirmations[i].gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void OnDisable()
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            playerCount++;
            uiInputConfirmations[playerCount].ConfirmInput();
            
        }
        
        

        
    }
}
