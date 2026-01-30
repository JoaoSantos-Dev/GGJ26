using System;
using Playersystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityBase, IDamageable
{
    [field: SerializeField]
    [field: Min(0)]
    public int Health { get; set; } = 100;

    [SerializeField] private float playerDefaultSpeed;
    private PlayerInputController inputController;
    private int maxHealth;
    private PlayerInput playerInput;
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInventoryController InventoryController { get; private set; }

    public event Action<int> HealthChanged;
    public event Action Death;
    private void Awake()
    {
        maxHealth = Health;
        inputController = new PlayerInputController(GetComponent<PlayerInput>());
        MovementHandler =
            new PlayerMovementHandler(transform, playerDefaultSpeed, () => inputController.MovementDirection);
        InventoryController = new PlayerInventoryController(MovementHandler as PlayerMovementHandler);
        StateMachine = new PlayerStateMachine(inputController, InventoryController,
            MovementHandler as PlayerMovementHandler);
    }

    private void Update()
    {
        StateMachine.Update();
        if (inputController.Hability.WasPressedThisFrame()) TakeDamage(20);
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.TryGetComponent(out MaskBase mask))
        {
            // Test

            /*Mask collider of effect overlaps with the player's, causing it to be an extension of him. Remember to adjust on each
            mask class that creates an effect collider to calculate an offset based on the boundries of the player's collider */
            if (mask == InventoryController.EquipedMask) return;

            DestroyChildren();
            var maskChild = Instantiate(mask.gameObject, transform);
            maskChild.transform.localPosition = Vector3.zero;
            InventoryController.EquipMask(maskChild.GetComponent<MaskBase>());
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Clamp(damage, 0, maxHealth);
        HealthChanged?.Invoke(Health);
        if (Health == 0) Death?.Invoke();
    }




    //Created only for test purposes.
    private void DestroyChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}