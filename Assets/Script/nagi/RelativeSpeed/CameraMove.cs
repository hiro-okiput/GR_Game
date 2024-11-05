using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform redPos, bluePos;
    public Camera cam;
    private Transform camPos;
    private Vector3 center = new Vector3(0,0,0);
    private float radius;
    private float margin=0.5f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        camPos = cam.gameObject.GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        radius = 0.0f;
        radius = Vector3.Distance(center, redPos.position);
        radius = Mathf.Max(radius, Vector3.Distance(center, bluePos.position));
        distance = (radius + margin) / Mathf.Sin(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //Debug.Log(distance);
        camPos.localPosition = new Vector3(0, distance, 0);
        camPos.LookAt(this.transform);
    }
}
