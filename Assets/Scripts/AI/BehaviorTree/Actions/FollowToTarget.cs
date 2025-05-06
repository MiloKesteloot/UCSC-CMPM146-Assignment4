using UnityEngine;

public class FollowToTarget : BehaviorTree
{
    Transform leader;
    Transform target;
    float distanceFromTarget;
    bool in_progress;
    Vector3 start_point;

    public override Result Run() {
        if (!in_progress)
        {
            in_progress = true;
            start_point = agent.transform.position;
            leader = agent.blackboard["closest-zombie"].transform;
        }

        if (leader == null)
        {
            return Result.FAILURE;
        }

        Vector3 targetDirection = target.position - agent.transform.position;
        Vector3 followDirection = leader.position - agent.transform.position;
        Vector3 leaderDirection = target.position - leader.position;
        if (targetDirection.magnitude < distanceFromTarget)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            in_progress = false;
            return Result.SUCCESS;
        }
        else if (targetDirection.magnitude < leaderDirection.magnitude)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            return Result.IN_PROGRESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = followDirection.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public FollowToTarget(float distanceFromTarget) : base() {
        this.target = GameManager.Instance.player.transform;
        this.distanceFromTarget = distanceFromTarget;
        this.in_progress = false;
    }

    public override BehaviorTree Copy() {
        return new FollowToTarget(distanceFromTarget);
    }
}