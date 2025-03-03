using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static BossStateMachine.TypesOfAttack;

public class BossStateMachine : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	[SerializeField] private Rigidbody2D _rigidbody2D;
	//[SerializeField] private InputController _inputController;
	//public BossAnimationController bossAnimationController { get; private set; }
	private StateMachine2 _stateMachine;
	public string currentState { 
		get { 
			return _stateMachine.CurrentState.ToString();
		}
	}

	public BossBehaviour behaviour;
	public bool isPassive;
	public int countOfAttack;
	public bool isAttackEnded;
	public TypesOfAttack currentAttack;

	private static System.Random random = new System.Random();
	public enum TypesOfAttack
	{
		DashTypeAttack,
		ComboTypeAttack,
	}

	private void Awake()
	{
		InitializeStateMachine();

		isPassive = true;
		isAttackEnded = true;
		countOfAttack = 0;
		currentAttack = GetRandomAttackType();
	}
	private void Update()
	{
		_stateMachine.OnUpdate();
		//currentState = _stateMachine.CurrentState.ToString();
	}
	private void InitializeStateMachine()
	{
		BossPassiveState passiveState = new BossPassiveState(this);
		BossSwordComboAttachState swordComboAttachState = new BossSwordComboAttachState(this);
		BossDashState dashState = new BossDashState(this);

		/// From PassiveState
		AddTransitionToState(passiveState, swordComboAttachState, () => 
			!isPassive
			&& currentAttack.Is(ComboTypeAttack)
		);
		AddTransitionToState(passiveState, dashState, () =>
			!isPassive
			&& currentAttack.Is(DashTypeAttack)
		);
		///

		/// From Attack to Attack
		AddTransitionToState(dashState, swordComboAttachState, () =>
			isAttackEnded
			&& currentAttack.Is(ComboTypeAttack)
			&& countOfAttack < 4
		); ;
		AddTransitionToState(dashState, dashState, () =>
			isAttackEnded
			&& currentAttack.Is(DashTypeAttack)
			&& currentState == nameof(BossDashState)
			&& countOfAttack < 4
		);

		AddTransitionToState(swordComboAttachState, dashState, () =>
			//behaviour.isReadyToAttack 
			isAttackEnded
			&& currentAttack.Is(DashTypeAttack)
			&& countOfAttack < 4
		);
		AddTransitionToState(swordComboAttachState, swordComboAttachState, () =>
			//behaviour.isReadyToAttack
			isAttackEnded
			&& currentAttack.Is(ComboTypeAttack)
			&& currentState == nameof(BossSwordComboAttachState)
			&& countOfAttack < 4
		);
		///

		/// To Passive
		AddTransitionToState(swordComboAttachState, passiveState, () =>
			isPassive
		//&& behaviour.isReadyToAttack 
		//&& countOfAttack >= 4
		);
		AddTransitionToState(dashState, passiveState, () =>
			isPassive
		//&& behaviour.isReadyToAttack
		//&& countOfAttack >= 4
		);
		///

		_stateMachine = new StateMachine2(passiveState);
	}
	public void AddTransitionToState<T1, T2>(T1 state1, T2 state2, Func<bool> condition) 
	where T1 : State2
	where T2 : State2
	{
		var transition = new StateTransition(state2, new FuncStateCondition(condition));
		state1.AddTransition(transition);
	}
	public static TypesOfAttack GetRandomAttackType()
	{
		Array values = Enum.GetValues(typeof(TypesOfAttack));
		return (TypesOfAttack)values.GetValue(random.Next(values.Length));
	}
}
public static class AttackExtensions
{
	public static bool Is(this BossStateMachine.TypesOfAttack currentAttack, BossStateMachine.TypesOfAttack attackType)
	{
		return currentAttack == attackType;
	}
}