using UnityEngine;

public class TeleportMask : MaskBase
{
    [Header("FOR TESTING PURPOSES")]
    [Tooltip("If true, the mask will teleport the player to a random position within a certain distance, and the teleport distance will be ignored.")]
    [SerializeField] private bool randomTeleport;
    [SerializeField] private float teleportDistance = 10f;

    [SerializeField] private float teleportRadius = 10f;

    public override event System.Action OnHabilityEnd;

    protected virtual void Awake()
    {
        base.Awake();
        MaskType = MaskType.Teleport;
    }
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        if (randomTeleport)
        {
            // Change the logic to consider the screen/scenary boundries.
            Vector3 randomPosition = Random.insideUnitCircle * Random.Range(1, teleportRadius + 1);
            playerMovementHandler.AddToEntityPosition(randomPosition);
        }
        else
        {
            playerMovementHandler.AddToEntityPosition(playerMovementHandler.LastMovementDirection * teleportDistance);
        }

        OnHabilityEnd?.Invoke();
    }
}
