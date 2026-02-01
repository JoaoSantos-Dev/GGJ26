using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Core
{
    
    [CreateAssetMenu(fileName = "CharacterSprites", menuName = "Game/CharacterSprites", order = 1)]
    public class CharacterSpritesSO : ScriptableObject
    {
        [field: SerializeField] public Sprite[] Heads { get; private set; }
        [field: SerializeField] public Sprite HitFace { get; private set; }
        
        public Sprite GetRandomHeadSprite()
        {
            int randomIndex = Random.Range(0, Heads.Length);
            return Heads[randomIndex];
        }

    }
}