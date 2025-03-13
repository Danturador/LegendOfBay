using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerJump playerJump;
    private Rigidbody2D rb;
    public InputController inputController {  get; private set; }
    private bool _platformTrigger;
    private string _platformtriggerName = "PlatformTrigger";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
        inputController = new InputController();
        inputController.Enable();    
    }
    private void Start()
    {
        OnEnabled();
    }

    private void OnEnabled()
   {
        
        inputController.Gameplay.Jump.performed += OnJump;
        inputController.Gameplay.Jump.canceled += exitJump;
        inputController.Gameplay.Dash.performed += OnDash;
        inputController.Gameplay.Escape.performed += OnEscape;
   }

    private void OnDisabled()
    {
        inputController.Gameplay.Jump.performed -= OnJump;
        inputController.Gameplay.Jump.canceled -= exitJump;
        inputController.Gameplay.Dash.performed -= OnDash;
        inputController.Gameplay.Escape.performed -= OnEscape;
    }
    private void Update()
    {
        if (inputController.Gameplay.Movement.ReadValue<Vector2>().x < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
        else if (inputController.Gameplay.Movement.ReadValue<Vector2>().x > 0)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else 
        { 
        
        }
    }
    void FixedUpdate()
    {
        Vector2 moveInput = inputController.Gameplay.Movement.ReadValue<Vector2>();
        if (!playerDash.IsDashing())
        {
            playerMovement.Move(moveInput);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(inputController.Gameplay.Movement.ReadValue<Vector2>().y < 0 && _platformTrigger)
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

    private void OnEscape(InputAction.CallbackContext context) 
    { 
        Application.Quit();
    }

    public InputController GetInputController()
    {
        return inputController;
    }
}
