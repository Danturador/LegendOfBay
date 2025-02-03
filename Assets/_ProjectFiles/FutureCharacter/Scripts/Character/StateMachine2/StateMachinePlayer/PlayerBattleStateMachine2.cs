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
    private bool _isMediumAttack;
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
        _inputController.Gameplay.MediumAttack.performed += OnMediumAttack;

    }

    private void OnMediumAttack(InputAction.CallbackContext context)
    {
       _isMediumAttack = true;
    }

    private void OnSmallAttack(InputAction.CallbackContext context)
    {
        _isSmallAttack = true;
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        currentState = _stateMachine.CurrentState.ToString();
        _isSmallAttack = false;
        _isMediumAttack = false;
    }
    

    private void InitializeStateMachine()
    {
        var playerAnimationController = new PlayerAnimationController(_animator);

        var emptyState = new PlayerEmptyState2();
        var smallAttackState = new PlayerSmallAttackState2(playerAnimationController);
        var mediumAttackState = new PlayerMediumAttackState2(playerAnimationController);

        emptyState.AddTransition(new StateTransition(smallAttackState, new FuncStateCondition(() => _isSmallAttack )));
        emptyState.AddTransition(new StateTransition(mediumAttackState, new FuncStateCondition(() => _isMediumAttack )));
        smallAttackState.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => _isSmallAttack == false && _isMediumAttack == false)));
        mediumAttackState.AddTransition(new StateTransition(emptyState, new FuncStateCondition(() => _isSmallAttack == false && _isMediumAttack == false)));



        _stateMachine = new StateMachine2(emptyState);
    }
}
