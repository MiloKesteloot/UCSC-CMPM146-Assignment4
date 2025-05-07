using UnityEngine;

public class Attack : BehaviorTree
{

    public override Result Run()
    {
        Debug.Log("Attacking!");
        EnemyAction act = agent.GetAction("attack");
        if (act == null) return Result.FAILURE;

        bool success = act.Do(GameManager.Instance.player.transform);
        
        return (success ? Result.SUCCESS : Result.FAILURE);
    }

    public Attack() : base()
    {
       
    }

    public override BehaviorTree Copy()
    {
        return new Attack();
    }
}
