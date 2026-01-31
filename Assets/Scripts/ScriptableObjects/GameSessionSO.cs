using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "PlayerSession", menuName = "Game/PlayerSession")]
    public class GameSessionSO : ScriptableObject
    {
        [field: SerializeField, Range(1, 4)] public int MaxPlayer { get; set; } = 2;
        [field: SerializeField] public PlayerDataSO[] playerDatas { get; private set; }
        [field: SerializeField] public CharacterSpritesSO characterSprites { get; private set; }



        
    }
}