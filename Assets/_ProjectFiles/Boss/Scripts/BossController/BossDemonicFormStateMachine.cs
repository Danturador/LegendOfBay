using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static BossDemonicFormStateMachine.TypesOfAttack;
using static DemonicFormAnimationType;
using static BossHumanFormStateMachine;

public class BossDemonicFormStateMachine : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	public DemonicFormAnimationController animationController { get; private set; }
	private StateMachine2 _stateMachine;
	public string currentState { 
		get { 
			return _stateMachine.CurrentState.ToString();
		}
	}

	public CascadeOfNeedles cascadeOfNeedles;
	public NegativeEnergyCascade negativeEnergyCascade;
	public bool isAttackEnded;
	public TypesOfAttack currentAttack;
	public TypesOfAttack nextAttack;
	public string currentState_;

	private static System.Random random = new System.Random();
	private Queue<TypesOfAttack> recentAttacks = new Queue<TypesOfAttack>();
	private int maxRecentAttacks = 2;
	public Action OnDeath;
	public enum TypesOfAttack
	{
		CascadeOfNeedlesTypeAttack,
		NegativeEnergyCascadeTypeAttack,
	}
	private void Awake()
	{
		//_stateMachine = new StateMachine2(new DemonicPassiveState());
		animationController = new DemonicFormAnimationController(_animator);
		OnDeath += HandleDeath;
	}
	private void OnDestroy()
	{
		OnDeath -= HandleDeath;
	}
	private void HandleDeath()
	{
		animationController.SetBool(IsDead, true);

		cascadeOfNeedles.StopAllCoroutines();
		negativeEnergyCascade.StopAllCoroutines();

		cascadeOfNeedles.Deinitialize();
		negativeEnergyCascade.DeinitializeComplitely();
	}
	public void InitializeDemonicForm()
	{
		InitializeStateMachine();
		isAttackEnded = false;

		//currentAttack = ChooseNextAttack();
		//nextAttack = ChooseNextAttack();
	}
	private void Update()
	{
		_stateMachine.OnUpdate();
		currentState_ = currentState;
	}
	private void InitializeStateMachine()
	{

		CascadeOfNeedlesState cascadeOfNeedlesState = new CascadeOfNeedlesState(this, cascadeOfNeedles, animationController);
		NegativeEnergyCascadeState negativeEnergyCascadeState = new NegativeEnergyCascadeState(this, negativeEnergyCascade, animationController);
		DeathState deathState = new DeathState();

		AddTransitionToState(cascadeOfNeedlesState, negativeEnergyCascadeState, () =>
			isAttackEnded
			&& nextAttack.Is(NegativeEnergyCascadeTypeAttack)
		);
		AddTransitionToState(cascadeOfNeedlesState, cascadeOfNeedlesState, () =>
			isAttackEnded
			&& nextAttack.Is(CascadeOfNeedlesTypeAttack)
			&& currentAttack.Is(CascadeOfNeedlesTypeAttack)
		);

		AddTransitionToState(negativeEnergyCascadeState, cascadeOfNeedlesState, () =>
			isAttackEnded
			&& nextAttack.Is(CascadeOfNeedlesTypeAttack)
		);
		AddTransitionToState(negativeEnergyCascadeState, negativeEnergyCascadeState, () =>
			isAttackEnded
			&& nextAttack.Is(NegativeEnergyCascadeTypeAttack)
			&& currentAttack.Is(NegativeEnergyCascadeTypeAttack)
		);

		AddTransitionToState(negativeEnergyCascadeState, deathState, () =>
			animationController.GetBool(IsDead)
		);
		AddTransitionToState(cascadeOfNeedlesState, deathState, () =>
			animationController.GetBool(IsDead)
		);

		_stateMachine = new StateMachine2(cascadeOfNeedlesState);
		//_stateMachine.SetState(cascadeOfNeedlesState);
	}
	public void HandleDemonicAttackCompletion()
	{
		isAttackEnded = true;
		currentAttack = nextAttack;
		nextAttack = ChooseNextAttack();
	}
	public TypesOfAttack ChooseNextAttack()
	{
		//if (currentState == nameof(CascadeOfNeedlesState))
		//{
		//	return NegativeEnergyCascadeTypeAttack;
		//}

		//return CascadeOfNeedlesTypeAttack;
		List<TypesOfAttack> availableAttacks = new List<TypesOfAttack>();

		foreach (TypesOfAttack attack in Enum.GetValues(typeof(TypesOfAttack)))
		{
			if (recentAttacks.Count < maxRecentAttacks || !recentAttacks.Contains(attack))
			{
				availableAttacks.Add(attack);
			}
		}

		if (availableAttacks.Count == 0)
		{
			recentAttacks.Clear();
			availableAttacks.AddRange(Enum.GetValues(typeof(TypesOfAttack)) as TypesOfAttack[]);
		}

		nextAttack = availableAttacks[random.Next(availableAttacks.Count)];

		recentAttacks.Enqueue(nextAttack);
		if (recentAttacks.Count > maxRecentAttacks)
		{
			recentAttacks.Dequeue();
		}

		return nextAttack;
	}
}