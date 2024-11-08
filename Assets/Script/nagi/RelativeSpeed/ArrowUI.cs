using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowUI : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropDown;
    [SerializeField]
    private TMP_InputField inputField;

    public void DirectionChoice()
    {
        switch(dropDown.value)
        {
            case 0:  //east
                this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 0);
                break;
            case 1:  //west
                this.gameObject.transform.rotation = new Quaternion(0, -90, 0, 0);
                break;
            case 2:  //south
                this.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                break;
        }
    }

    public void ScaleChoice()
    {
        float value=float.Parse(inputField.text);
        this.gameObject.transform.localScale = new Vector3(1, 1, value);
    }
}
