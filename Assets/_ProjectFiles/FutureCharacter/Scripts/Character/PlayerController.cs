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
    [SerializeField] private bool possibleUseGrapplingHook;
    [SerializeField] private bool movableItem;
    [SerializeField] private GameObject moveItemGameobject;
    private Rigidbody2D rb;
    [Inject] private InputController _inputController;
    
    //public InputController inputController {  get; private set; }
    private bool _platformTrigger;
    private string _platformtriggerName = "PlatformTrigger";
    private string _movableObjectTriggerLayerName = "MovableObjectTrigger";

    private Inventory inventory;
    private Vector2 moveInput;
    
    public bool IsMovingItem { get; private set; }
    public Collider2D Collider { get; private set; }
    public InputController InputController => _inputController;

    void Awake()
    {
		transform.position = saveSystemController.gameData.Position;
        grappingHookEnable = saveSystemController.gameData.HaveGrapplingHook;
        rb = GetComponent<Rigidbody2D>();
        grapplingHook = GetComponent<GrapplingHook>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
		inventory = GetComponent<Inventory>();
        Collider = GetComponent<Collider2D>();

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
	//		Debug.Log("� ��� ��� ����������� ����� ��� ���� �����.");
	//	}
	//}
	private void Start()
    {
        OnEnabled();
    }

    private void OnEnabled()
   {
        
        _inputController.Gameplay.Jump.performed += OnJump;
        _inputController.Gameplay.Jump.canceled += exitJump;
        _inputController.Gameplay.Dash.performed += OnDash;
        _inputController.Gameplay.UseAction.performed += OnUseAction;
        _inputController.Gameplay.MovingItem.performed += OnMovingItem;
    }

    private void OnDisabled()
    {
        _inputController.Gameplay.Jump.performed -= OnJump;
        _inputController.Gameplay.Jump.canceled -= exitJump;
        _inputController.Gameplay.Dash.performed -= OnDash;
        _inputController.Gameplay.UseAction.performed -= OnUseAction;
    }
    private void Update()
    {
        if (_inputController.Gameplay.Movement.ReadValue<Vector2>().x < 0 && IsMovingItem == false)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
        else if (_inputController.Gameplay.Movement.ReadValue<Vector2>().x > 0 && IsMovingItem == false)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else 
        { 
        
        }

    }
    void FixedUpdate()
    {
        moveInput = _inputController.Gameplay.Movement.ReadValue<Vector2>();
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
        if(_inputController.Gameplay.Movement.ReadValue<Vector2>().y < 0 && _platformTrigger)
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
            playerDash.PerformDash(moveInput);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName)) 
        {
            _platformTrigger = true;
        }
        if(collision.GetComponent<MovableItem>() != null && collision.gameObject.layer == LayerMask.NameToLayer(_movableObjectTriggerLayerName))
        {
            Debug.Log("colision enter!!!");
            movableItem = true;
            moveItemGameobject = collision.gameObject;
        }
        if (collision.gameObject.name == "GrappingHook" && grappingHookEnable == false)
        {
            grappingHookEnable = true;
            Destroy(collision.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName))
         {
             _platformTrigger = false;
         }
        
         if(collision.GetComponent<MovableItem>() != null && collision.gameObject.layer == LayerMask.NameToLayer(_movableObjectTriggerLayerName))
         {
            Debug.Log("Colision exit");
            movableItem = false;
         }
        if (collision.GetComponent<BackLightGrapplingTrigger>() != null)
        {
            possibleUseGrapplingHook = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<BackLightGrapplingTrigger>() != null)
        {
            possibleUseGrapplingHook = true;
        }
    }

    private void OnUseAction(InputAction.CallbackContext context)
    {
        if (grappingHookEnable && possibleUseGrapplingHook) 
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
                moveItemGameobject.GetComponent<MovableItem>()?.DropItem();
            }
            IsMovingItem = false;
            moveItemGameobject = null;


        }
    }

    public void ReceiveHook() => grappingHookEnable = true;
}
