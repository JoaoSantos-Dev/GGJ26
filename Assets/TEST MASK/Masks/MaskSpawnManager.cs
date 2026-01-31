using UnityEngine;

public class MaskSpawnManager : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private Vector2 areaSize = new Vector2(5f, 5f);

    [Header("Spawn Settings")]
    [SerializeField] private GameObject prefabToSpawn;

    private MaskSpawnController maskSpawnController;

    public void Spawn()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("Prefab não definido no AreaSpawner.");
            return;
        }

        Vector2 randomPoint = GetRandomPointInArea();
        Instantiate(prefabToSpawn, randomPoint, Quaternion.identity);
    }

    private Vector2 GetRandomPointInArea()
    {
        Vector2 halfSize = areaSize * 0.5f;

        Vector2 localPoint = new Vector2(
            Random.Range(-halfSize.x, halfSize.x),
            Random.Range(-halfSize.y, halfSize.y)
        );

        return (Vector2)transform.position + localPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0f));
    }
}
