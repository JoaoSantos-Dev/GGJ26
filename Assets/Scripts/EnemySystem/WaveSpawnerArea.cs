using UnityEngine;
using System.Collections;

namespace GameplaySystem.Spawning
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WaveSpawnerArea2D : MonoBehaviour
    {
        public WaveData config;
        public float intervaloEntreHordas = 5f;

        private BoxCollider2D area;

        private void Awake()
        {
            area = GetComponent<BoxCollider2D>();
            area.isTrigger = true;
        }

        private void Start()
        {
            if (config != null && config.hordas.Count > 0)
                StartCoroutine(RotineOfSpawn());
        }

        private IEnumerator RotineOfSpawn()
        {
            for (int i = 0; i < config.hordas.Count; i++)
            {
                Debug.Log($"Iniciando: {config.hordas[i].nomeDaHorda}");
                yield return StartCoroutine(RunHorde(config.hordas[i]));

                yield return new WaitForSeconds(intervaloEntreHordas);
            }
            Debug.Log("Todas as hordas concluídas!");
        }

        private IEnumerator RunHorde(Wave horda)
        {
            int spawned = 0;
            while (spawned < horda.totalDeInimigos)
            {
                for (int i = 0; i < horda.quantidadePorVez; i++)
                {
                    if (spawned >= horda.totalDeInimigos) break;

                    SpawnEnemie(horda.inimigosPossiveis);
                    spawned++;
                }
                yield return new WaitForSeconds(horda.intervaloEntreSpawn);
            }
        }

        private void SpawnEnemie(GameObject[] prefabs)
        {
            if (prefabs.Length == 0) return;

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            Vector3 spawnPos = SpawnPoin();

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        private Vector3 SpawnPoin()
        {
            Bounds bounds = area.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);


            return new Vector3(x, y, transform.position.z);
        }
    }
}