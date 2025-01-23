using UnityEngine;

public class AttackState : IEnterState, IUpdateState, IExitState
{
    public void Enter()
    {
        Debug.Log("AttackState enter");
    }

    public void Exit()
    {
        Debug.Log("AttackState exit");
    }

    public void Update()
    {
        Debug.Log("AttackState update");
    }
}