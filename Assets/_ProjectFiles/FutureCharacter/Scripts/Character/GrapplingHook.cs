using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private Transform[] grapplePoints;
    [SerializeField] private float grappleSpeed = 5f;
    [SerializeField] private float impulseForce = 5f;
    [SerializeField] private float maxGrappleDistance = 100f;
    private DistanceJoint2D distanceJoint;
    private float defaultGravityScale;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;

    public bool isGrappling { get; private set; }
    private Transform lastGrapplePoint;
    private Coroutine grappleCooldownCoroutine;
    private float grappleCooldown = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (isGrappling)
        {
            MoveTowardsGrapplePoint();
        }
    }

    public void StartGrapple()
    {
        Transform closestPoint = FindClosestAvailableGrapplePoint();
        if (closestPoint != null)
        {
            float distanceToClosestPoint = Vector2.Distance(transform.position, closestPoint.position);
            if (distanceToClosestPoint <= maxGrappleDistance)
            {
                distanceJoint.connectedAnchor = closestPoint.position;
                distanceJoint.enabled = true;
                isGrappling = true;
                lineRenderer.enabled = true;
                lastGrapplePoint = closestPoint;

                if (grappleCooldownCoroutine != null)
                {
                    StopCoroutine(grappleCooldownCoroutine);
                }
                grappleCooldownCoroutine = StartCoroutine(GrappleCooldown());
            }
            else
            {
                Debug.Log("point out in distance");
            }
        }
        else
        {
            Debug.Log("no grapping points");
        }
    }

    private void StopGrapple()
    {
        distanceJoint.enabled = false;
        StartCoroutine(Grappling());
        lineRenderer.enabled = false;
    }

    private void MoveTowardsGrapplePoint()
    {
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.distance -= grappleSpeed;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, distanceJoint.connectedAnchor);

        if (Vector2.Distance(transform.position, distanceJoint.connectedAnchor) < 0.7f)
        {
            StopGrapple();

            Vector2 directionToGrapplePoint = (distanceJoint.connectedAnchor - (Vector2)transform.position).normalized;

            Vector2 currentVelocity = rb.velocity.normalized;

            Vector2 finalDirection = (directionToGrapplePoint + currentVelocity).normalized;

            rb.AddForce(finalDirection * impulseForce, ForceMode2D.Impulse);
        }
    }

    private Transform FindClosestAvailableGrapplePoint()
    {
        Transform closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform point in grapplePoints)
        {
            float distance = Vector2.Distance(transform.position, point.position);
            bool isOnCooldown = point == lastGrapplePoint && grappleCooldownCoroutine != null;

            if (distance < closestDistance && !isOnCooldown)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    private IEnumerator GrappleCooldown()
    {
        yield return new WaitForSeconds(grappleCooldown);
        lastGrapplePoint = null;
        grappleCooldownCoroutine = null;
    }

    private IEnumerator Grappling()
    {
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.5f);
        isGrappling = false;
        rb.gravityScale = defaultGravityScale;
    }
}