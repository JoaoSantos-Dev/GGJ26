using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CharacterPusher : MonoBehaviour
{
   [SerializeField]  private LayerMask targetLayer;
    [SerializeField] private float force;

    // void Awake()
    // {
    //     targetLayer =  LayerMask.GetMask("Character");;
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Correct line.Uncomment when the effect collider problem has been solved.
        //if (!collision.TryGetComponent(out EntityBase entity)) return;
        if (!IsInTargetLayer(collision.gameObject,targetLayer)) return;
        if (!collision.TryGetComponent(out EnemyController entity)) return;
        // if (collision.transform == transform) return;
        Vector3 direction = (entity.transform.position - transform.position).normalized;
        entity.MovementHandler.PushEntity(force, direction, 0.5f);
    }

    bool IsInTargetLayer(GameObject obj, LayerMask mask)
{
    return (mask & (1 << obj.layer)) != 0;
}
 
}
