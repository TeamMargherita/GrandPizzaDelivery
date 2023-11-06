using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 한석호 작성
public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite[] startSprite;
    private UnityEngine.UI.Image img;

    public void Awake()
    {
        img = this.GetComponent<UnityEngine.UI.Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = startSprite[0];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = startSprite[1];
    }
}
