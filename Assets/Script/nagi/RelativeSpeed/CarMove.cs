using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Dropdown dropDown;

    private float speed;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (start)
        {
            if (dropDown.value == 0)
            {
                this.transform.position += new Vector3(speed * 0.5f, 0, 0) * Time.deltaTime;
            }
            if (dropDown.value == 1)
            {
                this.transform.position += new Vector3(-speed * 0.5f, 0, 0) * Time.deltaTime;
            }
            if (dropDown.value == 2)
            {
                this.transform.position += new Vector3(0, 0, -speed * 0.5f) * Time.deltaTime;
            }
            if (dropDown.value == 3)
            {
                this.transform.position += new Vector3(0, 0, speed * 0.5f) * Time.deltaTime;
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

    public void StartButton()
    {
        start = true;
    }
}
