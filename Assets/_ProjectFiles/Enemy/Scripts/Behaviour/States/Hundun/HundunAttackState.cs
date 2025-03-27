using UnityEngine;

public class HundunAttackState : IEnterState, IUpdateState, IExitState
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

    public bool CanAttack()
    {
        return false;
    }
}