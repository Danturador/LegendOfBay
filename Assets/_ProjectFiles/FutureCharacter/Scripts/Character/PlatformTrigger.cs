using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private string _triggerLayername = "PlatformTrigger";


    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()  
    {
        if (_rb.velocity.y < -12f)
        {
            _boxCollider.isTrigger = false;
        }
        
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_triggerLayername) && _rb.velocity.y <= 0)
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_triggerLayername))
        {
            Physics2D.IgnoreLayerCollision(7, 6, true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            _boxCollider.isTrigger = true;
        }
    }
}
