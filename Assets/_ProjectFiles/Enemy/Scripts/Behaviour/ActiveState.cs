using UnityEngine;

public class ActiveState : IEnterState, IUpdateState, IExitState
{
    public void Enter()
    {
        Debug.Log("Chase enter");
    }

    public void Exit()
    {
        Debug.Log("Chase exit");
    }

    public void Update()
    {
        Debug.Log("Chase update");
    }
}