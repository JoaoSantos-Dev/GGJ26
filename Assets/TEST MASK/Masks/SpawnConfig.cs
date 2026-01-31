using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfig", menuName = "ScriptableObjects/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    [field:SerializeField] public List<GameObject> MaskPrefabs { get; private set; }
    [field:SerializeField] public float SpawnInterval { get; private set; }
    [field:SerializeField] public int MaxMaskOnScreen { get; private set; }
}
