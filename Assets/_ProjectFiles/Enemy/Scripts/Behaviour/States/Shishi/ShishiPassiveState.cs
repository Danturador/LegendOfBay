using UnityEngine;

public class ShishiPassiveState : IEnterState, IUpdateState, IExitState
{
    public void Enter()
    {
        Debug.Log("Shishi passive state entered");
    }

    public void Exit()
    {
        Debug.Log("Shishi passive state exit");
    }

    public void Update()
    {
        Debug.Log("Shishi passive state update");
    }
}