
public class EnemyController : EntityBase
{
    private void Awake()
    {
        MovementHandler = new EnemyMovementHandler(transform);
    }
}
