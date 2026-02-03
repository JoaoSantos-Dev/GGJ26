using StunSystem;
using UnityEngine;

namespace GameplaySystem.AI
{
    public class EnemyAI : MonoBehaviour
    {
        protected EnemyController enemyController;
        public bool Active { get; set; } = true;

        protected virtual void Awake()
        {
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
            
        }


        protected virtual void UpdateAIBehaviour()
        {
            if (!Active) return;
        }
        
    }


}