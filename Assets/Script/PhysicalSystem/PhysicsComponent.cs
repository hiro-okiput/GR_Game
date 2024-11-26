using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GR_Game.EnvironmentData;

public class PhysicsComponent : MonoBehaviour
{
    [SerializeField]
    private bool gravityEnable = true;
    
    private Vector3 moveVector = Vector3.zero;
    private bool contact = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        Bounds myBounds = GetComponent<Collider>().bounds;

        Collider[] colliders = Physics.OverlapBox(myBounds.center, myBounds.extents);

        contact = false;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Shape shape = collider.gameObject.GetComponent<Shape>();
                if (shape != null && !shape.GetEnablePysics()) continue;

                contact = true;
            }
        }

        if (contact) return;

        if (gravityEnable) moveVector.y += GetGravity() * Physics.gravity.y * 0.01f * Time.fixedDeltaTime;
        transform.position += moveVector;
    }
}
