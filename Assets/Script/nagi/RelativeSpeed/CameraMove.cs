using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera cam;
    [SerializeField]
    private GameObject blue, red;

    private Vector3 bluePos, redPos,midPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        bluePos = blue.transform.position;
        redPos = red.transform.position;
        midPos = new Vector3((redPos.x + bluePos.x) / 2, 0, (redPos.z + bluePos.z) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
