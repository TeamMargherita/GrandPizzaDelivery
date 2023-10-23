using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoPizza : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.time < 64800 && GameManager.Instance.time >= 21600)
        {
            return;
        }

        GameManager.Instance.time += 3600;
        LoadScene.Instance.LoadRhythm();
    }
}
