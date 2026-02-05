using StunSystem;
using UnityEngine;

namespace GameplaySystem.AI
{
    public class EnemyAI : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected EnemyController enemyController;
        public bool Active { get; set; } = true;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            enemyController = GetComponent<EnemyController>();
        }

        protected virtual void OnEnable()
        {
           enemyController.StunStateChange += OnStunStateChange;
        }

        protected virtual void OnDisable()
        {
            enemyController.StunStateChange -= OnStunStateChange;
        }

        private void OnStunStateChange(bool value)
        {
            Active = !value;
            rb.simulated = !value;
            rb.linearVelocity = Vector2.zero;
            
        }


        protected virtual void UpdateAIBehaviour()
        {
            if (!Active) return;
        }
        
    }


}