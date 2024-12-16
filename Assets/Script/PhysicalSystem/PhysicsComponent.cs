using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GR_Game.EnvironmentData;
using GR_Game.Struct;
using GR_Game.Enum;
using GR_Game.Math;
using System.Linq;
using static UnityEditor.PlayerSettings;

public class PhysicsComponent : MonoBehaviour
{
    [SerializeField]
    private bool gravityEnable = true;
    
    [SerializeField]
    private float boundsTolerance = 0.01f;

    [SerializeField]
    private float mass = 1f;
    [SerializeField]
    private float repulsion = 0f;
    [SerializeField]
    float frictionCoefficient = 0.5f;
    [SerializeField]
    float staticFrictionCoefficient = 0.6f;

    PhysicsComponent hitPhysicsCom;

    private bool isHit = false;

    private Vector3 moveVelocity = Vector3.zero;

    private List<Vector3> hitPointCenters = new();

    private List<Vector3> contactForces = new();

    private Vector3 gravity = Vector3.zero;

    private List<Vector3> hitMoveVelocityList = new();

    private List<float> hitMassList = new();

    private List<Vector3> sinkDirectionList = new();

    private List<Vector3> nomalAxisList = new();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (gravityEnable)
        {
            if(gravity == Vector3.zero) gravity = GetGravity() * Physics.gravity * Time.fixedDeltaTime * 0.03f;
            if(!isHit) moveVelocity += gravity;
        }

        Bounds myBounds = GetComponent<Collider>().bounds;

        myBounds.Expand(boundsTolerance);

        List<Collider> colliders = Physics.OverlapBox(myBounds.center, myBounds.extents, transform.rotation).ToList();

        Vector3 totalSink = Vector3.zero;

        isHit = false;

        hitPointCenters.Clear();
        contactForces.Clear(); 
        hitMoveVelocityList.Clear();
        hitMassList.Clear();
        sinkDirectionList.Clear();
        nomalAxisList.Clear();

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                hitPhysicsCom = collider.GetComponent<PhysicsComponent>();
                Shape shape = collider.gameObject.GetComponent<Shape>();

                float hitMass = 1;
                Vector3 hitMoveVelocity = Vector3.zero;

                if (hitPhysicsCom != null)
                {
                    hitMass = hitPhysicsCom.GetMass();
                    hitMoveVelocity = hitPhysicsCom.GetMoveVelocity();
                }

                hitMoveVelocityList.Add(hitMoveVelocity);
                hitMassList.Add(hitMass);

                if (shape != null && !shape.GetEnablePysics()) continue;

                Vector3 sinkDirection = Vector3.zero;
                float sinkDepth = 0;

                CalculateSink(collider.gameObject.transform, out sinkDirection, out sinkDepth);

                sinkDirectionList.Add(sinkDirection);
                nomalAxisList.Add(sinkDirection);

                totalSink += sinkDirection * sinkDepth * 0.5f;

                if (GR_GameMath.Vector3Dot(sinkDirection, -moveVelocity.normalized) > 0.5f || moveVelocity != Vector3.zero)
                {
                    if (hitPhysicsCom != null) CalculateVelocity(hitMoveVelocity, hitMass, sinkDirection);
                    else moveVelocity = Vector3.zero;

                    isHit = true;
                }
            }
        }
        for(int i = 0; i < sinkDirectionList.Count; i++)
        {
            CalculateForces(hitMoveVelocityList[i], nomalAxisList[i], hitMassList[i], sinkDirectionList);
        }

        transform.position += moveVelocity;
        transform.position += totalSink;
    }

    private void CalculateVelocity(Vector3 hitMoveVelocity, float hitMass, Vector3 normal)
    {
        if (Vector3.Dot(hitMoveVelocity, -moveVelocity.normalized) == 0)
        {
            moveVelocity = Vector3.zero;
            return;
        }

        Vector3 velocityNormal = Vector3.Dot(moveVelocity, normal) * normal;
        Vector3 hitVelocityNomal = Vector3.Dot(hitMoveVelocity, normal) * normal;

        Vector3 velocityTangent = moveVelocity - velocityNormal;

        Vector3 velocityNormalAfter = (mass * velocityNormal + hitMass * hitVelocityNomal) / (mass + hitMass);

        if(Vector3.Magnitude(velocityNormalAfter + velocityTangent) > 0.02f) moveVelocity = velocityNormalAfter + velocityTangent;
    }

    private void CalculateSink(Transform hitObj, out Vector3 direction, out float depth)
    {
        direction = Vector3.zero;
        depth = 0;

        Vector3[] myAxes = GetAxes(transform);
        Vector3[] hitAxes = GetAxes(hitObj);
        List<Vector3> checkAxes = GetCheckAxes(myAxes, hitAxes);

        List<Vector3> myVertices = GetVertices(transform);
        List<Vector3> hitVertices = GetVertices(hitObj);

        float minOverlap = float.MaxValue;
        Vector3 minOverlapAxis = Vector3.zero;

        float checkProjectionVal = 0;

        for (int i = 0; i < checkAxes.Count; i++)
        {
            float nowProjectionVal;

            float overlap = CalculateOverlap(checkAxes[i], myVertices, hitVertices, out nowProjectionVal);

            if (overlap < 0) return;

            if (overlap < minOverlap)
            {
                minOverlap = overlap;
                minOverlapAxis = checkAxes[i];
                checkProjectionVal = nowProjectionVal;
            }
        }

        CalculateHitPoint(minOverlapAxis, myVertices, hitVertices, checkProjectionVal, hitObj);

        if (Vector3.Dot(minOverlapAxis, transform.position - hitObj.transform.position) < 0)
        {
            minOverlapAxis = -minOverlapAxis;
        }

        direction = minOverlapAxis;
        depth = minOverlap;
    }

    private void CalculateForces(Vector3 hitMoveVelocity, Vector3 nomalAxis, float hitMass, List<Vector3> sinkDirectionList)
    {
        float relativeVelocity = Vector3.Dot(moveVelocity - hitMoveVelocity, nomalAxis);

        float impulse = -(1 + repulsion) * relativeVelocity / ((1 / mass) + (1 / hitMass));

        float normalForce = impulse / Time.fixedDeltaTime;

        float gravityNormalForce = Vector3.Dot(GetGravity() * gravity * mass, nomalAxis);

        contactForces.Add((normalForce + gravityNormalForce) * nomalAxis);

        Vector3 tangentGravity = gravity - Vector3.Dot(gravity, nomalAxis) * nomalAxis;

        Vector3 tangentVelocity = moveVelocity - Vector3.Dot(moveVelocity, nomalAxis) * nomalAxis;

        float tangentSpeed = tangentVelocity.magnitude;
        
        float maxStaticFriction = staticFrictionCoefficient * mass * gravity.magnitude;

        if (tangentSpeed == 0 && tangentGravity.magnitude <= maxStaticFriction)
        {
            return;
        }

        Vector3 frictionForce = Vector3.zero;

        if (tangentSpeed > 0)
        {
            float frictionMagnitude = Mathf.Min(frictionCoefficient * mass * (gravity).magnitude, tangentSpeed);

            frictionForce = -tangentVelocity.normalized * frictionMagnitude;

            contactForces.Add(frictionForce);
        }

        Vector3 slidingForce = tangentGravity + frictionForce;

        for (int i = 0; i < sinkDirectionList.Count; i++)
        {
            if (GR_GameMath.Vector3Dot(sinkDirectionList[i], -slidingForce.normalized) > 0.5f) slidingForce = Vector3.zero;
        }

        contactForces.Add(slidingForce);

        moveVelocity += slidingForce;
    }

    private void CalculateHitPoint(Vector3 axis, List<Vector3> myVertices, List<Vector3> hitVertices, float checkProjectionVal, Transform hitObj)
    {
        List<Vector3> myhitPoints = new();
        List<Vector3> otherHitPoints = new();
        List<Vector3> hitPoints;

        for (int i = 0; i < myVertices.Count; i++)
        {
            float projection = GR_GameMath.Vector3Dot(myVertices[i], axis);
            if (Mathf.Abs(projection - checkProjectionVal) < 0.01f) myhitPoints.Add(myVertices[i]);
        }

        for (int i = 0; i < hitVertices.Count; i++)
        {
            float projection = GR_GameMath.Vector3Dot(hitVertices[i], axis);
            if (Mathf.Abs(projection - checkProjectionVal) < 0.01f) otherHitPoints.Add(hitVertices[i]);
        }

        if (myhitPoints.Count > 1)
        {
            hitPoints = IsPointsWithinShape(otherHitPoints, myhitPoints, transform);

            if(hitPoints.Count != 0)
            {
                foreach(Vector3 point in IsPointsWithinShape(myhitPoints, otherHitPoints, hitObj))
                {
                    hitPoints.Add(point);
                }
            }
            else
            {
                hitPoints = myhitPoints;
            }

            Vector3 hitPointCenter = Vector3.zero;
            for (int i = 0; i < hitPoints.Count; i++)
            {
                hitPointCenter += hitPoints[i];
            }

            hitPointCenters.Add(hitPointCenter / hitPoints.Count);
        }
    }

    public List<Vector3> IsPointsWithinShape(List<Vector3> hitVertices, List<Vector3> hitPoints, Transform transform)
    {
        Bounds bounds = new Bounds(hitPoints[0], Vector3.zero);
        List<Vector3> containPoints = new();

        for (int i = 1; i < hitPoints.Count; i++)
        {
            bounds.Encapsulate(hitPoints[i]);
        }

        bounds.Expand(boundsTolerance);

        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        Debug.DrawLine(new Vector3(min.x, min.y, min.z), new Vector3(max.x, min.y, min.z), Color.green);
        Debug.DrawLine(new Vector3(min.x, min.y, min.z), new Vector3(min.x, max.y, min.z), Color.green);
        Debug.DrawLine(new Vector3(min.x, min.y, min.z), new Vector3(min.x, min.y, max.z), Color.green);

        Debug.DrawLine(new Vector3(max.x, max.y, max.z), new Vector3(min.x, max.y, max.z), Color.green);
        Debug.DrawLine(new Vector3(max.x, max.y, max.z), new Vector3(max.x, min.y, max.z), Color.green);
        Debug.DrawLine(new Vector3(max.x, max.y, max.z), new Vector3(max.x, max.y, min.z), Color.green);

        Debug.DrawLine(new Vector3(min.x, max.y, min.z), new Vector3(max.x, max.y, min.z), Color.green);
        Debug.DrawLine(new Vector3(max.x, min.y, min.z), new Vector3(max.x, max.y, min.z), Color.green);
        Debug.DrawLine(new Vector3(min.x, min.y, max.z), new Vector3(max.x, min.y, max.z), Color.green);

        Debug.DrawLine(new Vector3(min.x, max.y, max.z), new Vector3(min.x, max.y, min.z), Color.green);
        Debug.DrawLine(new Vector3(min.x, max.y, max.z), new Vector3(min.x, min.y, max.z), Color.green);
        Debug.DrawLine(new Vector3(max.x, min.y, max.z), new Vector3(max.x, min.y, min.z), Color.green);


        for (int i = 0; i < hitVertices.Count; i++)
        {
            if(bounds.Contains(hitVertices[i])) containPoints.Add(hitVertices[i]);
        }

        return containPoints;
    }

    private Vector3[] GetAxes(Transform objTransform)
    {
        return new Vector3[]
        {
            objTransform.right.normalized,
            objTransform.up.normalized,
            objTransform.forward.normalized,
        };
    }

    private List<Vector3> GetCheckAxes(Vector3[] myAxes, Vector3[] hitAxes)
    {

        List<Vector3> axes = new();

        for (int i = 0; i < myAxes.Length; i++)
        {
            axes.Add(myAxes[i]);
        }

        for (int i = 0; i < hitAxes.Length; i++)
        {
            axes.Add(hitAxes[i]);
        }

        for (int i = 0; i < myAxes.Length; i++)
        {
            for (int j = 0; j < hitAxes.Length; j++)
            {
                Vector3 cross = GR_GameMath.Vector3Cross(myAxes[i], hitAxes[j]);
                if (cross != Vector3.zero) axes.Add(cross.normalized);
            }
        }

        return axes;
    }

    private List<Vector3> GetVertices(Transform objTransform)
    {
        Vector3[] localVertices = objTransform.GetComponent<MeshFilter>().sharedMesh.vertices;
        Vector3 position = objTransform.position;

        List<Vector3> worldVertices = new();
        for (int i = 0; i < localVertices.Length; i++)
        {
            Vector3 scaledVertex = Vector3.Scale(localVertices[i], objTransform.localScale);

            if (worldVertices.Contains(position + objTransform.rotation * scaledVertex)) continue;
            worldVertices.Add(position + objTransform.rotation * scaledVertex);
        }

        return worldVertices;
    }

    private float CalculateOverlap(Vector3 axis, List<Vector3> myVertices, List<Vector3> hitVertices, out float checkProjectionVal)
    {
        float myMin = float.MaxValue;
        float myMax = float.MinValue;
        float hitMin = float.MaxValue;
        float hitMax = float.MinValue;

        checkProjectionVal = 0;

        for (int i = 0; i < myVertices.Count; i++)
        {
            float projection = GR_GameMath.Vector3Dot(myVertices[i], axis);
            myMin = Mathf.Min(myMin, projection);
            myMax = Mathf.Max(myMax, projection);
        }

        for (int i = 0; i < hitVertices.Count; i++)
        {
            float projection = GR_GameMath.Vector3Dot(hitVertices[i], axis);
            hitMin = Mathf.Min(hitMin, projection);
            hitMax = Mathf.Max(hitMax, projection);
        }

        if (myMax < hitMin || hitMax < myMin)
        {
            return -1;
        }

        if (Mathf.Min(myMax, hitMax) == myMax) checkProjectionVal = myMax;
        if (Mathf.Max(myMin, hitMin) == myMin) checkProjectionVal = myMin;

        return Mathf.Min(myMax, hitMax) - Mathf.Max(myMin, hitMin);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < hitPointCenters.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPointCenters[i], 0.05f);

            Gizmos.color = Color.blue;
            for (int j = 0; j < contactForces.Count; j++)
            {
                Gizmos.DrawLine(hitPointCenters[i], hitPointCenters[i] + contactForces[j]);
                Gizmos.color = Color.green;
            }
        }

        
    }

    public Vector3 GetMoveVelocity() => moveVelocity;

    public float GetMass() => mass;
}
