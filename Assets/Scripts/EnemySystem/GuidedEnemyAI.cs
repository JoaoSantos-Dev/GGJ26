using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameplaySystem.AI
{
    public class GuidedEnemyAI : EnemyAI
    {
        public float speed = 3f;
        public float retargetTime = 30f;

        private Transform target;
        private float timer;

        private PlayersLifeCycle playersManager;

        private void Start()
        {
            playersManager = FindFirstObjectByType<PlayersLifeCycle>(); // em decadência mas funciona :)
            PickTarget();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= retargetTime || target == null)
            {
                PickTarget();
            }

            if (target != null)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
                var isLookingRight = (target.position.x - transform.position.x) < 0;
                enemyController.SetRendererFlip(isLookingRight);
            }
        }

        private void PickTarget()
        {
            timer = 0;

            var players = playersManager?.Players;

            if (players == null || players.Count == 0)
            {
                target = null;
                return;
            }

            target = players[Random.Range(0, players.Count)].transform;
        }
    }
}