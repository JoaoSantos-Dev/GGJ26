using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

namespace Playersystem
{


    public class PlayerSpawner : MonoBehaviour
    {
        private const int spawnRadius = 3;
        [SerializeField]  private PlayerInputManager inputController;
        
        private void Start()
        {
            inputController.onPlayerJoined += OnPlayerJoined;
        }

        private void OnDestroy()
        {
            inputController.onPlayerJoined -= OnPlayerJoined;
        }

        //Subscribed on Player Input Manager
        public void OnPlayerJoined(PlayerInput playerInput)
        {
            SetRandomPosition(playerInput.transform);
        }


        private void SetRandomPosition(Transform transform)
        {      
            Vector3 randomPosition = UnityEngine.Random.insideUnitCircle * spawnRadius / 2*transform.lossyScale.magnitude;
            randomPosition.z = randomPosition.y;
            randomPosition.y = 0;
            transform.position = this.transform.position +  randomPosition;
            
        }
        

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius*transform.lossyScale.magnitude);
        }
    }
    
    
}