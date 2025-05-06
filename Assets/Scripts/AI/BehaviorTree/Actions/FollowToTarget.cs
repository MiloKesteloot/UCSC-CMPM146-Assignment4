using UnityEngine;

public class FollowToTarget : BehaviorTree
{
    Transform leader;
    Transform target;
    float distanceFromTarget;
    bool in_progress;
    Vector3 start_point;

    public override Result Run() {
        // if (!in_progress)
        // {
        //     in_progress = true;
        //     start_point = agent.transform.position;
        // }
        // Vector3 direction = target.position - agent.transform.position;
        // if ((direction.magnitude < arrived_distance) || (agent.transform.position - start_point).magnitude >= distance)
        // {
        //     agent.GetComponent<Unit>().movement = new Vector2(0, 0);
        //     in_progress = false;
        //     return Result.SUCCESS;
        // }
        // else
        // {
        //     agent.GetComponent<Unit>().movement = direction.normalized;
        //     return Result.IN_PROGRESS;  
        // }
        return Result.FAILURE;
    }

    public FollowToTarget(float distanceFromTarget) : base() {
        this.leader = null; // get zombie from blackboard
        this.target = GameManager.Instance.player.transform;
        this.distanceFromTarget = distanceFromTarget;
        this.in_progress = false;
    }

    public override BehaviorTree Copy() {
        return new FollowToTarget(distanceFromTarget);
    }
}