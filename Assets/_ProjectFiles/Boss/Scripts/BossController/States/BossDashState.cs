using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashState : AbstractBossState
{
	public BossDashState(BossStateMachine bossStateMachine) : base(bossStateMachine) { }

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		Debug.Log("Boss is dashing!");
		_stateMachine.StartCoroutine(UseAttack());
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_behaviour.DashTowardsPlayer());
		
		yield return _stateMachine.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());

		HandleAttackCompletion();
	}
}
