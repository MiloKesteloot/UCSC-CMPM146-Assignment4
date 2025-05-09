using UnityEngine;

public class Buff : BehaviorTree
{
    public override Result Run()
    {
        GameObject target = agent.blackboard["close-enemy"];
        EnemyAction act = agent.GetAction("buff");
        if (act == null) return Result.FAILURE;
        
        bool success = act.Do(target.transform);
        Debug.Log("Buffed enemy " + ((EnemyAttack)target.GetComponent<EnemyController>().GetAction("attack")).StrengthFactor);
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public Buff() : base()
    {

    }

    public override BehaviorTree Copy()
    {
        return new Buff();
    }
}
