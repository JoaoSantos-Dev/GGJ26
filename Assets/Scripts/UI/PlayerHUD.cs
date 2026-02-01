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
        [SerializeField] private Image image_MaskIcon;
        [SerializeField] private Image image_healthBarBG;
        [SerializeField] private Image image_maskDurationBG;
        [SerializeField] private Image image_healthBar;
        private int maxHealth;
        private PlayerController playerController;
        private Material material;
        private static int oultlineColorPropertyID = Shader.PropertyToID("_SolidOutline");
        private void Awake()
        {
            image_healthBar.fillAmount = 0;
        }

        private void Start()
        {
            HideMaskIcon();
        }

        private void OnDestroy()
        {
            playerController.HealthChanged -= OnHealthChange;
            playerController.InventoryController.MaskEquiped -= ShowMaskEquiped;
            playerController.InventoryController.MaskUnequiped -= HideMaskIcon;
        }
        

        public async void Initialize(PlayerController playerController)
        {
            await UniTask.Delay(200);
            image_healthBar.DOFillAmount(1, 0.2f).SetEase(Ease.OutQuad);
            this.playerController = playerController;
            SetImageOutlineColor(this.playerController);
            maxHealth = playerController.Health;
            playerController.HealthChanged += OnHealthChange;
            playerController.InventoryController.MaskEquiped += ShowMaskEquiped;
            playerController.InventoryController.MaskUnequiped += HideMaskIcon;
        }


        private void OnHealthChange(int health)
        {
            var healthPercentage = health / (float)maxHealth;
            image_healthBar.DOFillAmount(healthPercentage, 0.5f).SetEase(Ease.OutQuad);
        }

        private void SetImageOutlineColor(PlayerController player)
        {
            image_healthBar.color = Color.white;
            image_healthBarBG.color = player.VisualConfig.Color;
            image_maskDurationBG.color = player.VisualConfig.Color;
            
            var maskMaterial = image_MaskIcon.material;
            maskMaterial.SetColor(oultlineColorPropertyID, player.VisualConfig.Color);
        }
        
        
        private void ShowMaskEquiped(MaskBase mask)
        {
            image_MaskIcon.sprite = mask.maskIcon;
            image_MaskIcon.enabled = true;
            Debug.Log("Equiped mask UI");
        }

        private void HideMaskIcon()
        {
            image_MaskIcon.sprite = null;
            image_MaskIcon.enabled = false;
        }
        
        
    }
}