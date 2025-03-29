using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundContoller : MonoBehaviour
{
    private float startPosition, length;
    public GameObject cam;
    public float parallaxEffect;
	private void Start()
    {
		cam = FindObjectOfType<Camera>().gameObject;
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);


        // infinite scrolling
        //if (movement > startPosition + length)
        //{
        //    startPosition += length;
        //}
        //else if (movement > startPosition - length)
        //{
        //    startPosition -= length;
        //}
    }
}
