using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HumanFormAnimationType;

public class BossPassiveState : State2
{
	private BossHumanFormStateMachine _stateMachine;
	private HumanFormAnimationController _bossAnimationController;
	public BossPassiveState(BossHumanFormStateMachine bossStateMachine, HumanFormAnimationController bossAnimationController)
	{
		_stateMachine = bossStateMachine;
		_bossAnimationController = bossAnimationController;
	}
	public override void OnStateEnter()
	{
		SetPassiveValue(true);

		_stateMachine.StartCoroutine(BackAggrassive());
	}

	public override void OnStateExit()
	{
	}
	private IEnumerator BackAggrassive()
	{
		yield return new WaitForSeconds(4f);

		SetPassiveValue(false);
	}
	private void SetPassiveValue(bool isPassive)
	{
		_stateMachine.isPassive = isPassive;
		_bossAnimationController.SetBool(IsPassive, isPassive);
	}
}
