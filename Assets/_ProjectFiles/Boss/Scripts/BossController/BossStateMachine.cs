using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossStateMachine : MonoBehaviour
{
	[SerializeField] private string currentState;
	[SerializeField] private Animator _animator;
	[SerializeField] private Rigidbody2D _rigidbody2D;
	//[SerializeField] private InputController _inputController;
	//public BossAnimationController bossAnimationController { get; private set; }
	private StateMachine2 _stateMachine;

	public BossBehaviour behaviour;
	public bool isPassive;
	public int countOfAttack;

	private void Awake()
	{
		InitializeStateMachine();

		isPassive = true;
		countOfAttack = 0;
	}
	private void Update()
	{
		_stateMachine.OnUpdate();
		currentState = _stateMachine.CurrentState.ToString();
	}
	private void InitializeStateMachine()
	{
		BossPassiveState passiveState = new BossPassiveState(this);
		BossSwordComboAttachState swordComboAttachState = new BossSwordComboAttachState(this);

		AddTransitionToState(passiveState, swordComboAttachState, () => !isPassive);
		AddTransitionToState(swordComboAttachState, passiveState, () => isPassive && behaviour.isReadyToAttack && countOfAttack >= 4);

		_stateMachine = new StateMachine2(passiveState);
	}
	public void AddTransitionToState<T1, T2>(T1 state1, T2 state2, Func<bool> condition) 
	where T1 : State2
	where T2 : State2
	{
		var transition = new StateTransition(state2, new FuncStateCondition(condition));
		state1.AddTransition(transition);
	}
}
