using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerJump playerJump;
    private Rigidbody2D rb;
    private InputController _inputController;
    private bool _platformTrigger;
    private string _platformtriggerName = "PlatformTrigger";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
        _inputController = new InputController();
        _inputController.Enable();    
    }
    private void Start()
    {
        OnEnabled();
    }

    private void OnEnabled()
   {
        
        _inputController.Gameplay.Jump.performed += OnJump;
        _inputController.Gameplay.Jump.canceled += exitJump;
        _inputController.Gameplay.Dash.performed += OnDash;
   }

    private void OnDisabled()
    {
        _inputController.Gameplay.Jump.performed -= OnJump;
        _inputController.Gameplay.Jump.canceled -= exitJump;
        _inputController.Gameplay.Dash.performed -= OnDash;
    }

    void FixedUpdate()
    {
        Vector2 moveInput = _inputController.Gameplay.Movement.ReadValue<Vector2>();
        if (!playerDash.IsDashing())
        {
            playerMovement.Move(moveInput);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(_inputController.Gameplay.Movement.ReadValue<Vector2>().y < 0 && _platformTrigger)
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
        }
        else
        {
            playerJump.Jump(context);
            playerJump.HoldJump(true);
        }
       
    }

    private void exitJump(InputAction.CallbackContext context)
    {
        playerJump.HoldJump(false);
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        playerDash.PerformDash(new Vector2(rb.velocity.x, 0f).normalized);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName)) 
        {
            _platformTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName))
         {
             _platformTrigger = false;
         }
    }
}
