using UnityEngine;

public class GetLowestBuff : BehaviorTree
{
    float distance;
    string enemyType;
    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        GameObject weakEnemy = null;
        float weakest = -1;
        foreach (var enemy in nearby)
        {
            float strength = ((EnemyAttack)enemy.GetComponent<EnemyController>().GetAction("attack")).StrengthFactor;
            if (weakEnemy == null || strength < weakest) {
                weakEnemy = enemy;
                weakest = strength;
            }
        }

        if (weakEnemy == null)
        {
            //Debug.Log("Can't find a Leader");
            return Result.FAILURE;
        }

        string key = "weak-enemy";
        if (agent.blackboard.ContainsKey(key)) {
            agent.blackboard[key] = weakEnemy;
        } else
        {
            agent.blackboard.Add(key, weakEnemy);
        }
        Debug.Log("Found Leader");
        return Result.SUCCESS;            
    }

    public GetLowestBuff(string enemyType, float distance) : base()
    {
        this.enemyType = enemyType;
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new GetLowestBuff(enemyType, distance);
    }
}
