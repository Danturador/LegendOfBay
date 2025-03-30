using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossAnimationType;

public class BossSwordComboAttachState : State2
{
	private BossHumanFormStateMachine _stateMachine;
	private BossBehaviour _behaviour;
	private BossAnimationController _animationController;
	public BossSwordComboAttachState(BossHumanFormStateMachine stateMachine, BossBehaviour behaviour, BossAnimationController animationController)
	{
		_stateMachine = stateMachine;
		_behaviour = behaviour;
		_animationController = animationController;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;
		_animationController.SetBool(IsComboAttackState, true);

		_stateMachine.StartCoroutine(UseAttack());
	}
	public override void OnStateExit()
	{
		_animationController.SetBool(IsWalking, false);
		_animationController.SetBool(IsComboAttackState, false);
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_behaviour.PerformComboAttack());

		_animationController.SetBool(IsWalking, true);
		yield return _stateMachine.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());

		_stateMachine.HandleHumanAttackCompletion();
	}

}