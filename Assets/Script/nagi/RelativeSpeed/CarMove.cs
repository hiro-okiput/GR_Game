using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarMove : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Dropdown dropDown;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (dropDown.value == 0)
        {
            if (this.name == "Blue Car")
            {
                Vector3 blueVector = new Vector3(speed * 5, 0, 0);
                rb.AddForce(blueVector, ForceMode.Acceleration);
            }
            else if (this.name == "Red Car")
            {
                Vector3 redVector = new Vector3(speed * 5, 0, 0);
                rb.AddForce(redVector, ForceMode.Acceleration);
            }
        }
        if (dropDown.value == 1)
        {
            if (this.name == "Blue Car")
            {
                Vector3 blueVector = new Vector3(-speed * 5, 0, 0);
                rb.AddForce(blueVector, ForceMode.Acceleration);
            }
            else if (this.name == "Red Car")
            {
                Vector3 redVector = new Vector3(-speed * 5, 0, 0);
                rb.AddForce(redVector, ForceMode.Acceleration);
            }
        }
        if (dropDown.value == 2)
        {
            if (this.name == "Blue Car")
            {
                Vector3 blueVector = new Vector3(0, 0, -speed * 5);
                rb.AddForce(blueVector, ForceMode.Acceleration);
            }
            else if (this.name == "Red Car")
            {
                Vector3 redVector = new Vector3(0, 0, -speed * 5);
                rb.AddForce(redVector, ForceMode.Acceleration);
            }
        }
        if (dropDown.value == 3)
        {
            if (this.name == "Blue Car")
            {
                Vector3 blueVector = new Vector3(0, 0, speed * 5);
                rb.AddForce(blueVector, ForceMode.Acceleration);
            }
            else if (this.name == "Red Car")
            {
                Vector3 redVector = new Vector3(0, 0, speed * 5);
                rb.AddForce(redVector, ForceMode.Acceleration);
            }
        }
    }

    public void BlueSpeedGetText()
    {
        speed = float.Parse(inputField.text);
    }

    public void RedSpeedGetText()
    {
        speed = float.Parse(inputField.text);
    }
}
