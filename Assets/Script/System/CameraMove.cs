using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    private Vector3 initMousePosition = Vector3.zero;
    private Vector3 currentMousePosition = Vector3.zero;
    private Vector3 initRotate;

    // Update is called once per frame
    void Update()
    {
        currentMousePosition = Input.mousePosition;
        if (initMousePosition != Vector3.zero && Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            initMousePosition = Vector3.zero;
            initRotate = transform.eulerAngles;
        }
        if (currentMousePosition.x >= 0 && currentMousePosition.x <= Screen.width &&
            currentMousePosition.y >= 0 && currentMousePosition.y <= Screen.height)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Right))
            {
                initMousePosition = Input.mousePosition;
            }
        }
        if (initMousePosition != Vector3.zero && Input.GetMouseButton((int)MouseButton.Right))
        {
            Vector3 mouseMove = currentMousePosition - initMousePosition;
            Vector3 newRotate = Vector3.zero;
            newRotate.y = initRotate.y + mouseMove.x * rotateSpeed;
            newRotate.x = Mathf.Clamp(initRotate.x + mouseMove.y * rotateSpeed, -90f, 90f);
            transform.eulerAngles = newRotate;
        }
    }
}
