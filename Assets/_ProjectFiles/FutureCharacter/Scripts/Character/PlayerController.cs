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
    [SerializeField] private new Collider2D collider;
    [Header("Debug")] [SerializeField] private bool skipPlayerPositionLoad;
    private readonly string _movableObjectTriggerLayerName = "MovableObjectTrigger";

    //public InputController inputController {  get; private set; }
    private bool _platformTrigger;
    private readonly string _platformtriggerName = "PlatformTrigger";
    [Inject] private InputController inputController;

    private Inventory inventory;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [Inject] private SaveSystemController saveSystemController;
    public bool IsMovingItem { get; private set; }
    public Collider2D Collider => collider;


    private void Awake()
    {
        if (!skipPlayerPositionLoad) transform.position = saveSystemController.gameData.Position;

        rb = GetComponent<Rigidbody2D>();
        grapplingHook = GetComponent<GrapplingHook>();
        playerMovement.Initialize(rb);
        playerDash.Initialize(rb);
        playerJump.Initialize(rb);
        // inputController = new InputController();
        // inputController.Enable();
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
        if (inputController.Gameplay.Movement.ReadValue<Vector2>().x < 0 && IsMovingItem == false)
            gameObject.transform.localScale = new Vector2(-1, 1);
        else if (inputController.Gameplay.Movement.ReadValue<Vector2>().x > 0 && IsMovingItem == false)
            gameObject.transform.localScale = new Vector2(1, 1);
    }

    private void FixedUpdate()
    {
        var moveInput = inputController.Gameplay.Movement.ReadValue<Vector2>();

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

        if (collision.gameObject.name == "GrappingHook" && grappingHookEnable == false) grappingHookEnable = true;
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

    private void OnJump(InputAction.CallbackContext context)
    {
        if (inputController.Gameplay.Movement.ReadValue<Vector2>().y < 0 && _platformTrigger)
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

    public InputController GetInputController()
    {
        return inputController;
    }
}