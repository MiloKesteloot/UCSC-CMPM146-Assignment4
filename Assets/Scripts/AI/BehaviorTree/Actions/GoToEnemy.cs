using UnityEngine;

public class GoToEnemy : BehaviorTree
{
    string key;
    float arrived_distance;

    public override Result Run()
    {
        if(!agent.blackboard.ContainsKey(key) || agent.blackboard[key] == null) {
            return Result.FAILURE;
        }
        Transform target = agent.blackboard[key].transform;
        Vector3 direction = target.position - agent.transform.position;
        if (direction.magnitude < arrived_distance)
        {
            agent.GetComponent<Unit>().movement = new Vector2(0, 0);
            return Result.SUCCESS;
        }
        else
        {
            agent.GetComponent<Unit>().movement = direction.normalized;
            return Result.IN_PROGRESS;
        }
    }

    public GoToEnemy(string key, float arrived_distance) : base()
    {
        this.key = key;
        this.arrived_distance = arrived_distance;
    }

    public override BehaviorTree Copy()
    {
        return new GoToEnemy(key, arrived_distance);
    }
}

