using System;
using DG.Tweening;
using UnityEngine;

public class ShieldMask : MaskBase
{
    private Vector2 shieldScale;
    [SerializeField] private float shieldDuration = 2f;
    [Tooltip("The total time to the shiled to grow to its maximum size")]
    [SerializeField] private float defaultCastSpeed = 0.5f;
    // [SerializeField] private float pushForce = 3;
    [SerializeField] private CharacterPusher characterPusher; 

    public override event Action OnHabilityEnd;

    protected override void Awake()
    {
        base.Awake();
        MaskType = MaskType.Shield;
        characterPusher.gameObject.SetActive(false);
        shieldScale = characterPusher.transform.localScale;
    }
    public override void UseMaskHability()
    {
        base.UseMaskHability();

        // --- JOÃO: SOM DE DASH AQUI ---
        if (AudioManager.Instance != null && AudioManager.Instance.listaDeSons != null)
        {
            // Toca o som de Dash na posição atual do jogador
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.maskShield, transform.position);
        }

        characterPusher.transform.localScale = Vector2.zero;
        characterPusher.gameObject.SetActive(true);
        characterPusher.transform.DOScale(shieldScale, defaultCastSpeed)
        .SetEase(Ease.OutQuad)
        .SetUpdate(UpdateType.Fixed)
            .OnComplete(() =>
            {
                characterPusher.gameObject.SetActive(false);
            });

        OnHabilityEnd?.Invoke();
    }


}
