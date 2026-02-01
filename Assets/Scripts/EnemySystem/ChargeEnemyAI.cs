using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameplaySystem.AI
{
    public class EnemyAI : MonoBehaviour
    {
        protected EnemyController enemyController;

        protected virtual void Awake()
        {
            enemyController = GetComponent<EnemyController>();
        }
        
    }
    public class ChargeEnemyAI : EnemyAI
    {
        private enum EnemyState { Patrolling, Charging, Dashing }

        [Header("Movement Settings")]
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float changeDirectionInterval = 3f;

        [Header("Attack Settings")]
        [SerializeField] private float maxChargeTime = 2f;
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 0.5f;

        private EnemyState _currentState = EnemyState.Patrolling;
        private Vector2 _dashDirection;
        private Vector2 _patrolDirection;
        private Transform _targetInRange;

        private float _chargeTimer;
        private float _dashTimer;
        private float _patrolTimer;

        private void Start()
        {
            SetRandomPatrolDirection();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case EnemyState.Patrolling:
                    HandlePatrol();
                    break;
                case EnemyState.Charging:
                    HandleCharge();
                    break;
                case EnemyState.Dashing:
                    HandleDash();
                    break;
            }
            enemyController.SetRendererFlip(_patrolDirection.x < 0);

        }


        private void HandlePatrol()
        {
            _patrolTimer -= Time.deltaTime;
            //Perigoso: pois quando houver obstáculos, o transform será forçado a ocupar o espaço
            //não respeitando a física.
            transform.Translate(_patrolDirection * patrolSpeed * Time.deltaTime);

            if (_patrolTimer <= 0)
            {
                
                SetRandomPatrolDirection();
            }
        }

        private void HandleCharge()
        {
            _chargeTimer -= Time.deltaTime;

            if (_chargeTimer <= 0)
            {
                InitiateDash();
            }

            if (_targetInRange == null)
            {
                ResetToPatrol();
            }
        }

        private void HandleDash()
        {
            _dashTimer -= Time.deltaTime;
            transform.Translate(_dashDirection * dashSpeed * Time.deltaTime);

            if (_dashTimer <= 0)
            {
                
                enemyController.SetScale(Vector3.one);
                ResetToPatrol();
            }
        }



        private void SetRandomPatrolDirection()
        {
            _patrolDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _patrolTimer = changeDirectionInterval;
            enemyController.StartMoveAnimation();
        }

        private void InitiateDash()
        {
            enemyController.SetScale(new Vector3(1.2f,0.8f,1),dashDuration);

            if (_targetInRange != null)
            {

                _dashDirection = (_targetInRange.position - transform.position).normalized;
                _currentState = EnemyState.Dashing;
                _dashTimer = dashDuration;
            }
            else
            {
                ResetToPatrol();
            }
        }

        private void ResetToPatrol()
        {
            _currentState = EnemyState.Patrolling;
            _chargeTimer = maxChargeTime;
            SetRandomPatrolDirection();
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && _currentState == EnemyState.Patrolling)
            {
                _targetInRange = collision.transform;
                _currentState = EnemyState.Charging;
                _chargeTimer = maxChargeTime;
                enemyController.StopMoveAnimation();
                enemyController.SetScale(new Vector3(0.8f,1.2f,1),maxChargeTime);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _targetInRange = null;
            }
        }

    }
}