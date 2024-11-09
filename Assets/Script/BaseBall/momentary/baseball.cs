using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class baseball : MonoBehaviour
{
    //[Tooltip("球速")] public float ballSpeed;
    [Tooltip("球速スライダ")] public Slider BallSpeedSlider;

    //[Tooltip("スイングスピード")] public float swingSpeed;
    [Tooltip("スイングスピードスライダ")] public Slider BatSpeedSlider;

    [SerializeField] public GameObject Ball;
    [SerializeField, Tooltip("リリースポイント")] private Vector3 releasePoint;

    [SerializeField, Tooltip("重力")] private float g;


    //簡易版
    [SerializeField] private float batRestitutionCoefficient = 0.5f;        // 反発係数（1以下に設定する）


    [System.Serializable]
    public class Pitcher
    {
        public float vx = 0;        //いらない可能性あり
        public float vy = 0;
        public float vz = 0;        //いらない可能性大
    }

    [System.Serializable]
    public class Batter             //打球角度を決める
    {
        public float Hx = 0;        //いらない可能性あり
        public float Hy = 0;
        public float Hz = 0;        //いらない可能性大
    }

    [SerializeField] private Batter batter = new Batter();
    [SerializeField] private Pitcher pitcher = new Pitcher();

    private float m_ballSpeed;
    private float m_batSpeed;
    private Vector3 ballPosition;
    private bool isHit = false;



    // Start is called before the first frame update
    void Start()
    {
        Ball.transform.position = releasePoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))         //投球開始、計算メソッドへ
        {
            Playball();
        }
    }

    private void Playball()         //パラメータ確定、計算メソッド呼び出し
    {
        //メンバ変数にそれぞれの速さを保存
        //この時にkm/hからm/sに変換しておく
        m_ballSpeed = (float)(BallSpeedSlider.value / 3.6);
        m_batSpeed = (float)(BatSpeedSlider.value / 3.6);

        float vx = pitcher.vx;
        float vy = -m_ballSpeed;
        float vz = pitcher.vz;
        float Hx = batter.Hx;
        float Hy = m_batSpeed;
        float Hz = batter.Hz;
        float timeElapsed = 0f;

        ballPosition = releasePoint; // 初期位置に設定

        StartCoroutine(CalculateBallMovement(vx, vy, vz, Hx, Hy, Hz, timeElapsed));
    }

    //ここでボールの位置を計算する
    private IEnumerator CalculateBallMovement(float vx, float vy, float vz, float Hx, float Hy, float Hz, float timeElapsed)
    {
        while (true)
        {
            timeElapsed += Time.deltaTime;

            // ボールの座標を計算
            float x = vx * timeElapsed;
            float y = ballPosition.y + vy * timeElapsed;
            float z = ballPosition.z + vz * timeElapsed;

            if (!isHit && y <= 0)
            {
                // 衝突時の速度計算
                vx += Hx;
                vy += Hy;          //反発係数を計算
                //vy = -vy + Hy * batRestitutionCoefficient;          //反発係数を計算
                vz += Hz;

                // 衝突後の位置を計算
                timeElapsed = 0f;
                ballPosition = new Vector3(x, y, z);
                isHit = true;                                       //衝突判定
            }
            else if (isHit)
            {
                // 衝突後のボールの座標を計算
                x = ballPosition.x + vx * timeElapsed;
                y = vy * timeElapsed;
                //y = ballPosition.y + vy * timeElapsed;
                z = vz * timeElapsed - (0.5f * 9.8f * Mathf.Pow(timeElapsed, 2));
                //z = ballPosition.z + vz * timeElapsed - (0.5f * 9.8f * Mathf.Pow(timeElapsed, 2));

            }

            // ボールが地面にぶつかるまで移動
            if (z >= 0)
            {
                // ボールの位置を更新
                Ball.transform.position = new Vector3(x, y, z);
                isHit = false;
            }
            else
            {
                // ボールが地面にぶつかったら、zを0に設定して停止
                Ball.transform.position = new Vector3(x, y, 0);

                Result();       //結果を表示

                break; // ループを終了
            }

            yield return null;
        }
    }

    private void Result()
    {
        //飛距離を表示するためのメソッド
        //とりあえず座標を表示しておく、のちに(0,0,0)からの距離を計算し何m飛んだかをみられるようにする
        Debug.Log(Ball.transform.position);
    }

}
