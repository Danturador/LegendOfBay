using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBossState : State2
{
	protected BossStateMachine _stateMachine;
	protected BossBehaviour _behaviour;

	protected AbstractBossState(BossStateMachine bossStateMachine)
	{
		_stateMachine = bossStateMachine;
		_behaviour = bossStateMachine.behaviour;
	}
	public override void OnStateEnter()
	{
		_stateMachine.isAttackEnded = false;
		//_stateMachine.currentState = _stateMachine.CurrentState.ToString();
	}
	public void HandleAttackCompletion()
	{
		_stateMachine.countOfAttack++;
		if (_stateMachine.countOfAttack == 4)
		{
			_stateMachine.countOfAttack = 0;
			_stateMachine.isPassive = true;
		}

		_stateMachine.isAttackEnded = true;

		//if (_stateMachine.currentAttack.Is(BossStateMachine.TypesOfAttack.ComboTypeAttack))
		//	_stateMachine.currentAttack = BossStateMachine.TypesOfAttack.DashTypeAttack;
		//else 
		//	_stateMachine.currentAttack = BossStateMachine.TypesOfAttack.ComboTypeAttack;
		_stateMachine.currentAttack = BossStateMachine.GetRandomAttackType();
	}
}