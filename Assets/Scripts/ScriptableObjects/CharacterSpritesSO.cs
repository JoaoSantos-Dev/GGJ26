using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Core
{

    public enum MaskType
    {
        DASH,
        TELEPORT,
        KNOCKBACK,
        SHIELD
    }

    [CreateAssetMenu(fileName = "CharacterSprites", menuName = "Game/CharacterSprites", order = 1)]
    public class CharacterSpritesSO : ScriptableObject
    {
        [field: SerializeField] public Sprite[] Heads { get; private set; }
        [field: SerializeField] public Sprite HitFace { get; private set; }

        [ SerializedDictionary("Mask", "sprite")]
        public SerializedDictionary<MaskType, Sprite> Masks;



    }
}