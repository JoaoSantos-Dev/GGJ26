using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MaskSpawnManager : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private Vector2 spawnArea = new Vector2(5f, 5f);
    [Space(10)]

    [Header("Spawn Config")]
    [SerializeField] private SpawnConfig spawnConfig;

    public static MaskSpawnManager Instance { get; private set; }

    private MaskSpawnController maskSpawnController;
    private Cooldown spawnCooldown;
    private Queue<GameObject> spawnQueue;
    private List<MaskBase> masksOnScreen;
    private MaskType lastSpawnedMask;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        spawnCooldown = new Cooldown();
        maskSpawnController = new MaskSpawnController();
        spawnQueue = new Queue<GameObject>();
        masksOnScreen = new List<MaskBase>();
    }

    private void Update()
    {
        SpawnNewMask();
    }

    private void ConfigureSpawn()
    {
        if (spawnConfig.MaskPrefabs.Count == 0) return;
        List<GameObject> list = spawnConfig.MaskPrefabs;

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        spawnQueue = new Queue<GameObject>(list);
    }

    public void SpawnNewMask()
    {
        if (!CanSpawnAnotherMask()) return;
        spawnCooldown.Prepare(spawnConfig.SpawnInterval);

        DOVirtual.DelayedCall(Random.Range(0f, 2f),() =>
        {
            if (spawnQueue.Count == 0) ConfigureSpawn();

            GameObject maskToSpawn = spawnQueue.Dequeue();

            if (maskToSpawn.TryGetComponent(out MaskBase mask) && mask.MaskType == lastSpawnedMask)
            {
                spawnQueue.Enqueue(maskToSpawn);
                maskToSpawn = spawnQueue.Dequeue();
                mask = maskToSpawn.GetComponent<MaskBase>();
                lastSpawnedMask = mask.MaskType;
            }
            else
            {
                lastSpawnedMask = maskToSpawn.GetComponent<MaskBase>().MaskType;
            }

            TrackMask(maskSpawnController.SpawnMask(maskToSpawn, GetRandomPointInArea()));
            spawnCooldown.Resume();
        });
    }

    private bool CanSpawnAnotherMask()
    {
        return spawnCooldown.IsReady && masksOnScreen.Count < spawnConfig.MaxMaskOnScreen;
    }

    public void TrackMask(MaskBase mask)
    {
        masksOnScreen.Add(mask);
    }
    public void UntrackMask(MaskBase mask)
    {
        masksOnScreen.Remove(mask);
    }

    private Vector3 GetRandomPointInArea()
    {
        // The radius of the masks circle collider.
        float radius = 0.5f;
        Vector2 halfSize = spawnArea * 0.5f;
        Vector2 randomPoint = new Vector2(Random.Range(-halfSize.x, halfSize.x), Random.Range(-halfSize.y, halfSize.y));

        foreach (MaskBase mask in masksOnScreen)
        {
            if (Vector2.Distance((Vector2)transform.position + randomPoint, mask.transform.position) <= radius * 2) return GetRandomPointInArea();
        }

        return (Vector2)transform.position + randomPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnArea.x, spawnArea.y, 0f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea.x, spawnArea.y, 0f));
    }
}
