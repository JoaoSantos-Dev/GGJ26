using UnityEngine;

namespace GameplaySystem.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy Selection")]
        [Tooltip("Add enemy prefabs to this list in the Inspector")]
        [SerializeField] private GameObject[] enemyPrefabs;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private bool spawnImmediately = false;

        private float _spawnTimer;

        private void Start()
        {
            _spawnTimer = spawnImmediately ? 0f : spawnInterval;
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0f)
            {
                SpawnRandomEnemy();
                _spawnTimer = spawnInterval;
            }
        }

        private void SpawnRandomEnemy()
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            {
                Debug.LogWarning($"[EnemySpawner] {gameObject.name} has no prefabs assigned in the inspector!");
                return;
            }

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject selectedPrefab = enemyPrefabs[randomIndex];

            if (selectedPrefab != null)
            {
                Instantiate(selectedPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
