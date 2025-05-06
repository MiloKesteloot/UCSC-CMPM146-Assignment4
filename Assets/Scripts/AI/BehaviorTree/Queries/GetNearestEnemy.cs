using UnityEngine;

public class GetNearestEnemy : BehaviorTree
{
    float distance;
    string enemyType;
    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        EnemyController closestEnemy = null;
        float closestDistance = -1;
        foreach (var enemy in nearby)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            float dist = (ec.transform.position - agent.transform.position).magnitude;
            if (ec.monster == enemyType && (closestEnemy == null || dist < closestDistance)) {
                closestEnemy = ec;
                closestDistance = dist;
            }
        }
        agent.blackboard.Add("closestenemy-" + enemyType, closestEnemy);
        if (closestEnemy == null) {
            return Result.FAILURE;
        } else {
            return Result.SUCCESS;            
        }
    }

    public GetNearestEnemy(string enemyType, float distance) : base()
    {
        this.enemyType = enemyType;
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new Attack();
    }
}
