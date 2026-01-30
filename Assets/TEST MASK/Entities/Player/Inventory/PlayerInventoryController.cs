
using System.Diagnostics;

public class PlayerInventoryController 
{
    public MaskBase EquipedMask { get; private set; }

    private PlayerMovementHandler playerMovementHandler;

    public PlayerInventoryController(PlayerMovementHandler playerMovementHandler)
    {
        this.playerMovementHandler = playerMovementHandler;
    }

    public void EquipMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Equiping mask");
        EquipedMask = mask;
        EquipedMask.OnEquip(playerMovementHandler as PlayerMovementHandler);
    }

    public void UnequipMask()
    {
        EquipedMask.OnUnequip();
        EquipedMask = null;
    }

    public void TryGetNewMask(MaskBase mask)
    {
        // Adjust here to put the required countdown before equiping a mask.
        if (EquipedMask == null) EquipMask(mask);
    }
}
