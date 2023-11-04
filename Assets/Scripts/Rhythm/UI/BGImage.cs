using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGImage : MonoBehaviour
{
    public Image[] Images;

    private float sizeX = 16f;
    private float sizeY = 9f;
    private float posX = 0;
    private float checkX = 0;
    private int curIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(sizeX, -sizeY) * 5f * Time.deltaTime);
        posX = transform.localPosition.x;
        if(posX - checkX > 1920f)
        {
            int beforeIndex = (curIndex - 1) >= 0 ? curIndex - 1 : Images.Length - 1;
            Images[curIndex].rectTransform.localPosition = Images[beforeIndex].rectTransform.localPosition + new Vector3(-1920f, 1080f);
            checkX = checkX + 1920f;
            curIndex = (curIndex + 1) < Images.Length ? curIndex + 1 : 0;
        }
    }
}
