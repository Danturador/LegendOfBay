using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
	[SerializeField] private Camera minimapCamera;
	[SerializeField] private float zoomStep, minCameraSize, maxCameraSize;
	
	[SerializeField] private SpriteRenderer mapRenderer;
	private float mapMinX, mapMaxX, mapMinY, mapMaxY;

	private Vector3 dragOrigin;
	private bool isMinimapActive = false;

	private void Awake()
	{
		mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
		mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

		mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
		mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;

		minimapCamera.gameObject.SetActive(false);
	}	

	private void Update()
	{
		ToggleMinimap();

		if (isMinimapActive)
		{
			PanCamera();

			float scroll = Input.GetAxis("Mouse ScrollWheel");
			if (scroll > 0f)
				ZoomIn();
			else if (scroll < 0f)
				ZoomOut();
		}
	}

	private void PanCamera()
	{
		if (Input.GetMouseButtonDown(0))
			dragOrigin = minimapCamera.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton(0))
		{
			Vector3 difference = dragOrigin - minimapCamera.ScreenToWorldPoint(Input.mousePosition);

			minimapCamera.transform.position = ClampCamera(minimapCamera.transform.position + difference);
		}
	}
	
	private void ZoomIn()
	{
		float newSize = minimapCamera.orthographicSize - zoomStep;
		minimapCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize,maxCameraSize);

		minimapCamera.transform.position = ClampCamera(minimapCamera.transform.position);
	}
	private void ZoomOut()
	{
		float newSize = minimapCamera.orthographicSize + zoomStep;
		minimapCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize,maxCameraSize);
		
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
		if (Input.GetKeyDown(KeyCode.M))
		{
			isMinimapActive = !isMinimapActive;
			minimapCamera.gameObject.SetActive(isMinimapActive);
		}
	}
}
