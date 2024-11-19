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

    int direction = 0; //east=0,west=1,south=2,north=3

    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void DirectionChoice()
    {
        direction = dropDown.value;
        switch(direction)
        {
            //case 0:  //east
            //    this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 0);
            //    break;
            case 1:  //west
                rectTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2:  //south
                rectTransform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 3:  //north
                rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    public void ScaleChoice()
    {
        float width = 50;
        float height = 50;

        float value=float.Parse(inputField.text);
        if(value>=20)
        {
            value = 20;
        }

        Vector2 pos = rectTransform.position;

        switch(direction)
        {
            case 0:
                width += value * 30;
                pos.x = width / 2 + 960;
                pos.y = 540;
                break;
            case 1:
                width += value * 30;
                pos.x = -width / 2 + 960;
                pos.y = 540;
                break;
            case 2:
                width += value * 30;
                pos.x = 960;
                pos.y = -width / 2 + 540;
                break;
            case 3:
                width += value * 30;
                pos.x = 960;
                pos.y = width / 2 + 540;
                break;
        }
        rectTransform.position = pos;
        rectTransform.sizeDelta = new Vector2(width, height);
    }
}
