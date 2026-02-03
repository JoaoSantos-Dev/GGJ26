using System;
using UnityEngine;

namespace StunSystem
{
   [RequireComponent(typeof(CircleCollider2D))]
    public class StunApplier : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;


        private void Start()
        {
            SetActive(false);
        }

        public void Apply(IStunable stun) 
        {
            stun.Apply();
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != targetLayer) return;
            if(!other.TryGetComponent(out IStunable stunable)) return;
            stunable.Apply();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    
    }




}