using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DemonicFormAnimationType;

public class CascadeOfNeedlesState : State2
{
	private IDemonicAttack _demonicBehaviour;
	private BossDemonicFormStateMachine _stateMachine;
	private DemonicFormAnimationController _animationController;
	public CascadeOfNeedlesState(BossDemonicFormStateMachine bossStateMachine, CascadeOfNeedles demonicBehaviour, DemonicFormAnimationController animationController)
	{
		_stateMachine = bossStateMachine;
		this._demonicBehaviour = demonicBehaviour;
		_animationController = animationController;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;
		//_animationController.SetBool(IsCascadeOfNeedles, true);

		_stateMachine.StartCoroutine(UseAttack());
	}
	public override void OnStateExit()
	{
		//_animationController.SetBool(IsCascadeOfNeedles, false);
	}
	private IEnumerator UseAttack()
	{
		_animationController.SetBool(IsCascadeOfNeedlesRepeat, true);

		yield return _stateMachine.StartCoroutine(_demonicBehaviour.AttackPattern(SetCascadeOfNeedles));

		_animationController.SetBool(IsCascadeOfNeedlesRepeat, false);

		yield return new WaitForSeconds(3f);
		
		_stateMachine.HandleDemonicAttackCompletion();
	}
	private void SetCascadeOfNeedles(bool isAttack, bool isLongAttack)
	{
		_animationController.SetBool(IsCascadeOfNeedles, isAttack);
		_animationController.SetBool(IsCascadeOfNeedlesLong, isLongAttack);
	}
}
