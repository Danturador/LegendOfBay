using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPassiveState : State2
{
	private BossHumanFormStateMachine _stateMachine;
	public BossPassiveState(BossHumanFormStateMachine bossStateMachine)
	{
		_stateMachine = bossStateMachine;
	}
	public override void OnStateEnter()
	{
		_stateMachine.isPassive = true;
		_stateMachine.StartCoroutine(BackAggrassive());
	}

	public override void OnStateExit()
	{
	}
	private IEnumerator BackAggrassive()
	{
		yield return new WaitForSeconds(4f);

		_stateMachine.isPassive = false;
	}
}
