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
                                            new AbilityReadyQuery("heal"),
                                            new GetHurtEnemy(5f, 100f),
                                            new GoToEnemy("hurt-enemy", 5f),
                                            new Heal()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new AbilityReadyQuery("permabuff"),
                                            new GetLowestBuff("skeleton", 100f),
                                            new GoToEnemy("weak-enemy", 5f),
                                            new PermaBuff()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new MoveToPlayer(40),
                                            new MoveFromPlayer(35)
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", true),
                                            new Selector(new BehaviorTree[] {
                                                new Sequence(new BehaviorTree[] {
                                                    new AbilityReadyQuery("buff"),
                                                    new GetClosestToPlayer(100f),
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
                                            new Selector(new BehaviorTree[] {
                                                new CheckFlag("ATTACK", true),
                                                new NearbyEnemiesQuery(20, 9999f),
                                            }),
                                            new SetFlag("ATTACK", true),
                                            new MoveToPlayer(agent.GetAction("attack").range),
                                            new Attack()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new MoveToPlayer(40),
                                            new MoveFromPlayer(35)
                                            // new GoTo(secretMeetingSpot, 4f)
                                        }),

                                        // new Sequence(new BehaviorTree[] {
                                        //     new CheckFlag("ATTACK", true),
                                        //     new MoveToPlayer(1f),
                                        //     new Attack()
                                        // }),
                                        // new Sequence(new BehaviorTree[] {
                                        //     new CheckFlag("ATTACK", false),
                                        //     new GoTo(secretMeetingSpot, 4f)
                                        // })
                                     });
        }
        else if (agent.monster == "skeleton")
        {
            result = new Selector(new BehaviorTree[] {
                                        new Sequence(new BehaviorTree[] {
                                            new Selector(new BehaviorTree[] {
                                                new CheckFlag("ATTACK", true),
                                                new NearbyEnemiesQuery(20, 9999f),
                                            }),
                                            new SetFlag("ATTACK", true),

                                            new GetNearestEnemy("zombie", 9999f),
                                            new FollowToTarget("closest-zombie", 10f, 1f),
                                            new MoveToPlayer(agent.GetAction("attack").range),
                                            new Attack()
                                        }),
                                        new Sequence(new BehaviorTree[] {
                                            new CheckFlag("ATTACK", false),
                                            new MoveToPlayer(40),
                                            new MoveFromPlayer(35)
                                            // new GoTo(secretMeetingSpot, 4f)
                                        }),
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
