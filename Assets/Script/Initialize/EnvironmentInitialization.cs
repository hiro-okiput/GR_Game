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
    [SerializeField, Header("�������K�v��")]
    private int needInitializeCount = 1;

    private TimeData setTime;   //���ԏ������p

    public List<TMP_InputField> initializerUIList;  //�l�̕\���ύX�p
    public List<Slider> initializerSliderList;      //�X���C�_�[�̏ꏊ�ύX�p

    private void Start()
    {
        //���Ԃ̏�����
        setTime.Initialize();
        EnvironmentData.SetTime(setTime);
    }

    //float�^�ł̍ő�l�ŏ��l���ߊm�F
    public float CheckMinMax(float min, float max, float value)
    {
        if(value < min) return min;
        if(value > max) return max;

        return value;
    }

    //int�^�ł̍ő�l�ŏ��l���ߊm�F
    public int CheckMinMax(int min, int max, int value)
    {
        if (value < min) return min;
        if (value > max) return max;

        return value;
    }

    //�d�͂̏�����(�X���C�_�[����)
    public void SetGravity(Slider slider)
    {
        SetValue(initializerUIList[0], slider, slider.value);

        EnvironmentData.SetGravity(slider.value);
    }

    //�d�͂̏�����(�l���͂���)
    public void SetGravity(TMP_InputField input)
    {
        Slider slider = initializerSliderList[0];

        float gravity = CheckMinMax(slider.minValue, slider.maxValue, float.Parse(input.text));

        SetValue(input, slider, gravity);

        EnvironmentData.SetGravity(gravity);
    }

    //���������̒l�\���ύX����
    private void SetValue(TMP_InputField textField, Slider slider, float value)
    {
        textField.text = value.ToString("f2");

        slider.value = float.Parse(value.ToString("f2"));
    }

    //�����_���Ɋ���������
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
    
    //�n���Ɠ������ɏ�����
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

    //�������I������
    public void EndInitialize()
    {
        SceneManager.LoadScene("Main");
    }
}
