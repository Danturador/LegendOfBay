using UnityEngine;
using System.Collections;
using Spine.Unity.Examples;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashPower = 30f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private SkeletonGhost _sceletonGhost;

    private Rigidbody2D _rb;
    private bool _canDash = true;
    private bool _isDashing;
    private float _originalGravity;

    public void Initialize(Rigidbody2D rigidbody)
    {
        _rb = rigidbody;
        _originalGravity = _rb.gravityScale;
        _sceletonGhost = GetComponentInChildren<SkeletonGhost>();
        _sceletonGhost.ghostingEnabled = false;
    }

    public void PerformDash(Vector2 moveDirection)
    {
        if (_canDash && moveDirection.x != 0)
        {
            StartCoroutine(Dash(moveDirection));
        }
    }

    private IEnumerator Dash(Vector2 moveDirection)
    {
        _canDash = false;
        _isDashing = true;
        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2(moveDirection.x * dashPower, 0f);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        if (_sceletonGhost != null)
        {
            _sceletonGhost.ghostingEnabled = true;
        }
        yield return new WaitForSeconds(dashingTime);
        _rb.gravityScale = _originalGravity;
        _rb.velocity = Vector2.zero;
        _isDashing = false;
        Physics2D.IgnoreLayerCollision(7, 9, false);
        if (_sceletonGhost != null)
        {
            _sceletonGhost.ghostingEnabled = false;
        }
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    public bool IsDashing()
    {
        return _isDashing;
    }
}