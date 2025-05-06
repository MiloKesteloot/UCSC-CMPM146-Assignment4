using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        Transform secretMeetingSpot = AIWaypointManager.Instance.Get(3).transform;
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        new GoTo(secretMeetingSpot, 4f),
                                        new PermaBuff(),
                                        new Heal()
                                    });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new NearbyEnemiesQuery(2, 7f),
                                            new MoveToPlayer(4f),
                                            new Attack()
                                        }),
                                        new GoTo(secretMeetingSpot, 4f)
                                     });
        }
        else if (agent.monster == "skeleton")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new NearbyEnemiesQuery(2, 7f),
                                            new GetNearestEnemy("zombie", 7f),
                                            new FollowToTarget(4f),
                                            new Attack()
                                        }),
                                        new GoTo(secretMeetingSpot, 4f)
                                     });
        }

        // do not change/remove: each node should be given a reference to the agent
        foreach (var n in result.AllNodes())
        {
            n.SetAgent(agent);
        }
        return result;
    }
}
