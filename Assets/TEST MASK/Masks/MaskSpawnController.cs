using UnityEngine;

public class MaskSpawnController 
{
    public MaskBase SpawnMask(GameObject maskPrefab, Vector3 position)
    {
        GameObject mask = GameObject.Instantiate(maskPrefab, position, Quaternion.identity);
        return mask.GetComponent<MaskBase>();
    }

}
