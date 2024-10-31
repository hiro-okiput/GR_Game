using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour
{
    #region 変数宣言
    ////初速
    private int velocity0 = 0;
    //角度
    private int theta = 0;
    //方向
    Vector3 direction;
    ////重力
    private float gravity = 0;
    ////時間
    //private int time = 0;

    //一度だけ
    public bool firstCheck = false;

    [SerializeField]
    private Button launchButton;
    [SerializeField]
    private Slider thetaSlider,velocitySlider,gravitySlider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //角度調整
    public void ChangeThetaValue()
    {
        theta = (int)thetaSlider.value;
    }
    //速度調整
    public void ChangeVelocityValue()
    {
        velocity0 = (int)velocitySlider.value;
    }

    public void ChangeGravityValue()
    {
        gravity = gravitySlider.value;
    }

    //発射時
    public void LaunchButton()
    {
        if (!firstCheck)
        {
            ChangeRadian();

            Vector3 force = velocity0 * direction;

            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            Debug.Log(gravity);
            rb.velocity = new Vector3(rb.velocity.x, -gravity, rb.velocity.z);
            rb.AddForce(force, ForceMode.Impulse);

            firstCheck = true;
        }
    }

    //ラジアン変換
    void ChangeRadian()
    {
        float rad = theta * 3.14f / 180;

        direction.x = Mathf.Cos(rad);
        direction.y = Mathf.Sin(rad);
    }
}
