using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    #region �ϐ��錾
    ////����
    //private int velocity0 = 0;
    ////�p�x
    //private int theta = 0;
    ////�d��
    //private int gravity = 0;
    ////����
    //private int time = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //���ƂŊp�x��ݒ肵���玩���I��Vector3�^�ɂȂ�����悤�ɂ���
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
