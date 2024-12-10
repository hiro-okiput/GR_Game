using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GR_Game.EnvironmentData;
using GR_Game.Struct;
using GR_Game.Enum;

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

    private Vector3 CalculateOverlap(Bounds myBounds, Bounds hitBounds)
    {
        Vector3 overlap = Vector3.zero;

        Vector3 myBoundsMax = myBounds.max + moveVelocity;
        Vector3 myBoundsMin = myBounds.min + moveVelocity;
        Vector3 myBoundsCenter = myBounds.center + moveVelocity;

        BoundsData myBoundsData = new();
        myBoundsData.Initialize();
        myBoundsData.SetValue(Axes.X, myBoundsMax, myBoundsMin, myBoundsCenter);
        BoundsData hitBoundsData = new();
        hitBoundsData.Initialize();
        hitBoundsData.SetValue(Axes.X, hitBounds.max, hitBounds.min, hitBounds.center);

        Vector3 hitPoint = CalculateHitPointCenter(Axes.X, myBoundsData, hitBoundsData);

        return overlap;
    }

    private Vector3 CalculateHitPointCenter(Axes axes, BoundsData myBoundsData, BoundsData hitBoundsData)
    {
        Vector3 result = Vector3.zero;
        float[] hitPoint = { 0, 0, 0 };

        if (hitBoundsData.max[0] >= myBoundsData.min[0] && hitBoundsData.max[0] >= myBoundsData.min[0]) hitPoint[0] = hitBoundsData.max[0];
        else if (hitBoundsData.min[0] <= myBoundsData.max[0]) hitPoint[0] = hitBoundsData.min[0];

        for (int i = 1; i < 3; i++)
        {
            if (hitBoundsData.max[i] == myBoundsData.min[i]) hitPoint[i] = hitBoundsData.max[i];
            else if (hitBoundsData.min[i] == myBoundsData.max[i]) hitPoint[i] = hitBoundsData.min[i];
            else if (myBoundsData.min[i] <= hitBoundsData.max[i] && hitBoundsData.max[i] <= myBoundsData.max[i])
            {
                if (myBoundsData.min[i] <= hitBoundsData.min[i] && hitBoundsData.min[i] <= myBoundsData.max[i])
                {
                    hitPoint[i] = hitBoundsData.center[i];
                }
                else
                {
                    hitPoint[i] = myBoundsData.min[i] + ((hitBoundsData.max[i] - myBoundsData.min[i]) / 2);
                }
            }
            else
            {
                if (myBoundsData.min[i] <= hitBoundsData.min[i] && hitBoundsData.min[i] <= myBoundsData.max[i])
                {
                    hitPoint[i] = hitBoundsData.min[i] + ((myBoundsData.max[i] - hitBoundsData.min[i]) / 2);
                }
                else
                {
                    hitPoint[i] = myBoundsData.center[i];
                }
            }
        }

        switch (axes)
        {
            case Axes.X:
                result.x = hitPoint[0];
                result.y = hitPoint[1];
                result.z = hitPoint[2];
                break;
            case Axes.Y:
                result.x = hitPoint[1];
                result.y = hitPoint[0];
                result.z = hitPoint[2];
                break;
            case Axes.Z:
                result.x = hitPoint[2];
                result.y = hitPoint[0];
                result.z = hitPoint[1];
                break;
        }

        return result;
    }
}
