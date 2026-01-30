using System;
using UnityEngine;

public class MaskBase : MonoBehaviour
{
    [field: SerializeField] public Sprite MaskSprite { get; private set; }
    [field: SerializeField] public float MaskDuration { get; private set; }
    [field: SerializeField] public float HabilityCooldown { get; private set; }

    public event Action OnHabilityUsed;
    public virtual event Action OnHabilityEnd;

    protected PlayerMovementHandler playerMovementHandler;

    public virtual void UseMaskHability()
    {
        OnHabilityUsed?.Invoke();
    }

    public virtual void OnEquip(PlayerMovementHandler playerMovementHandler)
    {
        this.playerMovementHandler = playerMovementHandler;
        if (TryGetComponent(out Collider2D collider)) Destroy(collider);
    }

    public virtual void OnUnequip()
    {
        playerMovementHandler = null;
    }
}
