using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reset : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Slider thetaSlider, velocitySlider;
    [SerializeField]
    private TMP_InputField thetaField;

    private Vector3 ballPos;
    private Rigidbody rb;

    MoveBall moveBall;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = ball.transform.position;
        rb = ball.GetComponent<Rigidbody>();
        moveBall = ball.GetComponent<MoveBall>();
    }

    public void ResetButton()
    {
        ball.transform.position = ballPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        thetaSlider.value = 0;
        velocitySlider.value = 0;
        thetaField.text = "0";
        moveBall.firstCheck = false;
    }
    
}
