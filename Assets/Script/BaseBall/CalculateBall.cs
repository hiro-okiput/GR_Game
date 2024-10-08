using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBall : MonoBehaviour
{
    [SerializeField] public float vx = 30;    // m/s (x方向の初速度)
    [SerializeField] public float vy = 0;     // m/s (y方向の初速度)
    [SerializeField] public float vz = 20;    // m/s (z方向の初速度)
    [SerializeField] public float Hx = 10;    // m/s (打つ力による x 方向の速度増加)
    [SerializeField] public float Hy = 5;     // m/s (打つ力による y 方向の速度増加)
    [SerializeField] public float Hz = 15;    // m/s (打つ力による z 方向の速度増加)
    [SerializeField] public float g = 1f;      // 重力加速度 (m/s^2)

    private float timeElapsed = 0f; // 経過時間

    void Start()
    {
        // 初期位置を設定
        transform.position = new Vector3(0, 10, 0); // Y軸は1に設定
    }

    void Update()
    {
        // 時間を増加
        timeElapsed += Time.deltaTime;

        // ボールの座標を計算
        float x = (vx + Hx) * timeElapsed;
        float y = (vy + Hy) * timeElapsed - 0.5f * g * Mathf.Pow(timeElapsed, 2);

        // ボールが地面にぶつかるまで移動
        if (y >= 0)
        {
            // ボールの位置を更新
            transform.position = new Vector3(x, y, 0);
            Debug.Log($"x:{x}, y:{y}");
        }
        else
        {
            // ボールが地面にぶつかったら、yを0に設定して停止
            transform.position = new Vector3(x, 0, 0);
            enabled = false; // スクリプトを無効にして更新を停止
        }
    }
}
