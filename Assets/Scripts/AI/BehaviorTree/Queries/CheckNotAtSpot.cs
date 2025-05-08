using UnityEngine;

public class CheckNotAtSpot : BehaviorTree
{
    Transform target;
    float arrived_distance;

    public override Result Run()
    {
        Vector3 direction = target.position - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            return Result.FAILURE;
        }
        return Result.SUCCESS;
    }

    public CheckNotAtSpot(Transform target, float arrived_distance) : base()
    {
        this.target = target;
        this.arrived_distance = arrived_distance;
    }

    public override BehaviorTree Copy()
    {
        return new CheckNotAtSpot(target, arrived_distance);
    }
}
