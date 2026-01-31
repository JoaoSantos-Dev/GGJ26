using UnityEngine;

namespace GameplaySystem.AI
{
    public class RandomTargetEnemyIA : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float retargetInterval = 30f;

        private Transform _currentTarget;
        private float _retargetTimer;

        private void Start()
        {
            SelectRandomTarget();
        }

        private void Update()
        {
            HandleRetargeting();
            MoveTowardsTarget();
        }

        private void HandleRetargeting()
        {
            _retargetTimer += Time.deltaTime;

            // Troca de alvo se o tempo acabar ou se o alvo atual for perdido (morte/desconexão)
            if (_retargetTimer >= retargetInterval || _currentTarget == null)
            {
                SelectRandomTarget();
                _retargetTimer = 0f;
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

        private void SelectRandomTarget()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length == 0)
            {
                _currentTarget = null;
                return;
            }

            // Se houver mais de um player, tenta escolher um diferente do atual
            if (players.Length > 1)
            {
                Transform previousTarget = _currentTarget;
                while (_currentTarget == previousTarget)
                {
                    int randomIndex = Random.Range(0, players.Length);
                    _currentTarget = players[randomIndex].transform;
                }
            }
            else
            {
                _currentTarget = players[0].transform;
            }
        }
    }
}