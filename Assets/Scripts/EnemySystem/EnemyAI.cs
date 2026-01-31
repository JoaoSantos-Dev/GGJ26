using UnityEngine;

namespace GameplaySystem.AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FollowerEnemyAI : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 3f;

        [Header("Targeting")]
        [SerializeField] private float retargetInterval = 0.25f;
        [SerializeField] private float giveUpDistance = 20f;

        private Transform currentTarget;
        private float retargetTimer;

        private Rigidbody2D rb;
        private PlayersLifeCycle playersLifeCycle;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playersLifeCycle = FindFirstObjectByType<PlayersLifeCycle>();
        }

        private void Update()
        {
            UpdateTarget();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdateTarget()
        {
            retargetTimer -= Time.deltaTime;

            if (playersLifeCycle == null || playersLifeCycle.Players.Count == 0)
            {
                currentTarget = null;
                return;
            }

            if (currentTarget != null)
            {
                float distance = Vector2.Distance(transform.position, currentTarget.position);
                if (distance > giveUpDistance)
                {
                    currentTarget = null;
                }
            }

            if (currentTarget == null || retargetTimer <= 0f)
            {
                currentTarget = FindClosestPlayer();
                retargetTimer = retargetInterval;
            }
        }

        private Transform FindClosestPlayer()
        {
            Transform closest = null;
            float shortestDistance = Mathf.Infinity;

            foreach (var player in playersLifeCycle.Players)
            {
                if (player == null) continue;

                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closest = player.transform;
                }
            }

            return closest;
        }

        private void Move()
        {
            if (currentTarget == null) return;

            Vector2 direction = (currentTarget.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
