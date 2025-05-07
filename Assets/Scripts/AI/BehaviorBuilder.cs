using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        Transform secretMeetingSpot = AIWaypointManager.Instance.Get(2).transform;
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Sequence(new BehaviorTree[] {
                                        // new GoTo(secretMeetingSpot, 4f),
                                        // new PermaBuff(),
                                        // new Heal()
                                        new MoveToPlayer(1f),
                                    });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new Selector(new BehaviorTree[] {
                                                new CheckFlag("ATTACK", true),
                                                new NearbyEnemiesQuery(6, 7f),
                                            }),
                                            new SetFlag("ATTACK", true),
                                            
                                            new MoveToPlayer(1f),
                                            new Attack()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new GoTo(secretMeetingSpot, 4f)
                                        })
                                     });
        }
        else if (agent.monster == "skeleton")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new Selector(new BehaviorTree[] {
                                                new CheckFlag("ATTACK", true),
                                                new NearbyEnemiesQuery(6, 7f),
                                            }),
                                            new SetFlag("ATTACK", true),

                                            new GetNearestEnemy("zombie", 7f),
                                            new FollowToTarget("closest-zombie", 4f),
                                            new MoveToPlayer(1f),
                                            new Attack()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new GoTo(secretMeetingSpot, 4f)
                                        })
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
