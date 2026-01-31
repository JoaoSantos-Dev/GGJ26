using UnityEngine;
using System.Collections;

namespace GameplaySystem.Spawning
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class WaveSpawnerArea : MonoBehaviour
    {
        public WaveData config;
        public float intervaloEntreHordas = 5f;

        public float TempoAteProximaHorda { get; private set; }
        public bool EmIntervaloEntreHordas { get; private set; }

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
                yield return StartCoroutine(RunHorde(config.hordas[i]));

                EmIntervaloEntreHordas = true;
                TempoAteProximaHorda = intervaloEntreHordas;

                while (TempoAteProximaHorda > 0)
                {
                    TempoAteProximaHorda -= Time.deltaTime;
                    yield return null;
                }

                EmIntervaloEntreHordas = false;
            }
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