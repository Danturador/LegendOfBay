using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLightGrapplingTrigger : MonoBehaviour
{
    [SerializeField] private BackLightGrapplingPoints backLightGrapplingPoints;
    private void Start()
    {
        backLightGrapplingPoints = FindAnyObjectByType<BackLightGrapplingPoints>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.GetComponentInParent<PlayerController>() != null && collision.gameObject.layer != 8)
        {
            if(backLightGrapplingPoints != null)
            {
                backLightGrapplingPoints.BackLightActivate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerController>() != null && collision.gameObject.layer != 8)
        {
            
            if (backLightGrapplingPoints != null)
            {
                backLightGrapplingPoints.BackLightDisable();
            }
        }
       
    }
}

