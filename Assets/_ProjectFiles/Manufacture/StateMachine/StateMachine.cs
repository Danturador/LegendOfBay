using System;
using System.Linq;

public abstract class StateMachine
{
    protected readonly IState[] _states;
    private readonly Transition[] _transitions;
    private IState _current;

    public StateMachine()
    {
        var behaviour = SetMachineBehaviour();
        _states = behaviour.states;
        _transitions = behaviour.transitions;
        _current = _states[0];
    }

    protected abstract (IState[] states, Transition[] transitions) SetMachineBehaviour();

    public void Update()
    {
        if (_current is IUpdateState updateState) updateState.Update();

        foreach (var transition in _transitions)
            if (transition.From == _current.GetType() && transition.Condition())
                TranslateTo(transition.To);
    }

    private void TranslateTo(Type targetType)
    {
        if (_current is IExitState exitState)
            exitState.Exit();

        _current = _states.First(x => x.GetType() == targetType);

        if (_current is IEnterState enterState) enterState.Enter();
    }
}