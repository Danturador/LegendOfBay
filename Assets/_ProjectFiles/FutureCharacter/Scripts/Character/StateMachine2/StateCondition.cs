public abstract class StateCondition
{
    public abstract bool IsConditionSatisfied();

    public virtual void Tick()
    {
    }

    public virtual void InitializeCondition()
    {
    }

    public virtual void DeInitializeCondition()
    {
    }
}
