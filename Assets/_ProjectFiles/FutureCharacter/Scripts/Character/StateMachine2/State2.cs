using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class State2
{
    public List<StateTransition> Transitions { get; private set; } =
        new List<StateTransition>();

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }

    public void InitializeTransitions()
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.InitializeCondition();
        }
    }

    public void DeInitializeTransitions()
    {
        foreach (var stateTransition in Transitions)
        {
            stateTransition.DeInitializeCondition();
        }
    }

    public void AddTransition(StateTransition transition)
    {
        Transitions.Add(transition);
    }

    public void RemoveTransition(StateTransition transition)
    {
        if (Transitions.Contains(transition))
        {
            Transitions.Remove(transition);
        }
    }
}