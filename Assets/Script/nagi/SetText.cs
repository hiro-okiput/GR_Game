using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        inputField.text = "0";
    }

    public void GetText()
    {
        slider.value = float.Parse(inputField.text);
    }
    public void GetValue()
    {
        int value = 0;
        value = (int)slider.value;
        inputField.text = value.ToString();
    }
}
