using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GR_Game.EnvironmentData;

public class PhysicsComponent : MonoBehaviour
{
    [SerializeField]
    private bool gravityEnable = true;

    private Vector3 moveVelocity = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(gravityEnable) moveVelocity.y += GetGravity() * Physics.gravity.y * Time.fixedDeltaTime * 0.01f;

        Bounds myBounds = GetComponent<Collider>().bounds;

        Collider[] colliders = Physics.OverlapBox(myBounds.center, myBounds.extents);

        Vector3 totalOverlap = Vector3.zero;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Shape shape = collider.gameObject.GetComponent<Shape>();
                if (shape != null && !shape.GetEnablePysics()) continue;
                totalOverlap += CalculateOverlap(myBounds, collider);
            }
        }

        transform.position += moveVelocity;
        transform.position += totalOverlap;
    }

    private Vector3 CalculateOverlap(Bounds myBounds, Collider hitCollider)
    {
        Vector3 overlap = Vector3.zero;
        Bounds hitBounds = hitCollider.bounds;

        Vector3 hitPoint = hitCollider.ClosestPointOnBounds(transform.position);

        Debug.Log(hitPoint);

        Vector3 BoundsMax = myBounds.max + moveVelocity;
        Vector3 BoundsMin = myBounds.min + moveVelocity;

        if (hitPoint.y <= myBounds.center.y)
        {
            moveVelocity.y = 0;
            overlap.y = Mathf.Max(0, hitPoint.y - BoundsMin.y);
        }
        else overlap.y = -(Mathf.Max(0, BoundsMax.y - hitPoint.y));

        if (hitPoint.x <= myBounds.center.x)
        {
            moveVelocity.y = 0;
            overlap.x = Mathf.Max(0, hitPoint.x - BoundsMin.x);
        }
        else overlap.x = -(Mathf.Max(0, BoundsMax.x - hitPoint.x));

        return overlap * 0.5f;
    }
}
