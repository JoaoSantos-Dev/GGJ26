using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{

    public class UIGameplayInicialization : UiSwitch
    {
        [SerializeField] private PlayerSessionSO playerSession;
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

            for (int i = 0; i < playerSession.MaxPlayer; i++)
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
