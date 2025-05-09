using UnityEngine;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        Transform secretMeetingSpot = AIWaypointManager.Instance.Get(3).transform;
        BehaviorTree result = null;
        if (agent.monster == "warlock")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new CheckNotAtSpot(secretMeetingSpot, 4f),
                                            new GoTo(secretMeetingSpot, 4f)
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new AbilityReadyQuery("heal"),
                                            new GetHurtEnemy(5f),
                                            new Heal()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new AbilityReadyQuery("permabuff"),
                                            // new FindLowestBuffEnemy(),
                                            new PermaBuff()
                                        }),
                                        /*new Sequence(new BehaviorTree[] {
                                            // new CloseToPlayer(10f),
                                            // new RunAway(10f)
                                        }),*/
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", true),
                                            new Selector(new BehaviorTree[] {
                                                new Sequence(new BehaviorTree[] {
                                                    new AbilityReadyQuery("buff"),
                                                    // new FindPlayerClosestEnemy(),
                                                    new Buff()
                                                }),
                                                new Sequence(new BehaviorTree[] {
                                                    new GetNearestEnemy("zombie", 900f),
                                                    new FollowToTarget("closest-zombie", 10f, 3f),
                                                }),
                                            }),
                                        })
                                    });
        }
        else if (agent.monster == "zombie")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", true),
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
                                                new NearbyEnemiesQuery(3, 7f),
                                            }),
                                            new SetFlag("ATTACK", true),

                                            new GetNearestEnemy("zombie", 900f),
                                            new FollowToTarget("closest-zombie", 5f, 1f),
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
