using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSprite : MonoBehaviour
{
    public GameObject[] Sprites;

    private float sizeX = 16f;
    private float sizeY = 9f;
    private float posX = 0;
    private float checkX = 0;
    private int curIndex = 0;

    void Update()
    {
        transform.Translate(new Vector3(sizeX, -sizeY) * 5f * Time.deltaTime);
        posX = transform.localPosition.x;
        if (posX - checkX > 1920f)
        {
            int beforeIndex = (curIndex - 1) >= 0 ? curIndex - 1 : Sprites.Length - 1;
            Sprites[curIndex].transform.localPosition = Sprites[beforeIndex].transform.localPosition + new Vector3(-1920f, 1080f);
            checkX = checkX + 1920f;
            curIndex = (curIndex + 1) < Sprites.Length ? curIndex + 1 : 0;
        }
    }
}
