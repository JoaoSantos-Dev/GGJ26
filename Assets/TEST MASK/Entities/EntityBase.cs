using System;
using StunSystem;
using UnityEngine;

public class EntityBase : StunBase 
{
    public Rigidbody2D Rigidbody2D { get; private set; }

    public MovementHandler MovementHandler { get; protected set; }

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
