using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static BossHumanFormStateMachine.TypesOfAttack;

public class BossHumanFormStateMachine : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	public BossAnimationController bossAnimationController { get; private set; }
	private StateMachine2 _stateMachine;
	public string currentState { 
		get { 
			return _stateMachine.CurrentState.ToString();
		}
	}

	public BossBehaviour humanBehaviour;
	public bool isPassive;
	public int countOfAttack;
	public bool isAttackEnded;
	public TypesOfAttack nextAttack;

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
		isAttackEnded = false;
		countOfAttack = 0;
		nextAttack = ChooseNextAttack();
	}
	private void Update()
	{
		_stateMachine.OnUpdate();
		//currentState = _stateMachine.CurrentState.ToString();
	}
	private void InitializeStateMachine()
	{
		BossAnimationController animationController = new(_animator);

		BossPassiveState passiveState = new BossPassiveState(this, animationController);
		BossSwordComboAttachState swordComboAttachState = new BossSwordComboAttachState(this, humanBehaviour, animationController);
		BossDashState dashState = new BossDashState(this, humanBehaviour, animationController);

		/// From PassiveState
		AddTransitionToState(passiveState, swordComboAttachState, () =>
			!isPassive
			&& nextAttack.Is(ComboTypeAttack)
		);
		AddTransitionToState(passiveState, dashState, () =>
			!isPassive
			&& nextAttack.Is(DashTypeAttack)
		);
		///

		/// From Attack to Attack
		AddTransitionToState(dashState, swordComboAttachState, () =>
			isAttackEnded
			&& nextAttack.Is(ComboTypeAttack)
			&& countOfAttack < 4
		); ;
		AddTransitionToState(dashState, dashState, () =>
			isAttackEnded
			&& nextAttack.Is(DashTypeAttack)
			&& currentState == nameof(BossDashState)
			&& countOfAttack < 4
		);

		AddTransitionToState(swordComboAttachState, dashState, () =>
			//behaviour.isReadyToAttack 
			isAttackEnded
			&& nextAttack.Is(DashTypeAttack)
			&& countOfAttack < 4
		);
		AddTransitionToState(swordComboAttachState, swordComboAttachState, () =>
			//behaviour.isReadyToAttack
			isAttackEnded
			&& nextAttack.Is(ComboTypeAttack)
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
	public void HandleHumanAttackCompletion()
	{
		countOfAttack++;

		if (countOfAttack == 4)
		{
			countOfAttack = 0;
			isPassive = true;
		}

		isAttackEnded = true;

		nextAttack = ChooseNextAttack();
	}
	public static void AddTransitionToState<T1, T2>(T1 state1, T2 state2, Func<bool> condition) 
	where T1 : State2
	where T2 : State2
	{
		var transition = new StateTransition(state2, new FuncStateCondition(condition));
		state1.AddTransition(transition);
	}
	public TypesOfAttack ChooseNextAttack()
	{
		if (humanBehaviour.isPlayerNear)
		{
			return TypesOfAttack.ComboTypeAttack;
		}
		else
		{
			return TypesOfAttack.DashTypeAttack;
		}
	}
}
public static class AttackExtensions
{
    public static bool Is<T>(this T currentAttack, T attackType) where T : System.Enum
    {
        return currentAttack.Equals(attackType);
    }
}