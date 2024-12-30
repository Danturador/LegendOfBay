using UnityEngine;

public class ThroughPlatformCheck : MonoBehaviour
{
    private GameObject parentGameobject;
    private bool throughPlatformCheck = true;
    private void OnEnable()
    {
        parentGameobject = transform.parent.gameObject;
        parentGameobject.layer = 6;
    }

    private void FixedUpdate()
    {
        throughPlatformCheck = Physics2D.OverlapBox(transform.position,new Vector2(1,1),0,3);
        if(throughPlatformCheck == false ) 
        {
            parentGameobject.layer = 7;
            this.gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 2));
    }
}

