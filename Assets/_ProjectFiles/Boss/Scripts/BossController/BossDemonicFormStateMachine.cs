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
	public TypesOfAttack previousAttack;
	public TypesOfAttack currentAttack;

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
		_stateMachine = new StateMachine2(new DemonicPassiveState());
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
		negativeEnergyCascade.Deinitialize();
	}
	public void InitializeDemonicForm()
	{
		InitializeStateMachine();
		isAttackEnded = false;
		currentAttack = ChooseNextAttack();
	}
	private void Update()
	{
		_stateMachine.OnUpdate();
	}
	private void InitializeStateMachine()
	{

		CascadeOfNeedlesState cascadeOfNeedlesState = new CascadeOfNeedlesState(this, cascadeOfNeedles, animationController);
		NegativeEnergyCascadeState negativeEnergyCascadeState = new NegativeEnergyCascadeState(this, negativeEnergyCascade, animationController);
		DeathState deathState = new DeathState();

		AddTransitionToState(cascadeOfNeedlesState, negativeEnergyCascadeState, () =>
			isAttackEnded
			&& currentAttack.Is(NegativeEnergyCascadeTypeAttack)
		);
		AddTransitionToState(cascadeOfNeedlesState, cascadeOfNeedlesState, () =>
			isAttackEnded
			&& currentAttack.Is(CascadeOfNeedlesTypeAttack)
			&& previousAttack.Is(CascadeOfNeedlesTypeAttack)
		);

		AddTransitionToState(negativeEnergyCascadeState, cascadeOfNeedlesState, () =>
			isAttackEnded
			&& currentAttack.Is(CascadeOfNeedlesTypeAttack)
		);
		AddTransitionToState(negativeEnergyCascadeState, negativeEnergyCascadeState, () =>
			isAttackEnded
			&& currentAttack.Is(NegativeEnergyCascadeTypeAttack)
			&& previousAttack.Is(NegativeEnergyCascadeTypeAttack)
		);

		AddTransitionToState(negativeEnergyCascadeState, deathState, () =>
			animationController.GetBool(IsDead)
		);
		AddTransitionToState(cascadeOfNeedlesState, deathState, () =>
			animationController.GetBool(IsDead)
		);

		_stateMachine.SetState(cascadeOfNeedlesState);
	}
	public void HandleDemonicAttackCompletion()
	{
		isAttackEnded = true;
		previousAttack = currentAttack;
		currentAttack = ChooseNextAttack();
	}
	public static TypesOfAttack GetRandomAttackType()
	{
		Array values = Enum.GetValues(typeof(TypesOfAttack));
		return (TypesOfAttack)values.GetValue(random.Next(values.Length));
	}
	public TypesOfAttack ChooseNextAttack()
	{
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

		currentAttack = availableAttacks[random.Next(availableAttacks.Count)];

		recentAttacks.Enqueue(currentAttack);
		if (recentAttacks.Count > maxRecentAttacks)
		{
			recentAttacks.Dequeue();
		}

		return currentAttack;
	}
}