using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    #region 変数宣言
    ////初速
    //private int velocity0 = 0;
    ////角度
    //private int theta = 0;
    ////重力
    //private int gravity = 0;
    ////時間
    //private int time = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //あとで角度を設定したら自動的にVector3型になおせるようにする
        Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);

        float forceMagnitude = 20.0f;

        Vector3 force = forceMagnitude * forceDirection;

        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

        rb.AddForce(force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
