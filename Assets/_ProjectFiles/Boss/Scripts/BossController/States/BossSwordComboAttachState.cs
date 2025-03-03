using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordComboAttachState : State2
{
	private BossStateMachine _stateMachine;
	private BossBehaviour _behaviour;
	public BossSwordComboAttachState(BossStateMachine bossStateMachine)
	{
		_stateMachine = bossStateMachine;
		_behaviour = bossStateMachine.behaviour;
	}
	public override void OnStateEnter()
	{
		_stateMachine.StartCoroutine(BackPassive());
		_behaviour.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());
		Debug.Log("Boss is attacking!");
	}

	public override void OnStateExit()
	{
		Debug.Log("Boss has finished combo");
		_stateMachine.countOfAttack = 0;
	}
	private IEnumerator BackPassive()
	{
		Debug.Log("Boss combo attack1");
		yield return new WaitForSeconds(2f);

		Debug.Log("Boss combo attack2");
		_stateMachine.isPassive = true;
		_stateMachine.countOfAttack++;
	}
}
