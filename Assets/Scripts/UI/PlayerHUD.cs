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

        private void OnDestroy()
        {
            playerController.HealthChanged -= OnHealthChange;
        }

        public void Initialize(PlayerController playerController)
        {
            this.playerController = playerController;
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