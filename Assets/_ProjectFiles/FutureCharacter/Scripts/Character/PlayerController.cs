using _ProjectFiles.SaveSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private GrapplingHook grapplingHook;
    [SerializeField] private bool grappingHookEnable;
    [SerializeField] private bool movableItem;
    [SerializeField] private GameObject moveItemGameobject;
    [Inject] private InputController _inputController;
    private readonly string _movableObjectTriggerLayerName = "MovableObjectTrigger";

    //public InputController inputController {  get; private set; }
    private bool _platformTrigger;
    private readonly string _platformtriggerName = "PlatformTrigger";

    private Inventory inventory;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [Inject] private SaveSystemController saveSystemController;

    public bool IsMovingItem { get; private set; }
    public InputController InputController => _inputController;
    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        transform.position = saveSystemController.gameData.Position;
        grappingHookEnable = saveSystemController.gameData.HaveGrapplingHook;
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        grapplingHook = GetComponent<GrapplingHook>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
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
    //		Debug.Log("� ��� ��� ����������� ����� ��� ���� �����.");
    //	}
    //}
    private void Start()
    {
        OnEnabled();
    }

    private void Update()
    {
        if (_inputController.Gameplay.Movement.ReadValue<Vector2>().x < 0 && IsMovingItem == false)
            gameObject.transform.localScale = new Vector2(-1, 1);
        else if (_inputController.Gameplay.Movement.ReadValue<Vector2>().x > 0 && IsMovingItem == false)
            gameObject.transform.localScale = new Vector2(1, 1);
    }

    private void FixedUpdate()
    {
        var moveInput = _inputController.Gameplay.Movement.ReadValue<Vector2>();

        if (!playerDash.IsDashing() && !grapplingHook.isGrappling)
        {
            if (IsMovingItem)
                playerMovement.Move(moveInput, 4);
            else
                playerMovement.Move(moveInput);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName)) _platformTrigger = true;
        if (collision.GetComponent<MovableItem>() != null &&
            collision.gameObject.layer == LayerMask.NameToLayer(_movableObjectTriggerLayerName))
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
        if (collision.gameObject.layer == LayerMask.NameToLayer(_platformtriggerName)) _platformTrigger = false;

        if (collision.GetComponent<MovableItem>() != null &&
            collision.gameObject.layer == LayerMask.NameToLayer(_movableObjectTriggerLayerName))
        {
            Debug.Log("Colision exit");
            movableItem = false;
        }
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

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_inputController.Gameplay.Movement.ReadValue<Vector2>().y < 0 && _platformTrigger)
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
        if (IsMovingItem == false) playerDash.PerformDash(new Vector2(rb.velocity.x, 0f).normalized);
    }

    private void OnUseAction(InputAction.CallbackContext context)
    {
        if (grappingHookEnable) grapplingHook.StartGrapple();
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
            if (GetComponentInChildren<MovableItem>()) moveItemGameobject.GetComponent<MovableItem>()?.DropItem();
            IsMovingItem = false;
            moveItemGameobject = null;
        }
    }

    public void ReceiveHook()
    {
        grappingHookEnable = true;
    }
}