using GameplaySystem;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayersLifeCycle playersLifeCycle;
        [SerializeField] private CinemachineTargetGroup targetGroup;
        
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


        private void OnPlayerJoined(PlayerController player)
        {
            targetGroup.AddMember(player.transform, 1, 1);
        }

        private void OnPlayerLeft(PlayerController player)
        {
            targetGroup.RemoveMember(player.transform);
        }
    }
}