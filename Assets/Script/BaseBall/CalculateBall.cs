using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBall : MonoBehaviour
{
    [SerializeField] public float vx = 30;    // m/s (x�����̏����x)
    [SerializeField] public float vy = 0;     // m/s (y�����̏����x)
    [SerializeField] public float vz = 20;    // m/s (z�����̏����x)
    [SerializeField] public float Hx = 10;    // m/s (�ł͂ɂ�� x �����̑��x����)
    [SerializeField] public float Hy = 5;     // m/s (�ł͂ɂ�� y �����̑��x����)
    [SerializeField] public float Hz = 15;    // m/s (�ł͂ɂ�� z �����̑��x����)
    [SerializeField] public float g = 1f;      // �d�͉����x (m/s^2)

    private float timeElapsed = 0f; // �o�ߎ���

    void Start()
    {
        // �����ʒu��ݒ�
        transform.position = new Vector3(0, 10, 0); // Y����1�ɐݒ�
    }

    void Update()
    {
        // ���Ԃ𑝉�
        timeElapsed += Time.deltaTime;

        // �{�[���̍��W���v�Z
        float x = (vx + Hx) * timeElapsed;
        float y = (vy + Hy) * timeElapsed - 0.5f * g * Mathf.Pow(timeElapsed, 2);

        // �{�[�����n�ʂɂԂ���܂ňړ�
        if (y >= 0)
        {
            // �{�[���̈ʒu���X�V
            transform.position = new Vector3(x, y, 0);
            Debug.Log($"x:{x}, y:{y}");
        }
        else
        {
            // �{�[�����n�ʂɂԂ�������Ay��0�ɐݒ肵�Ē�~
            transform.position = new Vector3(x, 0, 0);
            enabled = false; // �X�N���v�g�𖳌��ɂ��čX�V���~
        }
    }
}
