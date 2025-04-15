using System.Collections.Generic;

public class StateTransition
{
    public State2 StateTo { get; private set; }
    public StateCondition Condition { get; private set; }

    public StateTransition(State2 state, StateCondition stateConditionCondition)
    {
        StateTo = state;
        Condition = stateConditionCondition;
    }

    public void MultiStateTransition(List<State2> states, StateCondition condition)
    {
        foreach (var state in states)
        {
            StateTo = state;
            Condition = condition;
        }
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
