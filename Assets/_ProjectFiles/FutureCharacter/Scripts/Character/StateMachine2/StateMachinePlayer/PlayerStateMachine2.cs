using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerStateMachine2 : MonoBehaviour
{
    [SerializeField] private string currentState;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private bool _jump;
    public PlayerAnimationController playerAnimationController { get; private set; }
    private InputController _inputController;
    private StateMachine2 _stateMachine;
    private PlayerJump _playerJump;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        
        InitializeStateMachine();
    }
    private void Start()
    {
        _inputController = GetComponentInParent<PlayerController>().inputController;
        _playerJump = GetComponentInParent<PlayerJump>();
    }

    private void Update()
    {
        velocityX = _rigidbody2D.velocity.x; //for test
        velocityY = _rigidbody2D.velocity.y; //for test
        _isGrounded = _playerJump._isGrounded;
        _stateMachine.OnUpdate();
        currentState = _stateMachine.CurrentState.ToString();
    }
    private void InitializeStateMachine()
    {
        var playerAnimationController = new PlayerAnimationController(_animator);

        var idleState = new PlayerIdleState2(playerAnimationController);
        var runState = new PlayerRunState2(playerAnimationController);
        var jumpState = new PlayerJumpState2(playerAnimationController);
        var jumpFallState = new PlayerJumpFall2(playerAnimationController);
        var doubleJumpState = new PlayerDoubleJumpState2(playerAnimationController);

        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _inputController.Gameplay.Movement.ReadValue<Vector2>().x !=0 && _isGrounded)));
        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _inputController.Gameplay.Movement.ReadValue<Vector2>().x == 0 && _isGrounded)));
        idleState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => _rigidbody2D.velocity.y > 0 && _isGrounded==false)));
        runState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => _rigidbody2D.velocity.y > 0 && _isGrounded == false)));
        runState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _rigidbody2D.velocity.y < 0 && _isGrounded == false)));
        jumpState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.y == 0)));
        jumpState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _isGrounded == false && _rigidbody2D.velocity.y < 0.02f)));
        jumpFallState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.x == 0)));
        jumpFallState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.x != 0)));



        _stateMachine = new StateMachine2(idleState);
    }
}
