using UnityEngine;

public class PermaBuff : BehaviorTree
{
    public override Result Run()
    {
        var target = agent.blackboard["weak-enemy"];
        EnemyAction act = agent.GetAction("permabuff");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(target.transform);
        Debug.Log("Buffed enemy " + ((EnemyAttack)target.GetComponent<EnemyController>().GetAction("attack")).StrengthFactor);
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public PermaBuff() : base()
    {

    }

    public override BehaviorTree Copy()
    {
        return new PermaBuff();
    }
}
