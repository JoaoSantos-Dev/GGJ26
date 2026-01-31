using System;
using DG.Tweening;
using UnityEngine;

public enum MaskType { Dash, Shield, Teleport, Push }
public class MaskBase : MonoBehaviour
{
    [field: SerializeField] public Sprite MaskSprite { get; private set; }
    [field: SerializeField] public float MaskDuration { get; private set; }
    [field: SerializeField] public float HabilityCooldown { get; private set; }

    //For test purposes. Should come from the inventory settings.
    [field: SerializeField] public float DropRadius { get; private set; }
    public Cooldown Cooldown { get; private set; }
    public Cooldown MaskDurationCountdown { get; private set; }
    public MaskType MaskType { get; protected set; }

    public event Action OnHabilityUsed;
    public event Action OnDurationExpired;
    public virtual event Action OnHabilityEnd;

    protected PlayerMovementHandler playerMovementHandler;

    private void Start()
    {
        Cooldown = new Cooldown();
        MaskDurationCountdown = new Cooldown();
    }

    public virtual void UseMaskHability()
    {
        if (!Cooldown.IsReady) return;
        Cooldown.Start(HabilityCooldown);
        OnHabilityUsed?.Invoke();
    }

    public virtual void OnEquip(PlayerMovementHandler playerMovementHandler)
    {
        this.playerMovementHandler = playerMovementHandler;
        MaskDurationCountdown.Start(MaskDuration, () => OnDurationExpired?.Invoke());
        MaskSpawnManager.Instance.UntrackMask(this);
        if (TryGetComponent(out Collider2D collider)) collider.enabled = false;
    }

    public virtual void OnUnequip()
    {
        playerMovementHandler = null;
        Drop();
    }

    private void Drop()
    {
        Debug.Log("Dropping Mask");
        MaskSpawnManager.Instance.TrackMask(this);
        Vector3 randomPosition = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(1, DropRadius + 1);
        transform.parent = null;
        Action completed = null;
        if (TryGetComponent(out Collider2D collider))
        {
            collider.transform.localScale = Vector3.one;
            completed = () => collider.enabled = true;
        }
        transform.DOMove(transform.position + randomPosition, 1f).OnComplete(() =>
        {
            completed?.Invoke();
        });
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
