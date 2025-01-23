public class StateTransition
{
    public State2 StateTo { get; private set; }
    public StateCondition Condition { get; private set; }

    public StateTransition(State2 state, StateCondition stateConditionCondition)
    {
        StateTo = state;
        Condition = stateConditionCondition;
    }

    public void InitializeCondition()
    {
        Condition.InitializeCondition();
    }

    public void DeInitializeCondition()
    {
        Condition.DeInitializeCondition();
    }
}
