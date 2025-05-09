using UnityEngine;

public class GetClosestToPlayer : BehaviorTree
{
    float distance;
    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        GameObject closeEnemy = null;
        float closestDistance = 999999;
        foreach (var enemy in nearby)
        {
            float dist = (enemy.transform.position - agent.transform.position).magnitude;
            if (dist < closestDistance) {
                closeEnemy = enemy;
                closestDistance = dist;
            }
        }

        if (closeEnemy == null)
        {
            return Result.FAILURE;
        }

        string key = "close-enemy";
        if (agent.blackboard.ContainsKey(key)) {
            agent.blackboard[key] = closeEnemy;
        } else
        {
            agent.blackboard.Add(key, closeEnemy);
        }
        return Result.SUCCESS;            
    }

    public GetClosestToPlayer(float distance) : base()
    {
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new GetClosestToPlayer(distance);
    }
}
