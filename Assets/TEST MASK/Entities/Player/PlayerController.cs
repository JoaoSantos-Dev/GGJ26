using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityBase
{
    [SerializeField] private float playerDefaultSpeed;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInventoryController InventoryController { get; private set; }
    public PlayerInput playerInput;
    private PlayerInputController inputController;
    private void Awake()
    {
            
        inputController = new PlayerInputController(playerInput);
        MovementHandler = new PlayerMovementHandler(transform, playerDefaultSpeed, () => inputController.MovementDirection);
        InventoryController = new PlayerInventoryController(MovementHandler as PlayerMovementHandler);
        StateMachine = new PlayerStateMachine(inputController, InventoryController, MovementHandler as PlayerMovementHandler);
    }

    private void Update()
    {
        StateMachine.Update();
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
            GameObject maskChild = Instantiate(mask.gameObject, transform);
            maskChild.transform.localPosition = Vector3.zero;
            InventoryController.EquipMask(maskChild.GetComponent<MaskBase>());
        }
    }

    //Created only for test purposes.
    private void DestroyChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
