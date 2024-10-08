using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour
{
    #region •Ï”éŒ¾
    ////‰‘¬
    private float velocity0 = 0;
    //Šp“x
    private float theta = 0;
    //•ûŒü
    Vector3 direction;
    ////d—Í
    //private int gravity = 0;
    ////ŠÔ
    //private int time = 0;

    //ˆê“x‚¾‚¯
    bool firstCheck = false;

    [SerializeField]
    private Button launchButton;
    [SerializeField]
    private Slider thetaSlider,velocitySlider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theta = thetaSlider.value;
        velocity0 = velocitySlider.value;
    }

    public void LaunchButton()
    {
        if (!firstCheck)
        {
            ChangeRadian();

            Vector3 force = velocity0 * direction;

            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

            rb.AddForce(force, ForceMode.Impulse);

            firstCheck = true;
        }
    }

    void ChangeRadian()
    {
        Debug.Log(thetaSlider.value);
        float rad = theta * 3.14f / 180;

        direction.x = Mathf.Cos(rad);
        direction.y = Mathf.Sin(rad);
    }
}
