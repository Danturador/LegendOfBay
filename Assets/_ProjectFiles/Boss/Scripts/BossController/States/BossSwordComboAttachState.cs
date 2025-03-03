using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordComboAttachState : AbstractBossState
{
	public BossSwordComboAttachState(BossStateMachine bossStateMachine) : base(bossStateMachine) { }

	public override void OnStateEnter()
	{
		base.OnStateEnter();

		Debug.Log("Boss is attacking!");
		_stateMachine.StartCoroutine(UseAttack());
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_behaviour.PerformComboAttack());

		yield return _stateMachine.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());

		HandleAttackCompletion();
	}

}