using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerStateMachine2 : MonoBehaviour
{
    [SerializeField] private string currentState;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    public PlayerAnimationController playerAnimationController { get; private set; }
    private InputController _inputController;
    private StateMachine2 _stateMachine;
    private PlayerJump _playerJump;
    private PlayerDash _playerDash;
    [SerializeField]private bool _isLanding;
    private bool _isGrounded => _playerJump._isGrounded;
    private bool _isDashing => _playerDash.IsDashing();
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
        _playerDash = GetComponentInParent<PlayerDash>();
    }

    private void Update()
    {
        velocityX = _rigidbody2D.velocity.x; //for test
        velocityY = _rigidbody2D.velocity.y; //for test
        if (_rigidbody2D.velocity.y < -15f) 
        { 
            _isLanding = true;
        }
       
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
        var landingState = new PlayerLandingState2(playerAnimationController);
        var dashState = new PlayerDashState2(playerAnimationController);

        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _inputController.Gameplay.Movement.ReadValue<Vector2>().x !=0 && _isGrounded)));
        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _inputController.Gameplay.Movement.ReadValue<Vector2>().x == 0 && _isGrounded)));
        idleState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => _rigidbody2D.velocity.y > 1f && _isGrounded==false)));
        runState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => _rigidbody2D.velocity.y > 1f && _isGrounded == false)));
        runState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _rigidbody2D.velocity.y < -1f && _isGrounded == false)));
        jumpState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.y == 0)));
        jumpState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _isGrounded == false && _rigidbody2D.velocity.y < -1f)));

        //  jumpState.AddTransition(new StateTransition(doubleJumpState, new FuncStateCondition(() => )));
        doubleJumpState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _isGrounded == false && _rigidbody2D.velocity.y < -1f)));
        doubleJumpState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.y == 0)));

        jumpFallState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.x == 0 && _isLanding ==false)));
        jumpFallState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _isGrounded && _rigidbody2D.velocity.x != 0 && _isLanding == false)));
        jumpFallState.AddTransition(new StateTransition(jumpState, new FuncStateCondition(() => _rigidbody2D.velocity.y > 1f && _isGrounded == false)));

        jumpFallState.AddTransition(new StateTransition(landingState, new FuncStateCondition(() => _isLanding && _isGrounded)));
        landingState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded )));

        runState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => _isDashing)));
        jumpState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => _isDashing)));
        doubleJumpState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => _isDashing)));
        jumpFallState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => _isDashing)));
        dashState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isDashing == false && _isGrounded && _rigidbody2D.velocity.x == 0)));
        dashState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _isDashing == false && _isGrounded && _rigidbody2D.velocity.x != 0))); // fix it!
        dashState.AddTransition(new StateTransition(jumpFallState, new FuncStateCondition(() => _isDashing == false && _isGrounded == false && _rigidbody2D.velocity.y <= 0)));





        _stateMachine = new StateMachine2(idleState);
    }
}
