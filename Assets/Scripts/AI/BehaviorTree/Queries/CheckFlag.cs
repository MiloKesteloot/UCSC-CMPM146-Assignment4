public class CheckFlag : BehaviorTree
{
    string flag;
    bool shouldBeTrue;

    public override Result Run()
    {
        if (((bool) GameManager.Instance.blackboard[flag]) == shouldBeTrue) {
            return Result.SUCCESS;
        }
        return Result.FAILURE;
    }

    public CheckFlag(string flag, bool shouldBeTrue) : base()
    {
        this.flag = flag;
        this.shouldBeTrue = shouldBeTrue;
    }

    public override BehaviorTree Copy()
    {
        return new CheckFlag(flag, shouldBeTrue);
    }
}
