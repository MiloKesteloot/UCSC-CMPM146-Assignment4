using UnityEngine;

public class FollowToTarget : BehaviorTree
{
    Transform leader;
    Transform target;
    float distanceFromTarget;
    bool in_progress;
    Vector3 start_point;
    string key;

    public override Result Run() {
        if (!in_progress)
        {
            in_progress = true;
            start_point = agent.transform.position;
            leader = agent.blackboard[key].transform;
        }

        if (leader == null)
        {
            Debug.Log("Leader is null");
            return Result.FAILURE; // TODO check if this should be true. If there are no zombies but we are attacking, I think we should still attack
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
            Debug.Log("Going backwards");
            agent.GetComponent<Unit>().movement = -targetDirection.normalized;
            return Result.IN_PROGRESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = followDirection.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public FollowToTarget(string key, float distanceFromTarget) : base() {
        this.target = GameManager.Instance.player.transform;
        this.key = key;
        this.distanceFromTarget = distanceFromTarget;
        this.in_progress = false;
    }

    public override BehaviorTree Copy() {
        return new FollowToTarget(key, distanceFromTarget);
    }
}
