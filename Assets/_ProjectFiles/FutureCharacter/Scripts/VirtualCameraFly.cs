using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraFly : MonoBehaviour
{
    public float moveSpeed = 15f;
    private float cameraSize = 8;
    [SerializeField] Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (Input.GetKey(KeyCode.R))
        {
            cameraSize += 0.1f;
        }

        if (Input.GetKey(KeyCode.T))
        {
            cameraSize -= 0.1f;
        }

        cam.orthographicSize = cameraSize; 

       cam.transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

