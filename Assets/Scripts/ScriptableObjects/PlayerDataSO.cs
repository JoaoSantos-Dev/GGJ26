using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        [field: SerializeField, Range(1,4)] public int Id { get; private set; } = 1;
        [field: SerializeField] public Color Color { get; private set; }


    }
    
}