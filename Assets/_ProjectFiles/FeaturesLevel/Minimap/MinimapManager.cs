using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private float zoomStep, minCameraSize, maxCameraSize;
    [SerializeField] private SpriteRenderer mapRenderer;
    [SerializeField] private InputController _inputController;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private Vector3 dragOrigin;
    private bool isMinimapActive;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;

        minimapCamera.gameObject.SetActive(false);
    }

    private void Start()
    {
        _inputController = FindAnyObjectByType<PlayerController>().inputController;

        isMinimapActive = true;
        ToggleMinimap();

        _inputController.Gameplay.OpenMap.performed += ctx => ToggleMinimap();
        _inputController.Map.CloseMap.performed += ctx => ToggleMinimap();

        //_inputController.Map.MapMovement.performed += ctx => PanCamera();
        _inputController.Map.MapZoom.performed += ctx => HandleZoom();
    }
	private void OnDestroy()
	{
        _inputController.Gameplay.OpenMap.performed -= ctx => ToggleMinimap();
        _inputController.Map.CloseMap.performed -= ctx => ToggleMinimap();

        //_inputController.Map.MapMovement.performed -= ctx => PanCamera();
        _inputController.Map.MapZoom.performed -= ctx => HandleZoom();
	}
	private void Update()
	{
		if (isMinimapActive)
		{
			PanCamera();
		}
	}

	private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll > 0f)
            ZoomIn();
        else if (scroll < 0f)
            ZoomOut();
    }

    private void PanCamera()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            dragOrigin = minimapCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 difference = dragOrigin - minimapCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            minimapCamera.transform.position = ClampCamera(minimapCamera.transform.position + difference);
        }
    }

    private void ZoomIn()
    {
        float newSize = minimapCamera.orthographicSize - zoomStep;
        minimapCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);
        minimapCamera.transform.position = ClampCamera(minimapCamera.transform.position);
    }

    private void ZoomOut()
    {
        float newSize = minimapCamera.orthographicSize + zoomStep;
        minimapCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);
        minimapCamera.transform.position = ClampCamera(minimapCamera.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float cameraHeight = minimapCamera.orthographicSize;
        float cameraWidth = minimapCamera.orthographicSize * minimapCamera.aspect;

        float minX = mapMinX + cameraWidth;
        float maxX = mapMaxX - cameraWidth;
        float minY = mapMinY + cameraHeight;
        float maxY = mapMaxY - cameraHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    private void ToggleMinimap()
    {
        isMinimapActive = !isMinimapActive;
        minimapCamera.gameObject.SetActive(isMinimapActive);

        if (isMinimapActive)
        {
            Vector3 playerPosition = FindAnyObjectByType<PlayerController>().transform.position;
            minimapCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y, minimapCamera.transform.position.z);
            minimapCamera.orthographicSize = Mathf.Clamp(minCameraSize, minCameraSize, maxCameraSize);

            _inputController.Gameplay.Disable();
            _inputController.Map.Enable();
        }
        else
        {
            _inputController.Map.Disable();
            _inputController.Gameplay.Enable();
        }
    }
}