using UnityEngine;

public class PassiveState : IEnterState, IUpdateState, IExitState
{
    public void Enter()
    {
        Debug.Log("Patrol enter");
    }

    public void Exit()
    {
        Debug.Log("Patrol exit");
    }

    public void Update()
    {
        Debug.Log("Patrol update");
    }
}