using UnityEngine;

namespace GameplaySystem.AI
{
    public class FollowerEnemyIA : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float detectionInterval = 0.2f; // Busca 5 vezes por segundo (mais otimizado)

        private Transform _currentTarget;
        private float _detectionTimer;

        private void Update()
        {
            HandleTargetDetection();
            MoveTowardsTarget();
        }

        private void HandleTargetDetection()
        {
            _detectionTimer -= Time.deltaTime;

            // Se o alvo atual sumiu ou o timer zerou, busca novamente
            if (_currentTarget == null || _detectionTimer <= 0)
            {
                _currentTarget = GetClosestPlayer();
                _detectionTimer = detectionInterval;
            }
        }

        private void MoveTowardsTarget()
        {
            if (_currentTarget == null) return;

            transform.position = Vector2.MoveTowards(
                transform.position,
                _currentTarget.position,
                movementSpeed * Time.deltaTime
            );
        }

        private Transform GetClosestPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            float shortestDistance = Mathf.Infinity;
            Transform closestTransform = null;

            foreach (GameObject player in players)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestTransform = player.transform;
                }
            }

            return closestTransform;
        }
    }
}