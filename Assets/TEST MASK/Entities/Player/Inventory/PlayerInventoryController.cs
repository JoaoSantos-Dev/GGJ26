
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController 
{
    public MaskBase EquipedMask { get; private set; }
    public bool CanUseMaskHability => EquipedMask != null && EquipedMask.Cooldown.IsReady;

    private PlayerMovementHandler playerMovementHandler;

    //Adjust here to get (from elsewhere on the player settings/configs) to get the default time for equipping a mask
    private Cooldown equipMaskCountdown;
    private readonly SpriteRenderer maskRenderer;
    public event Action<MaskBase> MaskEquiped;
    public event Action MaskUnequiped;
    public PlayerInventoryController(SpriteRenderer maskRenderer, PlayerMovementHandler playerMovementHandler)
    {
        this.maskRenderer = maskRenderer;
        this.playerMovementHandler = playerMovementHandler;
        equipMaskCountdown = new Cooldown();
    }

    public void EquipMask(MaskBase mask)
    {
        if (EquipedMask != null) UnequipMask();
        EquipNewMask(mask);
        
    }

    public void UnequipMask()
    {
        EquipedMask.MaskDurationCountdown.Pause();
        EquipedMask.OnDurationExpired -= OnMaskExpired;
        EquipedMask.OnUnequip();
        EquipedMask = null;
        maskRenderer.sprite = null;
        MaskUnequiped?.Invoke();
    }

    private void EquipNewMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Equiping mask");
        MaskEquiped?.Invoke(mask);
        mask.transform.SetParent(playerMovementHandler.EntityTransform, false);
        mask.transform.localPosition = UnityEngine.Vector3.zero;
        mask.gameObject.SetActive(false);
        maskRenderer.sprite = mask.MaskSprite;
        maskRenderer.enabled = true;
        EquipedMask = mask;
        EquipedMask.OnDurationExpired += OnMaskExpired;
        EquipedMask.OnEquip(playerMovementHandler);
        
    }

    private void OnMaskExpired()
    {
        EquipedMask.OnDurationExpired -= OnMaskExpired;
        EquipedMask.Destroy();
        EquipedMask = null;
        maskRenderer.sprite = null;
        
    }

    public void TryGetNewMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Trying to get new mask");
        equipMaskCountdown.Start(mask.timeToGetMask/*Time to equip mask. Must be configured from the player settings or other place.*/, () => EquipMask(mask));
        mask.StartPicking();
    }

    public void ResetEquipCooldown()
    {
        equipMaskCountdown.Reset();
    }
}
