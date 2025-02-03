using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private float zoomStep, minCameraSize, maxCameraSize;
	
	[SerializeField] private SpriteRenderer mapRenderer;
	private float mapMinX, mapMaxX, mapMinY, mapMaxY;

	private Vector3 dragOrigin;

	private void Awake()
	{
		mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
		mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

		mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
		mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
	}	

	private void Update()
	{
		PanCamera();

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		
		if (scroll  > 0f)
			ZoomIn();
		else if (scroll < 0f) ZoomOut();
	}

	private void PanCamera()
	{
		if (Input.GetMouseButtonDown(0))
			dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButton(0))
		{
			Vector3 difference = dragOrigin - mainCamera.ScreenToWorldPoint(Input.mousePosition);

			mainCamera.transform.position = ClampCamera(mainCamera.transform.position + difference);
		}
	}
	
	private void ZoomIn()
	{
		float newSize = mainCamera.orthographicSize - zoomStep;
		mainCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize,maxCameraSize);

		mainCamera.transform.position = ClampCamera(mainCamera.transform.position);
	}
	private void ZoomOut()
	{
		float newSize = mainCamera.orthographicSize + zoomStep;
		mainCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize,maxCameraSize);
		
		mainCamera.transform.position = ClampCamera(mainCamera.transform.position);
	}

	private Vector3 ClampCamera(Vector3 targetPosition)
	{
		float cameraHeight = mainCamera.orthographicSize;
		float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;

		float minX = mapMinX + cameraWidth;
		float maxX = mapMaxX - cameraWidth;
		float minY = mapMinY + cameraHeight;
		float maxY = mapMaxY - cameraHeight;

		float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
		float newY = Mathf.Clamp(targetPosition.y, minY, maxY);
		
		return new Vector3(newX, newY, targetPosition.z);
	}
}
