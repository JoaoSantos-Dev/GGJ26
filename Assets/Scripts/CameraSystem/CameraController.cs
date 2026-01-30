using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;
        [SerializeField] private CinemachineTargetGroup targetGroup;

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
            targetGroup.AddMember(playerInput.transform, 1, 1);
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            targetGroup.RemoveMember(playerInput.transform);
        }
    }
}