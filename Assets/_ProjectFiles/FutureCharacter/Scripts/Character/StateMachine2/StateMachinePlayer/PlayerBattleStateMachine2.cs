using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattleStateMachine2 : MonoBehaviour
{
    [SerializeField] private string currentState;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public PlayerAnimationController playerAnimationController { get; private set; }
    private InputController _inputController;
    private StateMachine2 _stateMachine;
    private bool _isSmallAttack;
   [SerializeField] private bool comboWindow = true;
   [SerializeField] private int currentComboCount = 0;
    private int maxComboCount = 3;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();

        InitializeStateMachine();
    }
    private void Start()
    {
        _inputController = GetComponentInParent<PlayerController>().inputController;
        _inputController.Gameplay.SmallAttack.performed += OnSmallAttack;

    }

    private void OnSmallAttack(InputAction.CallbackContext context)
    {
        if (comboWindow && currentComboCount < maxComboCount)
        {
            comboWindow = false;
            currentComboCount++;
            _isSmallAttack = true;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            if(currentComboCount == 3)
            {
                currentCoroutine = StartCoroutine(AttackTime(1.2f));
            }
            else
            {
                currentCoroutine = StartCoroutine(AttackTime(0.4f));
            }
            
        }
        else 
        {
            _isSmallAttack = false;
        }

    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        currentState = _stateMachine.CurrentState.ToString();
        _isSmallAttack = false;
    }

    public void ResetComboWindow()
    {
        comboWindow = true;
    }
    public void ResetCombo()
    {
        currentComboCount = 0;
        comboWindow = true;
        
    }


    private void InitializeStateMachine()
    {
        var playerAnimationController = new PlayerAnimationController(_animator);

        var emptyState = new PlayerEmptyState2(playerAnimationController);
        var comboAttack1 = new ComboAttackState(playerAnimationController, 1);
        var comboAttack2 = new ComboAttackState(playerAnimationController, 2);
        var comboAttack3 = new ComboAttackState(playerAnimationController, 3);

        emptyState.AddTransition(new StateTransition(comboAttack1, new FuncStateCondition(() => _isSmallAttack && currentComboCount == 1)));
        comboAttack1.AddTransition(new StateTransition(comboAttack2, new FuncStateCondition(() => _isSmallAttack && currentComboCount == 2)));
        comboAttack2.AddTransition(new StateTransition(comboAttack3, new FuncStateCondition(() => _isSmallAttack && currentComboCount == 3)));

        comboAttack1.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => currentComboCount == 0)));
        comboAttack2.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => currentComboCount == 0)));
        comboAttack3.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => currentComboCount == 0)));

        


        _stateMachine = new StateMachine2(emptyState);
    }

    private IEnumerator AttackTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetCombo();
    }


}
