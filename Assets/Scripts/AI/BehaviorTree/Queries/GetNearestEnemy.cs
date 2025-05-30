using UnityEngine;

public class GetNearestEnemy : BehaviorTree
{
    float distance;
    string enemyType;
    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        GameObject closestEnemy = null;
        float closestDistance = -1;
        foreach (var enemy in nearby)
        {
            float dist = (enemy.transform.position - agent.transform.position).magnitude;
            if (enemy.GetComponent<EnemyController>().monster == enemyType && (closestEnemy == null || dist < closestDistance)) {
                closestEnemy = enemy;
                closestDistance = dist;
            }
        }

        if (closestEnemy == null)
        {
            //Debug.Log("Can't find a Leader");
            return Result.FAILURE;
        }

        string key = "closest-" + enemyType;
        if (agent.blackboard.ContainsKey(key)) {
            agent.blackboard[key] = closestEnemy;
            Debug.Log("Found new Leader " + closestEnemy.transform);
        } else
        {
            agent.blackboard.Add(key, closestEnemy);
        }
        Debug.Log("Found Leader");
        return Result.SUCCESS;            
    }

    public GetNearestEnemy(string enemyType, float distance) : base()
    {
        this.enemyType = enemyType;
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new GetNearestEnemy(enemyType, distance);
    }
}
