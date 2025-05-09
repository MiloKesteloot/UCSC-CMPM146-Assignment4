using UnityEngine;

public class GetHurtEnemy : BehaviorTree
{
    float distance;
    float minHeal;
    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        GameObject hurtEnemy = null;
        float mosthurt = -1;
        foreach (var enemy in nearby)
        {
            Hittable hittable = enemy.GetComponent<EnemyController>().hp;
            float needHP = hittable.max_hp - hittable.hp;
            if (hurtEnemy == null || needHP > mosthurt) {
                hurtEnemy = enemy;
                mosthurt = needHP;
            }
        }

        if (hurtEnemy == null || mosthurt < minHeal)
        {
            //Debug.Log("Can't find a Leader");
            return Result.FAILURE;
        }

        string key = "hurt-enemy";
        if (agent.blackboard.ContainsKey(key)) {
            agent.blackboard[key] = hurtEnemy;
        } else
        {
            agent.blackboard.Add(key, hurtEnemy);
        }
        Debug.Log("Found Leader");
        return Result.SUCCESS;            
    }

    public GetHurtEnemy(float minHeal, float distance) : base()
    {
        this.minHeal = minHeal;
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new GetHurtEnemy(minHeal, distance);
    }
}
