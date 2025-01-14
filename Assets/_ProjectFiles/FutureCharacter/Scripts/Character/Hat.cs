using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidbody2D.velocity.x < 0) 
        {
            transform.localPosition = new Vector3(0.34f, 0.15f, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(rigidbody2D.velocity.x > 0)
        {
            transform.localPosition = new Vector3(-0.43f, 0.15f, 0);
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        else
        {

        }
    }

}