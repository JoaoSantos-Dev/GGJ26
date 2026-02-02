using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image image_MaskIconBG;
        [SerializeField] private Image image_MaskIcon;
        [SerializeField] private Image image_healthBarBG;
        [SerializeField] private Image image_maskDurationBG;
        [SerializeField] private Image image_healthBar;
        [SerializeField] private Image image_maskDuration;
        private int maxHealth;
        private PlayerController playerController;
        private Material material;
        private static int oultlineColorPropertyID = Shader.PropertyToID("_SolidOutline");
        private MaskBase maskBase;
        private void Awake()
        {
            image_healthBar.fillAmount = 0;
        }

        private void Start()
        {
            ResetMaskConfig();
        }

        private void OnDestroy()
        {
            playerController.HealthChanged -= OnHealthChange;
            playerController.InventoryController.MaskEquiped -= OnMaskEquiped;
            playerController.InventoryController.MaskUnequiped -= OnMaskUnequiped;

        }
        

        public async void Initialize(PlayerController playerController)
        {
            await UniTask.Delay(200);
            image_healthBar.DOFillAmount(1, 0.2f).SetEase(Ease.OutQuad);
            this.playerController = playerController;
            SetImageOutlineColor(this.playerController);
            maxHealth = playerController.Health;
            playerController.HealthChanged += OnHealthChange;
            playerController.InventoryController.MaskEquiped += OnMaskEquiped;
            playerController.InventoryController.MaskUnequiped += OnMaskUnequiped;

        }



        private void OnHealthChange(int health)
        {
            var healthPercentage = health / (float)maxHealth;
            image_healthBar.DOFillAmount(healthPercentage, 0.5f).SetEase(Ease.OutQuad);
        }

        private void SetImageOutlineColor(PlayerController player)
        {
            image_healthBar.color = Color.white;
            image_healthBarBG.color = player.CharacterConfig.Color;
            image_maskDurationBG.color = player.CharacterConfig.Color;
            
            var maskMaterial = image_MaskIcon.material;
            maskMaterial.SetColor(oultlineColorPropertyID, player.CharacterConfig.Color);
        }
        
        
        private void OnMaskEquiped(MaskBase mask)
        {
            image_MaskIconBG.sprite = mask.maskIcon;
            image_MaskIconBG.enabled = true;
            image_MaskIcon.sprite = mask.maskIcon;
            image_MaskIcon.enabled = true;
            maskBase = mask;
            mask.MaskDurationCountdown.Updated += OnCooldowMaskUpdated;
            mask.UseHabilityCooldown.Updated += OnHabilityCooldownUpdate;


        }
        
        private void OnMaskUnequiped(MaskBase mask)
        {
            ResetMaskConfig();
            mask.MaskDurationCountdown.Updated -= OnCooldowMaskUpdated;
            mask.UseHabilityCooldown.Updated -= OnHabilityCooldownUpdate;

        }

        private void ResetMaskConfig()
        {
            image_MaskIconBG.sprite = null;
            image_MaskIconBG.enabled = false;
            image_MaskIcon.sprite = null;
            image_MaskIcon.enabled = false;
            maskBase = null;
            image_maskDuration.fillAmount = 0;
        }
        
        private void OnCooldowMaskUpdated(float remeiningTime)
        {
            image_maskDuration.fillAmount =  remeiningTime / maskBase.MaskDurationCountdown.Duration;
        }
        
        private void OnHabilityCooldownUpdate(float value)
        {
            image_MaskIcon.fillAmount = 1- value / maskBase.UseHabilityCooldown.Duration;
        }


        
    }
}