using System;
using StunSystem;
using UnityEngine;

public class DashMask : MaskBase
{
    [SerializeField] private float dashDistance = 1f;
    [SerializeField] private float dashSpeed = 0.1f;
    public override event Action OnHabilityEnd;
    [SerializeField] private StunApplier stunApplier;

    protected override void Awake()
    {
        base.Awake();
        MaskType = MaskType.Dash;
    }

    private void OnEnable()
    {
        OnHabilityUsed += OnHabilityUsedMethod;
        OnHabilityEnd += OnHabilityEndMethod;
    }

    private void OnDisable()
    {
        OnHabilityUsed -= OnHabilityUsedMethod;
        OnHabilityEnd -= OnHabilityEndMethod;
    }
    

    public override void UseMaskHability()
    {
        base.UseMaskHability();

        // --- JOÃO: SOM DE DASH AQUI ---
        if (AudioManager.Instance != null && AudioManager.Instance.listaDeSons != null)
        {
            // Toca o som de Dash na posição atual do jogador
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.maskDash, transform.position);
        }
        // ------------------------------

        Vector3 dashDirection = new Vector3(playerMovementHandler.LastMovementDirection.x, playerMovementHandler.LastMovementDirection.y, 0).normalized;
        playerMovementHandler.AddToEntityPosition(dashDirection * dashDistance, dashSpeed, default, default, () => { OnHabilityEnd?.Invoke(); });
        
    }
    
    
    private void OnHabilityUsedMethod()
    {
        stunApplier.SetActive(true);
        
        //TODO: Refact later
        playerMovementHandler.EntityTransform.GetComponent<Collider2D>().enabled = false;

    }
    private void OnHabilityEndMethod()
    {
        stunApplier.SetActive(false);
        
        //TODO: Refact later
        playerMovementHandler.EntityTransform.GetComponent<Collider2D>().enabled = true;

    }
    
    
}
