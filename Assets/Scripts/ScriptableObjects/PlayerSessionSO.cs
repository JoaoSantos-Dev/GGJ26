using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSession", menuName = "Game/PlayerSession")]
public class PlayerSessionSO : ScriptableObject
{
    [field: SerializeField, Range(2,4)] public int MaxPlayer { get;  set; } = 2;
}
