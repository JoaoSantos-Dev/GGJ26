using UnityEngine;
using System.Collections;

namespace GameplaySystem.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] prefabs;
        public float interval = 5f;
        public bool spawnOnStart;

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            if (!spawnOnStart)
                yield return new WaitForSeconds(interval);

            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(interval);
            }
        }

        private void Spawn()
        {
            if (prefabs.Length == 0) return;

            var prefab = prefabs[Random.Range(0, prefabs.Length)];
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}