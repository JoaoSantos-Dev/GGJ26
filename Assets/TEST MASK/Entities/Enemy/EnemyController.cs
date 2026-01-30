
using System;
using Playersystem;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : EntityBase
{
    [SerializeField, Min(1)] private int damage = 10;
    private void Awake()
    {
        MovementHandler = new EnemyMovementHandler(transform);
        
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerController player))
            {
                player.TakeDamage(damage);
            }
        }
    }
}
