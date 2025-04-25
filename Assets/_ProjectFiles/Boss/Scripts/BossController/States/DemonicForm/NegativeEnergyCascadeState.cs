using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DemonicFormAnimationType;

public class NegativeEnergyCascadeState : State2
{
	private IDemonicAttack _demonicBehaviour;
	private BossDemonicFormStateMachine _stateMachine;
	private DemonicFormAnimationController _animationController;
	public NegativeEnergyCascadeState(BossDemonicFormStateMachine bossStateMachine, NegativeEnergyCascade demonicBehaviour, DemonicFormAnimationController animationController)
	{
		_stateMachine = bossStateMachine;
		this._demonicBehaviour = demonicBehaviour;
		_animationController = animationController;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;
		//_animationController.SetBool(IsNegativeEnergyCascade, true);

		_stateMachine.StartCoroutine(UseAttack());
	}
	public override void OnStateExit()
	{
		//_animationController.SetBool(IsNegativeEnergyCascade, false);
	}
	private IEnumerator UseAttack()
	{
		//yield return _stateMachine.StartCoroutine(_demonicBehaviour.AttackPattern());
		yield return _stateMachine.StartCoroutine(_demonicBehaviour.AttackPattern(SetCascadeOfNeedles));

		_stateMachine.HandleDemonicAttackCompletion();
	}
	private void SetCascadeOfNeedles(bool value)
	{
		_animationController.SetBool(IsNegativeEnergyCascade, value);
	}
}
