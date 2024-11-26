using GR_Game.Math;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Camera mainCamera;
    private bool enablePysics = false; 

    // Update is called once per frame
    void Update()
    {
        if (enablePysics) return;
        Vector3 mousePosition = Input.mousePosition;
        if (mainCamera == null) mainCamera = Camera.main;
        if (mousePosition.x >= 0 && mousePosition.x <= Screen.width &&
            mousePosition.y >= 0 && mousePosition.y <= Screen.height)
        {
            Vector3 screenPoint = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            transform.position = screenPoint;
            if(!GetComponent<MeshRenderer>().enabled)GetComponent<Renderer>().enabled = true;
            if(Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                enablePysics = true;
                GetComponent<PhysicsComponent>().enabled = true;
            }
        }
    }

    public bool GetEnablePysics() => enablePysics;
}
