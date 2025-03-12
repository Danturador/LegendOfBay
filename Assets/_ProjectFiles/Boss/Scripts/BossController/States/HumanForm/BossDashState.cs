using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashState : State2
{
	private BossHumanFormStateMachine _stateMachine;
	private BossBehaviour _behaviour;
	public BossDashState(BossHumanFormStateMachine bossStateMachine, BossBehaviour bossBehaviour)
	{
		_stateMachine = bossStateMachine;
		_behaviour = bossBehaviour;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;

		_stateMachine.StartCoroutine(UseAttack());
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_behaviour.DashTowardsPlayer());
		
		yield return _stateMachine.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());

		_stateMachine.HandleHumanAttackCompletion();
	}
}
