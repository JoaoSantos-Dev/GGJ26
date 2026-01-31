
using UnityEngine.UI;

public class PlayerInventoryController 
{
    public MaskBase EquipedMask { get; private set; }
    public bool CanUseMaskHability => EquipedMask != null && EquipedMask.Cooldown.IsReady;

    private PlayerMovementHandler playerMovementHandler;

    //Adjust here to get (from elsewhere on the player settings/configs) to get the default time for equipping a mask
    private Cooldown equipMaskCountdown;
   

    public PlayerInventoryController(PlayerMovementHandler playerMovementHandler)
    {
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
    }

    private void EquipNewMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Equiping mask");
        mask.transform.SetParent(playerMovementHandler.EntityTransform, false);
        mask.transform.localPosition = UnityEngine.Vector3.zero;
        EquipedMask = mask;
        EquipedMask.OnDurationExpired += OnMaskExpired;
        EquipedMask.OnEquip(playerMovementHandler);
    }

    private void OnMaskExpired()
    {
        EquipedMask.OnDurationExpired -= OnMaskExpired;
        EquipedMask.TestDestroy();
        EquipedMask = null;
    }

    public void TryGetNewMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Trying to get new mask");
        equipMaskCountdown.Start(2f/*Time to equip mask. Must be configured from the player settings or other place.*/, () => EquipMask(mask));
    }

    public void ResetEquipCooldown()
    {
        equipMaskCountdown.Reset();
    }
}
