using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeOfNeedlesState : State2
{
	private IDemonicAttack _demonicBehaviour;
	private BossDemonicFormStateMachine _stateMachine;
	public CascadeOfNeedlesState(BossDemonicFormStateMachine bossStateMachine, CascadeOfNeedles demonicBehaviour)
	{
		_stateMachine = bossStateMachine;
		this._demonicBehaviour = demonicBehaviour;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;

		_stateMachine.StartCoroutine(UseAttack());
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_demonicBehaviour.AttackPattern());

		_stateMachine.HandleDemonicAttackCompletion();
	}
}
