using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossAnimationType;

public class BossDashState : State2
{
	private BossHumanFormStateMachine _stateMachine;
	private BossBehaviour _behaviour;
	private BossAnimationController _animationController;
	public BossDashState(BossHumanFormStateMachine stateMachine, BossBehaviour behaviour, BossAnimationController animationController)
	{
		_stateMachine = stateMachine;
		_behaviour = behaviour;
		_animationController = animationController;
	}

	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;
		_animationController.SetBool(IsDashState, true);

		_stateMachine.StartCoroutine(UseAttack());
	}
	public override void OnStateExit()
	{
	}
	private IEnumerator UseAttack()
	{
		yield return _stateMachine.StartCoroutine(_behaviour.DashTowardsPlayer());
		_animationController.SetBool(IsDashState, false);

		_animationController.SetTrigger(Walk);
		yield return _stateMachine.StartCoroutine(_behaviour.MoveTowardsPlayerCoroutine());
		//_animationController.SetBool(IsWalking, false);

		_stateMachine.HandleHumanAttackCompletion();
	}
}
