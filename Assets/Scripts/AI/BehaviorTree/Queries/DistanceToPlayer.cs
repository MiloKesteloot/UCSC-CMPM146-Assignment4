using UnityEngine;

public class DistanceToPlayer : BehaviorTree
{
    float distance;
    bool closerThan;

    public override Result Run()
    {
        Vector3 direction = GameManager.Instance.player.transform.position - agent.transform.position;
        if (closerThan) {
            return direction.magnitude < distance ? Result.SUCCESS : Result.FAILURE;
        }
        return direction.magnitude >= distance ? Result.SUCCESS : Result.FAILURE;
    }

    public DistanceToPlayer(float distance, bool closerThan) : base()
    {
        this.distance = distance;
        this.closerThan = closerThan;
    }

    public override BehaviorTree Copy()
    {
        return new DistanceToPlayer(distance, closerThan);
    }
}
