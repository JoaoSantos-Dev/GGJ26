using System;
using Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Playersystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterData
{
    private int id;
    private readonly Color color;
    private readonly Sprite headSprite;
    public Color Color => color;
    public Sprite HeadSprite => headSprite;
    public int ID => id;

    public CharacterData(Sprite sprite, Color color, int id)
    {
        this.color = color;
        this.headSprite = sprite;
        this.id = id;

    }

}

public class PlayerController : EntityBase, IDamageable
{
    public CharacterData CharacterConfig { get; private set; }
    [field: SerializeField] public CharacterSpritesSO CharacterSprites { get; private set; }
    private Animator animator;
    [SerializeField] private SpriteRenderer baseSpriteRenderer;
    [SerializeField] private SpriteRenderer characterRenderer;
    [field: SerializeField] public SpriteRenderer HeadRenderer { get;private set; }
    [field: SerializeField] public SpriteRenderer MaskRenderer { get; private set; }
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

    public event Action<int> HealthChanged;
    public event Action<PlayerController> Death;

    protected override void Awake()
    {
        base.Awake();
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

        AnimationController = new AnimationController(characterRenderer, MaskRenderer, HeadRenderer, animator);
        maxHealth = Health;
        inputController = new PlayerInputController(PlayerInput);
        MovementHandler = new PlayerMovementHandler(transform, 
            Rigidbody2D,
            playerDefaultSpeed,
            () => inputController.MovementDirection);
        InventoryController = new PlayerInventoryController(MaskRenderer, MovementHandler as PlayerMovementHandler);
        StateMachine = new PlayerStateMachine(inputController, InventoryController,
            MovementHandler as PlayerMovementHandler, this);
    }


    private void OnEnable()
    {
        StunStateChange += OnStunStateChange;
    }

    private void OnDisable()
    {
        StunStateChange -= OnStunStateChange;
    }

    private void OnStunStateChange(bool value)
    { 
        HeadRenderer.enabled = !value;
        MaskRenderer.enabled = !value;
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.simulated = !value;
        if (value)
        {
            AnimationController.PlayStun();
            
        }
        else AnimationController.PlayIdle();
   
    }

    private void Update()
    {
        if (StunActive) return;
        StateMachine.Update();
        // if (inputController.Hability.WasPressedThisFrame()) TakeDamage(20);
    }

    private void FixedUpdate()
    {
        if (StunActive) return;
        StateMachine.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(StunActive) return;
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

        MaskRenderer.transform.localPosition = hitMaskLocalPosition;
        MaskRenderer.transform.rotation = Quaternion.Euler(hitMaskLocalRotation);

        HeadRenderer.sprite = CharacterSprites.HitFace;
        HeadRenderer.color = Color.red;
        characterRenderer.color = Color.red;
        characterRenderer.DOColor(Color.white, 0.3f);
        HeadRenderer.DOColor(Color.white, 0.3f);

        await UniTask.Delay(300);
        HeadRenderer.sprite = CharacterConfig.HeadSprite;
        MaskRenderer.transform.localPosition = Vector3.up *-.1f;
        MaskRenderer.transform.rotation = quaternion.identity;


    }

    public void SetCharacterData(Sprite head, Color color, int id)
    {
        CharacterConfig = new(head, color, id);
        HeadRenderer.sprite = CharacterConfig.HeadSprite;
        if (baseSpriteRenderer != null) baseSpriteRenderer.color = CharacterConfig.Color;

    }

}