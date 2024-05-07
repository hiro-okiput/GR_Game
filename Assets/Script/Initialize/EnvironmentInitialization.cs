using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GR_Game.Struct;
using GR_Game;

public class EnvironmentInitialization : MonoBehaviour
{
    [SerializeField, Header("初期化必要数")]
    private int needInitializeCount = 1;

    private TimeData setTime;   //時間初期化用

    public List<TMP_InputField> initializerUIList;  //値の表示変更用
    public List<Slider> initializerSliderList;      //スライダーの場所変更用

    private void Start()
    {
        //時間の初期化
        setTime.Initialize();
        EnvironmentData.SetTime(setTime);
    }

    //float型での最大値最小値超過確認
    public float CheckMinMax(float min, float max, float value)
    {
        if(value < min) return min;
        if(value > max) return max;

        return value;
    }

    //int型での最大値最小値超過確認
    public int CheckMinMax(int min, int max, int value)
    {
        if (value < min) return min;
        if (value > max) return max;

        return value;
    }

    //重力の初期化(スライダーから)
    public void SetGravity(Slider slider)
    {
        SetValue(initializerUIList[0], slider, slider.value);

        EnvironmentData.SetGravity(slider.value);
    }

    //重力の初期化(値入力から)
    public void SetGravity(TMP_InputField input)
    {
        Slider slider = initializerSliderList[0];

        float gravity = CheckMinMax(slider.minValue, slider.maxValue, float.Parse(input.text));

        SetValue(input, slider, gravity);

        EnvironmentData.SetGravity(gravity);
    }

    //初期化時の値表示変更処理
    private void SetValue(TMP_InputField textField, Slider slider, float value)
    {
        textField.text = value.ToString("f2");

        slider.value = float.Parse(value.ToString("f2"));
    }

    //ランダムに環境を初期化
    public void GenerateRandomEnvironment()
    {
        for (int i = 0; i < needInitializeCount; i++)
        {
            Slider slider = initializerSliderList[i];

            TMP_InputField inputField = initializerUIList[i];

            float value = Random.Range(slider.minValue, slider.maxValue);

            SetValue(inputField, slider, value);
            switch(i)
            {
                case 0:
                    EnvironmentData.SetGravity(value);
                    break;
            }
            
        }
    }
    
    //地球と同じ環境に初期化
    public void ToEarthData()
    {
        EnvironmentData.InitializeToEarth();

        for (int i = 0; i < needInitializeCount; i++)
        {
            float value = 0;
            switch (i)
            {
                case 0:
                    value = 1;
                    break;
            }
            Slider slider = initializerSliderList[i];

            TMP_InputField inputField = initializerUIList[i];

            SetValue(inputField, slider, value);
        }
    }

    //初期化終了処理
    public void EndInitialize()
    {
        SceneManager.LoadScene("Main");
    }
}
