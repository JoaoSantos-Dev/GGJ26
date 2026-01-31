using System.Collections.Generic;
using UnityEngine;

public class SpawnConfig : MonoBehaviour
{
    [field:SerializeField] public List<GameObject> MaskPrefabs { get; set; }
    [field:SerializeField] public int MinMaskOnScreen { get; private set; }
    [field:SerializeField] public int MaxMaskOnScreen { get; private set; }
}
