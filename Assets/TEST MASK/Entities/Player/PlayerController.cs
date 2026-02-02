using System;
using Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Playersystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterData
{
    public Color Color { get; set; }
    public Sprite HeadSprite { get; set; }

    public CharacterData(Sprite sprite, Color color)
    {
        this.Color = color;
        this.HeadSprite = sprite;

    }

}

public class PlayerController : EntityBase, IDamageable
{
    public CharacterData VisualConfig { get; set; }
    [field: SerializeField] public CharacterSpritesSO CharacterSprites { get; private set; }
    private Animator animator;
    [SerializeField] private SpriteRenderer baseSpriteRenderer;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer maskRenderer;
    [field: SerializeField]
    [field: Min(0)]
    public int Health { get; set; } = 100;

    [SerializeField] private float playerDefaultSpeed;
    private PlayerInputController inputController;
    private int maxHealth;
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } 
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInventoryController InventoryController { get; private set; }
    public AnimationController AnimationController { get; private set; }

    public Rigidbody2D Rigidbody2D { get; private set; }
    public event Action<int> HealthChanged;
    public event Action<PlayerController> Death;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        // --- JOÃO: ADICIONEI ESTE BLOCO AQUI ---
        if (animator != null)
        {
            // Isso anexa o script de áudio diretamente no objeto que tem o Animator
            if (animator.gameObject.GetComponent<AnimationAudioRelay>() == null)
            {
                animator.gameObject.AddComponent<AnimationAudioRelay>();
            }
        }

        AnimationController = new AnimationController(characterRenderer, maskRenderer, headRenderer, animator);
        maxHealth = Health;
        inputController = new PlayerInputController(PlayerInput);
        MovementHandler = new PlayerMovementHandler(transform, 
            Rigidbody2D,
            playerDefaultSpeed,
            () => inputController.MovementDirection);
        InventoryController = new PlayerInventoryController(maskRenderer, MovementHandler as PlayerMovementHandler);
        StateMachine = new PlayerStateMachine(inputController, InventoryController,
            MovementHandler as PlayerMovementHandler, this);
    }

    private void Update()
    {
        StateMachine.Update();
        // if (inputController.Hability.WasPressedThisFrame()) TakeDamage(20);
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

            //DestroyChildren();

            InventoryController.TryGetNewMask(mask);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MaskBase mask))
        {
            InventoryController.ResetEquipCooldown();
            mask.StopPicking();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Clamp(damage, 0, maxHealth);
        HealthChanged?.Invoke(Health);
        ApplyHitEffect();
        if (Health <= 0) Death?.Invoke(this);
        
    }

    //Created only for test purposes.
    private void DestroyChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private static readonly Vector3 hitMaskLocalPosition = new Vector3(0, 1f, 0);
    private static readonly Vector3 hitMaskLocalRotation = new Vector3(-64, 0, 0);

    public async void ApplyHitEffect()
    {
        // --- JOÃO: SOM DE DANO AQUI ---
        if (AudioManager.Instance != null && AudioManager.Instance.listaDeSons != null)
        {
            // som configurado no scriptableoject
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.tomarDano, transform.position);
        }
        // ------------------------------

        maskRenderer.transform.localPosition = hitMaskLocalPosition;
        maskRenderer.transform.rotation = Quaternion.Euler(hitMaskLocalRotation);

        headRenderer.sprite = CharacterSprites.HitFace;
        headRenderer.color = Color.red;
        characterRenderer.color = Color.red;
        characterRenderer.DOColor(Color.white, 0.3f);
        headRenderer.DOColor(Color.white, 0.3f);

        await UniTask.Delay(300);
        headRenderer.sprite = VisualConfig.HeadSprite;
        maskRenderer.transform.localPosition = Vector3.up *-.1f;
        maskRenderer.transform.rotation = quaternion.identity;


    }

    public void SetVisual(Sprite head, Color color)
    {
        VisualConfig = new(head, color);
        headRenderer.sprite = VisualConfig.HeadSprite;
        if (baseSpriteRenderer != null) baseSpriteRenderer.color = VisualConfig.Color;

    }

}