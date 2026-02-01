
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController
{
    public MaskBase EquipedMask { get; private set; }
    public bool CanUseMaskHability => EquipedMask != null && EquipedMask.UseHabilityCooldown.IsReady;

    private PlayerMovementHandler playerMovementHandler;

    //Adjust here to get (from elsewhere on the player settings/configs) to get the default time for equipping a mask
    private Cooldown equipMaskCountdown;
    private readonly SpriteRenderer maskRenderer;
    public event Action<MaskBase> MaskEquiped;
    public event Action<MaskBase> MaskUnequiped;
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
        MaskUnequiped?.Invoke(EquipedMask);
        EquipedMask.MaskDurationCountdown.Pause();
        EquipedMask.OnDurationExpired -= OnMaskExpired;
        EquipedMask.OnUnequip();
        EquipedMask = null;
        maskRenderer.sprite = null;
    }

    private void EquipNewMask(MaskBase mask)
    {
        UnityEngine.Debug.Log("Equiping mask");
        maskRenderer.sprite = mask.MaskSprite;
        maskRenderer.enabled = true;
        mask.transform.localPosition = Vector3.zero;
        mask.OnDurationExpired += OnMaskExpired;
        mask.transform.SetParent(playerMovementHandler.EntityTransform, false);
        mask.maskRenderer.enabled = (false);
        EquipedMask = mask;

        //João adicionou o try pro som de equipar máscara aqui
        try
        {
            if (AudioManager.Instance != null)
            {
                Debug.Log("Chamando PlayEffect do Manager...");
                AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.vestirMascara, playerMovementHandler.EntityTransform.position);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogWarning("Erro ao tocar som de máscara: " + e.Message);
        }
        //Coloquei aqui no meio pois o código trava na linha de baixo, não tive tempo de verificar o porque
        //Pode ser erro ou uma chamada diferente, talvez não implementada?

        MaskEquiped?.Invoke(mask);
        EquipedMask.OnEquip(playerMovementHandler);

    }

    private void OnMaskExpired()
    {
        //João adicionou o try pro som de expirar máscara aqui
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.expirarMascara, playerMovementHandler.EntityTransform.position);
        }

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
