using System;
using UnityEngine;

namespace StunSystem
{
    public class StunApplier : MonoBehaviour
    {
        private Collider2D collider2D;
        private void Awake()
        {
            collider2D = GetComponent<Collider2D>();
            collider2D.isTrigger = true;
        }

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
            if(!other.TryGetComponent(out IStunable stunable)) return;
            print("STUNNING "+other.name);
            stunable.Apply();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    
    }




}