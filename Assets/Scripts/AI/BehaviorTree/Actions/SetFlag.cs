public class SetFlag : BehaviorTree
{
    string flag;
    bool value;

    public override Result Run()
    {
        var blackboard = GameManager.Instance.blackboard;
        if (blackboard.ContainsKey(flag)) {
            blackboard[flag] = value;
        } else {
            blackboard.Add(flag, value);
        }
        
        return Result.SUCCESS;
    }

    public SetFlag(string flag, bool value) : base()
    {
        this.flag = flag;
        this.value = value;
    }

    public override BehaviorTree Copy()
    {
        return new SetFlag(flag, value);
    }
}
