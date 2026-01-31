using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image image_healthBar;
        private int maxHealth;
        private PlayerController playerController;

        private void Awake()
        {
            image_healthBar.fillAmount = 0;
        }

        private void OnDestroy()
        {
            playerController.HealthChanged -= OnHealthChange;
        }

        public async void Initialize(PlayerController playerController)
        {
            await UniTask.Delay(200);
            image_healthBar.DOFillAmount(1, 0.2f).SetEase(Ease.OutQuad);
            this.playerController = playerController;
            image_healthBar.color = playerController.VisualConfig.Color;
            maxHealth = playerController.Health;
            playerController.HealthChanged += OnHealthChange;
        }

        private void OnHealthChange(int health)
        {
            var healthPercentage = health / (float)maxHealth;
            image_healthBar.DOFillAmount(healthPercentage, 0.5f).SetEase(Ease.OutQuad);
        }
    }
}