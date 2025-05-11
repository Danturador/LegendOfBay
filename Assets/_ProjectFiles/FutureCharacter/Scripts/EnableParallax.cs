using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParallax : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            gameObject.GetComponentInParent<ParallaxEffect>().enabled = true;
            Destroy(gameObject);
        }
    }
}
