using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour
{
    #region �ϐ��錾
    ////����
    private int velocity0 = 0;
    //�p�x
    private int theta = 0;
    //����
    Vector3 direction;
    ////�d��
    //private int gravity = 0;
    ////����
    //private int time = 0;

    //��x����
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

    }

    //�p�x����
    public void ChangeThetaValue()
    {
        theta = (int)thetaSlider.value;
    }
    //���x����
    public void ChangeVelocityValue()
    {
        velocity0 = (int)velocitySlider.value;
    }

    //���ˎ�
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

    //���W�A���ϊ�
    void ChangeRadian()
    {
        float rad = theta * 3.14f / 180;

        direction.x = Mathf.Cos(rad);
        direction.y = Mathf.Sin(rad);
    }
}
