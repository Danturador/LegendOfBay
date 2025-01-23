using System.Collections;
using UnityEngine;

public class StateMachine2
{
    public State2 CurrentState { get; private set; }

    public StateMachine2(State2 state)
    {
        SetState(state);
    }

    public void OnUpdate()
    {
        var newIndex = IsTransitionsCondition();
        if (newIndex != -1
        )
        {
            SetState(CurrentState.Transitions[newIndex].StateTo);
        }
        else
        {
            CurrentState.OnUpdate();
        }
    }

    private int IsTransitionsCondition()
    {
        var currentTransitions = CurrentState.Transitions;
        for (var i = 0; i != currentTransitions.Count; i++)
        {
            var condition = currentTransitions[i].Condition;
            condition.Tick();
            if (condition.IsConditionSatisfied())
            {
                return i;
            }
        }

        return -1;
    }

    public void SetState(State2 state)
    {
        CurrentState?.OnStateExit();
        CurrentState?.DeInitializeTransitions();

        CurrentState = state;
        CurrentState.OnStateEnter();
        CurrentState.InitializeTransitions();
    }
}
