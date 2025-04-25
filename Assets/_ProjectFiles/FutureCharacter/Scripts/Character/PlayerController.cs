using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using _ProjectFiles.SaveSystem;
public class PlayerController : MonoBehaviour
{
	[Inject] private SaveSystemController saveSystemController;
	[SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private GrapplingHook grapplingHook;
    [SerializeField] private bool grappingHookEnable;
    [SerializeField] private bool movableItem;
    [SerializeField] private GameObject moveItemGameobject;
    private Rigidbody2D rb;
    public InputController inputController {  get; private set; }
    private bool _platformTrigger;
    private string _platformtriggerName = "PlatformTrigger";
	private Inventory inventory;
    private Vector2 moveInput;
    public bool IsMovingItem { get; private set; }


    void Awake()
    {
		transform.position = saveSystemController.gameData.Position;
        rb = GetComponent<Rigidbody2D>();
        grapplingHook = GetComponent<GrapplingHook>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
        inputController = new InputController();
        inputController.Enable();
		inventory = GetComponent<Inventory>();
	}

	//public void TryOpenDoor(Door door)
	//{
	//	Key key = inventory.GetKey(door.doorID);
	//	if (key != null)
	//	{
	//		door.TryOpen(key);
	//	}
	//	else
	//	{
	//		Debug.Log("У вас нет подходящего ключа для этой двери.");
	//	}
	//}
	private void Start()
    {
        OnEnabled();
    }

    private void OnEnabled()
   {
        
        inputController.Gameplay.Jump.performed += OnJump;
        inputController.Gameplay.Jump.canceled += exitJump;
        inputController.Gameplay.Dash.performed += OnDash;
        inputController.Gameplay.UseAction.performed += OnUseAction;
        inputController.Gameplay.MovingItem.performed += OnMovingItem;
    }

    private void OnDisabled()
    {
        inputController.Gameplay.Jump.performed -= OnJump;
        inputController.Gameplay.Jump.canceled -= exitJump;
        inputController.Gameplay.Dash.performed -= OnDash;
        inputController.Gameplay.UseAction.performed -= OnUseAction;
    }
    private void Update()
    {
        if (inputController.Gameplay.Movement.ReadValue<Vector2>().x < 0 && IsMovingItem == false)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
        else if (inputController.Gameplay.Movement.ReadValue<Vector2>().x > 0 && IsMovingItem == false)
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

        if (!playerDash.IsDashing() && !grapplingHook.isGrappling)
        {
            if (IsMovingItem)
            {
                playerMovement.Move(moveInput,4);
            }
            else
            {
                playerMovement.Move(moveInput);
            }
            
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
            if (IsMovingItem == false) 
            {
                playerJump.Jump(context);
                playerJump.HoldJump(true);
            }
        }
    }

    private void exitJump(InputAction.CallbackContext context)
    {
        playerJump.HoldJump(false);
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if(IsMovingItem == false)
        {
            playerDash.PerformDash(new Vector2(rb.velocity.x, 0f).normalized);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName)) 
        {
            _platformTrigger = true;
        }
        if(collision.GetComponent<MovableItem>() != null)
        {
            Debug.Log("colision enter!!!");
            movableItem = true;
            moveItemGameobject = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName))
         {
             _platformTrigger = false;
         }
         if(collision.gameObject.name == "GrappingHook" && grappingHookEnable == false)
         {
            grappingHookEnable = true;
         }
         if(collision.GetComponent<MovableItem>() != null)
         {
            Debug.Log("Colision exit");
            movableItem = false;
         }
    }

    private void OnUseAction(InputAction.CallbackContext context)
    {
        if (grappingHookEnable) 
        {
            grapplingHook.StartGrapple();
        }
    }

    private void OnMovingItem(InputAction.CallbackContext context)
    {
        if (movableItem)
        {
            moveItemGameobject.GetComponent<MovableItem>().ToggleParent(transform);
            IsMovingItem = true;
            movableItem = false;
        }
        else
        {
            if (GetComponentInChildren<MovableItem>())
            {
                moveItemGameobject.GetComponent<MovableItem>().DropItem();
                IsMovingItem = false;
                moveItemGameobject = null;
            }

        }
    }
    public InputController GetInputController()
    {
        return inputController;
    }
}
