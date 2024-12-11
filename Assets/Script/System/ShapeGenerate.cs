using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShapeGenerate : MonoBehaviour
{
    public List<GameObject> shapeObjectList;

    private GameObject shapeObject;
    private GameObject nowObject;
    private int shapeIndex = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (shapeObject != shapeObjectList[shapeIndex] || nowObject.GetComponent<Shape>().GetEnablePysics())
        {
            if (shapeObject != shapeObjectList[shapeIndex]) Destroy(nowObject);
            nowObject = Instantiate(shapeObjectList[shapeIndex]);
            shapeObject = shapeObjectList[shapeIndex];
        }
    }
}
